using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Web.Services;
using System.Xml.Serialization;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    /// <summary>
    /// Summary description for TCSservice
    /// </summary>
    [WebService(Namespace = "http://www.elfainstall.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TCSservice : WebService
    {
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        private static string _state, _city, _zip, _storeCode;
        private static decimal _taxRate;

        [WebMethod(Description = "Receiving order data xml file in string format")]
        public bool GetOrder(string Request)
        {
            var serializer = new XmlSerializer(typeof(Order));
            TextReader tr = new StringReader(Request);
            try
            {
                var newDoc = (Order)serializer.Deserialize(tr);
                tr.Close();
                bool res = SaveOrder(newDoc);
                return res;
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("TCSservice", "GetOrder", ex.Message);
                return false;
            }
        }

        [WebMethod(Description = "Mark order as cancelled")]
        public bool CancelOrder(string OrderNumber)
        {
            try
            {
                var sglJob = new OrderData();
                int orderID = sglJob.CheckOrder(OrderNumber, null);
                if (orderID > 1)
                {
                    SendCancellation(orderID);
                    sglJob.DeleteOrder(orderID, "Cancelled by TCS (by Cancel Service)", "TCS");
                    sglJob.CloseAppointment(orderID);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("TCSservice", "CancelOrder", OrderNumber + ": " + ex.Message);
                return false;
            }
        }

        private static bool SaveOrder(Order XMLData)
        {
            bool result = false;
            decimal instMin = 0;
            decimal delivery = 0;
            decimal proc = 0;
            string orderNumber = "";
            try
            {
                bool deliveryOption = XMLData.Install.DeliveryOption80 == "Y";
                bool demolition = XMLData.Install.Demolition == "Y";
                char scopeOfDemo = XMLData.Install.ScopeOfDemo == "Basic" ? 'B' : 'A';
                SqlDataReader drInfo;
                var sqlJob = new OrderData();
                var storeData = new SessionData();
                orderNumber = XMLData.Store.OrderNumber;
                string storeCode = XMLData.Store.Name;
                string deliveryInfo = "";
                string solutionDescription = XMLData.Install.SolutionDescription;
                if (XMLData.Install.FulfillmentType == "CustomerPickup")
                {
                    deliveryInfo = " Customer Pickup " + XMLData.Install.CustomerPickupDate + " " +
                                   XMLData.Install.CustomerPickupAppointment;
                }
                if (XMLData.Install.FulfillmentType == "OutsideDelivery")
                {
                    deliveryInfo = " Outside Delivery " + XMLData.Install.OutsideDeliveryDate + " " +
                                   XMLData.Install.OutsideDeliveryWindowStart;
                }
                solutionDescription = solutionDescription + deliveryInfo;
                if (storeCode == "Ship Order") storeCode = "ATH";
                int storeID = sqlJob.GetStoreIDbyCode(storeCode);
                if (orderNumber.Substring(0, 3) == "042")
                {
                    storeCode = "LEX";
                    storeID = sqlJob.GetStoreIDbyCode(storeCode);
                }
                if (orderNumber.Substring(0, 3) == "026")
                {
                    storeCode = "WHP";
                    storeID = sqlJob.GetStoreIDbyCode(storeCode);
                }
                if (storeID > 0)
                {
                    _city = XMLData.Customer.City;
                    _state = XMLData.Customer.State;
                    _zip = XMLData.Customer.Zip;
                    _storeCode = storeCode;
                    drInfo = storeData.StoreInfo(storeID);
                    if (drInfo.HasRows)
                    {
                        drInfo.Read();
                        instMin = decimal.Parse(drInfo["Inst_Min"].ToString());
                        delivery = decimal.Parse(drInfo["Delivery"].ToString());
                        proc = decimal.Parse(drInfo["Inst_Proc"].ToString()) / 100;
                    }
                    drInfo.Close();
                    decimal instPrice = XMLData.Charges.RetailPrice * proc;
                    if (XMLData.Install.AdditionalComments.IndexOf("EPCP discount")>=0)
                    {
                        instPrice = instPrice * 0.75M;
                    }
                    //decimal instPrice = XMLData.Charges.Total * proc;
                    if (instPrice < instMin) instPrice = instMin;
                    if (!deliveryOption) delivery = 00.0M;
                    decimal orderPrice = delivery + instPrice + XMLData.Charges.AdditionalDemo;
                    string duration = "";
                    DateTime installDate = DateTime.Parse("01/01/1900");
                    DateTime startTime = installDate;
                    DateTime endTime = installDate;
                    DateTime datetimeVal;
                    string installStartTime = "";
                    if (DateTime.TryParse(XMLData.Install.InstallDate, out datetimeVal))
                    {
                        installDate = datetimeVal;
                    }
                    if (XMLData.Install.InstallWindowStartDate != null && XMLData.Install.InstallWindowStartTime != null)
                    {
                        installStartTime = XMLData.Install.InstallWindowStartTime;
                        if (
                            DateTime.TryParse(
                                XMLData.Install.InstallWindowStartDate + " " + XMLData.Install.InstallWindowStartTime,
                                out datetimeVal))
                        {
                            startTime = datetimeVal;
                        }
                    }
                    else startTime = DateTime.Parse("01/01/1900");

                    if (XMLData.Install.InstallWindowEndDate != null && XMLData.Install.InstallWindowEndTime != null)
                    {
                        if (
                            DateTime.TryParse(
                                XMLData.Install.InstallWindowEndDate + " " + XMLData.Install.InstallWindowEndTime,
                                out datetimeVal))
                        {
                            endTime = datetimeVal;
                        }
                    }
                    else startTime = DateTime.Parse("01/01/1900");

                    duration = (endTime - startTime).Hours + "h";
                    int orderID = sqlJob.CheckOrder(orderNumber, XMLData.Customer.LastName.Trim());
                    if (orderID == 0) // New Order
                    {
                        int customerID = sqlJob.NewCustomer(XMLData.Customer.FirstName,
                                                                XMLData.Customer.MiddleName,
                                                                XMLData.Customer.LastName,
                                                                XMLData.Customer.Address1,
                                                                XMLData.Customer.Address2,
                                                                XMLData.Customer.City,
                                                                XMLData.Customer.State,
                                                                XMLData.Customer.Zip,
                                                                XMLData.Customer.HomePhone,
                                                                XMLData.Customer.OtherPhone,
                                                                XMLData.Customer.Email);
                        if (customerID > 0)
                        {
                            DateTime orderDate = DateTime.Parse(XMLData.Store.SaleDate);
                            orderID = sqlJob.NewOrderTCS(orderNumber, customerID, storeID, orderDate,
                                                             XMLData.Store.Planner, "ASAP",
                                                             deliveryOption, demolition, scopeOfDemo,
                                                             XMLData.Charges.RetailPrice,
                                                             instPrice, delivery, XMLData.Charges.AdditionalDemo, orderPrice,
                                                             installDate,
                                                             installStartTime,
                                                             duration,
                                                             solutionDescription,
                                                             XMLData.Install.AdditionalComments, XMLData.Charges.Total);
                            sqlJob.NewAppointment(orderID, startTime, endTime, XMLData.Install.AdditionalComments);

                            if (XMLData.Attributes != null && XMLData.Attributes.Attribute!=null)
                            {
                                OrderAttributes allAttributes = XMLData.Attributes;
                                foreach (OneAttribute attr in allAttributes.Attribute)
                                {
                                    if (attr.value.ToUpper() == "TRUE")
                                    {
                                        string attName = attr.name.Trim();
                                        sqlJob.SaveOrderAttribute(orderID, attName);
                                    }
                                }
                            }

                            if (orderID > 0)
                            {
                                result = true;
                                SendEmail(storeCode, orderID, installDate); 
                                SaveTax(orderID, orderPrice); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                if (installDate < DateTime.Parse("01/01/2000") && XMLData.Customer.Email.Trim() != "")
                                {
                                    //SendCustomerEmail(XMLData.Customer.Email.Trim()); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                }
                            }
                        }
                    }
                    else // UpdateRowSource existing order
                    {
                        if (orderID > 1)
                        {
                            if (XMLData.Charges.RetailPrice > 0)
                            {
                                SendNotification(storeCode, orderID, installDate); 

                                sqlJob.UpdateOrderTCSInfo(orderID, "ASAP", deliveryOption, demolition,
                                                              scopeOfDemo, XMLData.Charges.RetailPrice, instPrice, delivery,
                                                              XMLData.Charges.AdditionalDemo,
                                                              orderPrice,
                                                              installDate,
                                                              installStartTime,
                                                              duration,
                                                              solutionDescription,
                                                              XMLData.Install.AdditionalComments, XMLData.Charges.Total);
                                sqlJob.UpdateAppointment(orderID, startTime, endTime);
                                sqlJob.UpdateAddress(orderID, XMLData.Customer.Address1, XMLData.Customer.Address2,
                                                 XMLData.Customer.City, XMLData.Customer.State, XMLData.Customer.Zip,
                                                 XMLData.Customer.HomePhone);
                                SaveTax(orderID,orderPrice);  // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                                //var saveLog = new SessionData();
                                //saveLog.AddErrorLog("TCSservice", "SaveOrder",
                                //                    orderNumber + ": was updated. Install Date - " + installDate + "  Store: " + storeCode);
                            }
                            else
                            {
                                SendCancellation(orderID);
                                sqlJob.DeleteOrder(orderID, "Cancelled by TCS (0 Price Amount)","TCS");
                                sqlJob.CloseAppointment(orderID);
                            }
                        }
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("TCSservice", "SaveOrder", orderNumber + ": " + ex.Message);
                result = false;
            }
            return result;
        }

        static string GetDuration(string Start, string End, string OrderNumber)
        {
            try
            {
                DateTime data1 = DateTime.Parse("01/01/2010 " + Start);
                DateTime data2 = DateTime.Parse("01/01/2010 " + End);
                int duration = data2.Hour - data1.Hour + 1;
                if (duration < 0) duration = -duration;
                return duration.ToString().Trim() + "h";
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("TCSservice", "GetDuration", OrderNumber + ": " + ex.Message);
                return "";
            }
        }

        static void SendEmail(string StoreCode, int OrderID, DateTime InstallDate)
        {
            string email = "";
            var regionInfo = new SessionData();
            SqlDataReader drInfo = regionInfo.RegionByStoreCode(StoreCode);
            if (drInfo.HasRows)
            {
                while (drInfo.Read())
                {
                    string current = drInfo["Email"].ToString().Trim();
                    if (current != "")
                    {
                        if (email == "") email = current;
                        else email = email + "," + current;
                    }
                }
            }
            drInfo.Close();

            if (email == "") return;

            var orderInfo = new OrderData();
            drInfo = orderInfo.NewOrderDetails(OrderID);
            drInfo.Read();

            var mailSeverName = MailServer; 
            const string from = "confirmation@elfainstall.com";
            string to = email;
            string subject = "elfa Installation Service Request for Store "
                    + drInfo["StoreName"] + " Order # " + drInfo["OrderNumb"] + " (Region notification)";
            string body = "<table border='0' width='500'><tr><td align='center'><img border='0' src='http://www.elfainstall.com/images/logo.gif'><br>";
            body = body + "<strong><font size='4'>elfa<span style='vertical-align:super; font-weight:bold'>®</span> Installation Service<br />Installation Request</font></strong></td></tr></table>";
            body = body + "<br>Date: " + DateTime.Now.ToShortDateString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            body = body + "Store: " + drInfo["StoreName"] + "<br>";
            body = body + "Customer Name: " + drInfo["fName"].ToString().Trim() + " " + drInfo["lName"] + "<br>";
            body = body + "Customer Address: " + drInfo["address1"].ToString().Trim() + " " + drInfo["address2"] + "<br>";
            body = body + "City: " + drInfo["city"] + "&nbsp;&nbsp;&nbsp;State: " + drInfo["state"] + "&nbsp;&nbsp;&nbsp;&nbsp;Zip: " + drInfo["zip"] + "<br>";
            body = body + "Customer Phone Numbers: (Home) &nbsp;&nbsp;" + drInfo["hphone"] + "&nbsp;&nbsp; &nbsp;&nbsp;";
            if (drInfo["phone2"].ToString().Trim() != "")
            { body = body + " (Other) &nbsp;&nbsp;" + drInfo["phone2"]; }
            if (InstallDate > DateTime.Parse("01/01/2000"))
            {
                body = body + "<br>Installation Date: " +
                       InstallDate.ToLongDateString() + ", Installation Time: " +
                       drInfo["InstallTime"];
            }
            body = body + "<br><strong>Total installation price: " + float.Parse(drInfo["OrderPrice"].ToString()).ToString("c") + "</strong><br><br>";
            body = body + "For additional instructions check http://www.elfainstall.com <br><br><br>";
            body = body + "This is automated e-mail. Do not reply.";
            drInfo.Close();
            try
            {
                //MailAddress copy = new MailAddress("Vladimir.TX@gmail.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //message.Bcc.Add(copy);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
                message.Dispose();
                //var saveLog = new SessionData();
                //saveLog.AddErrorLog("TCSservice", "SendEmail", OrderID + ": Email sent: " + email);
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("TCSservice", "SendEmail", OrderID + ": " + ex.Message + " - to " + email);
            }
        }
        static void SendNotification(string StoreCode, int OrderID, DateTime NewDate)
        {
            string email = "";
            try
            {
                var regionInfo = new SessionData();
                SqlDataReader drInfo = regionInfo.RegionByStoreCode(StoreCode);
                if (drInfo.HasRows)
                {
                    while (drInfo.Read())
                    {
                        string current = drInfo["Email"].ToString().Trim();
                        if (current != "")
                        {
                            if (email == "") email = current;
                            else email = email + "," + current;
                        }
                    }
                }
                if (email == "") email = "nodailey@containerstore.com";
                drInfo.Close();
                var orderInfo = new OrderData();
                drInfo = orderInfo.NewOrderDetails(OrderID);
                drInfo.Read();
                var oldDate = DateTime.Parse(drInfo["InstallDate"].ToString());
                if (oldDate != NewDate)
                {
                    //const string mailSeverName = "relay-hosting.secureserver.net";
                    var mailSeverName = MailServer;
                    const string from = "confirmation@elfainstall.com";
                    string to = email;
                    string subject = "Installation Date Changed for Order # " + drInfo["OrderNumb"] +
                                     " (Region notification)";
                    string body = "TCS changed installation date for Order # " + drInfo["OrderNumb"] + ".<br>";
                    body = body + "New installation date is " + NewDate.ToLongDateString() + ".<br>";
                    body = body + "See details on the web site http://www.elfainstall.com .<br><br>";
                    body = body + "Comments: " + drInfo["InstNotes"] + "<br>";
                    body = body + "This is automated e-mail. Do not reply.";
                    drInfo.Close();
                    //var copy = new MailAddress("Vladimir.TX@gmail.com");
                    var message = new MailMessage(from, to, subject, body);
                    message.IsBodyHtml = true;
                    //message.Bcc.Add(copy);
                    var copy1 = new MailAddress("nodailey@containerstore.com");
                    message.CC.Add(copy1);
                    var mailClient = new SmtpClient(mailSeverName);
                    mailClient.UseDefaultCredentials = true;
                    mailClient.Send(message);
                    message.Dispose();
                    //var saveLog = new SessionData();
                    //saveLog.AddErrorLog("TCSservice", "SendNotification", OrderID + ": Email sent: " + email);
                }
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("TCSservice", "SendNotification", OrderID + ": " + ex.Message + " - to " + email);
            }
        }

        static void SendCancellation(int OrderID)
        {
            string email = "";
            try
            {
                var orderInfo = new OrderData();
                SqlDataReader drInfo = orderInfo.GetOrderRegions(OrderID);
                if (drInfo.HasRows)
                {
                    while (drInfo.Read())
                    {
                        string current = drInfo["Email"].ToString().Trim();
                        if (current != "")
                        {
                            if (email == "") email = current;
                            else email = email + "," + current;
                        }
                    }
                }
                if (email == "") email = "nodailey@containerstore.com";
                drInfo.Close();
                drInfo = orderInfo.NewOrderDetails(OrderID);
                drInfo.Read();
                var mailSeverName = MailServer;
                const string from = "confirmation@elfainstall.com";
                string to = email;
                string subject = "Cancellation for Order # " + drInfo["OrderNumb"] + " (Region notification)";
                string body = "TCS cancelled Order # " + drInfo["OrderNumb"] + " for customer " + drInfo["fName"] + " " +
                              drInfo["lName"] + ".<br>";
                body = body + "See details on the web site http://www.elfainstall.com .<br><br>";
                body = body + "This is automated e-mail. Do not reply.";
                drInfo.Close();
                //var copy = new MailAddress("Vladimir.TX@gmail.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //message.Bcc.Add(copy);
                var copy1 = new MailAddress("nodailey@containerstore.com");
                message.CC.Add(copy1);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
                //var saveLog = new SessionData();
                //saveLog.AddErrorLog("TCSservice", "SendCancellation", OrderID + ": Email sent: " + email);
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("TCSservice", "SendCancellation", OrderID + ": " + ex.Message + " - to " + email);
            }
        }

        static void SendCustomerEmail(string Email)
        {
            var mailSeverName = MailServer;
            const string from = "confirmation@elfainstall.com";
            //string to = Email;
            string to = "Vladimir.TX@gmail.com";
            const string subject = "The Container Store Installation Service";
            string body = "<table border='0' width='600'><tr><td align='center'><img border='0' src='http://www.elfainstall.com/images/logo.gif'><br/>";
            body += "<strong><font size='4'>elfa<span style='vertical-align:super; font-weight:bold'>®</span> Installation Service</font></strong></td></tr>";
            body += "<tr><td align='center'>";
            body += "<br/><br/>";
            body += "<font size='3'>Congratulations on your purchase and<br />thank you for your interest in our installation service.</font>";
            body += "<br /><br />";
            body += "<font size='5'>Your order is currently unscheduled.</font>";
            body += "</td></tr>";
            body += "<tr><td align='left'>";
            body += "<br /><br />";
            body += "<font size='2'>Please call us toll-free at 888-202-7622 to schedule your installation. ";
            body += "Have your order confirmation number available for faster service. ";
            body += "Most installations occur 4-7 days after being scheduled. ";
            body += "If you are not ready to schedule at this time, please call with an estimated date ";
            body += "of installation to avoid future emails.<br /><br />";
            body += "We look forward to speaking with you!</font>";
            body += "</td></tr></table>";
            try
            {
                //MailAddress copy = new MailAddress("nodailey@containerstore.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //message.Bcc.Add(copy);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("TCSservice", "SendCustomerEmail", "Error: " + ex.Message + " - to " + Email);
            }
        }

        static decimal GetTaxRate()
        {
            decimal taxRate = 0;
            if (_state == "OH")
            {
                if (_storeCode == "COL") taxRate = 0.0675m;
                if (_storeCode == "CIN") taxRate = 0.065m;
            }
            else
            {
                if (_state == "TX")
                {
                    taxRate = _storeCode == "ATX" ? 0.08m : 0.0825m;
                }
                else
                {
                    //if (_storeCode == "STL") taxRate = 0.09425m;
                    //else
                    //{
                        var tax = new OrderData();
                        taxRate = tax.GetTaxRate(_zip, _city);
                    //}
                }
            }
            return taxRate;
        }

        static void SaveTax(int OrderID,decimal OrderPrice)
        {
            if (_state == "NY" || _state == "NJ" || _state == "PA" || _state == "OH" || _state == "FL" || _state == "MN" || _state == "WA" || _state == "TX")
            {
                decimal taxRate = GetTaxRate();
                if (taxRate > 0)
                {
                    decimal tax = OrderPrice * taxRate;
                    OrderPrice = OrderPrice + tax;
                    var orderInfo = new OrderData();
                    orderInfo.UpdateTax(OrderID,tax,OrderPrice);
                }
            }
        }

    }
}
