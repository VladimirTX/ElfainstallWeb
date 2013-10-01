using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class NewSelection : System.Web.UI.Page
    {
        SqlDataReader _drInfo;
        private string _storeID, _status, _vendorID;
        private int _userID;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            var checkLog = new SessionData();
            _status = checkLog.UserStatus(_userID).Trim();
            if (_status != "Admin") Response.Redirect("Login.aspx");
            if (!IsPostBack)
            {
                FillStoreList();
                FillVendorList();
            }
        }
        void FillStoreList()
        {
            var installers = new OrderData();
            _drInfo = installers.StoreList();
            ddlStores.DataSource = _drInfo;
            ddlStores.DataValueField = "StoreID";
            ddlStores.DataTextField = "Store";
            ddlStores.DataBind();
            _drInfo.Close();
        }
        void FillVendorList()
        {
            var vendors = new OrderData();
            _drInfo = vendors.VendorList();
            ddlVendors.DataSource = _drInfo;
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataTextField = "Vendor";
            ddlVendors.DataBind();
            _drInfo.Close();
        }
        protected void BtnContinueClick(object Sender, EventArgs E)
        {
            if (ddlStores.SelectedIndex > 0 && ddlTypes.SelectedIndex > 0)
            {
                _storeID = ddlStores.SelectedValue;
                string type = ddlTypes.SelectedValue;
                Response.Redirect("NewOrder.aspx?StoreID=" + _storeID + "&Type=" + type);
            }
        }
        protected void BtnDummyOrderClick(object Sender, EventArgs E)
        {
            _vendorID = ddlVendors.SelectedValue.Trim();
            if (_vendorID != "0")
            {
                Response.Redirect("FulfillOrder.aspx?VendorID=" + _vendorID);
            }
        }
    }
}