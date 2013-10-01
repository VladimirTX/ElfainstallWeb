using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml;
using ElfaInstall.Classes;
using Telerik.Web.UI;
using Telerik.Web.UI.Skins;

namespace ElfaInstall
{
    public partial class Schedule : System.Web.UI.Page
    {
        static readonly string ConnString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];

        readonly SqlConnection _objConn = new SqlConnection(ConnString);
        private int _orderID, _regionID, _vendorID, _marketID;
        SqlDataReader _drOrder, _drInfo;
        private string _status;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["RegionID"] != null)
                {
                    _regionID = int.Parse(Request.QueryString["RegionID"]);
                    Response.Cookies["RegionID"].Value = _regionID.ToString();
                }
                else
                {
                    if (Request.Cookies["RegionID"] != null)
                    {
                        _regionID = int.Parse(Request.Cookies["RegionID"].Value);
                    }
                    else Response.Redirect("Login.aspx");
                }
                _marketID = 0;
                FillMarketList(_regionID);
            }
            else
            {
                if (Request.Cookies["RegionID"] != null)
                {
                    _regionID = int.Parse(Request.Cookies["RegionID"].Value);
                }
                else Response.Redirect("Login.aspx");
                _marketID = int.Parse(ddlMarket.SelectedValue);
            }
            RadScheduler1.SelectedDate = DateTime.Today;
            sdsAppointments.SelectCommand = "SELECT * FROM Appointments WHERE Completed=0";
            sdsVendors.SelectCommand = "pGetActiveVendors " + _regionID + "," + _marketID;
            rgAssign.DataSourceID = "sdsToAssign";
            sdsToAssign.SelectCommand = "pToAssignRegionOrders " + _regionID + "," + _marketID;
            rgOpen.DataSourceID = "sdsOpenOrders";
            sdsOpenOrders.SelectCommand = "pOpenRegionOrders " + _regionID + "," + _marketID;
            _status = Request.Cookies["status"].Value;
            if (_status != "Admin")
            {
                btnReturn.Visible = false;
                lblRegion.Visible = false;
            }
            else
            {
                lblRegion.Text = "Region: " + GetRegionName();
            }
        }
        private string GetRegionName()
        {
            string regName = "";
            var region = new SessionData();
            _drInfo = region.RegionInfo(_regionID);
            if(_drInfo.HasRows)
            {
                _drInfo.Read();
                regName = _drInfo["RegionName"].ToString();
            }
            _drInfo.Close();
            _drInfo.Dispose();
            return regName;
        }
        protected void RgOpenItemCommand(object Source, GridCommandEventArgs E)
        {
            if (E.CommandName == "Schedule")
            {
                var item = (GridDataItem)E.Item;
                string str = item.GetDataKeyValue("OrderID").ToString();
                hfOrderID.Value = str;
                pnlOpen.Visible = false;
                FillOrderData(int.Parse(str));
                pnlSchedule.Visible = true;
            }
        }
        void FillOrderData(int OrderID)
        {
            var details=new OrderData();
            _drOrder = details.NewOrderDetails(OrderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                lblOrderNumb.Text = _drOrder["OrderNumb"].ToString();
                lblOrderDate.Text = DateTime.Parse(_drOrder["OrderDate"].ToString()).ToShortDateString();
                lblAddress.Text = _drOrder["Address1"].ToString();
                lblCity.Text = _drOrder["City"] + ", " + _drOrder["State"];
                lblCustomer.Text = _drOrder["fName"] + " " + _drOrder["lName"];
                lblInstPrice.Text = decimal.Parse(_drOrder["BaseInstallPrice"].ToString()).ToString("c");
                lblPhone.Text = _drOrder["hPhone"].ToString();
                txtDescription.Text = _drOrder["SolutionDescr"].ToString();
                txtComments.Text = _drOrder["InstNotes"].ToString();
            }
            _drOrder.Close();
            _drOrder.Dispose();
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            _orderID = int.Parse(hfOrderID.Value);
            if (rdInstall.SelectedDate != null)
            {
                string dateInstall = rdInstall.SelectedDate.Value.ToShortDateString();
                if (rtStart.SelectedDate != null)
                {
                    string timeStart = rtStart.SelectedDate.Value.ToShortTimeString();
                    if (rtEnd.SelectedDate != null)
                    {
                        var details = new OrderData();
                        string timeEnd = rtEnd.SelectedDate.Value.ToShortTimeString();
                        string dateStart = dateInstall + " " + timeStart;
                        string dateEnd = dateInstall + " " + timeEnd;
                        _drOrder = details.NewOrderDetails(_orderID);
                        _drOrder.Read();
                        string orderNumb = _drOrder["OrderNumb"].ToString();
                        string notes = txtComments.Text.Trim();
                        _drOrder.Close();
                        details.SaveInstallation(orderNumb, DateTime.Parse(dateStart), DateTime.Parse(dateEnd), notes, _orderID);
                        SendScheduled(_orderID, DateTime.Parse(dateStart), DateTime.Parse(dateEnd));
                        rdInstall.SelectedDate = null;
                        rtStart.SelectedDate = null;
                        rtEnd.SelectedDate = null;
                    }
                }
            }
            //Response.Redirect("Schedule.aspx");
            pnlOpen.Visible = true;
            pnlSchedule.Visible = false;
        }
        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            //Response.Redirect("Schedule.aspx");
            pnlOpen.Visible = true;
            pnlSchedule.Visible = false;
            rdInstall.SelectedDate = null;
            rtStart.SelectedDate = null;
            rtEnd.SelectedDate = null;
        }

        protected void BtnSelectClick(object Sender, EventArgs E)
        {
            if (ddlVendors.SelectedIndex > 0)
            {
                _vendorID = int.Parse(ddlVendors.SelectedValue);
                _orderID = int.Parse(hfOrderID.Value);
                var vendor = new OrderData();
                vendor.AssignVendor(_orderID, _vendorID);
                SendEmail(_orderID);
                //Response.Redirect("Schedule.aspx");
                pnlNotAssigned.Visible = true;
                pnlVendors.Visible = false;
            }
        }
        protected void BtnCancel2Click(object Sender, EventArgs E)
        {
            //Response.Redirect("Schedule.aspx");
            pnlNotAssigned.Visible = true;
            pnlVendors.Visible = false;
        }
        protected void RgAssignItemCommand(object Source, GridCommandEventArgs E)
        {
            if (E.CommandName == "Assign")
            {
                var item = (GridDataItem)E.Item;
                string str = item.GetDataKeyValue("OrderID").ToString();
                hfOrderID.Value = str;
                _orderID = int.Parse(str);
                pnlNotAssigned.Visible = false;
                FillInstList();
                FillOrderData2(_orderID);
                pnlVendors.Visible = true;
            }
        }
        void FillInstList()
        {
            _regionID = int.Parse(Request.Cookies["RegionID"].Value);
            SessionData vendors = new SessionData();
            _drOrder = vendors.VendorsByRegion(_regionID);
            ddlVendors.DataSource = _drOrder;
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataTextField = "VendorName";
            ddlVendors.DataBind();
            _drOrder.Close();
            _drOrder.Dispose();
        }
        private SqlDataReader MarketList(int RegionID)
        {
            var objComm = new SqlCommand
                              {
                                  Connection = _objConn,
                                  CommandType = CommandType.StoredProcedure,
                                  CommandText = "pMarketsByRegion"
                              };
            objComm.Parameters.AddWithValue("@RegionID", RegionID);
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        void FillMarketList(int RegionID)
        {
            _drInfo = MarketList(RegionID);
            ddlMarket.DataSource = _drInfo;
            ddlMarket.DataValueField = "MarketID";
            ddlMarket.DataTextField = "MarketName";
            ddlMarket.DataBind();
            _drInfo.Close();
            _drInfo.Dispose();
        }
        void FillOrderData2(int OrderID)
        {
            var details = new OrderData();
            _drOrder = details.NewOrderDetails(OrderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                lblOrderNumb2.Text = _drOrder["OrderNumb"].ToString();
                lblOrderDate2.Text = DateTime.Parse(_drOrder["OrderDate"].ToString()).ToShortDateString();
                lblAddress2.Text = _drOrder["Address1"].ToString();
                lblCity2.Text = _drOrder["City"] + ", " + _drOrder["State"];
                lblCustomer2.Text = _drOrder["fName"] + " " + _drOrder["lName"];
                lblInstPrice2.Text = decimal.Parse(_drOrder["BaseInstallPrice"].ToString()).ToString("c");
                lblPhone2.Text = _drOrder["hPhone"].ToString();
                lblDate.Text = DateTime.Parse(_drOrder["InstallDate"].ToString()).ToShortDateString();
                lblTime.Text = _drOrder["InstallTime"].ToString();
                lblDuration.Text = _drOrder["Duration"].ToString();
                txtDescription2.Text = _drOrder["SolutionDescr"].ToString();
                txtComments2.Text = _drOrder["InstNotes"].ToString();
            }
            _drOrder.Close();
            _drOrder.Dispose();
        }
        void SendScheduled(int OrderID,DateTime InstallStart, DateTime InstallEnd)
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            string orderNumber = _drOrder["OrderNumb"].ToString();
            string orderDate = ConvertDate(DateTime.Parse(_drOrder["OrderDate"].ToString()), false);
            _drOrder.Close();
            _drOrder.Dispose();
            InstallEnd = InstallEnd.AddMinutes(-30);
            string installDateBegin = ConvertDate(InstallStart, true);
            string installDateEnd = ConvertDate(InstallEnd, true);
            XmlDocument doc = XMLDocSchedule(orderNumber.Trim(), orderDate, installDateBegin, installDateEnd);
            HttpSOAPRequest(OrderID,doc);
        }
        private static void HttpSOAPRequest(int OrderID, XmlDocument Xmlfile)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create("http://www.containerstore.com/eisService/schedule.wsdl");
                req.ContentType = "text/xml;charset=\"utf-8\"";
                req.Accept = "text/xml";
                req.Method = "POST";

                Stream stm = req.GetRequestStream();
                Xmlfile.Save(stm);
                stm.Close();
                WebResponse resp = req.GetResponse();
                stm = resp.GetResponseStream();
                var r = new StreamReader(stm);
                r.ReadToEnd();
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("Schedule", "HttpSOAPRequest", OrderID + ": " + ex.Message);
            }
        }
        private static XmlDocument XMLDocSchedule(string OrderNumb, string OrderDate, string InstallStart, string InstallEnd)
        {
            string document =
                @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:sch='http://containerstore.com/eis/schemas'>";
            document += XMLHeader();
            document += "<soapenv:Body><sch:ScheduleRequest>";
            document += "<sch:OrderId>" + OrderNumb + "</sch:OrderId>";
            document += "<sch:OrderDate>" + OrderDate + "</sch:OrderDate>";
            document += "<sch:InstallDateBegin>" + InstallStart + "</sch:InstallDateBegin>";
            document += "<sch:InstallDateEnd>" + InstallEnd + "</sch:InstallDateEnd>";
            document += "</sch:ScheduleRequest></soapenv:Body></soapenv:Envelope>";
            var doc = new XmlDocument();
            doc.LoadXml(document);
            return doc;
        }
        private static string XMLHeader()
        {
            const string header = @"<soapenv:Header xmlns:wsse='http://schemas.xmlsoap.org/ws/2002/07/secext'>
                <wsse:Security xmlns:wsse='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd'>
                <wsse:UsernameToken wsu:Id='UsernameToken-4391369' xmlns:wsu='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd'>
                <wsse:Username>EISUsername</wsse:Username>
                <wsse:Password Type='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText'>EISPassword</wsse:Password>
                </wsse:UsernameToken>
                </wsse:Security>
                </soapenv:Header>";
            return header;
        }
        private static string ConvertDate(DateTime GetDate, bool IfTime)
        {
            string result = GetDate.Year + "-" + GetDate.Month.ToString().PadLeft(2, '0') + "-" +
                            GetDate.Day.ToString().PadLeft(2, '0');
            if (IfTime)
            {
                result += " " + GetDate.Hour.ToString().PadLeft(2, '0') + ":" + GetDate.Minute.ToString().PadLeft(2, '0') +
                          ":00";
            }
            return result;
        }
        void SendEmail(int OrderID)
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            //const string mailSeverName = "relay-hosting.secureserver.net";
            var mailSeverName = MailServer; 
            const string from = "confirmation@elfainstall.com";
            string to;
            if (_drOrder["vemail"].ToString().Trim() != "")
            { to = _drOrder["vemail"].ToString().Trim(); }
            else
            {
                _drOrder.Close();
                return;
            }
            string subject = "elfa Installation Service Request for Store "
                    + _drOrder["StoreName"] + " Order # " + _drOrder["OrderNumb"] + " (Installer notification)";
            string body = "<table border='0' width='500'><tr><td align='center'><img border='0' src='http://www.elfainstall.com/images/logo.gif'><br>";
            body = body + "<strong><font size='4'>elfa<span style='vertical-align:super; font-weight:bold'>®</span> Installation Service<br /> Customer Request Form</font></strong></td></tr></table>";
            body = body + "<br>Date: " + DateTime.Now.ToShortDateString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            body = body + "Store: " + _drOrder["StoreName"] + "<br>";
            body = body + "Customer Name: " + _drOrder["fName"].ToString().Trim() + " " + _drOrder["lName"] + "<br>";
            body = body + "Customer Address: " + _drOrder["address1"].ToString().Trim() + " " + _drOrder["address2"] + "<br>";
            body = body + "City: " + _drOrder["city"] + "&nbsp;&nbsp;&nbsp;State: " + _drOrder["state"] + "&nbsp;&nbsp;&nbsp;&nbsp;Zip: " + _drOrder["zip"] + "<br>";
            body = body + "Customer Phone Numbers: (Home) &nbsp;&nbsp;" + _drOrder["hphone"] + "&nbsp;&nbsp; &nbsp;&nbsp;";
            if (_drOrder["phone2"].ToString().Trim() != "")
            { body = body + " (Other) &nbsp;&nbsp;" + _drOrder["phone2"]; }
            if (DateTime.Parse(_drOrder["InstallDate"].ToString()) > DateTime.Parse("01/01/2000"))
            {
                body = body + "<br>Installation Date: " +
                       DateTime.Parse(_drOrder["InstallDate"].ToString()).ToLongDateString() + ", Installation Time: " +
                       _drOrder["InstallTime"];
            }
            body = body + "<br><strong>Total installation price: " + float.Parse(_drOrder["OrderPrice"].ToString()).ToString("c") + "</strong><br><br>";
            body = body + "For additional instructions check http://www.elfainstall.com <br><br><br>";
            body = body + "This is automated e-mail. Do not reply.";
            _drOrder.Close();
            try
            {
                //var copy = new MailAddress("vladimir.tx@gmail.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //message.Bcc.Add(copy);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
                //var saveLog = new SessionData();
                //saveLog.AddErrorLog("Schedule", "SendEmail", OrderID + ": Email sent to vendor.");
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("Schedule", "SendEmail", OrderID + ": " + ex.Message);
            }
        }
    }
}