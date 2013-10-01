using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;
using System.Xml;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class VendorOrders : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        int _userID, _vendorID, _orderID, _status, _oldStatus, _payType;
        private string _vendorName, _solution, _comments, _timeStart, _timeEnd, _installedBy, _orderNumb, _installNotes, _checkNumb, _accounting;
        SqlDataReader _drInfo;
        private DateTime _instDate, _payDate, _orderDate, _vendorDate;
        private DateTime _startTime, _endTime;
        bool _pickedUp, _installed, _addMember, _confirmed, _bySurvey;
        decimal _delivery, _ordPrice, _instPrice, _demoPrice, _milesPrice, _miscPrice, _parkingPrice, _tipPrice, _promoPrice, _vendorDue, _tax;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            var userInfo = new SessionData();
            string sStatus = userInfo.UserStatus(_userID);
            if (sStatus == "Vendor")
            {
                _vendorID = userInfo.GetVendorID(_userID);
                if (_vendorID == 0)
                {
                    userInfo = null;
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                userInfo = null;
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                _drInfo = userInfo.VendorInfo(_vendorID);
                _drInfo.Read();
                _vendorName = _drInfo["VendorName"].ToString();
                _drInfo.Close();
                lblHeader.Text = "Open Orders List for " + _vendorName;
                BindDataGrid();
                hfUpdated.Value = "false";
            }
        }
        protected void GrdOrdersPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void GrdOrdersSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        void BindDataGrid()
        {
            sdsOrders.ConnectionString = ConString;
            //sdsOrders.SelectCommand = "sp_OrdersForVendor " + vendorID;
            sdsOrders.SelectCommand = "pOpenVendorOrders " + _vendorID;
        }
        protected void BtnReturnClick(object Sender, EventArgs E)
        {
            Session.RemoveAll();
        }
        protected void GrdOrdersSelectedIndexChanged(object Sender, EventArgs E)
        {
            ShowDetais();
        }
        void ShowTeam()
        {
            var members = new SessionData();
            if (members.CheckInstallers(_vendorID) > 0)
            {
                _drInfo = members.InstallerInfo(_vendorID);
                ddlInstalledBy.DataSource = _drInfo;
                ddlInstalledBy.DataValueField = "VendorName";
                ddlInstalledBy.DataTextField = "VendorName";
                ddlInstalledBy.DataBind();
                lblInstalledBy.Visible = true;
                ddlInstalledBy.Visible = true;
                if (_installedBy != "")
                { ddlInstalledBy.SelectedValue = _installedBy; }
                _addMember = true;
            }
            else _addMember = false;
            hfMembers.Value = _addMember.ToString();
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            GetOrderInfo();

            _pickedUp = cbPickedup.Checked;
            _installed = cbCompleted.Checked;
            _confirmed = cbConfirmed.Checked;
            _bySurvey = cbBySurvey.Checked;
            _checkNumb = txtCheckNumb.Text.Trim();
            if (_installed && (rdpInstDate.SelectedDate == null))
            { _installed = false; }
            _status = SetStatus();
            if (_status == 4 && _oldStatus < 4)
            {
                if (rbCC.Checked == false && rbCheck.Checked == false && rbNC.Checked == false && rbSQ.Checked == false && rbInv.Checked == false)
                {
                    msgBox1.alert("Please select payment type.");
                    return;
                }
                if (rbCheck.Checked && _checkNumb == "")
                {
                    msgBox1.alert("Please provide check number");
                    return;
                }
            }
            RecalculatePrice();
            //if (status > 2) pickedUp = true;
            //_comments = txtComments.Text.Trim();
            _installNotes = txtComments.Text.Trim();
            _comments = "";
            var orderInfo = new OrderData();
            orderInfo.NewUpdateOrder(_orderID,_pickedUp, _installed,_payDate,_status, _solution, _comments,
                _instPrice, _delivery, _demoPrice, _milesPrice, _miscPrice, _parkingPrice,
                _tipPrice,_promoPrice, _tax, _ordPrice, _confirmed, _checkNumb, _bySurvey);
            if (_oldStatus > 1)
            {
                string installer = bool.Parse(hfMembers.Value) ? ddlInstalledBy.SelectedItem.ToString() : "DEFAULT";
                orderInfo.UpdateInstaller(_orderID, installer);
            }
            if (_oldStatus < 3)
            {
                ViewCallInfo();
            }

            if (hfUpdated.Value == "true") // && rdpInstDate.SelectedDate != null)
            {
                string dateInstall;
                if (rdpInstDate.SelectedDate != null)
                {
                    dateInstall = rdpInstDate.SelectedDate.Value.ToShortDateString();
                }
                else dateInstall = "1900-01-01";
                if (rtpStart.SelectedDate != null)
                {
                    _timeStart = rtpStart.SelectedDate.Value.ToShortTimeString();
                }
                else {_timeStart = "00:00";}
                
                if (rtpEnd.SelectedDate != null)
                {
                    _timeEnd = rtpEnd.SelectedDate.Value.ToShortTimeString();
                }
                else {_timeEnd = "00:00";}                     
                string dateStart = dateInstall + " " + _timeStart;
                string dateEnd = dateInstall + " " + _timeEnd;
                orderInfo.SaveInstallation(_orderNumb, DateTime.Parse(dateStart), DateTime.Parse(dateEnd), _installNotes,_orderID);
                SendScheduled(_orderID, DateTime.Parse(dateStart), DateTime.Parse(dateEnd), _orderNumb, _orderDate);
                hfUpdated.Value = "false";
            }
            if (txtAccounting.Text.Trim() != "")
            {
                orderInfo.UpdateAccountComm(_orderID, txtAccounting.Text.Trim());
            }

            if ((hfConfirmed.Value != (_confirmed || _bySurvey).ToString()) && (_confirmed || _bySurvey))
            {
                orderInfo.SetConfirmedDate(_orderID);
            }

            if (_status == 4 && _oldStatus < 4)
            {
                _payType = 0;
                if (rbCC.Checked) _payType = 1;
                else
                {
                    if (rbCheck.Checked) _payType = 2;
                    else
                    { 
                        if (rbNC.Checked) _payType = 3; 
                        else
                        {
                            if (rbSQ.Checked) _payType = 4;
                            else if (rbInv.Checked) _payType = 5;
                        }
                    }
                }
                var userInfo = new SessionData();
                _drInfo = userInfo.VendorInfo(_vendorID);
                _drInfo.Read();
                int payID = int.Parse(_drInfo["PayID"].ToString());
                _vendorName = _drInfo["VendorName"].ToString();
                _drInfo.Close();
                if (payID > 0 && payID != _vendorID)
                {
                    orderInfo.AssignVendor(_orderID,payID);
                    orderInfo.UpdateInstaller(_orderID,_vendorName);
                }

                orderInfo.UpdateOrderPrice(_orderID, _instPrice, _delivery, _demoPrice, _milesPrice, _miscPrice,
                _tipPrice, _promoPrice, _ordPrice, _payType, _vendorDue, _vendorDate, _status);

                orderInfo.CloseAppointment(_orderID);
                SendInstalled(_orderID, _orderNumb, _orderDate);  // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                Response.Redirect("FeedBack.aspx?OrderID=" + _orderID);
            }
            Response.Redirect("VendorOrders.aspx"); 
        }
        int SetStatus()
        {
            int stat;
            if (_payDate > DateTime.Parse("01/01/1900"))
            { stat = 5; }
            else if (_installed)
            { stat = 4; }
            //else if (_pickedUp)
            //{ stat = 3; }
            else if (rdpInstDate.SelectedDate != null)
            { stat = 2; }
            else if (_vendorID != 0)
            { stat = 1; }
            else
            { stat = 0; }
            return stat;
        }
        void GetOrderInfo()
        {
            string result = grdOrders.SelectedRow.Cells[0].Text;
            _orderID = int.Parse(result);
            var orderInfo = new OrderData();
            _drInfo = orderInfo.NewOrderDetails(_orderID);
            _drInfo.Read();
            _orderDate = DateTime.Parse(_drInfo["OrderDate"].ToString());
            _vendorName = _drInfo["VendorName"].ToString();
            string customer = _drInfo["fName"].ToString().Trim() + " " + _drInfo["lName"];
            lblCustomer.Text = customer;
            _orderNumb = _drInfo["OrderNumb"].ToString().Trim();
            lblPhone.Text = _drInfo["hPhone"].ToString();
            _instDate = DateTime.Parse(_drInfo["InstallDate"].ToString());
            _startTime = DateTime.Parse(_drInfo["StartTime"].ToString());
            _endTime = DateTime.Parse(_drInfo["EndTime"].ToString());
            _pickedUp = bool.Parse(_drInfo["PickedUp"].ToString());
            //pickedUp = true;
            _installed = bool.Parse(_drInfo["Installed"].ToString());
            _payDate = DateTime.Parse(_drInfo["PaymentDate"].ToString());
            _solution = _drInfo["SolutionDescr"].ToString();
            //_comments = _drInfo["InstNotes"].ToString().Trim();
            _installNotes = _drInfo["InstNotes"].ToString().Trim();
            _instPrice = decimal.Parse(_drInfo["BaseInstallPrice"].ToString());
            _delivery = decimal.Parse(_drInfo["DeliveryPrice"].ToString());
            _demoPrice = decimal.Parse(_drInfo["DemoPrice"].ToString());
            _promoPrice = decimal.Parse(_drInfo["PromoPrice"].ToString());
            _milesPrice = decimal.Parse(_drInfo["MilesPrice"].ToString());
            _miscPrice = decimal.Parse(_drInfo["MiscPrice"].ToString());
            _parkingPrice = decimal.Parse(_drInfo["Parking"].ToString());
            _tipPrice = decimal.Parse(_drInfo["TipPrice"].ToString());
            _tax = decimal.Parse(_drInfo["Tax"].ToString());
            _ordPrice = decimal.Parse(_drInfo["OrderPrice"].ToString());
            _oldStatus = int.Parse(_drInfo["status"].ToString());
            _installedBy = _drInfo["Installer"].ToString().Trim();
            _confirmed = bool.Parse(_drInfo["Approved"].ToString());
            _bySurvey = bool.Parse(_drInfo["bySurvey"].ToString());
            hfConfirmed.Value = (_confirmed ||_bySurvey).ToString();
            _checkNumb = _drInfo["CheckNumb"].ToString().Trim();
            _payType = int.Parse(_drInfo["PayType"].ToString());
            _vendorDue = decimal.Parse(_drInfo["VendorDue"].ToString());
            if (_drInfo["VendorDate"].ToString().Trim() != "")
            { _vendorDate = DateTime.Parse(_drInfo["VendorDate"].ToString()); }
            else _vendorDate = DateTime.Parse("01/01/1900");
            _accounting = _drInfo["Comments4"].ToString().Trim();
            _drInfo.Close();
            if (_oldStatus < 4)
                CheckCalls();
            
        }
        protected void BtnCloseClick(object Sender, EventArgs E)
        {
            Response.Redirect("VendorOrders.aspx");
        }

        void CheckCalls()
        {
            var callsInfo = new OrderData();
            _drInfo = callsInfo.GetCallsInfo(_orderID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                if (bool.Parse(_drInfo["Call1"].ToString()))
                {
                    pnlCall1.Visible = true;
                    ddlCall1.SelectedValue = _drInfo["Result1"].ToString();
                    rdpDate1.SelectedDate = DateTime.Parse(_drInfo["Date1"].ToString());
                    pnlCall1.Visible = true;
                    pnlCall2.Visible = true;
                    //rdpDate2.SelectedDate = DateTime.Today;
                }
                if (bool.Parse(_drInfo["Call2"].ToString()))
                {
                    pnlCall2.Visible = true;
                    ddlCall2.SelectedValue = _drInfo["Result2"].ToString();
                    rdpDate2.SelectedDate = DateTime.Parse(_drInfo["Date2"].ToString());
                    pnlCall2.Visible = true;
                    pnlCall3.Visible = true;
                    //rdpDate3.SelectedDate = DateTime.Today;
                }
                if (bool.Parse(_drInfo["Call3"].ToString()))
                {
                    pnlCall3.Visible = true;
                    ddlCall3.SelectedValue = _drInfo["Result3"].ToString();
                    rdpDate3.SelectedDate = DateTime.Parse(_drInfo["Date3"].ToString());
                }
            }
            else
            {
                pnlCall1.Visible = true;
                //rdpDate1.SelectedDate = DateTime.Today;
            }
            _drInfo.Close();
        }

        void ViewCallInfo()
        {
            var callsInfo = new OrderData();
            _drInfo = callsInfo.GetCallsInfo(_orderID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                if (ddlCall1.SelectedIndex > 0 && !bool.Parse(_drInfo["Call1"].ToString()) && rdpDate1.SelectedDate!=null)
                { SaveCallInfo(1, DateTime.Parse(rdpDate1.SelectedDate.ToString()), ddlCall1.SelectedValue); }
                if (ddlCall2.SelectedIndex > 0 && !bool.Parse(_drInfo["Call2"].ToString()) && rdpDate2.SelectedDate!=null)
                { SaveCallInfo(2, DateTime.Parse(rdpDate2.SelectedDate.ToString()), ddlCall2.SelectedValue); }
                if (ddlCall3.SelectedIndex > 0 && !bool.Parse(_drInfo["Call3"].ToString()) && rdpDate3.SelectedDate!=null)
                { SaveCallInfo(3, DateTime.Parse(rdpDate3.SelectedDate.ToString()), ddlCall3.SelectedValue); }
                _drInfo.Close();
            }
            else
            {
                if (ddlCall1.SelectedIndex > 0 && rdpDate1.SelectedDate!=null)
                { SaveCallInfo(1, DateTime.Parse(rdpDate1.SelectedDate.ToString()), ddlCall1.SelectedValue); }
            }
        }
        void SaveCallInfo(int CallID, DateTime CallDate, string Status)
        {
            var callsInfo = new OrderData();
            callsInfo.SaveCallsInfo(_orderID, CallID, CallDate, Status);
        }
        protected void BtnResetClick(object Sender, EventArgs E)
        {
            rdpInstDate.SelectedDate = null;
            rtpStart.SelectedDate = null;
            rtpEnd.SelectedDate = null;
            rdpInstDate.Enabled = true;
            rtpStart.Enabled = true;
            rtpEnd.Enabled = true;
        }

        static void SendScheduled(int OrderID, DateTime InstallStart, DateTime InstallEnd, string OrderNumber, DateTime OrdDate)
        {
            string orderDate = ConvertDate(OrdDate, false);
            InstallEnd = InstallEnd.AddMinutes(-30);
            string installDateBegin = ConvertDate(InstallStart, true);
            string installDateEnd = ConvertDate(InstallEnd, true);
            var doc = XMLDocSchedule(OrderNumber.Trim(), orderDate, installDateBegin, installDateEnd);
            HttpSOAPRequest(OrderID, doc);
        }
        void SendInstalled(int OrderID,string OrderNumber, DateTime OrdDate)
        {
            var orderInfo = new OrderData();
            _drInfo = orderInfo.NewOrderDetails(OrderID);
            _drInfo.Read();
            string orderDate = ConvertDate(OrdDate, false);
            _drInfo.Close();
            var doc = XMLDocInstall(OrderNumber.Trim(), ConvertDate(DateTime.Parse(orderDate), false));
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
                saveLog.AddErrorLog("VendorOrders", "HttpSOAPRequest", "Order - " + OrderID + ": " + ex.Message);
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

        private static XmlDocument XMLDocInstall(string OrderNumb, string InstallDate)
        {
            string document = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:sch='http://containerstore.com/eis/schemas'>";
            document += XMLHeader();
            document += "<soapenv:Body><sch:OrderInstalledRequest>";
            document += "<sch:OrderId>" + OrderNumb + "</sch:OrderId>";
            document += "<sch:OrderDate>" + InstallDate + "</sch:OrderDate>";
            document += "</sch:OrderInstalledRequest></soapenv:Body></soapenv:Envelope>";
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

        protected void RdpInstDateSelectedDateChanged(object Sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs E)
        {
            hfUpdated.Value = "true";
        }

        protected void RtpStartSelectedDateChanged(object Sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs E)
        {
            hfUpdated.Value = "true";
        }

        protected void RtpEndSelectedDateChanged(object Sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs E)
        {
            hfUpdated.Value = "true";
        }

        protected void GrdOrdersRowDataBound(object Sender, GridViewRowEventArgs E)
        {
            if (E.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = E.Row.Cells[5];
                string sValue = cell.Text.Trim();
                if (sValue == "D")
                {
                    E.Row.ForeColor = System.Drawing.Color.Blue;
                    E.Row.Font.Bold = true;
                }
            }
        }

        protected void txtComments_TextChanged(object sender, EventArgs e)
        {
            hfUpdated.Value = "true";
        }

        void RecalculatePrice()
        {
            if (txtDemoPrice.Text.Trim() == "" || txtDemoPrice.Text.Trim() == "0.00")
            { _demoPrice = 0; }
            else
            { _demoPrice = decimal.Parse(txtDemoPrice.Text); }
            if (txtMilesPrice.Text.Trim() == "" || txtMilesPrice.Text.Trim() == "0.00")
            { _milesPrice = 0; }
            else
            { _milesPrice = decimal.Parse(txtMilesPrice.Text); }
            if (txtMiscPrice.Text.Trim() == "" || txtMiscPrice.Text.Trim() == "0.00")
            { _miscPrice = 0; }
            else
            { _miscPrice = decimal.Parse(txtMiscPrice.Text); }
            if (txtParking.Text.Trim() == "" || txtParking.Text.Trim() == "0.00")
            { _parkingPrice = 0; }
            else
            { _parkingPrice = decimal.Parse(txtParking.Text); }
            if (txtTipPrice.Text.Trim() == "" || txtTipPrice.Text.Trim() == "0.00")
            { _tipPrice = 0; }
            else
            { _tipPrice = decimal.Parse(txtTipPrice.Text); }
            if (txtTax.Text.Trim() == "" || txtTax.Text.Trim() == "0.00")
            { _tax = 0; }
            else
            { _tax = decimal.Parse(txtTax.Text.Trim()); }

            _ordPrice = _delivery + _demoPrice + _instPrice + _milesPrice + _miscPrice + _tipPrice + _parkingPrice - _promoPrice + _tax;
        }

        protected void BtnUpdateClick(object Sender, EventArgs E)
        {
            GetOrderInfo();
            RecalculatePrice();
            _comments = "";
            var orderInfo = new OrderData();
            orderInfo.NewUpdateOrder(_orderID, _pickedUp, _installed, _payDate, _status, _solution, _comments,
                _instPrice, _delivery, _demoPrice, _milesPrice, _miscPrice, _parkingPrice, _tipPrice,_promoPrice, _tax, _ordPrice, _confirmed, _checkNumb, _bySurvey);

            ShowDetais();
        }

        void ShowDetais()
        {
            GetOrderInfo();
            hfUpdated.Value = "false";
            cbCompleted.Checked = _installed;
            cbPickedup.Checked = _pickedUp;
            cbConfirmed.Checked = _confirmed;
            cbBySurvey.Checked = _bySurvey;
            cbCompleted.Enabled = !_installed;
            lblBase.Text = _instPrice.ToString("c");
            lblDelivery.Text = _delivery.ToString("c");
            txtDemoPrice.Text = _demoPrice.ToString();
            txtMilesPrice.Text = _milesPrice.ToString();
            txtMiscPrice.Text = _miscPrice.ToString();
            txtParking.Text = _parkingPrice.ToString();
            txtTipPrice.Text = _tipPrice.ToString();
            txtTax.Text = _tax.ToString();
            lblInvoice.Text = _ordPrice.ToString("c");
            if (_instDate > DateTime.Parse("01/01/2000"))
            {
                rdpInstDate.SelectedDate = _instDate;
                rdpInstDate.Enabled = false;  ///// !!!!!!!!!!!!!!!!!!!!
                rtpStart.SelectedDate = _startTime;
                rtpEnd.SelectedDate = _endTime;
                rtpStart.Enabled = false;   ///// !!!!!!!!!!!!!!!!!!!
                rtpEnd.Enabled = false;    ///// !!!!!!!!!!!!!!!!!!!!
                lblWarning.Visible = true;
                btnReset.Visible = true;
            }
            else
            {
                rdpInstDate.SelectedDate = null;
                rtpStart.SelectedDate = null;
                rtpEnd.SelectedDate = null;
            }
            txtComments.Text = _installNotes;
            pnlEdit.Visible = true;
            if (_oldStatus > 1)
            {
                lblWarning.Visible = true;
                ShowTeam();
            }
            if (_delivery > 0)
            {
                lblPickedUp.Visible = true;
                cbPickedup.Visible = true;
            }
            switch(_payType)
            {
                case 1:
                    rbCC.Checked = true;
                    break;
                case 2:
                    rbCheck.Checked = true;
                    break;
                case 3:
                    rbNC.Checked = true;
                    break;
                case 4:
                    rbSQ.Checked = true;
                    break;
                case 5:
                    rbInv.Checked = true;
                    break;
            }
            grdOrders.Visible = false;
            lblDelivComm.Visible = false;
            lblHeader.Text = "Open Order # " + _orderNumb;
            txtAccounting.Text = _accounting;
            if (_userID == 59) pnlInstallation.Visible = true;
        }
    }
}