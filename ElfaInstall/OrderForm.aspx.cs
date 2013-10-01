using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.UI;

namespace ElfaInstall
{
    public partial class OrderForm : Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _connElfa = new SqlConnection(ConString);
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                FillStores();
            }
        }
        void FillStores()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _connElfa;
            objComm.CommandType = CommandType.Text;
            const string sqLstring = "SELECT '' As StoreCode ,'' As Store, '' As State UNION " +
                                     "SELECT StoreCode,RTRIM(StoreName) + ', ' + State As Store,State " +
                                     "FROM tblStores ORDER BY State";
            objComm.CommandText = sqLstring;
            _connElfa.Open();
            _drInfo = objComm.ExecuteReader();
            ddlStores.DataSource = _drInfo;
            ddlStores.DataValueField = "StoreCode";
            ddlStores.DataTextField = "Store";
            ddlStores.DataBind();
            _drInfo.Close();
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_GetStateList";
            _drInfo = objComm.ExecuteReader();
            ddlStates.DataSource = _drInfo;
            ddlStates.DataValueField = "Code";
            ddlStates.DataTextField = "Code";
            ddlStates.DataBind();
            _drInfo.Close();
            _connElfa.Close();
        }
        bool SendEmail()
        {
            //const string mailSeverName = "relay-hosting.secureserver.net";
            var mailSeverName = MailServer; 
            const string from = "confirmation@elfainstall.com";
            bool result;
            const string to = "tcsinstall@containerstore.com";
            //to = "vlad@tomatex.com";
            const string subject = "On-line elfa® Installation Service Customer Request";
            string body = "Store: " + ddlStores.SelectedValue + "<br>";
            body = body + "Order confirmation number: " + txtOrderNumb.Text + "<br>";
            body = body + "Customer Name: " + txtCustomer.Text + "<br>";
            body = body + "Address of install: " + txtAddress.Text + "<br>";
            body = body + "City: " + txtCity.Text.Trim() + " State: " + ddlStates.SelectedValue + " Zip: " + txtZip.Text + "<br>";
            body = body + "Customer Phone Numbers: " + txtPhone1.Text.Trim();
            if (txtPhone2.Text.Trim() != "")
            { body = body + ", " + txtPhone2.Text + "<br>"; }
            else
            { body = body + "<br>"; }
            if (txtComments.Text.Trim() != "")
            { body = body + "Comments: " + txtComments.Text.Trim() + "<br>"; }
            body = body + "<br>Request submitted on " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToShortTimeString() + "<br><br><br>";
            body = body + "This is automated e-mail. Do not reply.";
            try
            {
                //var copy = new MailAddress("mnmurray@containerstore.com");
                //MailAddress copy1 = new MailAddress("Tom@elfainstall.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                //message.CC.Add(copy);
                //message.CC.Add(copy1);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
                result = true;
            }
            catch { result = false; }
            return result;
        }
        protected void ImageButton1_Click(object Sender, ImageClickEventArgs E)
        {
            if (SendEmail())
            { Response.Redirect("Thankyou.aspx"); }
        }
    }
}