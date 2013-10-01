using System;
using System.Configuration;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class VendorList : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
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
                BindDataGrid();
            }
        }
        void BindDataGrid()
        {
            string vend = "%";
            if (txtSearch.Text.Trim() != "") vend = "%" + txtSearch.Text.Trim() + "%";
            sdsVendors.ConnectionString = ConString;
            sdsVendors.SelectCommand = "SELECT VendorID,VendorName,City,State,active,adddate As Added FROM tblVendors WHERE VendorName LIKE '" + vend + "' ORDER BY active desc,State";
        }
        protected void GrvVendorsPageIndexChanged(object Sender, EventArgs E)
        { BindDataGrid(); }
        protected void GrvVendorsSorted(object Sender, EventArgs E)
        { BindDataGrid(); }
        protected void BtnAddNewClick(object Sender, EventArgs E)
        {
            Response.Redirect("EditVendor.aspx?VendorID=0");
        }

        protected void BtnSearchClick(object Sender, EventArgs E)
        {
            BindDataGrid();
            txtSearch.Text = "";
        }
    }
}