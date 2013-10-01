using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ElfaInstall
{
    public partial class MasterNew : MasterPage
    {
        private string _status;
        private int _userID;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
            }
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            if (Request.Cookies["status"] != null) _status = Request.Cookies["status"].Value;
            else Response.Redirect("Login.aspx");
            if (_status == "Admin")  MenuAdmin.Visible = true;
            if (_status == "Region") MenuRegion.Visible = true;
            if (_status == "Vendor") MenuVendor.Visible = true;
            if (_status == "Organizer") MenuOrganizer.Visible = true;
            lblStatus.Text = _status;
        }
    }
}