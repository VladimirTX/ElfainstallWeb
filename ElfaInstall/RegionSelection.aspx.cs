using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ElfaInstall
{
    public partial class RegionSelection : System.Web.UI.Page
    {
        static readonly string ConnString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConnString);
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                FillRegions();
            }
        }
        private void FillRegions()
        {
            _drInfo = RegionList();
            ddlRegions.DataSource = _drInfo;
            ddlRegions.DataValueField = "RegionID";
            ddlRegions.DataTextField = "RegionName";
            ddlRegions.DataBind();
            _drInfo.Close();

        }
        private SqlDataReader RegionList()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pGetRegionList";
            _objConn.Open();
            return objComm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        protected void btnRegion_Click(object Sender, EventArgs E)
        {
            if (ddlRegions.SelectedIndex > 0)
            {
                Response.Redirect("Schedule.aspx?RegionID=" + ddlRegions.SelectedValue);
            }
        }
    }
}