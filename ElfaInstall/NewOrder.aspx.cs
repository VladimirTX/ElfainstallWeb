using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class NewOrder : System.Web.UI.Page
    {
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        int _orderID, _customerID, _storeID, _userID;
        SqlDataReader _drInfo;
        string _storeName, _orderType, _storeNumb;
        decimal _instMin, _delivery, _ordPrice, _instPrice, _demoPrice, _proc;
        const string MoneyFormat = "{0:####.00}";

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            var userInfo = new SessionData();
            string status = userInfo.UserStatus(_userID);
            if (status == "Admin")
            {
                if (Request.QueryString["StoreID"] != null && Request.QueryString["Type"] != null)
                {
                    _storeID = int.Parse(Request.QueryString["StoreID"]);
                    _orderType = Request.QueryString["Type"].Trim();
                }
                else
                {
                    userInfo = null;
                    Response.Redirect("Orders.aspx");
                }
            }
            else Response.Redirect("Login.aspx");

            if (!IsPostBack)
            {
                _drInfo = userInfo.StoreInfo(_storeID);
                if (_drInfo.HasRows)
                {
                    _drInfo.Read();
                    _storeNumb = _drInfo["StoreNumb"].ToString();
                    _storeName = _drInfo["StoreName"].ToString();
                    _instMin = decimal.Parse(_drInfo["Inst_Min"].ToString());
                    _delivery = decimal.Parse(_drInfo["Delivery"].ToString());
                    _proc = decimal.Parse(_drInfo["Inst_Proc"].ToString()) / 100;
                    lblProc.Text = _drInfo["Inst_Proc"].ToString().Trim();
                }
                _drInfo.Close();
                lblStore.Text = _storeName;
                //txtEventDate.Text = DateTime.Today.ToShortDateString();
                rdpEventDate.SelectedDate=null;
                FillStates();

                if (_delivery > 0) cb1.Checked = true;
                _demoPrice = 0;
                txtDemoPrice.Text = String.Format(MoneyFormat, _demoPrice);
                txtDeliveryPrice.Text = String.Format(MoneyFormat, _delivery);
                txtBasicInst.Text = String.Format(MoneyFormat, 00.00);
                if (_orderType == "M")
                {
                    txtOrderNumb.Text = SetOrderNumber("M", _storeNumb);
                    txtDemoPrice.Text = String.Format(MoneyFormat, 00.00);
                    txtDeliveryPrice.Text = String.Format(MoneyFormat, 00.00);
                    txtBasicInst.Text = String.Format(MoneyFormat, 65.00);
                    txtOrderPrice.Text = String.Format(MoneyFormat, 65.00);
                    txtPurchasePrice.Text = String.Format(MoneyFormat, 00.00);
                    txtActual.Text = String.Format(MoneyFormat, 00.00);
                    cb1.Checked = false;
                }
                else
                    txtOrderNumb.Text = SetOrderNumber(_orderType.Substring(0, 3), _storeNumb);
            }
            _instMin = 180.00M;
            //else
            //{
            //    txtDemoPrice.Text = String.Format(MoneyFormat, _demoPrice);
            //    txtDeliveryPrice.Text = String.Format(MoneyFormat, _delivery);
            //    if (txtBasicInst.Text.Trim() != "")
            //    { _instPrice = Decimal.Parse(txtBasicInst.Text); }
            //    if (txtDemoPrice.Text.Trim() != "")
            //    { _demoPrice = Decimal.Parse(txtDemoPrice.Text); }
            //    if (!cb1.Checked) _delivery = 00.00M;
            //    RecalculatePrice();
            //}
        }
        protected void BtnSave2Click(object Sender, EventArgs E)
        {
            _customerID = SaveCustomer();
            _orderID = SaveOrder();
            //SendEmail();
            Response.Redirect("~/OrderDetails.aspx?OrderID=" + _orderID);
        }
        int SaveCustomer()
        {
            var customer = new OrderData();
            int custID = customer.NewCustomer(txtfName.Text.Trim(), txtMi.Text, txtlName.Text.Trim(),
                                              txtAddr1.Text, txtAddr2.Text, txtCity.Text.Trim(), ddlStates.SelectedValue,
                                              txtZip.Text, txtPhone1.Text, txtPhone2.Text, txtEmail.Text);
            return custID;
        }
        int SaveOrder()
        {
            int ordID = 0;
            try
            {
                if (txtDemoPrice.Text.Trim() == "") _demoPrice = 00.00M;
                else _demoPrice = Decimal.Parse(txtDemoPrice.Text.Trim());
                if (txtDeliveryPrice.Text.Trim() == "") _delivery = 00.00M;
                else _delivery = Decimal.Parse(txtDeliveryPrice.Text.Trim());
                DateTime? currdate = rdpEventDate.SelectedDate;
                string eventDate = currdate.ToString();
                var order = new OrderData();
                ordID = order.NewOrder(txtOrderNumb.Text, _customerID, _storeID, DateTime.Parse(eventDate),
                                           txtPlanner.Text, ddlPref.SelectedItem.ToString(), cb1.Checked, false, 'A',
                                           //char.Parse(ddlDemo.SelectedValue),
                                           decimal.Parse(txtPurchasePrice.Text), decimal.Parse(txtBasicInst.Text),
                                           _delivery, _demoPrice,
                                           decimal.Parse(txtOrderPrice.Text), txtSolution.Text, txtComments.Text,
                                           decimal.Parse(txtActual.Text));
                order.NewAppointment(ordID, DateTime.Parse("01/01/1900"), DateTime.Parse("01/01/1900"),txtComments.Text.Trim());
                if(_orderType!="M") order.UpdateOption(ordID, _orderType);
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("NewOrder", "SaveOrder", ex.Message);
            }
            return ordID;
        }
        void FillStates()
        {
            var states = new OrderData();
            _drInfo = states.StateList();
            ddlStates.DataSource = _drInfo;
            ddlStates.DataTextField = "name";
            ddlStates.DataValueField = "code";
            ddlStates.DataBind();
            _drInfo.Close();
        }
        //protected void TxtPurchasePriceTextChanged(object Sender, EventArgs E)
        //{
            //decimal proc = 0.3M;
            //instPrice = decimal.Parse(txtPurchasePrice.Text) * proc;
            //if (instPrice < instMin)
            //{ instPrice = instMin; }
            //txtBasicInst.Text = String.Format(MoneyFormat, instPrice);
            //RecalculatePrice();
