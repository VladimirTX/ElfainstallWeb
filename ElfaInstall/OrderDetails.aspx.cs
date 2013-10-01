using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class OrderDetails : System.Web.UI.Page
    {
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        public int OrderID, UserID;
        public string Url;
        SqlDataReader _drOrder, _drInfo;
        string _status, _userName, _orderNumb;
        private bool _delivery;
        private int _discountID;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { UserID = int.Parse(Request.Cookies["UserID"].Value); }
            var userInfo = new SessionData();
            _status = userInfo.UserStatus(UserID);
            _userName = userInfo.UserName(UserID).Trim();
            if (_status != "Admin" && _status != "Region" && _status != "Organizer") Response.Redirect("Login.aspx");
            if (Request.QueryString["OrderID"] != null)
            {
                OrderID = int.Parse(Request.QueryString["OrderID"]);
                Response.Cookies["OrderID"].Value = OrderID.ToString();
            }
            else
            {
                switch (_status)
                {
                    case "Admin":
                        Response.Redirect("Orders.aspx");
                        break;
                    case "Vendor":
                        Response.Redirect("VendorOrders.aspx");
                        break;
                    case "Region":
                        Response.Redirect("OrdersR.aspx");
                        break;
                    case "Organizer":
                        Response.Redirect("OrdersO.aspx");
                        break;
                    default:
                        Response.Redirect("Login.aspx");
                        break;
                }
            }
            if (_status == "Admin")
            {
                //btnDelete.Visible = true;
                btnLog.Visible = true;
                btnService.Visible = true;
            }
            if (_status == "Region" || _userName == "VolandTX") btnRequest.Visible = true;
            hfStatus.Value = _status;
            hfOrderID.Value = OrderID.ToString();
            var orderInfo = new OrderData();
            //orderInfo.AddOfficeComment(OrderID, _userName + " " + DateTime.Now + " " + "View order info<br/>");
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                _orderNumb = _drOrder["OrderNumb"].ToString();
                lblOrderID.Text = _orderNumb;
                lblStoreID.Text = _drOrder["StoreCode"].ToString();
                lblSaleDate.Text = DateTime.Parse(_drOrder["OrderDate"].ToString()).ToShortDateString();
                lblPlanner.Text = _drOrder["Planner"].ToString();
                if (_drOrder["Installer"].ToString().Trim() != "")
                { lblInstaller.Text = _drOrder["Installer"].ToString(); }
                else
                {
                    if (_drOrder["VendorName"].ToString().Trim() == "")
                    {
                        lblInstaller.Text = "Not assigned";
                        btnEmailVendor.Visible = false;
                    }
                    else lblInstaller.Text = _drOrder["VendorName"].ToString();
                }
                DateTime startDate = DateTime.Parse(_drOrder["StartTime"].ToString());
                DateTime endDate = DateTime.Parse(_drOrder["EndTime"].ToString());
                if (DateTime.Parse(_drOrder["StartTime"].ToString()) > DateTime.Parse("01/01/2000"))
                {
                    lblInstallDate.Text = startDate.ToLongDateString() + " " + startDate.ToShortTimeString();
                    lblTimeEnd.Text = endDate.ToLongDateString() + " " + endDate.ToShortTimeString();
                }
                else lblInstallDate.Text = "Not Scheduled";

                lblStatus.Text = _drOrder["sCurrent"].ToString();
                lblcName.Text = (_drOrder["fName"] + " " + _drOrder["mi"]).Trim() + " " + _drOrder["lName"];
                lblcAddress1.Text = _drOrder["address1"].ToString();
                lblcAddress2.Text = _drOrder["address2"].ToString();
                lblcCity.Text = _drOrder["city"].ToString();
                lblcState.Text = _drOrder["state"].ToString().Trim();
                lblcZip.Text = _drOrder["zip"].ToString();
                lblHphone.Text = _drOrder["hphone"].ToString();
                lblPhone2.Text = _drOrder["phone2"].ToString();
                string custEmail = _drOrder["email"].ToString().Trim();
                lblEmail.Text = custEmail;
                if (custEmail != "" && UserID == 23)
                {
                    lblLabelEmail.Visible = false;
                    btnEmailInvoice.Visible = true;
                }
                _delivery = bool.Parse(_drOrder["DeliveryOption"].ToString());
                cbDelivery.Checked = _delivery;
                if(_delivery)
                {
                    cbPickedUp.Checked = bool.Parse(_drOrder["PickedUp"].ToString());
                    lblPickedUp.Visible = true;
                    cbPickedUp.Visible = true;
                }
                //cbDemolition.Checked = bool.Parse(_drOrder["demolition"].ToString());
                //if (_drOrder["ScopeofDemo"].ToString() == "A")
                //{ lblDemo.Text = "Additional"; }
                //else
                //{ lblDemo.Text = "Basic"; }
                lblDelivery.Text = _drOrder["Delivery"].ToString();
                lblProc.Text = _drOrder["Inst_Proc"].ToString();
                lblSolution.Text = _drOrder["SolutionDescr"] + " / " + _drOrder["Tcscomments"];
                lblPurchasePrice.Text = decimal.Parse(_drOrder["PurchasePrice"].ToString()).ToString("c");
                lblInstallPrice.Text = decimal.Parse(_drOrder["BaseInstallPrice"].ToString()).ToString("c");
                lblDeliveryPrice.Text = decimal.Parse(_drOrder["DeliveryPrice"].ToString()).ToString("c");
                lblMilesPrice.Text = decimal.Parse(_drOrder["MilesPrice"].ToString()).ToString("c");
                lblDemoPrice.Text = decimal.Parse(_drOrder["DemoPrice"].ToString()).ToString("c");
                lblMisc.Text = decimal.Parse(_drOrder["MiscPrice"].ToString()).ToString("c");
                lblParking.Text = decimal.Parse(_drOrder["Parking"].ToString()).ToString("c");
                lblTip.Text = decimal.Parse(_drOrder["TipPrice"].ToString()).ToString("c");
                lblDiscount.Text = decimal.Parse(_drOrder["PromoPrice"].ToString()).ToString("c");
                lblTax.Text = decimal.Parse(_drOrder["Tax"].ToString()).ToString("c");
                lblTotalPrice.Text = decimal.Parse(_drOrder["OrderPrice"].ToString()).ToString("c");

                lblOffice.Text = _drOrder["comments"].ToString();
                lblComments.Text = _drOrder["InstNotes"].ToString();
                lblAccounting.Text = _drOrder["Comments4"].ToString();
                lblInvoice.Text = _drOrder["InvoiceNotes"].ToString();
                bool approved = bool.Parse(_drOrder["Approved"].ToString());
                bool bySurvey = bool.Parse(_drOrder["bySurvey"].ToString());
                cbConfirmed.Checked = approved;
                cbBySurvey.Checked = bySurvey;
                if(approved && _drOrder["Approved"]!=null && DateTime.Parse(_drOrder["ApproveDate"].ToString())>DateTime.Parse("01/01/2000"))
                {
                    lblConfirmed.Text = "(By phone " + DateTime.Parse(_drOrder["ApproveDate"].ToString()).ToShortDateString() + ")";
                }
                string option = _drOrder["Options"].ToString().Trim();
                if (option.Trim()!="" && option.IndexOf("ATH") != -1)
                {
                    lblPromo.Visible = true;
                    Url = "~/InvoiceATH.aspx?OrderID=" + OrderID;
                }
                else
                {
                    Url = "~/FinalInvoice.aspx?OrderID=" + OrderID;
                }
                lnkPrint.NavigateUrl = Url;
                var imgLink = new SecureData();
                string link = imgLink.GenerateHash(_drOrder["OrderNumb"].ToString().Trim(), CodedDate(DateTime.Parse(_drOrder["OrderDate"].ToString())), CodedDate(DateTime.Now.AddHours(-2)));
                link = "http://www.containerstore.com/elfaInstallAdmin/orderSummary.htm?order="
                       + _drOrder["OrderNumb"].ToString().Trim() + ":" +
                       CodedDate(DateTime.Parse(_drOrder["OrderDate"].ToString())) + ":" + CodedDate(DateTime.Now.AddHours(-2))
                       + "&key=" + link;
                hlImages.NavigateUrl = link;
                string custAddress = _drOrder["address1"] + " " + _drOrder["city"] + " " + _drOrder["state"].ToString().Trim() + ", " + _drOrder["zip"];
                //hlAddress.NavigateUrl = "http://www.map-generator.net/map.php?name=Customer Address&address=" + custAddress + "&width=500&height=400&maptype=map&zoom=14&t=1289604015 ";
                hlAddress.NavigateUrl = "http://maps.google.com/maps?q=" + custAddress;
                decimal retailPrice = decimal.Parse(_drOrder["PurchasePrice"].ToString());
                if(retailPrice>0)
                {
                    decimal actual = decimal.Parse(_drOrder["Actual"].ToString());
                    decimal procent = actual*100/retailPrice;
                    procent = Math.Round(procent, 0);
                    lblSalePrice.Text = "Sale Price " + procent + "% of Retail Price";
                }
                if (int.Parse(_drOrder["ExemptID"].ToString()) > 0) lblExempt.Visible = true;
                _discountID = int.Parse(_drOrder["DiscountID"].ToString());
            }
            _drOrder.Close();
            _drOrder = orderInfo.GetTroubleLog(OrderID);
            if(_drOrder.HasRows)
            {
                btnLog.BackColor = System.Drawing.Color.DodgerBlue;
            }
            _drOrder.Close();
            CheckCalls();
            if (_discountID > 0) lblReason.Text = orderInfo.DiscountReason(_discountID);
            if (_orderNumb.Substring(0, 3) == "ORG")
            {
                _drInfo = orderInfo.ShowOrganizerOrder(OrderID);
                _drInfo.Read();
                int referenceID = int.Parse(_drInfo["ReferenceID"].ToString());
                _drInfo.Close();
                Response.Redirect("~/OrganizerInvoice.aspx?OrderID=" + referenceID);
            }
        }
        protected void BtnEditClick(object Sender, EventArgs E)
        {
            Response.Redirect("EditOrder.aspx?OrderID=" + OrderID);
        }
        //protected void BtnDeleteClick(object Sender, EventArgs E)
        //{
        //    Response.Redirect("Confirm.aspx?OrderID=" + OrderID);
        //}
        void CheckCalls()
        {
            var callsInfo = new OrderData();
            _drInfo = callsInfo.GetCallsInfo(OrderID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                string lblText;
                if (bool.Parse(_drInfo["Call1"].ToString()))
                {
                    lblText = "Call #1:&nbsp;&nbsp;" + DateTime.Parse(_drInfo["Date1"].ToString()).ToShortDateString() + "&nbsp;&nbsp;" + DateTime.Parse(_drInfo["Date1"].ToString()).ToShortTimeString();
                    lblText = lblText + "&nbsp;&nbsp;" + _drInfo["Result1"];
                    lblCall1.Text = lblText;
                }
                if (bool.Parse(_drInfo["Call2"].ToString()))
                {
                    lblText = "Call #2:&nbsp;&nbsp;" + DateTime.Parse(_drInfo["Date2"].ToString()).ToShortDateString() + "&nbsp;&nbsp;" + DateTime.Parse(_drInfo["Date2"].ToString()).ToShortTimeString();
                    lblText = lblText + "&nbsp;&nbsp;" + _drInfo["Result2"];
                    lblCall2.Text = lblText;
                }
                if (bool.Parse(_drInfo["Call3"].ToString()))
                {
                    lblText = "Call #3:&nbsp;&nbsp;" + DateTime.Parse(_drInfo["Date3"].ToString()).ToShortDateString() + "&nbsp;&nbsp;" + DateTime.Parse(_drInfo["Date3"].ToString()).ToShortTimeString();
                    lblText = lblText + "&nbsp;&nbsp;" + _drInfo["Result3"];
                    lblCall3.Text = lblText;
                }
            }
            _drInfo.Close();
        }
        protected void BtnLogClick(object Sender, EventArgs E)
        {
            Response.Redirect("TroubleLog.aspx?OrderID=" + OrderID);
            //Response.Redirect("TroubleInfo.aspx?OrderID=" + OrderID);
        }

        static string CodedDate(DateTime Calcdate)
        {
            string date = Calcdate.Year + "-" +
                          Calcdate.Month.ToString().Trim().PadLeft(2, '0') + "-" +
                          Calcdate.Day.ToString().Trim().PadLeft(2, '0');
            return date;
        }

        protected void BtnServiceClick(object Sender, EventArgs E)
        {
            Response.Redirect("ServiceOrder.aspx?OrderID=" + OrderID);
        }

        protected void BtnRequestClick(object Sender, EventArgs E)
        {
            txtComments.Text = "plans not readable, needs new plans please.";
            radwindowPopup.VisibleOnPageLoad = true;
        }

        void SendRequest()
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            //const string mailSeverName = "relay-hosting.secureserver.net";
            var mailSeverName = MailServer; 
            const string from = "confirmation@elfainstall.com";
            const string to = "tcsinstall@containerstore.com";
            //const string to = "Vladimir.TX@gmail.com";
            const string subject = "eis order requests";
            string body = "Store: " + _drOrder["StoreCode"] + "<br>";
            body += "Order confirmation number: " + _drOrder["OrderNumb"] + "<br>";
            body += "Customer Name: " + _drOrder["fName"].ToString().Trim() + " " + _drOrder["lName"] + "<br>";
            body += "Address of install: " + _drOrder["address1"].ToString().Trim() + " " + _drOrder["address2"] + "<br>";
            body += "City: " + _drOrder["city"] + "&nbsp;&nbsp;&nbsp;State: " + _drOrder["state"] +
                    "&nbsp;&nbsp;&nbsp;&nbsp;Zip: " + _drOrder["zip"] + "<br>";
            body += "Customer Phone Number: " + _drOrder["hphone"] + "<br>";
            body += "Comments:" + txtComments.Text.Trim() + "<br>";
            body += "Request from " + _userName;
            _drOrder.Close();
            try
            {
                //var copy = new MailAddress("nodailey@containerstore.com");
                //var copy1 = new MailAddress("lisa@elfainstall.com");
                //var copy2 = new MailAddress("aramsey@elfainstall.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //message.CC.Add(copy);
                //message.CC.Add(copy1);
                //message.CC.Add(copy2);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
                //SessionData saveLog = new SessionData();
                //saveLog.AddErrorLog("EditOrder", "SendRequest", OrderID + ": Email sent to marnie@elfainstall.com");
                //msgBox1.alert("Request has been sent to 'eis order requests'");
            }
            catch (Exception ex)
            {
                SessionData saveLog = new SessionData();
                saveLog.AddErrorLog("EditOrder", "SendRequest", OrderID + ": " + ex.Message + " - to mnmurray@containerstore.com");
            }
        }
        protected void btnOk_Click(object Sender, EventArgs E)
        {
            radwindowPopup.VisibleOnPageLoad = false;
            SendRequest();
        }
        protected void btnCancel_Click(object Sender, EventArgs E)
        {
            radwindowPopup.VisibleOnPageLoad = false;
        }

        protected void BtnEmailStoreClick(object Sender, EventArgs E)
        {
            txtStoreComments.Text = "";
            rwStorePopup.VisibleOnPageLoad = true;
        }
        protected void BtnStoreSendClick(object Sender, EventArgs E)
        {
            rwStorePopup.VisibleOnPageLoad = false;
            SendStoreEmail();
        }
        protected void BtnStoreCancelClick(object Sender, EventArgs E)
        {
            rwStorePopup.VisibleOnPageLoad = false;
        }

        protected void BtnEmailVendorClick(object Sender, EventArgs E)
        {
            txtVendorComments.Text = "";
            rwVendorPopup.VisibleOnPageLoad = true;
        }

        protected void BtnVendorSendClick(object Sender, EventArgs E)
        {
            rwVendorPopup.VisibleOnPageLoad = false;
            SendVendorEmail();

        }
        protected void BtnVendorCancelClick(object Sender, EventArgs E)
        {
            rwVendorPopup.VisibleOnPageLoad = false;
        }

        void SendStoreEmail()
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            string storeCode = _drOrder["StoreCode"].ToString().Trim();
            string storeEmail = storeCode + "Managers@containerstore.com";
            var sessionInfo = new SessionData();
            _drInfo = sessionInfo.UserInfo(UserID);
            _drInfo.Read();
            string userName = _drInfo["UserName"].ToString().Trim();
            string userEmail = _drInfo["userEmail"].ToString().Trim();
            string userPhone = _drInfo["userPhone"].ToString().Trim();
            if (userPhone == "") userPhone = "888-202-7622";
            _drInfo.Close();

            //storeEmail = "nancy@elfainstall.com";  // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if(storeEmail=="")
            {
                _drOrder.Close();
                msgBox1.alert("Store Manager Email Missing.");
                return;
            }
            //const string mailSeverName = "relay-hosting.secureserver.net";
            var mailSeverName = MailServer; 
            const string from = "confirmation@elfainstall.com";
            string to = storeEmail;
            const string subject = "Order Installation Info";
            string body = "";
            body += "Order confirmation number: " + _drOrder["OrderNumb"] + "<br>";
            body += "Customer Name: " + _drOrder["fName"].ToString().Trim() + " " + _drOrder["lName"] + "<br>";
            body += "Address of install: " + _drOrder["address1"].ToString().Trim() + " " + _drOrder["address2"] + "<br>";
            body += "City: " + _drOrder["city"] + "&nbsp;&nbsp;&nbsp;State: " + _drOrder["state"] +
                    "&nbsp;&nbsp;&nbsp;&nbsp;Zip: " + _drOrder["zip"] + "<br>";
            body += "Customer Phone Number: " + _drOrder["hphone"] + "<br>";
            if (int.Parse(_drOrder["Status"].ToString()) > 1)
            {
                body += "Install. Date: " + DateTime.Parse(_drOrder["InstallDate"].ToString()).ToLongDateString() +
                        " at " + _drOrder["InstallTime"] + "<br>";
            }
            body += "Comments: " + txtStoreComments.Text.Trim() + "<br><br>";
            body += "If you have any questions please contact " + userName + " at " + userPhone ;
            if (userEmail != "") body += " or " + userEmail;
            body += ".<br><br>This is an auto generated email.  Please do not reply.";
            _drOrder.Close();
            try
            {
                //var copy = new MailAddress("Vladimir.TX@gmail.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //message.Bcc.Add(copy);
                if(userEmail!="")
                {
                    var copy1 = new MailAddress(userEmail);
                    message.CC.Add(copy1);
                }
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                SessionData saveLog = new SessionData();
                saveLog.AddErrorLog("EditOrder", "SendStoreEmail", OrderID + ": " + ex.Message + " - to " + storeEmail);
            }
        }

        void SendVendorEmail()
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            string vendorEmail = _drOrder["vemail"].ToString().Trim();
            string storeCode = _drOrder["StoreCode"].ToString();
            var regionInfo = new SessionData();
            _drInfo = regionInfo.RegionByStoreCode(storeCode);
            _drInfo.Read();
            string regionEmail = _drInfo["email"].ToString();
            _drInfo.Close();
            _drInfo = regionInfo.UserInfo(UserID);
            _drInfo.Read();
            string userName = _drInfo["UserName"].ToString().Trim();
            string userEmail = _drInfo["userEmail"].ToString().Trim();
            string userPhone = _drInfo["userPhone"].ToString().Trim();
            if (userPhone == "") userPhone = "888-202-7622";
            _drInfo.Close();
            //vendorEmail = "nancy@elfainstall.com";  // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var mailSeverName = MailServer;
            const string from = "confirmation@elfainstall.com";
            string to = vendorEmail;
            const string subject = "Order Installation Info";
            string body = "";
            body += "Order confirmation number: " + _drOrder["OrderNumb"] + "<br>";
            body += "Customer Name: " + _drOrder["fName"].ToString().Trim() + " " + _drOrder["lName"] + "<br>";
            body += "Address of install: " + _drOrder["address1"].ToString().Trim() + " " + _drOrder["address2"] + "<br>";
            body += "City: " + _drOrder["city"] + "&nbsp;&nbsp;&nbsp;State: " + _drOrder["state"] +
                    "&nbsp;&nbsp;&nbsp;&nbsp;Zip: " + _drOrder["zip"] + "<br>";
            body += "Customer Phone Number: " + _drOrder["hphone"] + "<br>";
            if (int.Parse(_drOrder["Status"].ToString()) > 1)
            {
                body += "Install. Date: " + DateTime.Parse(_drOrder["InstallDate"].ToString()).ToLongDateString() +
                        " at " + _drOrder["InstallTime"] + "<br>";
            }
            body += "Comments: " + txtVendorComments.Text.Trim() + "<br><br>";
            body += "If you have any questions please contact " + userName + " at " + userPhone;
            if (userEmail != "") body += " or " + userEmail;
            body += ".<br><br>This is an auto generated email.  Please do not reply.";
            _drOrder.Close();
            if (vendorEmail == "")
            {
                msgBox1.alert("Vendor Email Missing.");
                return;
            }
            try
            {
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                if(regionEmail.Trim()!="")
                {
                    var copy = new MailAddress(regionEmail);
                    message.CC.Add(copy);
                }
                if (userEmail != "")
                {
                    var copy2 = new MailAddress(userEmail);
                    message.CC.Add(copy2);
                }
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                SessionData saveLog = new SessionData();
                saveLog.AddErrorLog("OrderDetails", "SendVendorEmail", OrderID + ": " + ex.Message + " - to " + vendorEmail);
            }
        }
        private bool CustomerEmail()
        {
            bool result = false;
            var mailSeverName = MailServer;
            const string from = "confirmation@elfainstall.com";
            const string subject = "Order Invoice";
            string body = "";
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            //string to = _drOrder["email"].ToString().Trim();
            const string to = "nodailey@containerstore.com";
            decimal tax = decimal.Parse(_drOrder["Tax"].ToString());
            body += "<table style='width: 600px; font-family: Arial; font-weight:bold' cellpadding='0' cellspacing='0'>";
            body += "<tr><td colspan='2' align='center'>The Container Store Installation Service</td></tr>";
            body += "<tr><td colspan='2'><hr /></td></tr>";
            body += "<tr><td style='width: 300px' align='left'>500 FreeportPkwy Suite 100<br />Coppell,TX 75019<br />1-888-202-7622</td>";
            body += "<td style='width: 300px; font-size: x-large;'>Invoice</td></tr>";
            body += "<tr><td colspan='2'><hr /></td></tr></table>";
            body += "<table style='width: 600px; font-family: Arial; font-size: small;' cellpadding='0' cellspacing='0'>";
            body += "<tr><td align='center' colspan='4'><b>Order Information</b></td></tr>";
            body += "<tr><td align='left' style='width: 120px'>Store Order #</td><td align='left' style='width: 180px'>" + _drOrder["OrderNumb"] + "</td>";
            body += "<td align='left' style='width: 90px'>Installer</td><td align='left' style='width: 210px'>" + _drOrder["VendorName"] + "</td></tr>";
            body += "<tr><td align='left'>Store ID</td><td align='left'>" + _drOrder["StoreCode"] + "</td>";
            body += "<td align='left'>Install Date</td><td align='left'>" + _drOrder["StartTime"] + "</td></tr>";
            body += "<tr><td align='left'>Sale Date</td><td align='left'>" + DateTime.Parse(_drOrder["OrderDate"].ToString()).ToShortDateString() + "</td>";
            body += "<td align='left'>&nbsp;</td><td align='left'>&nbsp;</td></tr><tr><td colspan='4'><hr /></td></tr>";
            body += "<tr><td align='center' colspan='5'><b>Customer Information</b></td></tr>";
            body += "<tr><td align='left'>Customer Name</td><td align='left'>" + _drOrder["fName"].ToString().Trim() + " " + _drOrder["lName"] + "</td>";
            body += "<td align='left' colspan='2' rowspan='3' valign='top' style=' border: black 1pt solid;'>Installation Notes:</td></tr>";
            body += "<tr><td align='left'>Address</td><td align='left'>" + _drOrder["Address1"] + "</td></tr>";
            body += "<tr><td align='left'>City, State, Zip</td><td align='left'>" + _drOrder["city"] + ", " + _drOrder["state"] + " " + _drOrder["state"] + "</td></tr>";
            body += "<tr><td align='left'>Phone Number</td><td align='left'>" + _drOrder["hphone"] + "</td>";
            body += "<td align='left'>Email</td><td align='left'>" + _drOrder["email"] + "</td></tr>";
            body += "<tr><td>&nbsp;</td></tr>";
            string deliveryOption = bool.Parse(_drOrder["DeliveryOption"].ToString()) ? "Yes" : "No";
            body += "<tr><td align='right'>" + _drOrder["Delivery"] + "</td><td align='left'>&nbsp;Delivery Option - " + deliveryOption + "</td>";
            body += "<td align='left'>&nbsp;</td><td align='left'>&nbsp;</td></tr>";
            body += "<tr><td colspan='4' style='border: black 1pt solid;'>";
            body += "<table style='width: 100%'><tr><td align='center' colspan='3'><b>Charges</b></td></tr>";
            body += "<tr><td style='width: 300px'>Installation Charge</td><td align='right' style='width: 100px'>" + decimal.Parse(_drOrder["BaseInstallPrice"].ToString()).ToString("c") + "</td>";
            body += "<td style='width: 200px'>&nbsp;</td></tr>";
            body += "<tr><td>Delivery Option</td><td align='right'>" + decimal.Parse(_drOrder["DeliveryPrice"].ToString()).ToString("c") + "</td>";
            body += "<td style='width: 200px'>&nbsp;</td></tr>";
            body += "<tr><td>Aditional Services ($65.00/hr)</td><td align='right'>" + decimal.Parse(_drOrder["DemoPrice"].ToString()).ToString("c") + "</td>";
            body += "<td style='width: 200px'>&nbsp;</td></tr>";
            body += "<tr><td>Additional Mileage ($3.00/mile over 20 miles)</td><td align='right'>" + decimal.Parse(_drOrder["MilesPrice"].ToString()).ToString("c") + "</td>";
            body += "<td style='width: 200px'>&nbsp;</td></tr>";
            body += "<tr><td>Additional Painting ($65.00/hr)</td><td align='right'>" + decimal.Parse(_drOrder["MiscPrice"].ToString()).ToString("c") + "</td>";
            body += "<td style='width: 200px'>&nbsp;</td></tr>";
            body += "<tr><td>Parking Fee/Toll Charges</td><td align='right'>" + decimal.Parse(_drOrder["Parking"].ToString()).ToString("c") + "</td>";
            body += "<td style='width: 200px'>&nbsp;</td></tr>";
            body += "<tr><td>Adjustment</td><td align='right'>" + (-1 * decimal.Parse(_drOrder["PromoPrice"].ToString())).ToString("c") + "</td>";
            body += "<td style='width: 200px'>&nbsp;</td></tr>";
            body += "<tr><td>Other</td><td align='right'>" + decimal.Parse(_drOrder["TipPrice"].ToString()).ToString("c") + "</td><td style='width: 200px'>&nbsp;</td></tr>";
            if (tax > 0)
                body += "<tr><td>Tax</td><td align='right'>" + tax.ToString("c") + "</td><td style='width: 200px'>&nbsp;</td></tr>";
            body += "</table></td></tr><tr><td>&nbsp;</td></tr>";
            body += "<tr><td align='left' colspan='2'>Design Changes Approved by: ___________</td>";
            body += "<td align='left'>Total Charges</td><td align='left'>" + decimal.Parse(_drOrder["OrderPrice"].ToString()).ToString("c") + "</td></tr>";
            body += "<tr><td colspan='4'><hr /></td></tr></table>";
            body += "<table style='width: 600px; font-family: Arial; font-size: small;' cellpadding='2' cellspacing='2'>";
            body += "<tr><td align='center' colspan='3'><b>Payment</b></td></tr>";
            body += "<tr><td style='width: 100px'>By Check:</td><td style='width: 300px'>Check # _________________________</td>";
            body += "<td style='width: 200px'>Amount $ ________________</td></tr>";
            body += "<tr><td>By Credit Card:</td><td>Credit Card Type ____________________</td><td>Billing Zip Code ___________</td></tr>";
            body += "<tr><td colspan='2'>Credit Card # ________________________________________</td><td>Expiration ________________</td></tr>";
            body += "<tr><td colspan='3'>&nbsp;</td></tr>";
            body += "<tr><td colspan='3'>Signature &nbsp; &nbsp; &nbsp;__________________________________________</td></tr></table>";
            _drInfo.Close();
            if (to == "") return false;
            try
            {
                var message = new MailMessage(from, to, subject, body);
                var copy = new MailAddress("Vladimir.TX@gmail.com");
                message.Bcc.Add(copy);
                message.IsBodyHtml = true;
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
                result = true;
            }
            catch (Exception ex)
            {
                SessionData saveLog = new SessionData();
                saveLog.AddErrorLog("OrderDetails", "CustomerEmail", OrderID + ": " + ex.Message + " - to " + to);
                result = false;
            }
            return result;
        }

        protected void BtnEmailInvoiceClick(object Sender, EventArgs E)
        {
            if(CustomerEmail())
            {
                msgBox1.alert("Invoice has been emailed to customer.");
            }
        }
    }
}