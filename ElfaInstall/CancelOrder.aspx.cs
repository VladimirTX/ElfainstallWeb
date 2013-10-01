using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class CancelOrder : System.Web.UI.Page
    {
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        private string _orderID, _orderNumb, _vendorName, _status, _userName;
        SqlDataReader _drOrder;
        private int _userID;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            var userInfo = new SessionData();
            _status = userInfo.UserStatus(_userID);
            _userName = userInfo.UserName(_userID);
            if (Request.QueryString["OrderID"] != null)
            { _orderID = Request.QueryString["OrderID"]; }
            else
            {
                if (_status == "Vendor")
                    Response.Redirect("VendorOrders.aspx");
                else if (_status == "Region")
                    Response.Redirect("OrdersR.aspx");
                else Response.Redirect("Login.aspx");
            }
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(int.Parse(_orderID));
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                _orderNumb = _drOrder["OrderNumb"].ToString();
                _vendorName = _drOrder["VendorName"].ToString();
            }
            _drOrder.Close();
            lblOrderNumb.Text = _orderNumb;
        }
        protected void BtnNoClick(object Sender, EventArgs E)
        {
            if (_status == "Vendor")
                Response.Redirect("VendorOrders.aspx");
            else if (_status == "Region")
                Response.Redirect("OrdersR.aspx");
            else Response.Redirect("Login.aspx");
        }
        protected void BtnYesClick(object Sender, EventArgs E)
        {
            EmailSend();
            if (_status == "Vendor")
                Response.Redirect("VendorOrders.aspx");
            else if (_status == "Region")
                Response.Redirect("OrdersR.aspx");
            else Response.Redirect("Login.aspx");
        }

        void EmailSend()
        {
            try
            {
                var mailSeverName = MailServer; 
                const string from = "confirmation@elfainstall.com";
                const string to = "tom@elfainstall.com";
                string subject = "Cancellation request for Order # " + _orderNumb;
                string body = "<B>Coordinator " + _userName + "  Requested Cancellation of order # " + _orderNumb + "</B><br><br>";
                body = body + "Reason: " + txtReason.Text;

                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //var copy = new MailAddress("tom@elfainstall.com");
                //message.CC.Add(copy);
                var copy1 = new MailAddress("nodailey@containerstore.com");
                message.CC.Add(copy1);
                var copy2 = new MailAddress("lisa@elfainstall.com");
                message.CC.Add(copy2);
                //var copy3 = new MailAddress("john@elfainstall.com");
                //message.Bcc.Add(copy3);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("CancelOrder", "EmailSend", "Order - " + _orderID + ": " + ex.Message);
            }
        }
    }
}