//
        void RecalculatePrice()
        {
            _instPrice = decimal.Parse(txtBasicInst.Text);
            _ordPrice = _delivery + _demoPrice + _instPrice;
            txtOrderPrice.Text = String.Format(MoneyFormat, _ordPrice);
        }
        protected void Cb1CheckedChanged(object Sender, EventArgs E)
        {
            if (!cb1.Checked)
            { _delivery = 00.0M; }
            txtDeliveryPrice.Text = String.Format(MoneyFormat, _delivery);
            RecalculatePrice();
        }
        protected void TxtDemoPriceTextChanged(object Sender, EventArgs E)
        {
            if (txtDemoPrice.Text.Trim() == "")
            { _demoPrice = 00.00M; }
            else
            {
                _demoPrice = Decimal.Parse(txtDemoPrice.Text.Trim());
            }
            txtDemoPrice.Text = String.Format(MoneyFormat, _demoPrice);
            _delivery = Decimal.Parse(txtDeliveryPrice.Text.Trim());
            RecalculatePrice();
        }

        protected void TxtDeliveryPriceTextChanged(object Sender, EventArgs E)
        {
            if(txtDeliveryPrice.Text.Trim()=="")
            {
                _delivery = 00.00M;
            }
            else
            {
                _delivery = Decimal.Parse(txtDeliveryPrice.Text.Trim());
            }
            txtDeliveryPrice.Text = String.Format(MoneyFormat, _demoPrice);
            _demoPrice = Decimal.Parse(txtDemoPrice.Text.Trim());
            RecalculatePrice();
        }

        protected void TxtActualTextChanged(object Sender, EventArgs E)
        {
            //decimal proc = 0.3M;
            _proc = decimal.Parse(lblProc.Text.Trim())/100;
            _instPrice = decimal.Parse(txtActual.Text) * _proc;
            if (_instPrice < _instMin)
            { _instPrice = _instMin; }
            txtBasicInst.Text = String.Format(MoneyFormat, _instPrice);
            _delivery = Decimal.Parse(txtDeliveryPrice.Text.Trim());
            _demoPrice = Decimal.Parse(txtDemoPrice.Text.Trim());
            RecalculatePrice();
        }

        void SendEmail()
        {
            var orderInfo = new OrderData();
            _drInfo = orderInfo.NewOrderDetails(_orderID);
            _drInfo.Read();
            //const string mailSeverName = "relay-hosting.secureserver.net";
            var mailSeverName = MailServer; 
            const string from = "confirmation@elfainstall.com";
            const string to = "peter@elfainstall.com";
            //string to = "vlad@tomatex.com";
            string subject = "elfa Installation Service Request for Store "
                             + _drInfo["StoreName"] + " Order # " + _drInfo["OrderNumb"] +
                             " (New Order notification)";
            string body =
                "<table border='0' width='500'><tr><td align='center'><img border='0' src='http://www.elfainstall.com/images/logo.gif'><br>";
            body = body +
                   "<strong><font size='4'>elfa<span style='vertical-align:super; font-weight:bold'>®</span> Installation Service<br /> Customer Request Form</font></strong></td></tr></table>";
            body = body + "<br>Date: " + DateTime.Now.ToShortDateString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            body = body + "Store: " + _drInfo["StoreName"] + "<br>";
            body = body + "Customer Name: " + _drInfo["fName"].ToString().Trim() + " " + _drInfo["lName"] +
                   "<br>";
            body = body + "Customer Address: " + _drInfo["address1"].ToString().Trim() + " " +
                   _drInfo["address2"] + "<br>";
            body = body + "City: " + _drInfo["city"] + "&nbsp;&nbsp;&nbsp;State: " +
                   _drInfo["state"] + "&nbsp;&nbsp;&nbsp;&nbsp;Zip: " + _drInfo["zip"] + "<br>";
            body = body + "Customer Phone Numbers: (Home) &nbsp;&nbsp;" + _drInfo["hphone"] +
                   "&nbsp;&nbsp; &nbsp;&nbsp;";
            if (_drInfo["phone2"].ToString().Trim() != "")
            {
                body = body + " (Other} &nbsp;&nbsp;" + _drInfo["phone2"];
            }
            body = body + "<br><strong>Total installation price: " +
                   float.Parse(_drInfo["OrderPrice"].ToString()).ToString("c") + "</strong><br><br>";
            body = body + "For additional instructions check http://www.elfainstall.com <br><br><br>";
            body = body + "This is automated e-mail. Do not reply.";
            _drInfo.Close();
            try
            {
                //MailAddress copy = new MailAddress("vlad@tomatex.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //message.CC.Add(copy);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("NewOrder", "SendEmail", _orderID + ": " + ex.Message);
            }
        }
        string SetOrderNumber(string Type, string StoreNumb)
        {
            var info = new OrderData();
            int nextNumber = info.GetNextNumber();
            string orderNumber = Type + StoreNumb + nextNumber.ToString().Trim().PadLeft(5, '0');
            return orderNumber;
        }
    }
}