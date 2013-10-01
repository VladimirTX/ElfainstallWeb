using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class Deductions : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConString);
        string _dateStart, _dateEnd;
        private int _vendorID;
        SqlDataReader _drInfo;
        private int _total;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                FillInstList();
            }
        }

        private void BindDataGrid()
        {
            string command = "EXEC pRepDeductions '" + _dateStart + "','" + _dateEnd + "'," + _vendorID + "," + _total;
            sdsOrders.SelectCommand = command;
            sdsOrders.ConnectionString = ConString;
        }

        void FillInstList()
        {
            var installers = new OrderData();
            _drInfo = installers.ReportVendors();
            ddlVendors.DataSource = _drInfo;
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataTextField = "Vendor";
            ddlVendors.DataBind();
            _drInfo.Close();
        }

        protected void RbShowClick(object Sender, EventArgs E)
        {
            FillReport();
        }

        void FillReport()
        {
            if (rdpFrom.SelectedDate != null && rdpTo.SelectedDate != null && rdpFrom.SelectedDate.Value <= rdpTo.SelectedDate.Value)
            {
                _dateStart = rdpFrom.SelectedDate.ToString();
                _dateEnd = rdpTo.SelectedDate.ToString();
                _vendorID = int.Parse(ddlVendors.SelectedValue);
                BindDataGrid();
                GetTotal();
                lblTotal.Text = _total.ToString();
                pnlReport.Visible = true;
            }
        }

        public void GetTotal()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pRepDeductions";
            SqlParameter parmResult1 = objComm.Parameters.AddWithValue("@StartDate", _dateStart);
            SqlParameter parmResult2 = objComm.Parameters.AddWithValue("@EndDate", _dateEnd);
            SqlParameter parmResult3 = objComm.Parameters.AddWithValue("@VendorID", _vendorID);
            SqlParameter parmResult4 = objComm.Parameters.Add("@Total", SqlDbType.Int);
            parmResult4.Direction = ParameterDirection.Output;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _total = int.Parse(objComm.Parameters["@Total"].Value.ToString());
            _objConn.Close();
        }

        protected void GrdOrdersSorted(object Sender, EventArgs E)
        {
            FillReport();
        }
    }
}