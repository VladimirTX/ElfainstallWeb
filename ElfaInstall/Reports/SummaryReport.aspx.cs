using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class SummaryReport : System.Web.UI.Page
    {
        SqlDataReader _drInfo;
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
                FillInstList();
                FillMarkets();
                FillStoreList();
            }
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
        void FillStoreList()
        {
            var stores = new OrderData();
            _drInfo = stores.ReportStores();
            ddlStores.DataSource = _drInfo;
            ddlStores.DataValueField = "StoreID";
            ddlStores.DataTextField = "Store";
            ddlStores.DataBind();
            _drInfo.Close();
        }
        void FillMarkets()
        {
            var markets = new OrderData();
            _drInfo = markets.ReportMarkets();
            ddlMarkets.DataSource = _drInfo;
            ddlMarkets.DataValueField = "MarketID";
            ddlMarkets.DataTextField = "Market";
            ddlMarkets.DataBind();
            _drInfo.Close();
        }
        protected void BtnStoreClick(object Sender, EventArgs E)
        {
            hfTarget.Value = "Store";
            hfTargetName.Value = ddlStores.SelectedItem.ToString();
        }
        protected void BtnMarketClick(object Sender, EventArgs E)
        {
            hfTarget.Value = "Market";
            hfTargetName.Value = ddlMarkets.SelectedItem.ToString();
        }
        protected void BtnVendorClick(object Sender, EventArgs E)
        {
            hfTarget.Value = "Vendor";
            hfTargetName.Value = ddlVendors.SelectedItem.ToString();
        }
    }
}