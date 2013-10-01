using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class Feedback : System.Web.UI.Page
    {
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        int _orderID, _userID; 
        private SqlDataReader _drOrder, _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            if (Request.QueryString["OrderID"] != null)
            { _orderID = int.Parse(Request.QueryString["OrderID"]); }
            else Response.Redirect("VendorOrders.aspx");
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(_orderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                lblCustomer.Text = _drOrder["fName"].ToString().Trim() + " " + _drOrder["lName"].ToString().Trim();
                lblOrder.Text = _drOrder["OrderNumb"].ToString();
                lblDate.Text = DateTime.Parse(_drOrder["InstallDate"].ToString()).ToShortDateString();
                lblPrice.Text = float.Parse(_drOrder["PurchasePrice"].ToString()).ToString("c");
                lblCharge.Text = float.Parse(_drOrder["OrderPrice"].ToString()).ToString("c");
                lblDelivery.Text = bool.Parse(_drOrder["DeliveryOption"].ToString()) ? "Y" : "N";
            }
            _drOrder.Close();
            lblHeader.Text = "Installer Completed Installation Feedback ";
        }
        void EmailFeedback()
        {
            try
            {
                var orderInfo = new OrderData();
                _drInfo = orderInfo.NewOrderDetails(_orderID);
                _drInfo.Read();
                //const string mailSeverName = "relay-hosting.secureserver.net";
                var mailSeverName = MailServer; 
                const string from = "confirmation@elfainstall.com";
                const string to = "MTNEUHOFF@containerstore.com";
                string subject = "Installation Feedback for Order # " + _drInfo["OrderNumb"];
                string body = "<B>Installer Completed Installation Feedback</B><br><br>";
                body = body + "Store: " + _drInfo["StoreName"] + "&nbsp;&nbsp;&nbsp;Installer: " + _drInfo["VendorName"] + "<br>";
                body = body + "Order #: " + _drInfo["OrderNumb"] + "&nbsp;&nbsp;Inst. Date: " + DateTime.Parse(_drInfo["InstallDate"].ToString()).ToShortDateString() + "<br>";
                body = body + "Customer: " + _drInfo["fName"].ToString().Trim() + " " + _drInfo["lName"] + "<br>";
                body = body + "Elfa product amount: " + float.Parse(_drInfo["PurchasePrice"].ToString()).ToString("c") + "&nbsp;&nbsp;&nbsp;";
                body = body + "Installation charge: " + float.Parse(_drInfo["OrderPrice"].ToString()).ToString("c") + "<br>";
                string delivery = bool.Parse(_drInfo["DeliveryOption"].ToString()) ? "Y" : "N";
                body = body + "Delivery: " + delivery + "<br>";
                body = body + "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -<br>";
                body = body + "Overal Installation Time: " + txtInstH.Text.Trim() + " Hrs, " + txtInstM.Text.Trim() + " Min<br>";
                body = body + "Demolition: " + txtDemolH.Text.Trim() + " Hrs, " + txtDemolM.Text.Trim() + " Min<br>";
                body = body + "Missing Product: " + ddlMissing.SelectedValue + "&nbsp;&nbsp;&nbsp;&nbsp;Damaged Product: " + ddlDamaged.SelectedValue + "&nbsp;&nbsp;&nbsp;&nbsp;Design Flaw: " + ddlSlow.SelectedValue + "<br>";
                if (txtMissing.Text.Trim() != "")
                {
                    body = body + "Missing: " + txtMissing.Text + "<br>";
                    if (txtMResolution.Text.Trim() != "")
                    {
                        body = body + " Resolution: " + txtMResolution.Text + "<br>";
                    }
                }
                if (txtDamaged.Text.Trim() != "")
                {
                    body = body + "Damaged: " + txtDamaged.Text + "<br>";
                    if (txtDResolution.Text.Trim() != "")
                    {
                        body = body + " Resolution: " + txtDResolution.Text + "<br>";
                    }
                }
                body = body + "Installer Comments: " + txtComments.Text.Trim() + "<br>";
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //var copy = new MailAddress("lisa@elfainstall.com");
                //message.CC.Add(copy);
                var copy2 = new MailAddress("jtwunderlick@containerstore.com");
                message.CC.Add(copy2);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.ServicePoint.MaxIdleTime = 1;
                mailClient.Send(message);
                message.Dispose();
                //var saveLog = new SessionData();
                //saveLog.AddErrorLog("Feedback", "EmailFeedback", _orderID + ": Email sent.");
            }
            catch (Exception ex)
            {
                //string reason = ex.InnerException.ToString();
                //if (reason.Length > 1000) reason = reason.Substring(0, 999);
                var saveLog = new SessionData();
                saveLog.AddErrorLog("Feedback", "EmailFeedback", _orderID + ": " + ex.Message); // ex.Message);
            }
            finally
            { _drInfo.Close(); }
        }
        protected void BtnSaveDetailsClick(object Sender, EventArgs E)
        {
            var userInfo = new SessionData();
            string userName = userInfo.UserName(_userID).Trim();
            var orderInfo = new OrderData();
            orderInfo.AddFeedBack(_orderID, txtInstH.Text.Trim(), txtInstM.Text.Trim(), txtDemolH.Text.Trim(),
                        txtDemolM.Text.Trim(), ddlMissing.SelectedValue, ddlDamaged.SelectedValue, ddlSlow.SelectedValue, txtComments.Text.Trim());
            if (ddlMissing.SelectedValue == "Y")
            {
                string missing = userName + ": " + txtMissing.Text.Trim() + "</br>";
                orderInfo.AddMissing(_orderID, missing, txtMResolution.Text.Trim(),
                   txtMEmployee.Text.Trim(), ddlMDelivered.SelectedValue, ddlMPaid.SelectedValue);
            }
            if (ddlDamaged.SelectedValue == "Y")
            {
                string damaged = userName + ": " + txtDamaged.Text.Trim() + "</br>";
                orderInfo.AddDamaged(_orderID, damaged, txtDResolution.Text.Trim(),
                   txtDEmployee.Text.Trim(), ddlDDelivered.SelectedValue, ddlDPaid.SelectedValue);
            }
            EmailFeedback();
            Response.Redirect("VendorOrders.aspx");
        }
        protected void DdlMissingSelectedIndexChanged(object Sender, EventArgs E)
        {
            pnlMissing.Visible = ddlMissing.SelectedValue == "Y";
        }

        protected void DdlDamagedSelectedIndexChanged(object Sender, EventArgs E)
        {
            pnlDamaged.Visible = ddlDamaged.SelectedValue == "Y";
        }
    }
}