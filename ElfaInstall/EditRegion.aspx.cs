using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class EditRegion : System.Web.UI.Page
    {
        static readonly string ConnString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConnString);
        int _userID, _regionID;
        string _status;
        SqlDataReader _drInfo, _drStores;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.QueryString["RegionID"] != null)
            { _regionID = int.Parse(Request.QueryString["RegionID"]); }
            else { Response.Redirect("RegionList.aspx"); }
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            var checkLog = new SessionData();
            _status = checkLog.UserStatus(_userID).Trim();
            if (_status != "Admin") Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                FillStates();
                FillData();
                FillStores();
            }
        }
        void FillStates()
        {
            var states = new OrderData();
            _drInfo = states.StateList();
            ddlStates.DataSource = _drInfo;
            ddlStates.DataTextField = "name";
            ddlStates.DataValueField = "code";
            ddlStates.DataBind();
            _drInfo.Close();
        }
        void FillData()
        {
            var region = new SessionData();
            _drInfo = region.RegionInfo(_regionID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                txtName.Text = _drInfo["RegionName"].ToString().Trim();
                txtCity.Text = _drInfo["City"].ToString().Trim();
                if (_drInfo["state"].ToString().Trim() != "")
                { ddlStates.SelectedValue = _drInfo["state"].ToString(); }
                txtEmail.Text = _drInfo["email"].ToString().Trim();
                txtComments.Text = _drInfo["Comments"].ToString().Trim();
                rbYes.Checked = bool.Parse(_drInfo["Regional"].ToString());
                rbNo.Checked = !bool.Parse(_drInfo["Regional"].ToString());
            }
            _drInfo.Close();
        }
        void FillStores()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "Select StoreID FROM tblRegionStores WHERE RegionID=" + _regionID;
            _objConn.Open();
            _drStores = objComm.ExecuteReader(CommandBehavior.CloseConnection);
            if (_drStores.HasRows)
            {
                var stores = new SessionData();
                string storeList = "";
                while (_drStores.Read())
                {
                    int storeID = int.Parse(_drStores[0].ToString());
                    _drInfo = stores.StoreInfo(storeID);
                    _drInfo.Read();
                    string storeCode = _drInfo["StoreCode"].ToString();
                    string storeName = _drInfo["StoreName"].ToString();
                    string storeState = _drInfo["State"].ToString();
                    storeList += "<a href='DeleteRegStore.aspx?StoreID=" + storeID + "&RegionID=" + _regionID + "'>" + storeCode + "</a> - " + storeName +", "+storeState + "<br>";
                    _drInfo.Close();
                    lblStores.Text = storeList;
                }
            }
            _drStores.Close();
        }
        void BindDataGrid()
        {
            sdsStores.ConnectionString = ConnString;
            sdsStores.SelectCommand = "SELECT StoreID,StoreNumb,StoreCode,StoreName,State FROM tblStores WHERE StoreID NOT IN (Select StoreID FROM tblRegionStores WHERE RegionID=" + _regionID + ")  ORDER BY State, StoreName";
        }
        protected void BtnAddStoreClick(object Sender, EventArgs E)
        {
            BindDataGrid();
            pnlStores.Visible = true;
        }
        protected void GrvStoresPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void GrvStoresSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void GrvStoresSelectedIndexChanged(object Sender, EventArgs E)
        {
            string storeID = grvStores.SelectedRow.Cells[0].Text;
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "INSERT INTO tblRegionStores (RegionID,StoreID) Values(" + _regionID + "," + storeID + ")";
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
            Response.Redirect("EditRegion.aspx?RegionID=" + _regionID);
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.Text;
            objComm.CommandText = "UPDATE tblRegions SET City='" + txtCity.Text.Trim() + "',State='" + ddlStates.SelectedValue + "',email='" + txtEmail.Text.Trim() +"',comments='"+ txtComments.Text.Trim()+"',Regional='"+ rbYes.Checked + "' WHERE RegionID=" + _regionID;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            _objConn.Close();
            Response.Redirect("RegionList.aspx");
        }

        protected void BtnCloseClick(object Sender, EventArgs E)
        {
            pnlStores.Visible = false;
        }
    }
}