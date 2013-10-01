using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class TCSclosed : Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _dateStart, _dateEnd;
        DateTime _startDate, _endDate;
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            //_dateStart = ((DropDownList)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("ddlStart")).Text;
            //_dateEnd = ((DropDownList)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("ddlEnd")).Text;
            _dateStart = Request.QueryString["DateStart"];
            _dateEnd = Request.QueryString["DateEnd"];
            _startDate = DateTime.Parse(_dateStart);
            _endDate = DateTime.Parse(_dateEnd);
            GetLastDates();
            BindDataGrid();
        }
        void GetLastDates()
        {
            var dateList = new OrderData();
            _drInfo = dateList.LastFiscalDates(_dateStart, _dateEnd);
            _drInfo.Read();
            DateTime lastStart = DateTime.Parse(_drInfo["LastStart"].ToString());
            DateTime lastEnd = DateTime.Parse(_drInfo["LastEnd"].ToString());
            _drInfo.Close();
            lblDate.Text = "(" + _startDate.ToLongDateString() + " - " + _endDate.ToLongDateString() + " / " + lastStart.ToLongDateString() + " - " + lastEnd.ToLongDateString() + ")";
        }
        void BindDataGrid()
        {
            sdsOrders.ConnectionString = ConString;
            sdsOrders.SelectCommand = "sp_TCS_Completed_byDate '" + _startDate.ToShortDateString() + "','" + _endDate.ToShortDateString() + "'";
        }
    }
}