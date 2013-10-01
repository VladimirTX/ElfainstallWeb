using System;
using System.Web.UI;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class ProductProblems : Page
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
            if (rdpDateStart.SelectedDate != null && rdpDateEnd.SelectedDate != null)
            {
                hlCompleted.NavigateUrl = "~/Reports/ReportProduct.aspx?DateStart=" +
                                          rdpDateStart.SelectedDate.Value.ToShortDateString() + "&DateEnd=" +
                                          rdpDateEnd.SelectedDate.Value.ToShortDateString();
            }
            else hlCompleted.NavigateUrl = "";
        }
    }
}