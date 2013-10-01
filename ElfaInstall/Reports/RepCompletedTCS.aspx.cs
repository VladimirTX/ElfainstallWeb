using System;
using System.Data.SqlClient;
using System.Web.UI;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class RepCompletedTCS : Page
    {
        int _userID;
        string _status;
        SqlDataReader _drInfo;

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
                FillDateList();
            }
            string dateStart = ddlStart.SelectedValue;
            string dateEnd = ddlEnd.SelectedValue;
            int spaceStart = dateStart.IndexOf(' ');
            int spaceEnd = dateEnd.IndexOf(' ');
            hlCompleted.NavigateUrl = "~/Reports/TCSclosed.aspx?DateStart=" + dateStart.Substring(0, spaceStart) +
                                      "&DateEnd=" + dateEnd.Substring(0, spaceEnd);
            //hlCompleted.NavigateUrl = "~/Reports/TCSclosed.aspx?DateStart=" + ddlStart.SelectedValue.Substring(0, 10) +
            //                          "&DateEnd=" + ddlEnd.SelectedValue.Substring(0, 10);
        }
        void FillDateList()
        {
            var dateList = new OrderData();
            _drInfo = dateList.FiscalDates();
            ddlStart.DataSource = _drInfo;
            ddlStart.DataValueField = "StartDate";
            ddlStart.DataTextField = "StartDate";
            ddlStart.DataBind();
            _drInfo.Close();
            _drInfo = dateList.FiscalDates();
            ddlEnd.DataSource = _drInfo;
            ddlEnd.DataValueField = "EndDate";
            ddlEnd.DataTextField = "EndDate";
            ddlEnd.DataBind();
            _drInfo.Close();
        }
    }
}