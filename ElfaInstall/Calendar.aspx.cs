using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class Calendar : Page
    {
        SqlDataReader _drInfo;
        int _vendorID, _userID;
        string _vendorName;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var vendor = new SessionData();
                _vendorID = vendor.GetVendorID(_userID);
                _drInfo = vendor.VendorInfo(_vendorID);
                _drInfo.Read();
                _vendorName = _drInfo["VendorName"].ToString();
                _drInfo.Close();
                lblTitle.Text = "Installation Schedule for " + _vendorName;
                Response.Cookies["VendorID"].Value = _vendorID.ToString();
            }
            else _vendorID = int.Parse(Request.Cookies["VendorID"].Value);
        }
        string GetEventData(DateTime CurrentDate)
        {
            string result = "";
            var schedule = new OrderData();
            _drInfo = schedule.InstallSchedule(_vendorID, CurrentDate);
            try
            {
                int counter = 0;
                while (_drInfo.Read())
                {
                    counter++;
                    if (counter == 1)
                    {
                        if (_drInfo["OrderNumb"].ToString().Trim() == "DO NOT SCHEDULE")
                        { result = "<br>" + _drInfo["OrderNumb"].ToString().Trim(); }
                        else
                        { result = result + "# " + _drInfo["OrderNumb"].ToString().Trim() + "<br>" + _drInfo["InstallTime"].ToString().Trim(); }
                    }
                    else
                    {
                        result = result + "<hr># " + _drInfo["OrderNumb"].ToString().Trim() + "<br>" + _drInfo["InstallTime"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("Calendar", "GetEventData", "Vendor - " + _vendorID + ": " + ex.Message);
            }
            finally
            {
                _drInfo.Close();
            }
            return result;
        }
        protected void Cal1DayRender(object Sender, DayRenderEventArgs E)
        {
            string toShow;
            CalendarDay d = E.Day;
            TableCell c = E.Cell;
            if (d.IsOtherMonth)
            { c.Controls.Clear(); }
            else
            {
                try
                {
                    toShow = GetEventData(d.Date);
                    if (toShow != "")
                    {
                        var info = new LiteralControl();
                        info.Text = "<br>" + toShow.Trim();
                        c.Controls.Add(info);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }
        }
    }
}