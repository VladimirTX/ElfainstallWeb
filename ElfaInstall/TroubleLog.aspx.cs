using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class TroubleLog : System.Web.UI.Page
    {
        public int OrderID, UserID;
        SqlDataReader _drOrder, _drInfo;
        public string OrderNumb = "";
        private OrderData _orderInfo;
        private string _userName;
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { UserID = int.Parse(Request.Cookies["UserID"].Value); }
            if (Request.QueryString["OrderID"] != null)
            { OrderID = int.Parse(Request.QueryString["OrderID"]); }
            else
            { Response.Redirect("Orders.aspx"); }
            var checkLog = new SessionData();
            _userName = checkLog.UserName(UserID);
            if(!IsPostBack)
            {
                FillReasond();
                FillOrderData();
            }
        }

        void FillReasond()
        {
            var reasons = new OrderData();
            _drInfo = reasons.ShowProblemReasons();
            ddlReasons.DataSource = _drInfo;
            ddlReasons.DataValueField = "ReasonCode";
            ddlReasons.DataTextField = "Description";
            ddlReasons.DataBind();
            _drInfo.Close();
        }
        private void FillOrderData()
        {
            int vendorID = 0;
            _orderInfo = new OrderData();
            _drOrder = _orderInfo.NewOrderDetails(OrderID);
            if(_drOrder.HasRows)
            {
                _drOrder.Read();
                OrderNumb = _drOrder["OrderNumb"].ToString();
                lblOrder.Text = OrderNumb;
                lblCustomer.Text = _drOrder["fName"] + " " + _drOrder["lName"];
                lblStoreID.Text = _drOrder["StoreCode"].ToString();
                lblStoreNumb.Text="Store #: " + _drOrder["StoreNumb"];
                lblStoreOrder.Text = OrderNumb;
                lblInstaller.Text = _drOrder["VendorName"].ToString();
                string delivered = Decimal.Parse(_drOrder["Delivery"].ToString()) > 0 ? "Yes" : "No";
                lblDelivered.Text = delivered;
                DateTime installDate;
                installDate = DateTime.Parse(_drOrder["InstallDate"].ToString());
                if (installDate > DateTime.Parse("01/01/2000"))
                {
                    rdpInstallDate.SelectedDate = installDate;
                    rdpInstallDate.Enabled = false;
                }
                vendorID = int.Parse(_drOrder["VendorID"].ToString());
            }
            _drOrder.Close();
            _drOrder = _orderInfo.GetOrderProblems(OrderID);
            if(_drOrder.HasRows)
            {
                _drOrder.Read();
                lblMissing.Text = _drOrder["Mdetails"].ToString();
                lblDamaged.Text = _drOrder["Ddetails"].ToString();
                lblFlaw.Text = _drOrder["Fdetails"].ToString();
                lblTrip.Text = _drOrder["StoreTrip"].ToString();
                lblContact.Text = _drOrder["StoreContact"].ToString();
                ddlReasons.SelectedValue = _drOrder["ReasonCode"].ToString();
            }
            _drOrder.Close();
            string instPhone = _orderInfo.GetInstallerPhone(vendorID);
            lblInstPhone.Text = instPhone;
        }
        protected void BtsSaveClick(object Sender, EventArgs E)
        {
            if (txtDamaged.Text.Trim() == "" && txtFlaw.Text.Trim() == "" && txtMissing.Text.Trim() == ""
                && lblDamaged.Text.Trim() == "" && lblFlaw.Text.Trim() == "" && lblMissing.Text.Trim() == ""
                && ddlDamaged.SelectedIndex == 0 && ddlChange.SelectedIndex == 0 && ddlMissing.SelectedIndex == 0)
            {
                msgBox1.alert(
                    "At least one of the boxes (Missing Product, Damaged Product or Change to Design) has to be filled.");
                return;
            }
            if(ddlReasons.SelectedIndex==0)
            {
                msgBox1.alert("Please select Reason Code.");
                return;
            }
            SaveProblemData();
            SendEmail();
            Response.Redirect("OrderDetails.aspx?OrderID=" + OrderID);
        }
        private void SaveProblemData()
        {
            var userInfo = new SessionData();
            string userName = userInfo.UserName(UserID);
            string missingInfo = "";
            if (ddlMissing.SelectedIndex > 0) missingInfo = ddlMissing.SelectedValue.Trim() + " ";
            missingInfo = (missingInfo.Trim() + txtMissing.Text.Trim()).Trim();
            string damagedInfo = "";
            if (ddlDamaged.SelectedIndex > 0) damagedInfo = ddlDamaged.SelectedValue.Trim() + " ";
            damagedInfo = (damagedInfo.Trim() + txtDamaged.Text.Trim()).Trim();
            string flawInfo = "";
            if (ddlChange.SelectedIndex > 0) flawInfo = ddlChange.SelectedValue.Trim() + " ";
            flawInfo = (flawInfo.Trim() + txtFlaw.Text.Trim()).Trim();
            string tripInfo = "";
            if (ddlTrip.SelectedIndex > 0) tripInfo = ddlTrip.SelectedValue.Trim() + " ";
            tripInfo = (tripInfo.Trim() + txtTrip.Text.Trim()).Trim();
            string contactInfo = "";
            if (ddlContact.SelectedIndex > 0) contactInfo = ddlContact.SelectedValue.Trim() + " ";
            contactInfo = (contactInfo + txtContact.Text.Trim()).Trim();
            int reasonCode = int.Parse(ddlReasons.SelectedValue);

            _orderInfo = new OrderData();
            try
            {
                _orderInfo.AddTroubleLog(OrderID, flawInfo, tripInfo, contactInfo,missingInfo,
                    damagedInfo, userName.Trim(), reasonCode);
            }
            catch (Exception ex)
            {
                userInfo.AddErrorLog("TroubleLog", "SaveLogData", OrderID + ": " + ex.Message);
            }
        }
        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            Response.Redirect("OrderDetails.aspx?OrderID=" + OrderID);
        }
        private void SendEmail()
        {
            var storeInfo = new SessionData();
            var mailSeverName = MailServer; 
            const string from = "confirmation@elfainstall.com";
            string to = "";
            string storeCode = lblStoreID.Text.Trim();
            int storeID = storeInfo.StoreIDByCode(storeCode);
            string mtEmail = "";
            string icEmail = "";
            _drOrder = storeInfo.StoreInfo(storeID);
            if(_drOrder.HasRows)
            {
                _drOrder.Read();
                mtEmail = _drOrder["MTemail"].ToString();
                icEmail = _drOrder["ICemail"].ToString().Trim();
            }
            _drOrder.Close();
            if (storeCode == "BUC") storeCode = "BKH";
            if(storeCode=="ATH")
            {
                to = "M_MCWILLIAMS@containerstore.com";
            }
            else to = storeCode + "managers@containerstore.com";
            string subject = "TCSIS. "+ lblCustomer.Text + ", order # " + lblOrder.Text;
            string body = "<br> Customer Name: " + lblCustomer.Text + "<br>";
            body += "Store ID: " + lblStoreID.Text;
            body += "<br>Store Order #: " + lblStoreOrder.Text;
            body += "<br>Installer Name: " + lblInstaller.Text;
            body += "<br>Delivered by TCSIS: " + lblDelivered.Text;
            body += "<br>Description: ";
            if (txtMissing.Text.Trim() != "" || ddlMissing.SelectedIndex>0)
            {
                body += ddlMissing.SelectedValue + " " + txtMissing.Text.Trim() + "<br>";
            }
            if (txtDamaged.Text.Trim() != "" || ddlDamaged.SelectedIndex>0)
            {
                body += ddlDamaged.SelectedValue + " " + txtDamaged.Text.Trim() + "<br>";
            }
            if (txtFlaw.Text.Trim() != "" || ddlChange.SelectedIndex>0)
            {
                body += ddlChange.SelectedValue + " " + txtFlaw.Text.Trim() + "<br>";
            }
            if (ddlTrip.SelectedIndex>0 || txtTrip.Text.Trim() != "")
            {
                body += "Resolution: " + ddlTrip.SelectedValue + " " + txtTrip.Text.Trim() + "<br>";
            }
            string contactInfo = "";
            if (ddlContact.SelectedIndex > 0) contactInfo = ddlContact.SelectedValue.Trim() + " ";
            contactInfo = (contactInfo + txtContact.Text.Trim()).Trim();
            if(contactInfo!="")
            {
                body += "Contact at Store: " + contactInfo + "<br>";
            }
            string instDate = rdpInstallDate.SelectedDate != null ? rdpInstallDate.SelectedDate.Value.ToShortDateString() : "Not set";
            body += "Install Date: " + instDate;
            body += "<br><br><i>To respond to this email please forward to elfainstallationservice@containerstore.com or call 888-202-7622.</i>";
            try
            {
                var message = new MailMessage(from, to, subject, body);
                var copy = new MailAddress("nodailey@containerstore.com");
                if (storeCode == "6AV" || storeCode == "LEX")
                {
                    var copy1 = new MailAddress("MLmanagers@containerstore.com");
                    message.CC.Add(copy1);
                }
                if(storeCode=="ATH")
                {
                    var copy2 = new MailAddress("mnmurray@containerstore.com");
                    message.CC.Add(copy2);
                }
                if (mtEmail.Trim() != "")
                {
                    var copy3 = new MailAddress(mtEmail.Trim());
                    message.CC.Add(copy3);
                }
                if (icEmail!="")
                {
                    var copy5 = new MailAddress(icEmail);
                    message.CC.Add(copy5);
                }
                var copy4 = new MailAddress("baparnell@containerstore.com");
                message.IsBodyHtml = true;
                message.CC.Add(copy);
                message.CC.Add(copy4);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("Trouble Log", "SendEmail", OrderID + ": " + ex.Message);
            }
        }
    }
}