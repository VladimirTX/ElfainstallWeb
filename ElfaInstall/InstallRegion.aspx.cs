using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class InstallRegion : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        SqlDataReader drInfo;
        int UserID, RegionID;
        string Status;
        bool filter; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { UserID = int.Parse(Request.Cookies["UserID"].Value); }
                SessionData checkLog = new SessionData();
                Status = checkLog.UserStatus(UserID).Trim();
                if (Status == "Region")
                {
                    RegionID = checkLog.GetRegionID(UserID);
                    if (RegionID == 0)
                    {
                        checkLog = null;
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    checkLog = null;
                    Response.Redirect("Login.aspx");
                }
                hfRegionID.Value = RegionID.ToString();
                filter = false;
                FillInstList();
                BindDataGrid();
            }
            else
            {
                RegionID = int.Parse(hfRegionID.Value);
                if(ddlVendors.SelectedIndex!=0 || txtSearch.Text.Trim()!="")
                {
                    filter = true;
                }
            }
        }
        void BindDataGrid()
        {
            sdsLog.ConnectionString = ConString;
            if (filter == false)
            {
                sdsLog.SelectCommand = "sp_GetInstallRegion " + RegionID;
            }
            else
            {
                if (ddlVendors.SelectedIndex != 0)
                {
                    sdsLog.SelectCommand = " EXEC sp_GetInstallRegion_vendor " + ddlVendors.SelectedValue + "," + RegionID;
                }
                if (txtSearch.Text.Trim() != "")
                {
                    sdsLog.SelectCommand = "EXEC sp_GetInstallRegion_Order '" + txtSearch.Text.Trim() + "'," + RegionID;
                }
            }
            sdsLog.Dispose();
        }
        protected void grdLog_PageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void grdLog_Sorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }

        void FillInstList()
        {
            SessionData installers = new SessionData();
            drInfo = installers.VendorsByRegion(RegionID);
            ddlVendors.DataSource = drInfo;
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataTextField = "VendorName";
            ddlVendors.DataBind();
            drInfo.Close();
        }

        protected void BtnSearchClick(object Sender, EventArgs E)
        {
            if (ddlVendors.SelectedIndex != 0 || txtSearch.Text.Trim() != "")
            { filter = true; }
            else filter = false;
            BindDataGrid();
            txtSearch.Text = "";
        }
    }
}