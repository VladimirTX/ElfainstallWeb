using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    using System.IO;
    using System.Net;
    using System.Xml;

    public partial class CheckList : System.Web.UI.Page
    {
        public int OrderID;
        SqlDataReader _drOrder, _drInfo;
        private Space _oneSpace=new Space();
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        private string _storeCode;
        private decimal _oldService, _newService;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.QueryString["OrderID"] != null)
            { OrderID = int.Parse(Request.QueryString["OrderID"]); }
            else
            {
                Response.Redirect("Orders.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    int.Parse(Request.Cookies["UserID"].Value);
                }
                var orderInfo = new OrderData();
                _drOrder = orderInfo.NewOrderDetails(OrderID);
                _drOrder.Read();
                lblCustomer.Text = _drOrder["fName"] + " " + _drOrder["lName"];
                lblProductAmt.Text = _drOrder["PurchasePrice"].ToString();
                lblInstallAmt.Text = _drOrder["OrderPrice"].ToString();
                if (DateTime.Parse(_drOrder["StartTime"].ToString())>DateTime.Parse("01/01/2000"))
                rdpInstallDateStart.SelectedDate = DateTime.Parse(_drOrder["StartTime"].ToString());
                if (DateTime.Parse(_drOrder["EndTime"].ToString()) > DateTime.Parse("01/01/2000"))
                rdpInstallDateEnd.SelectedDate = DateTime.Parse(_drOrder["EndTime"].ToString());
                ddlVendor1.SelectedValue = _drOrder["VendorID"].ToString();
                txtComments.Text = _drOrder["InstNotes"].ToString();
                hfOrderNumber.Value = _drOrder["OrderNumb"].ToString();
                _storeCode = _drOrder["StoreCode"].ToString();
                _drOrder.Close();
                FillSpecialists();
                FillVendor1List();
                FillVendor2List();
                _drOrder = orderInfo.GetAthomeData(OrderID);
                if(_drOrder.HasRows)
                {
                    _drOrder.Read();
                    ddlSpecialist.SelectedValue = _drOrder["SpecialistID"].ToString();
                    ddlPickUp.SelectedValue = _drOrder["PickUp"].ToString().Trim();
                    ddlLocation.SelectedValue = _drOrder["Location"].ToString();
                    string staging = _drOrder["Staging"].ToString().Trim();
                    string styling = _drOrder["Styling"].ToString().Trim();
                    ddlStaging.SelectedValue = staging;
                    ddlStyling.SelectedValue = styling;
                    ddlVendor2.SelectedValue = _drOrder["Vendor2ID"].ToString();
                    txtProc1.Text = _drOrder["Proc1"].ToString();
                    txtProc2.Text = _drOrder["Proc2"].ToString();
                    if (staging == "N") rdpStagingDate.Enabled = false;
                    else
                    {
                        if (_drOrder["StagingDate"].ToString().Trim() != "") 
                            rdpStagingDate.SelectedDate = DateTime.Parse(_drOrder["StagingDate"].ToString());
                    }
                    if (styling == "N") rdpStylingDate.Enabled = false;
                    else
                    {
                        if (_drOrder["StylingDate"].ToString().Trim() != "") 
                            rdpStylingDate.SelectedDate = DateTime.Parse(_drOrder["StylingDate"].ToString());
                    }
                    txtSpecial.Text = _drOrder["Special"].ToString();
                    bool completed = bool.Parse(_drOrder["Completed"].ToString());
                    cbCompleted.Checked = bool.Parse(_drOrder["Completed"].ToString());
                    hfCompleted.Value = completed.ToString();
                    rntServices.Text = decimal.Parse(_drOrder["AddServices"].ToString()).ToString();
                    hfAddService.Value = rntServices.Text.Trim();
                    rntNonElfa.Text = decimal.Parse(_drOrder["NonElfaCost"].ToString()).ToString();
                }
                else
                {
                    rdpStagingDate.Enabled = false;
                    rdpStylingDate.Enabled = false;
                    rntServices.Value = 0;
                    rntNonElfa.Value = 0;
                    hfAddService.Value = "0";
                }
                _drOrder.Close();
                btnPrint.NavigateUrl = "~/Reports/AtHomeCheckList.aspx?OrderID=" + OrderID;
                hfVendor.Value = "false";
                hfUpdated.Value = "false";
            }
            ShowSpaces();
        }

        void FillSpecialists()
        {
            var specialists = new OrderData();
            _drInfo = specialists.ShowSpecialists();
            ddlSpecialist.DataSource = _drInfo;
            ddlSpecialist.DataValueField = "SpecialistID";
            ddlSpecialist.DataTextField = "Specialist";
            ddlSpecialist.DataBind();
            _drInfo.Close();
        }

        void FillVendor1List()
        {
            var vendors = new SessionData();
            int storeID = vendors.StoreIDByCode(_storeCode);
            _drInfo = vendors.VendorListByStore(storeID);
            ddlVendor1.DataSource = _drInfo;
            ddlVendor1.DataValueField = "VendorID";
            ddlVendor1.DataTextField = "VendorName";
            ddlVendor1.DataBind();
            _drInfo.Close();
        }

        void FillVendor2List()
        {
            var vendors = new SessionData();
            int storeID = vendors.StoreIDByCode(_storeCode);
            _drInfo = vendors.VendorListByStore(storeID);
            ddlVendor2.DataSource = _drInfo;
            ddlVendor2.DataValueField = "VendorID";
            ddlVendor2.DataTextField = "VendorName";
            ddlVendor2.DataBind();
            _drInfo.Close();
        }

        void ShowSpaces()
        {
            var details = new OrderData();
            _drInfo = details.GetSpacesInfo(OrderID);
            while(_drInfo.Read())
            {
                var tR1 = new TableRow();
                var tC1 = new TableCell();
                _oneSpace = (Space) LoadControl(("~/Space.ascx"));
                _oneSpace.OrderID = int.Parse(_drInfo["OrderID"].ToString());
                _oneSpace.SpaceID = int.Parse(_drInfo["SpaceID"].ToString());
                _oneSpace.SpaceName = _drInfo["SpaceName"].ToString();
                _oneSpace.SpaceNumber = _drInfo["SpaceNumber"].ToString();
                _oneSpace.Texture = bool.Parse(_drInfo["Texture"].ToString());
                _oneSpace.Description = _drInfo["Description"].ToString();
                _oneSpace.Color = int.Parse(_drInfo["Color"].ToString());
                _oneSpace.NonElfa = _drInfo["NonElfa"].ToString();
                _oneSpace.Instruction = _drInfo["Instruction"].ToString().Trim();
                _oneSpace.Removal = _drInfo["Removal"].ToString().Trim();
                _oneSpace.ColorName = _drInfo["ColorName"].ToString().Trim();
                if (_oneSpace.SpaceID > 0)
                {
                    tC1.Controls.Add(_oneSpace);
                    tR1.Cells.Add(tC1);
                    tblSpaces.Rows.Add(tR1);
                }
                _oneSpace = null;
            }
            _drInfo.Close();
        }

        protected void BtnAddClick(object Sender, EventArgs E)
        {
            SaveListHeader();

            var spaces = new OrderData();
            spaces.AddSpace(OrderID, "", "", false, "", 0, "", "","","");

            Response.Redirect("CheckList.aspx?OrderID=" + OrderID);
        }

        protected void BtnCloseClick(object Sender, EventArgs E)
        {
            SaveListHeader();

            ClientScript.RegisterStartupScript(Page.GetType(), "Close", "Close();", true);
        }

        void SaveListHeader()
        {
            DateTime? dateStart = rdpInstallDateStart.SelectedDate;
            DateTime? dateEnd = rdpInstallDateEnd.SelectedDate;
            _oldService = decimal.Parse(hfAddService.Value);
            _newService = decimal.Parse(rntServices.Text);


            if (dateStart != null && dateEnd != null)
            {
                if (dateEnd < dateStart) return;

                if (hfUpdated.Value == "true")
                {
                    SendScheduled((DateTime)dateStart, (DateTime)dateEnd);
                }
            }
            var athomeinfo = new OrderData();
            athomeinfo.UpdateSpaceHeader(OrderID, dateStart, dateEnd, txtComments.Text.Trim());
            athomeinfo.SaveAtHome(OrderID, int.Parse(ddlSpecialist.SelectedValue), ddlPickUp.SelectedValue,
                ddlLocation.SelectedValue, ddlStaging.SelectedValue, rdpStagingDate.SelectedDate,
                ddlStyling.SelectedValue, rdpStylingDate.SelectedDate, int.Parse(ddlVendor1.SelectedValue),
                int.Parse(ddlVendor2.SelectedValue), txtProc1.Text.Trim(), txtProc2.Text.Trim(),txtSpecial.Text.Trim(),
                cbCompleted.Checked, _newService, decimal.Parse(rntNonElfa.Text));
            if(cbCompleted.Checked && hfCompleted.Value=="False")
            {
                SendCompletionEmail();
            }
            if (_oldService != _newService)
            {
                athomeinfo.SaveAdditionalDemo(OrderID, _newService - _oldService);
            }
        }

        protected void DdlStagingSelectedIndexChanged(object Sender, EventArgs E)
        {
            if (ddlStaging.SelectedValue == "N")
            {
                rdpStagingDate.Enabled = false;
                rdpStagingDate.SelectedDate = null;
            }
            else rdpStagingDate.Enabled = true;
        }

        protected void DdlStylingSelectedIndexChanged(object Sender, EventArgs E)
        {
            if (ddlStyling.SelectedValue == "N")
            {
                rdpStylingDate.Enabled = false;
                rdpStylingDate.SelectedDate = null;
            }
            else rdpStylingDate.Enabled = true;
        }

        protected void RdpInstallDateStartSelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            hfUpdated.Value = "true";
        }

        protected void RdpInstallDateEndSelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            hfUpdated.Value = "true";
        }

        void SendScheduled(DateTime InstallStart, DateTime InstallEnd)
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            string orderNumber = _drOrder["OrderNumb"].ToString();
            string orderDate = ConvertDate(DateTime.Parse(_drOrder["OrderDate"].ToString()), false);
            _drOrder.Close();
            InstallEnd = InstallEnd.AddMinutes(-30);
            string installDateBegin = ConvertDate(InstallStart, true);
            string installDateEnd = ConvertDate(InstallEnd, true);
            XmlDocument doc = XMLDocSchedule(orderNumber.Trim(), orderDate, installDateBegin, installDateEnd);
            HttpSOAPRequest(doc);
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

        private string HttpSOAPRequest(XmlDocument Xmlfile)
        {
            string result = "";
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
                result = r.ReadToEnd();
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("CheckList", "HttpSOAPRequest", OrderID + ": " + ex.Message);
            }
            return result;
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

        void SendCompletionEmail()
        {
            var mailSeverName = MailServer;
            const string from = "confirmation@elfainstall.com";
            string to = "marnie@elfainstall.com";
            string subject = "ATHOME Checklist completed";
            string body = "ATHOME Checklist completed for:<br>";
            body = body + "Customer - " + lblCustomer.Text + ", Order # - " + hfOrderNumber.Value + "<br><br>";
            body = body + "For additional instructions check http://www.elfainstall.com <br><br><br>";
            body = body + "This is automated e-mail. Do not reply.";
            try
            {
                var copy = new MailAddress("katie@elfainstall.com");
                var copy1 = new MailAddress("mkmaher@containerstore.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                message.CC.Add(copy);
                message.CC.Add(copy1);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("CheckList", "SendCompletionEmail", OrderID + ": " + ex.Message);
            }
        }
    }
}