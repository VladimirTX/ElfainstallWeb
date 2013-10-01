using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace ElfaInstall
{
    public partial class DeleteRegStore : Page
    {
        static readonly string ConnString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConnString);
        int _storeID, _regionID;

        protected void Page_Load(object Sender, EventArgs E)
        {
            _storeID = int.Parse(Request.QueryString["StoreID"]);
            _regionID = int.Parse(Request.QueryString["RegionID"]);
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "SELECT StoreName FROM tblStores WHERE StoreID=" + _storeID;
            _objConn.Open();
            string storeName = objComm.ExecuteScalar().ToString();
            _objConn.Close();
            lblMember.Text = storeName + " ???";
        }
        protected void BtnNoClick(object Sender, EventArgs E)
        {
            Response.Redirect("EditRegion.aspx?RegionID=" + _regionID);
        }
        protected void BtnYesClick(object Sender, EventArgs E)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "DELETE FROM tblRegionStores WHERE RegionID=" + _regionID + " AND StoreID=" + _storeID;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
            Response.Redirect("EditRegion.aspx?RegionID=" + _regionID);
        }
    }
}