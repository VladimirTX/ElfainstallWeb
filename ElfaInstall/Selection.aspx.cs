using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class Selection : System.Web.UI.Page
    {
        SqlDataReader drInfo;
        string sParameter, Status;
        int UserID;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { UserID = int.Parse(Request.Cookies["UserID"].Value); }
                SessionData checkLog = new SessionData();
                Status = checkLog.UserStatus(UserID).Trim();
                checkLog = null;
                if (Status != "Admin") Response.Redirect("Login.aspx");
                FillInstList();
                FillStoreList();
                FillMarkets();
                hfVendor.Value = "";
            }
        }
        void FillInstList()
        {
            OrderData Installers = new OrderData();
            drInfo = Installers.VendorList();
            ddlVendors.DataSource = drInfo;
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataTextField = "Vendor";
            ddlVendors.DataBind();
            drInfo.Close();
            Installers = null;
        }
        void FillStoreList()
        {
            OrderData Stores = new OrderData();
            drInfo = Stores.StoreList();
            ddlStore.DataSource = drInfo;
            ddlStore.DataValueField = "StoreID";
            ddlStore.DataTextField = "Store";
            ddlStore.DataBind();
            drInfo.Close();
            Stores = null;
        }
        void FillMarkets()
        {
            OrderData Markets = new OrderData();
            drInfo = Markets.MarketList();
            ddlMarket.DataSource = drInfo;
            ddlMarket.DataValueField = "MarketID";
            ddlMarket.DataTextField = "Market";
            ddlMarket.DataBind();
            drInfo.Close();
            Markets = null;
        }
        protected void btnOrderNumb_Click(object Sender, EventArgs E)
        {
            if (txtOrderNumb.Text.Trim() != "")
            {
                sParameter = " WHERE OrderNumb LIKE ''%" + txtOrderNumb.Text.Trim() + "%''" + Open_Closed();
                hfParameter.Value = sParameter;
            }
        }
        protected void btnStore_Click(object Sender, EventArgs E)
        {
            if (ddlStore.SelectedValue != "0")
            {
                sParameter = " WHERE o.StoreID=" + ddlStore.SelectedValue + Open_Closed();
                hfParameter.Value = sParameter;
            }
        }
        protected void btnVendor_Click(object Sender, EventArgs E)
        {
            if (ddlVendors.SelectedValue != "0")
            {
                sParameter = " WHERE o.VendorID=" + ddlVendors.SelectedValue + Open_Closed();
                hfParameter.Value = sParameter;
                hfVendor.Value = ddlVendors.SelectedValue;
            }
        }
        protected void btnMarket_Click(object Sender, EventArgs E)
        {
            if (ddlMarket.SelectedValue != "0")
            {
                sParameter = " WHERE m.MarketID=" + ddlMarket.SelectedValue + Open_Closed();
                hfParameter.Value = sParameter;
            }
        }
        protected void btnDates_Click(object Sender, EventArgs E)
        {
            if (rdpDate1.SelectedDate!=null & rdpDate2.SelectedDate!=null)
            {
                sParameter = " WHERE o.OrderDate>=''" + rdpDate1.SelectedDate.Value.ToShortDateString()
                             + "'' AND o.OrderDate<=''" + rdpDate2.SelectedDate.Value.ToShortDateString() + "'' "; // +Open_Closed();
                hfParameter.Value = sParameter;
            }
        }
        protected void btnName_Click(object Sender, EventArgs E)
        {
            sParameter = " WHERE c.fName LIKE ''" + txtFirst.Text.Trim() + "%'' AND c.lName LIKE ''%" + txtLast.Text.Trim() + "%''" + Open_Closed();
            hfParameter.Value = sParameter;
        }
        string Open_Closed()
        {
            string result;
            if (rbOpen1.Checked)
            { result = " AND o.Status < 4 "; }
            else if (rbOpen2.Checked)
            { result = " AND o.Status > 3 "; }
            else
            result = " ";
            return result;
        }

        protected void BtnOptionsClick(object Sender, EventArgs E)
        {
            sParameter = " WHERE o.Options LIKE ''" + ddlOption.SelectedValue + "%'' " + Open_Closed();
            hfParameter.Value = sParameter;
        }
    }
}