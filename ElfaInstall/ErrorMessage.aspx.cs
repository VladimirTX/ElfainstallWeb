using System;

namespace ElfaInstall
{
    public partial class ErrorMessage : System.Web.UI.Page
    {
        string _errMessage, _redirectPage;

        protected void Page_Load(object Sender, EventArgs E)
        {
            _errMessage = Request.QueryString["Message"];
            _redirectPage = Request.QueryString["Page"];
            lblError.Text = _errMessage;
        }
        protected void BtnContinueClick(object Sender, EventArgs E)
        {
            Response.Redirect(_redirectPage);
        }
    }
}