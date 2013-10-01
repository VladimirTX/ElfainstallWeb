using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class VendorOrder : System.Web.UI.Page
    {
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        public int OrderID, UserID;
        SqlDataReader _drOrder;
        private string _status, _source, _userName;
        public string Url;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { UserID = int.Parse(Request.Cookies["UserID"].Value); }
            var userInfo = new SessionData();
            _status = userInfo.UserStatus(UserID);
            _userName = userInfo.UserName(UserID).Trim();
            if (Request.QueryString["OrderID"] != null)
            { OrderID = int.Parse(Request.QueryString["OrderID"]); }
            else
            {
                if (_status == "Admin") Response.Redirect("Orders.aspx");
                else if (_status == "Vendor") Response.Redirect("VendorOrders.aspx");
                else Response.Redirect("Login.aspx");
            }
            hlAtHome.NavigateUrl = "~/Reports/AtHomeCheckList.aspx?OrderID=" + OrderID;
            if (Request.QueryString["Installed"] == null || Request.QueryString["Installed"] == "False")
            {
                _source = "O"; 
                btnRequest.Visible = true;
            }
            else
            {
                _source = "K";
                btnCancel.Visible = false;
            }
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                lblOrderID.Text = _drOrder["OrderNumb"].ToString();
                lblStoreID.Text = _drOrder["StoreCode"].ToString();
                lblSaleDate.Text = DateTime.Parse(_drOrder["OrderDate"].ToString()).ToShortDateString();
                lblPlanner.Text = _drOrder["Planner"].ToString();
                if (_drOrder["Installer"].ToString().Trim() == "")
                { lblInstaller.Text = _drOrder["VendorName"].ToString(); }
                else
                { lblInstaller.Text = _drOrder["Installer"].ToString(); }
                if ((_drOrder["InstallDate"].ToString().Trim() != "") && (DateTime.Parse(_drOrder["InstallDate"].ToString()) > DateTime.Parse("01/01/2000")))
                {
                    lblInstallDate.Text = DateTime.Parse(_drOrder["InstallDate"].ToString()).ToShortDateString();
                }
                lblInstallTime.Text = _drOrder["InstallTime"].ToString();
                lblInstallPref.Text = _drOrder["InstallPref"].ToString();
                lblStatus.Text = _drOrder["sCurrent"].ToString();
                lblcName.Text = (_drOrder["fName"] + " " + _drOrder["mi"]).Trim() + " " + _drOrder["lName"];
                lblcAddress1.Text = _drOrder["address1"].ToString();
                lblcAddress2.Text = _drOrder["address2"].ToString();
                lblcCity.Text = _drOrder["city"].ToString();
                lblcState.Text = _drOrder["state"].ToString().Trim();
                lblcZip.Text = _drOrder["zip"].ToString();
                lblHphone.Text = _drOrder["hphone"].ToString();
                lblPhone2.Text = _drOrder["phone2"].ToString();
                lblEmail.Text = _drOrder["email"].ToString();
                cbDelivery.Checked = bool.Parse(_drOrder["DeliveryOption"].ToString());
                cbDemolition.Checked = bool.Parse(_drOrder["demolition"].ToString());
                if (_drOrder["ScopeofDemo"].ToString() == "A")
                { lblDemo.Text = "Additional"; }
                else
                { lblDemo.Text = "Basic"; }
                lblDelivery.Text = _drOrder["Delivery"].ToString();
                lblProc.Text = _drOrder["Inst_Proc"].ToString();
                lblSolution.Text = _drOrder["SolutionDescr"] + " / " + _drOrder["Tcscomments"];
                lblPurchasePrice.Text = decimal.Parse(_drOrder["PurchasePrice"].ToString()).ToString("c");
                lblInstallPrice.Text = decimal.Parse(_drOrder["BaseInstallPrice"].ToString()).ToString("c");
                lblDeliveryPrice.Text = decimal.Parse(_drOrder["DeliveryPrice"].ToString()).ToString("c");
                lblDemoPrice.Text = decimal.Parse(_drOrder["DemoPrice"].ToString()).ToString("c");
                lblMiles.Text = decimal.Parse(_drOrder["MilesPrice"].ToString()).ToString("c");
                lblMisc.Text = decimal.Parse(_drOrder["MiscPrice"].ToString()).ToString("c");
                lblParking.Text = decimal.Parse(_drOrder["Parking"].ToString()).ToString("c");
                lblTips.Text = decimal.Parse(_drOrder["TipPrice"].ToString()).ToString("c");
                lblDiscount.Text = (-decimal.Parse(_drOrder["PromoPrice"].ToString())).ToString("c");
                lblTax.Text = decimal.Parse(_drOrder["Tax"].ToString()).ToString("c");
                lblTotalPrice.Text = decimal.Parse(_drOrder["OrderPrice"].ToString()).ToString("c");
                lblComments.Text = _drOrder["InstNotes"].ToString();
                lblInvoice.Text = _drOrder["InvoiceNotes"].ToString();
                if (DateTime.Parse(_drOrder["OrderDate"].ToString()) >= DateTime.Parse("12/24/2009")
                    && DateTime.Parse(_drOrder["OrderDate"].ToString()) <= DateTime.Parse("02/16/2010"))
                {
                    lblPromo.Visible = true;
                }
                var imgLink = new SecureData();
                string link = imgLink.GenerateHash(_drOrder["OrderNumb"].ToString().Trim(), CodedDate(DateTime.Parse(_drOrder["OrderDate"].ToString())), CodedDate(DateTime.Today));
                link = "http://www.containerstore.com/elfaInstallAdmin/orderSummary.htm?order="
                       + _drOrder["OrderNumb"].ToString().Trim() + ":" +
                       CodedDate(DateTime.Parse(_drOrder["OrderDate"].ToString())) + ":" + CodedDate(DateTime.Today)
                       + "&key=" + link;
                hlImages.NavigateUrl = link;
                string custAddress = _drOrder["address1"] + " " + _drOrder["city"] + " " + _drOrder["state"].ToString().Trim() + ", " + _drOrder["zip"];
                //hlAddress.NavigateUrl = "http://www.map-generator.net/map.php?name=Customer Address&address=" + custAddress + "&width=500&height=400&maptype=map&zoom=14&t=1289604015 ";
                hlAddress.NavigateUrl = "https://maps.google.com/maps?q=" + custAddress;
                string option = _drOrder["Options"].ToString().Trim();
                if (option.Trim() != "" && option.IndexOf("ATH") != -1)
                {
                    hlAtHome.Visible = true;
                    Url = "~/InvoiceATH.aspx?OrderID=" + OrderID;
                }
                else
                {
                    Url = "~/PrintOrder.aspx?OrderID=" + OrderID;
                }
                lnkPrint.NavigateUrl = Url;
            }
            _drOrder.Close();
        }
        protected void BtnInvoiceClick(object Sender, EventArgs E)
        {
            Response.Redirect("PrintOrder.aspx?OrderID=" + OrderID);
        }
        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            Response.Redirect("CancelOrder.aspx?OrderID=" + OrderID);
        }
        static string CodedDate(DateTime Calcdate)
        {
            string date = Calcdate.Year + "-" +
                          Calcdate.Month.ToString().Trim().PadLeft(2, '0') + "-" +
                          Calcdate.Day.ToString().Trim().PadLeft(2, '0');
            return date;
        }
        protected void BtnReturnClick(object Sender, EventArgs E)
        {
            if(_source=="O")
            Response.Redirect("VendorOrders.aspx");
            else Response.Redirect("VendorInstall.aspx");
        }
        protected void BtnRequestClick(object Sender, EventArgs E)
        {
            txtComments.Text = "plans not readable, needs new plans please.";
            radwindowPopup.VisibleOnPageLoad = true;
        }

        protected void BtnNoClick(object Sender, EventArgs E)
        {
            radwindowPopup.VisibleOnPageLoad = false;
        }
        protected void BtnOkClick(object Sender, EventArgs E)
        {
            radwindowPopup.VisibleOnPageLoad = false;
            SendRequest();
        }

        void SendRequest()
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            _drOrder.Read();
            //const string mailSeverName = "relay-hosting.secureserver.net";
            var mailSeverName = MailServer;
            const string from = "confirmation@elfainstall.com";
            string storeCode = _drOrder["StoreCode"].ToString();
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
            string email = "";
            var regionInfo = new SessionData();
            SqlDataReader drInfo = regionInfo.RegionByStoreCode(storeCode);
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
            try
            {
                var message = new MailMessage(from, email, subject, body);
                message.IsBodyHtml = true;
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
                message.Dispose();
            }
            catch (Exception ex)
            {
                SessionData saveLog = new SessionData();
                saveLog.AddErrorLog("Vendor_Order", "SendRequest", OrderID + ": " + ex.Message + " - to " + email);
            }
        }
    }
}