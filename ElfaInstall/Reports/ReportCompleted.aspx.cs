using System;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class ReportCompleted : System.Web.UI.Page
    {
        int _userID;
        string _status;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status != "Admin") Response.Redirect("Login.aspx");
            }
            if (rdpDateStart.SelectedDate != null && rdpDateEnd.SelectedDate != null && ddlSelection.SelectedIndex > 0)
            {
                hlCompleted.NavigateUrl = "~/Reports/ShowClosed.aspx?DateStart=" +
                                          rdpDateStart.SelectedDate.Value.ToShortDateString() + "&DateEnd=" +
                                          rdpDateEnd.SelectedDate.Value.ToShortDateString() + "&Target=" +
                                          ddlSelection.SelectedValue;
            }
            else hlCompleted.NavigateUrl = "";
        }
    }
}