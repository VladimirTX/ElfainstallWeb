using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class LogPayReport : Page
    {
        int _userID;
        string _status;
        SqlDataReader _drInfo;
        string _dateStart, _dateEnd;
        static readonly string ConnString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConnString);

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Default.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status != "Admin") Response.Redirect("Default.aspx");
            }
        }
        protected void BtnCompletedClick(object Sender, EventArgs E)
        {
            string strCurrentDir = Server.MapPath(".") + "\\Reports\\";
            string fileName = strCurrentDir + "Report.xls";

            if (File.Exists(fileName))
            { File.Delete(fileName); }
            _dateStart = rdpDateStart.SelectedDate.ToString();
            _dateEnd = rdpDateEnd.SelectedDate.ToString();
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "sp_LogPayReport";
            objComm.Parameters.AddWithValue("@StartDate", _dateStart);
            objComm.Parameters.AddWithValue("@EndDate", _dateEnd);
            _objConn.Open();
            _drInfo = objComm.ExecuteReader(CommandBehavior.CloseConnection);
            var report = new CreateExcel();
            //lblText = report.SaveFile(drInfo, fileName);
            string lblText = report.PayReport(_drInfo, fileName);

            if (lblText == "Success")
            {
                string strMachineName = Request.ServerVariables["SERVER_NAME"];
                lblResult.Text = "<A href=http://" + strMachineName + "/Reports/Report.xls>View Report</a>";
            }
            else
            { lblResult.Text = lblText; }
            lblResult.Visible = true;
        }
        //protected void txtDateStart_TextChanged(object sender, EventArgs e)
        //{
        //    lblResult.Visible = false;
        //}
        //protected void txtDateEnd_TextChanged(object sender, EventArgs e)
        //{
        //    lblResult.Visible = false;
        //}
    }
}