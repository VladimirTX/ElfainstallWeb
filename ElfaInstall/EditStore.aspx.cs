using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class EditStore : System.Web.UI.Page
    {
        int _userID, _storeID, _vendorAdd, _vendorRem;
        string _status;
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.QueryString["StoreID"] != null)
            { _storeID = int.Parse(Request.QueryString["StoreID"]); }
            else { Response.Redirect("StoreList.aspx"); }
            if (_storeID > 0)
            {
                txtStoreNumb.ReadOnly = true;
                txtStoreCode.ReadOnly = true;
            }
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status != "Admin") Response.Redirect("Login.aspx");
                FillStates();
                FillMarkets();
                if (_storeID > 0)
                {
                    FillData();
                    FillVendors();
                }
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
        void FillMarkets()
        {
            var markets = new OrderData();
            _drInfo = markets.MarketList();
            ddlMarkets.DataSource = _drInfo;
            ddlMarkets.DataTextField = "Market";
            ddlMarkets.DataValueField = "MarketID";
            ddlMarkets.DataBind();
            _drInfo.Close();
        }
        void FillData()
        {
            var store = new SessionData();
            _drInfo = store.StoreInfo(_storeID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                txtStoreNumb.Text = _drInfo["StoreNumb"].ToString();
                txtStoreCode.Text = _drInfo["StoreCode"].ToString();
                txtStoreName.Text = _drInfo["StoreName"].ToString();
                string sMarketID = _drInfo["MarketID"].ToString().Trim();
                ddlMarkets.SelectedValue = sMarketID;
                txtAddress.Text = _drInfo["Address"].ToString();
                txtCity.Text = _drInfo["City"].ToString();
                string sState = _drInfo["State"].ToString();
                ddlStates.SelectedValue = sState;
                txtZip.Text = _drInfo["zip"].ToString();
                txtStorePhone.Text = _drInfo["phone"].ToString();
                txtGM.Text = _drInfo["GManager"].ToString();
                txtGMemail.Text = _drInfo["GMemail"].ToString();
                txtIC.Text = _drInfo["IC"].ToString();
                txtICemail.Text = _drInfo["ICemail"].ToString();
                txtMT.Text = _drInfo["MngTraining"].ToString();
                txtMTemail.Text = _drInfo["MTemail"].ToString();
                txtDirector.Text = _drInfo["AreaDirector"].ToString();
                txtADemail.Text = _drInfo["ADemail"].ToString();
                txtInstProc.Text = _drInfo["Inst_proc"].ToString();
                txtInstMin.Text = _drInfo["Inst_min"].ToString();
                txtDelivery.Text = _drInfo["Delivery"].ToString();
            }
            _drInfo.Close();
        }
        protected void BtnReturnClick(object Sender, EventArgs E)
        {
            Response.Redirect("StoreList.aspx");
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            if (_storeID > 0)
            { EditStoreInfo(); }
            else { AddNewStore(); }
            Response.Redirect("StoreList.aspx");
        }
        protected void BtnDeleteClick(object Sender, EventArgs E)
        {
            var orders = new OrderData();
            int total = orders.OrdersbyStore(_storeID);
            if (total > 0)
            {
                string page = "StoreList.aspx";
                string message = "Store " + txtStoreName.Text.Trim() + " can not be deleted, because some " +
                                 "orders related to this store!";
                Response.Redirect("ErrorMessage.aspx?Message=" + message + "&Page=" + page);
            }
            else
            {
                var stores = new SessionData();
                stores.DeleteStore(_storeID);
            }
        }
        void AddNewStore()
        {
            string storeNumb = txtStoreNumb.Text.Trim().PadLeft(3, '0');
            var storeData = new SessionData();
            _drInfo = storeData.CheckNewStore(storeNumb, txtStoreCode.Text.ToUpper(), txtStoreName.Text);
            if (_drInfo.HasRows)
            {
                int count = 0;
                string sOut = "";
                while (_drInfo.Read())
                {
                    ++count;
                    if (count > 1)
                    { sOut = sOut + ", " + _drInfo[0]; }
                    else { sOut = _drInfo[0].ToString(); }
                }
                _drInfo.Close();
                const string page = "StoreList.aspx";
                string message = "Store with the same Store Number OR Store Code OR Store Name already exists. " +
                                 "Check Store(s) " + sOut;
                Response.Redirect("ErrorMessage.aspx?Message=" + message + "&Page=" + page);
            }
            else
            {
                _drInfo.Close();
                storeData.NewStore(storeNumb, txtStoreCode.Text.ToUpper(),
                txtStoreName.Text, int.Parse(ddlMarkets.SelectedValue), txtAddress.Text,
                txtCity.Text, ddlStates.SelectedValue, txtStorePhone.Text, txtGM.Text,
                txtGMemail.Text.Trim(), txtIC.Text, txtICemail.Text.Trim(), txtMT.Text, txtMTemail.Text.Trim(),
                txtDirector.Text.Trim(), txtADemail.Text.Trim(), decimal.Parse(txtInstProc.Text), float.Parse(txtInstMin.Text),
                float.Parse(txtDelivery.Text));

                Response.Redirect("StoreList.aspx");
            }
        }
        void EditStoreInfo()
        {
            var storeData = new SessionData();
            storeData.EditStore(_storeID, txtStoreNumb.Text, txtStoreCode.Text.ToUpper(), txtStoreName.Text,
                int.Parse(ddlMarkets.SelectedValue), txtAddress.Text, txtCity.Text,
                ddlStates.SelectedValue, txtStorePhone.Text, txtGM.Text, txtGMemail.Text.Trim(),
                txtIC.Text, txtICemail.Text, txtMT.Text, txtMTemail.Text.Trim(), txtDirector.Text.Trim(), txtADemail.Text.Trim(),
                decimal.Parse(txtInstProc.Text), float.Parse(txtInstMin.Text),
                float.Parse(txtDelivery.Text));
            Response.Redirect("StoreList.aspx");
        }
        void FillVendors()
        {
            var vendorData = new SessionData();
            _drInfo = vendorData.AssignedVendors(_storeID);
            lstAssigned.DataSource = _drInfo;
            lstAssigned.DataValueField = "VendorID";
            lstAssigned.DataTextField = "Vendor";
            lstAssigned.DataBind();
            lstAssigned.SelectedIndex = -1;
            _drInfo.Close();
            _drInfo = vendorData.AvailableVendors(_storeID);
            lstAvailable.DataSource = _drInfo;
            lstAvailable.DataValueField = "VendorID";
            lstAvailable.DataTextField = "Vendor";
            lstAvailable.DataBind();
            lstAvailable.SelectedIndex = -1;
            _drInfo.Close();
            //_VendorAdd = 0;
            //_VendorRem = 0;
        }
        protected void BtnRemoveClick(object Sender, EventArgs E)
        {
            if (_vendorRem >= 0)
            {
                var vendorData = new SessionData();
                vendorData.RemoveAssignedVendor(_storeID, _vendorRem);
                Response.Redirect("EditStore.aspx?StoreID=" + _storeID);
            }
        }
        protected void BtnAssignClick(object Sender, EventArgs E)
        {
            if (_vendorAdd >= 0)
            {
                var vendorData = new SessionData();
                vendorData.AssignVendorToStore(_storeID, _vendorAdd);
                Response.Redirect("EditStore.aspx?StoreID=" + _storeID);
            }
        }
        protected void LstAvailableSelectedIndexChanged(object Sender, EventArgs E)
        {
            _vendorAdd = int.Parse(lstAvailable.SelectedValue);
        }
        protected void LstAssignedSelectedIndexChanged(object Sender, EventArgs E)
        {
            _vendorRem = int.Parse(lstAssigned.SelectedValue);
        }
    }
}