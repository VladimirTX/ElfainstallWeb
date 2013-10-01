using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI.WebControls;
using System.Xml;
using ElfaInstall.Classes;
using Telerik.Web.UI;

namespace ElfaInstall
{
    public partial class EditOrder : System.Web.UI.Page
    {
        int _userID, _status, _vendorID, _oldStatus, _payType;
        public int OrderID;
        SqlDataReader _drOrder, _drInfo;
        string _installer, _solution, _comments, _sStatus, _instName, _storeCode, _userName, _checkNumb, _state, _orderNumb;
        DateTime? _payDate, _instDate;
        bool _pickedUp, _installed, _confirmed, _delivOprion, _bySurvey;
        private decimal _delivery, _ordPrice, _instPrice, _demoPrice, _milesPrice, _miscPrice, _parkingPrice, _tipPrice, _discount, _tax;
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                if (Request.QueryString["OrderID"] != null)
                { OrderID = int.Parse(Request.QueryString["OrderID"]); }
                else
                { Response.Redirect("Orders.aspx"); }
                var checkLog = new SessionData();
                _sStatus = checkLog.UserStatus(_userID).Trim();
                if (_sStatus != "Admin" && _sStatus != "Region" && _sStatus != "Organizer")
                    Response.Redirect("Login.aspx");
                if (_sStatus != "Admin")
                {
                    btnCancel.Visible = true;
                    //txtOffice.Visible = false;
                    //lblAddNote.Visible = false;
                }
                else btnDelete.Visible = true;
                _userName = checkLog.UserName(_userID);
                hfLogin.Value = _sStatus;
                hfOrderID.Value = OrderID.ToString();
                FillStates();
                FillDiscounts();
                var orderInfo = new OrderData();
                _drOrder = orderInfo.NewOrderDetails(OrderID);
                if (_drOrder.HasRows)
                {
                    _drOrder.Read();
                    _orderNumb = _drOrder["OrderNumb"].ToString();
                    lblOrderID.Text = _orderNumb;
                    _storeCode = _drOrder["StoreCode"].ToString();
                    lblStoreID.Text = _storeCode;
                    lblSaleDate.Text = DateTime.Parse(_drOrder["OrderDate"].ToString()).ToShortDateString();
                    lblPlanner.Text = _drOrder["Planner"].ToString();
                    _installer = _drOrder["VendorID"].ToString().Trim();
                    _instName = _drOrder["VendorName"].ToString().Trim();
                    if (_instName == "") lblInstaller.Text = "Not assigned";
                    else lblInstaller.Text = _instName;
                    if ((_drOrder["PaymentDate"].ToString().Trim() != "") && (DateTime.Parse(_drOrder["PaymentDate"].ToString()) > DateTime.Parse("01/01/2000")))
                    { rdpPayment.SelectedDate = DateTime.Parse(_drOrder["PaymentDate"].ToString()); }
                    _pickedUp = bool.Parse(_drOrder["PickedUp"].ToString());
                    hfPickedUp.Value = _pickedUp.ToString();
                    cbCompleted.Checked = bool.Parse(_drOrder["Installed"].ToString());
                    txtFname.Text = _drOrder["fName"].ToString();
                    txtLname.Text = _drOrder["lName"].ToString();
                    txtAddr1.Text = _drOrder["address1"].ToString();
                    txtAddr2.Text = _drOrder["address2"].ToString();
                    txtCity.Text = _drOrder["city"].ToString();
                    txtZip.Text = _drOrder["zip"].ToString();
                    _state = _drOrder["state"].ToString().Trim();
                    ddlStates.SelectedValue = _drOrder["state"].ToString().Trim();
                    txtHphone.Text = _drOrder["hphone"].ToString().Trim();
                    txtPhone2.Text = _drOrder["phone2"].ToString().Trim();
                    txtEmail.Text = _drOrder["email"].ToString().Trim();
                    _delivOprion = bool.Parse(_drOrder["DeliveryOption"].ToString());
                    cbDelivery.Checked = _delivOprion;
                    if (_delivOprion)
                    {
                        cbPickedUp.Checked = bool.Parse(_drOrder["PickedUp"].ToString());
                        lblPickedUp.Visible = true;
                        cbPickedUp.Visible = true;
                    }
                    lblDelivery.Text = _drOrder["Delivery"].ToString();
                    //lblProc.Text = drOrder["Inst_Proc"].ToString();
                    txtSolution.Text = _drOrder["SolutionDescr"] + " / " + _drOrder["Tcscomments"];
                    lblPurchasePrice.Text = decimal.Parse(_drOrder["PurchasePrice"].ToString()).ToString("c");
                    rntInstallPrice.Text = decimal.Parse(_drOrder["BaseInstallPrice"].ToString()).ToString();
                    rntDeliveryPrice.Text = decimal.Parse(_drOrder["DeliveryPrice"].ToString()).ToString();
                    rntDemoPrice.Text = decimal.Parse(_drOrder["DemoPrice"].ToString()).ToString();
                    rntAddMiles.Text = decimal.Parse(_drOrder["MilesPrice"].ToString()).ToString();
                    rntMisc.Text = decimal.Parse(_drOrder["MiscPrice"].ToString()).ToString();
                    rntParking.Text = decimal.Parse(_drOrder["Parking"].ToString()).ToString("n");
                    rntTip.Text = decimal.Parse(_drOrder["TipPrice"].ToString()).ToString("n");
                    _discount = decimal.Parse(_drOrder["PromoPrice"].ToString());
                    rntDiscount.Text = _discount.ToString("n");
                    int discountID = int.Parse(_drOrder["DiscountID"].ToString());
                    ddlReasons.Visible = discountID > 0;
                    rntTax.Text = decimal.Parse(_drOrder["Tax"].ToString()).ToString("n");
                    lblTotalPrice.Text = decimal.Parse(_drOrder["OrderPrice"].ToString()).ToString("c");
                    lblOffice.Text = _drOrder["comments"].ToString();
                    txtComments.Text = _drOrder["InstNotes"].ToString();
                    txtInvoices.Text = _drOrder["InvoiceNotes"].ToString();
                    hfStoreID.Value = _drOrder["StoreID"].ToString();
                    _status = int.Parse(_drOrder["Status"].ToString());
                    hfStatus.Value = _status.ToString();
                    _oldStatus = _status;
                    if (_oldStatus == 2 || _oldStatus == 3) lblWarning.Visible = true;
                    if (_installer=="")
                        hfInstaller.Value = "0";
                    else hfInstaller.Value = _installer;
                    _vendorID = int.Parse(hfInstaller.Value);
                    cbConfirmed.Checked = bool.Parse(_drOrder["Approved"].ToString());
                    cbBySurvey.Checked = bool.Parse(_drOrder["bySurvey"].ToString());
                    hfConfirmed.Value = (bool.Parse(_drOrder["Approved"].ToString()) || bool.Parse(_drOrder["bySurvey"].ToString())).ToString();
                    if ((_drOrder["StartTime"].ToString().Trim() != "") && (DateTime.Parse(_drOrder["StartTime"].ToString()) > DateTime.Parse("01/01/2000")))
                    {
                        rdpInstallDate.SelectedDate = DateTime.Parse(_drOrder["StartTime"].ToString());
                        rtpTimeStart.SelectedDate = DateTime.Parse(_drOrder["StartTime"].ToString());
                        if(_oldStatus<5) btnReset.Visible = true;
                        rdpInstallDate.Enabled = false;
                        rtpTimeStart.Enabled = false;
                        rtpTimeEnd.Enabled = false;
                    }
                    else
                    {
                        rdpInstallDate.SelectedDate = null;
                        if ((_drOrder["InstallDate"].ToString().Trim() != "") && (DateTime.Parse(_drOrder["InstallDate"].ToString()) > DateTime.Parse("01/01/2000")))
                        { rdpInstallDate.SelectedDate = DateTime.Parse(_drOrder["InstallDate"].ToString()); }
                        rtpTimeStart.SelectedDate = null;
                    }
                    if ((_drOrder["EndTime"].ToString().Trim() != "") && (DateTime.Parse(_drOrder["EndTime"].ToString()) > DateTime.Parse("01/01/2000")))
                        rtpTimeEnd.SelectedDate = DateTime.Parse(_drOrder["EndTime"].ToString());
                    else rtpTimeEnd.SelectedDate = null;
                    ddlPaymentType.SelectedValue = _drOrder["PayType"].ToString().Trim();
                    hfCheckNumb.Value = _drOrder["CheckNumb"].ToString().Trim();
                    hfInstDate.Value = _drOrder["InstallDate"].ToString().Trim();
                    hfOption.Value = _drOrder["Options"].ToString().Trim();
                    string option = _drOrder["Options"].ToString().Trim();
                    ddlOption.SelectedValue = option;
                    ddlReasons.SelectedValue = _drOrder["DiscountID"].ToString().Trim();
                    if (option.Trim() != "" && option.IndexOf("ATH") != -1)
                    {
                        btnViewATH.Visible = true;
                        lblPromo.Visible = true;
                        btnReturnCalls.Visible = false;
                        pnlRegular.Visible = false;
                        pnlAtHome.Visible = true;
                        btnPrint.NavigateUrl = "~/Reports/AtHomeCheckList.aspx?OrderID=" + OrderID;
                        pnlPrintAtHome.Visible = true;
                    }
                    if (_state == "NY" || _state == "NJ" || _state == "PA" || _state == "OH" || _state == "FL" || _state == "MN" || _state == "WA" || _state == "TX")
                    {
                        int exemptID = int.Parse(_drOrder["ExemptID"].ToString());
                        if (exemptID > 0)
                        {
                            cbExempt.Checked = true;
                        }
                        else
                        {
                            btnTax.Visible = true;
                        }
                        cbExempt.Visible = true;
                    }
                    int specislStatus = int.Parse(_drOrder["SpecialStatus"].ToString());
                    if(specislStatus>0)
                    {
                        ddlSpecialStatus.SelectedValue = specislStatus.ToString();
                        rdpSpecial.SelectedDate = DateTime.Parse(_drOrder["StatusDate"].ToString());
                    }
                }
                _drOrder.Close();
                FillInstList();
                hfVendor.Value = "false";
                hfUpdated.Value = "false";
                lblInstaller.Visible = true;
                ddlInstallers.Visible = false;
                if(_status<5) btnInstallers.Visible = true;
                if (_status > 3) FillInstalledDiscounts();
                if (_orderNumb.Substring(0, 3) == "ORG")
                {
                    btnSave.Enabled = false;
                    btnSave1.Enabled = false;
                    btnPrint.Enabled = false;
                }
            }
            else
            {
                OrderID = int.Parse(hfOrderID.Value);
                _status = int.Parse(hfStatus.Value);
                _oldStatus = _status;
                _vendorID = int.Parse(hfInstaller.Value);
                //ddlOption.SelectedValue = hfOption.Value.Trim();
            }
            //if (_status < 2)
            //{ cbCompleted.Visible = false; }  --  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (_status < 4)
            { rdpPayment.Visible = false; }
            if (_status < 5)
            {
                btnClose.Visible = false;
                lblClose.Visible = false;
            }
            if(_status>5)
            {
                btnClose.Visible = false;
                lblClose.Visible = false;
            }
            if(_status>3)
            { btnCallLog.Visible = false; }
            if (hfLogin.Value == "Region" || hfLogin.Value == "Organizer")
            {
                btnCancel.Visible = true;
                rdpPayment.Enabled = false;
                btnClose.Enabled = false;
            }
            if (hfLogin.Value == "Organizer") btnCancel.Visible = false;
            btnInstallers.Text = _vendorID > 0 ? "Reassign" : "Assign";
            //if (status > 1) btnReset.Visible = true;
            //if (_userID == 23) Button1.Visible = true;
        }
        void FillInstList()
        {
            int storeID = int.Parse(hfStoreID.Value);
            var installers = new SessionData();
            _drInfo = installers.VendorListByStore(storeID);
            ddlInstallers.DataSource = _drInfo;
            ddlInstallers.DataValueField = "VendorID";
            ddlInstallers.DataTextField = "VendorName";
            ddlInstallers.DataBind();
            _drInfo.Close();
        }
        void FillDiscounts()
        {
            var reasons = new OrderData();
            _drInfo = reasons.ShowDiscountReasons();
            ddlReasons.DataSource = _drInfo;
            ddlReasons.DataValueField = "DiscountID";
            ddlReasons.DataTextField = "DiscountReason";
            ddlReasons.DataBind();
            _drInfo.Close();
        }
        void FillInstalledDiscounts()
        {
            var reasons = new OrderData();
            _drInfo = reasons.ShowInstalledDiscount();
            ddlReasons.DataSource = _drInfo;
            ddlReasons.DataValueField = "DiscountID";
            ddlReasons.DataTextField = "DiscountReason";
            ddlReasons.DataBind();
            _drInfo.Close();
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            if(UpdateOrder()) Response.Redirect("OrderDetails.aspx?OrderID=" + OrderID);
        }
        protected void BtnCloseClick(object Sender, EventArgs E)
        {
            if (_status == 5 || _status == 6)
            {
                _status = 6;
                UpdateOrder();
                Response.Redirect("OrderDetails.aspx?OrderID=" + OrderID);
            }
            else
            {
                const string msg = "You have to complete all steps before closing order!";
                string sPage = "EditOrder.aspx?OrderID=" + OrderID;
                Response.Redirect("ErrorMessage.aspx?Message=" + msg + "&Page=" + sPage);
            }
        }
        bool UpdateOrder()
        {
            var userInfo = new SessionData();
            var orderInfo = new OrderData();
            _userID = int.Parse(Request.Cookies["UserID"].Value);
            _userName = userInfo.UserName(_userID);
            if (rdpInstallDate.SelectedDate == null)
            {
                _instDate = DateTime.Parse("01/01/1900"); 
                //pickedUp = false;
            }
            else
            {
                _instDate = rdpInstallDate.SelectedDate;
                //pickedUp = true;
            }
            _pickedUp = cbPickedUp.Checked;
            _installed = cbCompleted.Checked;
            _confirmed = cbConfirmed.Checked;
            _bySurvey = cbBySurvey.Checked;
            _checkNumb = hfCheckNumb.Value; 

            //if (hfVendor.Value == "true")
            //{
            //    int newVendor = int.Parse(ddlInstallers.SelectedValue);
            //    if (newVendor != _vendorID) //  && newVendor>0
            //    {
            //        orderInfo.AssignVendor(OrderID, newVendor);
            //        SendEmail();
            //        _comments = _comments + " Vendor assigned " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " by " + _userName + "<br/>";
            //    }
            //}

            if (_installed) _pickedUp = true;
            if (_installed && rdpInstallDate.SelectedDate == null)
            {
                const string msg = "You have to schedule installation date and pick-up product before complete installation!";
                string page = "EditOrder.aspx?OrderID=" + OrderID;
                Response.Redirect("ErrorMessage.aspx?Message=" + msg + "&Page=" + page);
            }
            if (rdpPayment.SelectedDate==null)
            { _payDate = DateTime.Parse("01/01/1900"); }
            else
            {
                if (!_installed)
                {
                    const string msg = "You have to complete installation before selecting payment date!";
                    string page = "EditOrder.aspx?OrderID=" + OrderID; 
                    Response.Redirect("ErrorMessage.aspx?Message=" + msg + "&Page=" + page);
                }
                _payDate = rdpPayment.SelectedDate;
            }

            if (_status < 6) _status = SetStatus();
            if (_status < _oldStatus) _status = _oldStatus;
            _solution = txtSolution.Text;
            _comments = txtOffice.Text.Trim();
            //if (_comments == "") _comments = "Edit order";
            if (hfLogin.Value == "Admin" || (hfLogin.Value == "Region" && _comments != ""))
            {
                _comments = _userName + " " + DateTime.Now.ToShortDateString() + " " +
                            DateTime.Now.ToShortTimeString() +
                            " " + _comments + "<br/>";
            }
            else _comments = "";
            RecalculatePrice();
            //var orderInfo = new OrderData();
            //if (_status == 3) _pickedUp = true;
            if (_status == 4 && _oldStatus < 4)
            {
                if(ddlPaymentType.SelectedIndex==0)
                {
                    msgBox1.alert("Please select payment type.");
                    return false;
                }
                _payType = int.Parse(ddlPaymentType.SelectedValue);
                orderInfo.UpdatePaymentType(OrderID, _payType);
                orderInfo.CloseAppointment(OrderID);
                SendInstalled();
            }
            if (DateTime.Parse(hfInstDate.Value) != _instDate.Value)
            {
                string timeBegin = "";
                if (rtpTimeStart.SelectedDate != null)
                    timeBegin = rtpTimeStart.SelectedDate.Value.ToShortTimeString();
                string sched = " " + _instDate.Value.ToShortDateString().Trim().Replace("/20","/") + "@"+ timeBegin.Replace(":00 ","") ;
                txtComments.Text = txtComments.Text + sched + "; ";
            }
            if (hfVendor.Value == "true")
            {
                int newVendor = int.Parse(ddlInstallers.SelectedValue);
                if (newVendor != _vendorID  && newVendor>0)
                {
                    orderInfo.AssignVendor(OrderID, newVendor);
                    SendEmail();
                    string tmpComm = txtOffice.Text.Trim();
                    if (tmpComm == "") _comments = "";
                    _comments = _comments + " Vendor assigned " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " by " + _userName + "<br/>";
                }
            }
            orderInfo.NewUpdateOrder(OrderID, _pickedUp, _installed,
                _payDate, _status, _solution, _comments, _instPrice, _delivery, _demoPrice, _milesPrice,
                _miscPrice, _parkingPrice, _tipPrice, _discount, _tax, _ordPrice, _confirmed, _checkNumb,_bySurvey);
            orderInfo.UpdateCustomer(OrderID, txtHphone.Text.Trim(), 
                txtPhone2.Text.Trim(), txtEmail.Text.Trim(),txtComments.Text.Trim(),txtInvoices.Text.Trim());
            orderInfo.UpdateInstallAddress(OrderID, txtAddr1.Text, txtAddr2.Text, txtCity.Text, 
                ddlStates.SelectedValue,txtZip.Text);
            orderInfo.UpdateInstallName(OrderID,txtFname.Text.Trim(),"",txtLname.Text.Trim());
            orderInfo.SaveDiscount(OrderID,int.Parse(ddlReasons.SelectedValue));

            if (_status == 5 && _oldStatus < 5)
            {
                SetVendorDue();
            }
            if (hfUpdated.Value == "true" && _instDate > DateTime.Parse("01/01/2000") && !_installed)
            {
                if(rtpTimeStart.SelectedDate>rtpTimeEnd.SelectedDate)
                {
                    const string msg = "Installation Start Time cannot be later than Installation End Time!";
                    string page = "EditOrder.aspx?OrderID=" + OrderID;
                    Response.Redirect("ErrorMessage.aspx?Message=" + msg + "&Page=" + page);
                }
                string timeStart = "";
                string timeEnd = "";
                string dateInstall = _instDate.Value.ToShortDateString();
                if (rtpTimeStart.SelectedDate != null)
                    timeStart = rtpTimeStart.SelectedDate.Value.ToShortTimeString();
                if (rtpTimeEnd.SelectedDate != null)
                    timeEnd = rtpTimeEnd.SelectedDate.Value.ToShortTimeString();
                string dateStart = dateInstall + " " + timeStart;
                string dateEnd = dateInstall + " " + timeEnd;
                _drOrder = orderInfo.NewOrderDetails(OrderID);
                _drOrder.Read();
                string orderNumb = _drOrder["OrderNumb"].ToString();
                string notes = txtComments.Text.Trim();  
                _drOrder.Close();
                orderInfo.SaveInstallation(orderNumb, DateTime.Parse(dateStart), DateTime.Parse(dateEnd), notes, OrderID);
                SendScheduled(DateTime.Parse(dateStart), DateTime.Parse(dateEnd));
            }
            if ((hfConfirmed.Value != (_confirmed || _bySurvey).ToString()) && (_confirmed || _bySurvey))
            {
                orderInfo.SetConfirmedDate(OrderID);
                //send email to customer
                //SendCustomerEmail();
            }
            return true;
        }
        int SetStatus()
        {
            int stat;
            if (rdpPayment.SelectedDate != null)
            { stat = 5; }
            else if (_installed)
            { stat = 4; }
            //else if (_pickedUp)
            //{ stat = 3; }
            else if (rdpInstallDate.SelectedDate != null)
            { stat = 2; }
            else if (_vendorID != 0)
            { stat = 1; }
            else
            { stat = 0; }
            return stat;
        }
        void RecalculatePrice()
        {
            _instPrice = decimal.Parse(rntInstallPrice.Text);
            if (rntDeliveryPrice.Text.Trim() == "")
            { _delivery = 0; rntDeliveryPrice.Text = "0.00"; }
            else
            { _delivery = decimal.Parse(rntDeliveryPrice.Text); }
            if (rntDemoPrice.Text.Trim() == "")
            { _demoPrice = 0; rntDemoPrice.Text = "0.00"; }
            else
            { _demoPrice = decimal.Parse(rntDemoPrice.Text); }
            if (rntAddMiles.Text.Trim() == "")
            { _milesPrice = 0; rntAddMiles.Text = "0.00"; }
            else
            { _milesPrice = decimal.Parse(rntAddMiles.Text); }
            if (rntMisc.Text.Trim() == "")
            { _miscPrice = 0; rntMisc.Text = "0.00"; }
            else
            { _miscPrice = decimal.Parse(rntMisc.Text); }
            if (rntParking.Text.Trim() == "")
            { _parkingPrice = 0; rntParking.Text = "0.00"; }
            else
            { _parkingPrice = decimal.Parse(rntParking.Text); }
            if (rntTip.Text.Trim() == "")
            { _tipPrice = 0; rntTip.Text = "0.00"; }
            else
            { _tipPrice = decimal.Parse(rntTip.Text); }
            if (rntDiscount.Text.Trim() == "")
            { _discount = 0; rntDiscount.Text = "0.00"; }
            else
            { _discount = decimal.Parse(rntDiscount.Text); }
            if(rntTax.Text.Trim()=="")
            { _tax = 0; rntTax.Text = "0.00"; }
            else
            { _tax = decimal.Parse(rntTax.Text); }

            _ordPrice = _delivery + _demoPrice + _instPrice + _milesPrice + _miscPrice 
                + _parkingPrice + _tipPrice - _discount + _tax;
            lblTotalPrice.Text = decimal.Parse(_ordPrice.ToString()).ToString("c");
        }
        void SendEmail()
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            //const string mailSeverName = "relay-hosting.secureserver.net";
            var mailSeverName = MailServer;
            const string from = "confirmation@elfainstall.com";
            string to;
            string email = _drOrder["vemail"].ToString().Trim();
            if (email != "")
            { to = email; }
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
            if (_instDate > DateTime.Parse("01/01/2000"))
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
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
                message.Dispose();
                //SessionData saveLog = new SessionData();
                //saveLog.AddErrorLog("EditOrder", "SendEmail", OrderID + ": Email sent to " + email);
            }
            catch (Exception ex)
            {
                SessionData saveLog = new SessionData();
                saveLog.AddErrorLog("EditOrder", "SendEmail", OrderID + ": " + ex.Message + " - to " + email);
            }
        }

        void SendCustomerEmail()
        {
            OrderData orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            if (_drOrder["email"].ToString().Trim() != "")
            {
                string mailSeverName = MailServer;
                const string from = "confirmation@elfainstall.com";
                //string to = _drOrder["email"].ToString().Trim();
                string to = "Vladimir.TX@gmail.com";
                const string subject = "Your elfa Installation Service";
                string store = _drOrder["StoreCode"].ToString().Trim();
                bool show = true;
                if (store == "WDC" || store == "PAR" || store == "6AV" || store == "LEX" || store == "CHI" || store == "WHP" || store == "AVA" || store == "ROC" || store == "TYC") show = false;
                string body = "";
                body += "Dear " + _drOrder["fName"].ToString().Trim() + " " + _drOrder["lName"].ToString().Trim() +
                              ",<br><br>";
                body += "Thank you for choosing to install your customized elfa space with elfa Installation Service.<br><br>";
                body += "Your installation is scheduled for " +
                        DateTime.Parse(_drOrder["InstallDate"].ToString()).ToLongDateString() + " from " +
                        DateTime.Parse(_drOrder["StartTime"].ToString()).ToShortTimeString() + " to " + DateTime.Parse(_drOrder["EndTime"].ToString()).ToShortTimeString() + ". ";
                body += "Please allow a one hour window at the beginning of your above installation time slot for the installer to arrive. ";
                body += "For example, if your install time is from 9:00 am to 1:00 pm, the installer will arrive between 9:00 am and 10:00 am. ";
                body += "If your installer is running late, you will receive a call letting you know when to expect them.<br><br>";
                body += "Your estimated charge for installation is " +
                        decimal.Parse(_drOrder["BaseInstallPrice"].ToString()).ToString("c") + ". ";
                if (show)
                {
                    body += "If you chose to have your order delivered, there will be an additional Delivery fee of $80. ";
                }
                body += "elfa Installation Service covers a 20 miles radius from the purchase store. ";
                body += "The additional mileage charge is one-way $3 per mile beyond 20 miles. ";
                body += "For example, if the location of installation is 30 miles from the store, the additional fee is $30 ($3 x 10 additional miles beyond purchase store). ";
                body += "You will also be responsible for any additional parking fees or tolls incurred by our installers.<br><br>";
                body += "Our standard installation fee includes the removal of up to two shelves and closet rods per wall, plus related touch-ups with flat white paint. ";
                body += "The installer can paint the entire closet and remove additional existing shelving (such as shoe shelves, built-in dresser shelves, cabinets or drawers) for an additional fee. ";
                body += "Please contact us at 888-202-7622 immediately if additional work is being requested.<br><br>";

                body += "Our installer will vacuum/sweep the space after installing your elfa solution. <br>";
                body += "Installers will remove the demolition materials and product packaging from the installation area and place them on-site at your desired location (i.e. to the garage, trash can, etc.). ";
                body += "Installers <b>cannot</b> remove materials from your home to an off-site location.<br><br>";

                body +=
                    "The Container Store does not collect for elfa Installation Service fees. The installer will provide an invoice upon completion.  We accept cash, check, Master Card, Visa, AMEX or Discover.<br><br>";
                body += "Elfa installation has a 24 hour cancellation policy. ";
                body += "Please call our toll free number, 888-202-7622 if you need to cancel your scheduled installation appointment (press 1) or have any questions about your installation.<br><br>";
                body += "Many thanks – we look forward to installing your new elfa space!<br><br>";
                body += "Sincerely,<br>";
                body += "elfa Installation Service<br><br>";
                body += "<i>This is an automated e-mail. Do not reply.</i>";
                try
                {
                    var copy = new MailAddress("nodailey@containerstore.com");
                    var message = new MailMessage(from, to, subject, body);
                    message.IsBodyHtml = true;
                    message.Bcc.Add(copy);
                    var mailClient = new SmtpClient(mailSeverName);
                    mailClient.UseDefaultCredentials = true;
                    mailClient.Send(message);
                }
                catch (Exception ex)
                {
                    var saveLog = new SessionData();
                    saveLog.AddErrorLog("EditOrder", "SendCustomerEmail", OrderID + ": " + ex.Message);
                }
            }
            _drOrder.Close();
        }
        protected void BtnUpdateClick(object Sender, EventArgs E)
        {
            Response.Redirect("UpdatePrice.aspx?OrderID=" + OrderID);
        }
        protected void BtnResetClick(object Sender, EventArgs E)
        {
            rdpInstallDate.SelectedDate = null;
            rtpTimeStart.SelectedDate = null;
            rtpTimeEnd.SelectedDate = null;
            rdpInstallDate.Enabled = true;
            rtpTimeStart.Enabled = true;
            rtpTimeEnd.Enabled = true;
            hfStatus.Value = "1";
            //hfPickedUp.Value = "false";
            hfUpdated.Value = "true";
            OrderID = int.Parse(hfOrderID.Value);
            var orderInfo = new OrderData();
            orderInfo.CleanAppointment(OrderID);
            btnReset.Visible = false;
        }
        void SendScheduled(DateTime InstallStart, DateTime InstallEnd)
        {
            OrderID = int.Parse(hfOrderID.Value);
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
        void SendInstalled()
        {
            OrderID = int.Parse(hfOrderID.Value);
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            string orderNumber = _drOrder["OrderNumb"].ToString();
            string orderDate = ConvertDate(DateTime.Parse(_drOrder["OrderDate"].ToString()), false);
            _drOrder.Close();
            XmlDocument doc = XMLDocInstall(orderNumber.Trim(), ConvertDate(DateTime.Parse(orderDate), false));
            HttpSOAPRequest(doc);
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
                saveLog.AddErrorLog("EditOrder", "HttpSOAPRequest", OrderID + ": " + ex.Message);
            }
            return result;
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

        private static XmlDocument XMLDocInstall(string OrderNumb, string OrderDate)
        {
            string document = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:sch='http://containerstore.com/eis/schemas'>";
            document += XMLHeader();
            document += "<soapenv:Body><sch:OrderInstalledRequest>";
            document += "<sch:OrderId>" + OrderNumb + "</sch:OrderId>";
            document += "<sch:OrderDate>" + OrderDate + "</sch:OrderDate>";
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
        protected void BtnReturnClick(object Sender, EventArgs E)
        {
            Response.Redirect("OrderDetails.aspx?OrderID=" + OrderID);
        }

        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            Response.Redirect("CancelOrder.aspx?OrderID=" + OrderID);
        }

        protected void BtnCallLogClick(object Sender, EventArgs E)
        {
            pnlCallLog.Visible = true;
            pnlMain.Visible = false;
            CheckCalls();
        }
        void CheckCalls()
        {
            var callsInfo = new OrderData();
            _drInfo = callsInfo.GetCallsInfo(OrderID);
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
                    rdpDate2.SelectedDate = DateTime.Today;
                }
                if (bool.Parse(_drInfo["Call2"].ToString()))
                {
                    pnlCall2.Visible = true;
                    ddlCall2.SelectedValue = _drInfo["Result2"].ToString();
                    rdpDate2.SelectedDate = DateTime.Parse(_drInfo["Date2"].ToString());
                    pnlCall2.Visible = true;
                    pnlCall3.Visible = true;
                    rdpDate3.SelectedDate = DateTime.Today;
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
                rdpDate1.SelectedDate = DateTime.Today;
            }
            _drInfo.Close();
        }
        void ViewCallInfo()
        {
            var callsInfo = new OrderData();
            _drInfo = callsInfo.GetCallsInfo(OrderID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                if (ddlCall1.SelectedIndex > 0 && rdpDate1.SelectedDate != null && !bool.Parse(_drInfo["Call1"].ToString()))
                { SaveCallInfo(1, DateTime.Parse(rdpDate1.SelectedDate.ToString()), ddlCall1.SelectedValue); }
                if (ddlCall2.SelectedIndex > 0 && rdpDate2.SelectedDate != null && !bool.Parse(_drInfo["Call2"].ToString()))
                { SaveCallInfo(2, DateTime.Parse(rdpDate2.SelectedDate.ToString()), ddlCall2.SelectedValue); }
                if (ddlCall3.SelectedIndex > 0 && rdpDate3.SelectedDate != null && !bool.Parse(_drInfo["Call3"].ToString()))
                { SaveCallInfo(3, DateTime.Parse(rdpDate3.SelectedDate.ToString()), ddlCall3.SelectedValue); }
                _drInfo.Close();
            }
            else
            {
                if (ddlCall1.SelectedIndex > 0 && rdpDate1.SelectedDate != null)
                { SaveCallInfo(1, DateTime.Parse(rdpDate1.SelectedDate.ToString()), ddlCall1.SelectedValue); }
            }
        }
        void SaveCallInfo(int CallID, DateTime CallDate, string Status)
        {
            var callsInfo = new OrderData();
            callsInfo.SaveCallsInfo(OrderID, CallID, CallDate, Status);
        }
        protected void BtnReturnCallsClick(object Sender, EventArgs E)
        {
            pnlCallLog.Visible = false;
            pnlMain.Visible = true;
        }
        protected void BtnSaveCallsClick(object Sender, EventArgs E)
        {
            ViewCallInfo();
            Response.Redirect("OrderDetails.aspx?OrderID=" + OrderID);
        }

        protected void RdpInstallDateSelectedDateChanged(object Sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs E)
        {
            hfUpdated.Value = "true";
        }

        protected void RtpTimeStartSelectedDateChanged(object Sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs E)
        {
            hfUpdated.Value = "true";
        }

        protected void RtpTimeEndSelectedDateChanged(object Sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs E)
        {
            hfUpdated.Value = "true";
        }

        protected void RntInstallPriceTextChanged(object Sender, EventArgs E)
        {
            RecalculatePrice();
        }

        protected void RntDeliveryPriceTextChanged(object Sender, EventArgs E)
        {
            RecalculatePrice();
        }

        protected void RntAddMilesTextChanged(object Sender, EventArgs E)
        {
            RecalculatePrice();
        }

        protected void RntDemoPriceTextChanged(object Sender, EventArgs E)
        {
            RecalculatePrice();
        }

        protected void RntMiscTextChanged(object Sender, EventArgs E)
        {
            RecalculatePrice();
        }

        protected void RntDiscTextChanged(object Sender, EventArgs E)
        {
            if (rntDiscount.Value!=0)
            {
                ddlReasons.Visible = true;
                ddlReasons.Focus();
            }
            else
            {
                ddlReasons.Visible = false;
            }
            RecalculatePrice();
        }

        protected void DdlInstallersSelectedIndexChanged(object Sender, EventArgs E)
        {
            if (ddlInstallers.SelectedIndex > 0)
            { hfVendor.Value = "true"; }
        }

        protected void BtnInstallersClick(object Sender, EventArgs E)
        {
            lblInstaller.Visible = false;
            ddlInstallers.Visible = true;
            btnInstallers.Visible = false;
            hfVendor.Value = "true";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //SendCustomerEmail();
            OrderData orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            if (_drOrder["email"].ToString().Trim() != "" && _drOrder["email"].ToString().Trim().IndexOf('/') < 0)
            {
                var secData = new SecureData();
                string encoded = secData.Encrypt(OrderID.ToString());
                string flink = "Visit <a href='http://elfainstall.com/Customers/ProjectInfo.aspx?" + encoded +
                               "'>www.elfainstall.com</a>";

                string mailSeverName = MailServer;
                const string from = "confirmation@elfainstall.com";
                //string to = _drOrder["email"].ToString().Trim();
                string to = "nodailey@containerstore.com";
                const string subject = "Your elfa Installation Service";
                string body = "";
                body += "Dear " + _drOrder["fName"].ToString().Trim() + " " + _drOrder["lName"].ToString().Trim() + "<br><br>";
                body += "Congratulations on your Elfa purchase from The Container Store!!!!<br><br>";
                body +=
                    "We are excited about performing the installation services for you.  You can expect the same exceptional customer service from us as you receive from The Container Store. We will have you dancing in your new elfa space shortly.<br><br>";
                body +=
                    "Please click on the link below to start the process and confirm your installation date and time.<br><br>";
                body += "Your elfa Installation Services team is ready to serve.<br><br>";
                body += flink;
                body += "<br><br>";
                try
                {
                    var message = new MailMessage(from, to, subject, body);
                    var copy1 = new MailAddress("vlad@tomatex.com");
                    message.IsBodyHtml = true;
                    message.Bcc.Add(copy1);
                    var mailClient = new SmtpClient(mailSeverName);
                    mailClient.UseDefaultCredentials = true;
                    mailClient.Send(message);
                    Button1.Visible = false;
                }
                catch (Exception ex)
                {

                    var saveLog = new SessionData();
                    saveLog.AddErrorLog("EditOrder", "SendCustomerEmail", OrderID + ": " + ex.Message);
                }

            }
            _drOrder.Close();
        }

        void FillStates()
        {
            var states = new OrderData();
            _drInfo = states.StateList();
            ddlStates.DataSource = _drInfo;
            ddlStates.DataTextField = "code";
            ddlStates.DataValueField = "code";
            ddlStates.DataBind();
            _drInfo.Close();
        }

        protected void BtnSave1Click(object Sender, EventArgs E)
        {
            if(UpdateOrder()) Response.Redirect("OrderDetails.aspx?OrderID=" + OrderID);
        }

        private DateTime GetNextThursday(DateTime CurrDate)
        {
            DateTime date = CurrDate.AddDays(6);
            while (date.DayOfWeek != DayOfWeek.Thursday)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        private void SetVendorDue()
        {
            DateTime nextThursday = GetNextThursday(DateTime.Today);
            var orderInfo = new OrderData();
            bool deduct;
            SqlDataReader drOrder = orderInfo.NewOrderDetails(OrderID);
            drOrder.Read();
            int vendorID = int.Parse(drOrder["VendorID"].ToString());
            _instPrice = decimal.Parse(drOrder["BaseInstallPrice"].ToString());
            _delivery = decimal.Parse(drOrder["DeliveryPrice"].ToString());
            _demoPrice = decimal.Parse(drOrder["DemoPrice"].ToString());
            decimal promoPrice = decimal.Parse(drOrder["PromoPrice"].ToString());
            _milesPrice = decimal.Parse(drOrder["MilesPrice"].ToString());
            _miscPrice = decimal.Parse(drOrder["MiscPrice"].ToString());
            _parkingPrice = decimal.Parse(drOrder["Parking"].ToString());
            _tipPrice = decimal.Parse(drOrder["TipPrice"].ToString());
            int payType = int.Parse(drOrder["PayType"].ToString());
            deduct = bool.Parse(drOrder["Deduct"].ToString());
            decimal payProc = decimal.Parse(drOrder["PayProc"].ToString());
            drOrder.Close();

            decimal minPrice = 150 * 100 / payProc;
            decimal usePrice = _instPrice;
            if (minPrice > _instPrice) usePrice = minPrice;
            decimal basePrice = (usePrice + _delivery + _demoPrice - promoPrice) * payProc / 100;
            decimal vendorDue = basePrice + _milesPrice + _miscPrice + _tipPrice + _parkingPrice;
            if (deduct) vendorDue = vendorDue - 1.42m;

            //string paymentType = "";
            //paymentType = payType == 2 ? "CK" : "CC";
            //paymentType = " payment by " + paymentType;
            //_comments = paymentType + "; payd date " + nextThursday.ToShortDateString();

            orderInfo.UpdateVendorDue(OrderID,vendorDue,nextThursday);
        }

        protected void RntParkingTextChanged(object Sender, EventArgs E)
        {
            RecalculatePrice();
        }

        protected void RntTipTextChanged(object Sender, EventArgs E)
        {
            RecalculatePrice();
        }

        protected void DdlOptionSelectedIndexChanged(object Sender, EventArgs E)
        {
            string option = ddlOption.SelectedValue;
            var orderInfo = new OrderData();
            orderInfo.UpdateOption(OrderID,option);
            hfOption.Value = option.Trim();
        }

        protected void BtnDeleteClick(object Sender, EventArgs E)
        {
            Response.Redirect("Confirm.aspx?OrderID=" + OrderID);
        }

        protected void RntTaxTextChanged(object Sender, EventArgs E)
        {
            RecalculatePrice();
        }

        protected void BtnStoreSelectClick(object Sender, EventArgs E)
        {
            rwStorePopup.VisibleOnPageLoad = false;
            UpdateStore();
            Response.Redirect("EditOrder.aspx?OrderID=" + OrderID);
        }

        protected void BtnStoreCancelClick(object Sender, EventArgs E)
        {
            rwStorePopup.VisibleOnPageLoad = false;
        }

        protected void BtnChangeClick(object Sender, EventArgs E)
        {
            FillStores();
            rwStorePopup.VisibleOnPageLoad = true;
        }

        void FillStores()
        {
            var stores = new OrderData();
            _drInfo = stores.AllStores();
            ddlStores.DataSource = _drInfo;
            ddlStores.DataTextField = "Store";
            ddlStores.DataValueField = "StoreID";
            ddlStores.DataBind();
            _drInfo.Close();
        }

        void FillExempts()
        {
            var exempts = new OrderData();
            _drInfo = exempts.ShowExemptList();
            ddlExempts.DataSource = _drInfo;
            ddlExempts.DataTextField = "Description";
            ddlExempts.DataValueField = "ExemptID";
            ddlExempts.DataBind();
            _drInfo.Close();
        }

        void UpdateStore()
        {
            int storeID = int.Parse(ddlStores.SelectedValue);
            var orderInfo = new OrderData();
            orderInfo.UpdateStore(OrderID,storeID);
        }

        protected void BtnTaxClick(object Sender, EventArgs E)
        {
            CalculateTax();
        }

        decimal GetTaxRate()
        {
            decimal taxRate = 0;
            string state = ddlStates.SelectedValue;
            string city = txtCity.Text.Trim();
            string zip = txtZip.Text.Trim();
            _storeCode = lblStoreID.Text.Trim();
            if (state == "OH")
            {
                if (_storeCode == "COL") taxRate = 0.0675m;
                if (_storeCode == "CIN") taxRate = 0.065m;
            }
            else
            {
                if (state == "TX")
                {
                    taxRate = _storeCode == "ATX" ? 0.08m : 0.0825m;
                }
                else
                {
                    //if (_storeCode == "STL") taxRate = 0.09425m;
                    //else
                    //{
                        var tax = new OrderData();
                        taxRate = tax.GetTaxRate(zip, city);
                    //}
                }
            }
            return taxRate;
        }

        void CalculateTax()
        {
            decimal taxRate = GetTaxRate();
            if (taxRate > 0)
            {
                _instPrice = decimal.Parse(rntInstallPrice.Text);
                if (rntDeliveryPrice.Text.Trim() == "")
                { _delivery = 0; rntDeliveryPrice.Text = "0.00"; }
                else
                { _delivery = decimal.Parse(rntDeliveryPrice.Text); }
                if (rntDemoPrice.Text.Trim() == "")
                { _demoPrice = 0; rntDemoPrice.Text = "0.00"; }
                else
                { _demoPrice = decimal.Parse(rntDemoPrice.Text); }
                if (rntAddMiles.Text.Trim() == "")
                { _milesPrice = 0; rntAddMiles.Text = "0.00"; }
                else
                { _milesPrice = decimal.Parse(rntAddMiles.Text); }
                if (rntMisc.Text.Trim() == "")
                { _miscPrice = 0; rntMisc.Text = "0.00"; }
                else
                { _miscPrice = decimal.Parse(rntMisc.Text); }
                if (rntParking.Text.Trim() == "")
                { _parkingPrice = 0; rntParking.Text = "0.00"; }
                else
                { _parkingPrice = decimal.Parse(rntParking.Text); }
                if (rntTip.Text.Trim() == "")
                { _tipPrice = 0; rntTip.Text = "0.00"; }
                else
                { _tipPrice = decimal.Parse(rntTip.Text); }
                if (rntDiscount.Text.Trim() == "")
                { _discount = 0; rntDiscount.Text = "0.00"; }
                else
                { _discount = decimal.Parse(rntDiscount.Text); }

                _tax = (_delivery + _demoPrice + _instPrice + _milesPrice + _miscPrice
                        + _parkingPrice + _tipPrice - _discount) * taxRate;
                rntTax.Text = _tax.ToString();
                _ordPrice = _delivery + _demoPrice + _instPrice + _milesPrice + _miscPrice
                + _parkingPrice + _tipPrice - _discount + _tax;
                lblTotalPrice.Text = decimal.Parse(_ordPrice.ToString()).ToString("c");
            }
        }

        protected void BtnViewAthClick(object Sender, EventArgs E)
        {
            var baseMenu = (RadMenu)Master.FindControl("MenuAdmin");
            baseMenu.Visible = false;
            var orgMenu = (RadMenu)Master.FindControl("MenuOrganizer");
            orgMenu.Visible = false;
            var regMenu = (RadMenu)Master.FindControl("MenuRegion");
            regMenu.Visible = false;
            var logMenu = (RadMenu)Master.FindControl("MenuLogOff");
            logMenu.Visible = false;

            var retButton = new Button();
            retButton.Text = " Return ";
            retButton.Width = 120;
            retButton.Font.Bold = true;
            retButton.PostBackUrl = "~/OrderDetails.aspx?OrderID=" + OrderID;
            var pnl = (Panel) Master.FindControl("pnlMenu");
            pnl.Controls.Add(retButton);

            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            _orderNumb = _drOrder["OrderNumb"].ToString().Trim();
            _drOrder.Close();

            if (_orderNumb.Substring(0, 3) == "ORG")
            {
                _drOrder = orderInfo.ShowOrganizerOrder(OrderID);
                _drOrder.Read();
                int refNumber = int.Parse(_drOrder["ReferenceID"].ToString());
                _drOrder.Close();
                rwCheckList.NavigateUrl = "~/CheckList.aspx?OrderID=" + refNumber;
            }
            else
            {
                rwCheckList.NavigateUrl = "~/CheckList.aspx?OrderID=" + OrderID;
            }

            rwCheckList.DestroyOnClose = true;
            rwCheckList.VisibleOnPageLoad = true;
            this.pnlMain.Enabled = false;
        }

        protected void CbExemptCheckedChanged(object Sender, EventArgs E)
        {
            if(cbExempt.Checked)
            {
                FillExempts();
                rwTaxExempt.VisibleOnPageLoad = true;
            }
            else
            {
                btnTax.Visible = true;
                CalculateTax();
                SetExempt(0);
            }
        }
        void SetExempt(int ExemptID)
        {
            var exempt = new OrderData();
            exempt.SetTaxExempt(OrderID,ExemptID);
        }

        protected void BtnExemptSaveClick(object Sender, EventArgs E)
        {
            if (ddlExempts.SelectedIndex > 0)
            {
                rwTaxExempt.VisibleOnPageLoad = false;
                btnTax.Visible = false;
                rntTax.Text = "0.00";
                RecalculatePrice();
                int exemptID = int.Parse(ddlExempts.SelectedValue);
                SetExempt(exemptID);
            }
            else rwTaxExempt.VisibleOnPageLoad = false;
        }
        protected void BtnExemptCancelClick(object Sender, EventArgs E)
        {
            rwTaxExempt.VisibleOnPageLoad = false;
        }

        protected void BtnSpecialClick(object Sender, EventArgs E)
        {
            if (ddlSpecialStatus.SelectedIndex > 0 && rdpSpecial.SelectedDate == null) return;
            
            int statusid = int.Parse(ddlSpecialStatus.SelectedValue);
            var orderInfo = new OrderData();
            orderInfo.UpdateSpecialStatus(OrderID, statusid, rdpSpecial.SelectedDate.Value);
        }

        protected void BtnInvoiceClick(object Sender, EventArgs E)
        {
            string organuzer = "";
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            _orderNumb = _drOrder["OrderNumb"].ToString().Trim();
            _drOrder.Close();

            if (_orderNumb.Substring(0, 3) == "ORG")
            {
                _drOrder = orderInfo.ShowOrganizerOrder(OrderID);
                _drOrder.Read();
                int refNumber = int.Parse(_drOrder["ReferenceID"].ToString());
                _drOrder.Close();
                Response.Redirect("~/OrganizerInvoice.aspx?OrderID=" + refNumber);
            }
            else
            {
                _drOrder = orderInfo.ShowAtHomeData(OrderID);
                if (_drOrder.HasRows)
                {
                    _drOrder.Read();
                    organuzer = _drOrder["Specialist"].ToString().Trim();
                }
                _drOrder.Close();
                if (organuzer == "")
                {
                    msgBox1.alert("Organizer for this order not selected yet.");
                    return;
                }
                else Response.Redirect("~/OrganizerInvoice.aspx?OrderID=" + OrderID);
            }
        }
    }
}