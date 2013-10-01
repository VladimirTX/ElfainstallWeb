using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;
using System.Data.SqlClient;

namespace ElfaInstall
{
    public partial class RegionSearch : System.Web.UI.Page
    {
        SqlDataReader drInfo;
        int _userID, _regionID;
        string _status, sParameter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status == "Region")
                {
                    _regionID = checkLog.GetRegionID(_userID);
                    if (_regionID == 0)
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                Response.Cookies["RegionID"].Value = _regionID.ToString();
                hfRegionID.Value = _regionID.ToString();
                hfVendor.Value = "";
                FillInstList();
                FillMarkets();
                FillStoreList();
            }
            else
            { _regionID = int.Parse(hfRegionID.Value); }
        }

        void FillInstList()
        {
            var installers = new SessionData();
            drInfo = installers.VendorsByRegion(_regionID);
            ddlVendors.DataSource = drInfo;
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataTextField = "VendorName";
            ddlVendors.DataBind();
            drInfo.Close();
        }

        void FillStoreList()
        {
            var stores = new SessionData();
            drInfo = stores.StoresByRegion(_regionID);
            ddlStore.DataSource = drInfo;
            ddlStore.DataValueField = "StoreID";
            ddlStore.DataTextField = "Store";
            ddlStore.DataBind();
            drInfo.Close();
        }

        void FillMarkets()
        {
            var markets = new OrderData();
            drInfo = markets.MarketByRegion(_regionID);
            ddlMarket.DataSource = drInfo;
            ddlMarket.DataValueField = "MarketID";
            ddlMarket.DataTextField = "MarketName";
            ddlMarket.DataBind();
            drInfo.Close();
        }

        protected void BtnOrderNumbClick(object Sender, EventArgs E)
        {
            if (txtOrderNumb.Text.Trim() != "")
            {
                sParameter = " AND OrderNumb LIKE ''%" + txtOrderNumb.Text.Trim() + "%''" + OpenClosed();
                hfParameter.Value = sParameter;
            }
        }

        protected void BtnStoreClick(object Sender, EventArgs E)
        {
            if (ddlStore.SelectedValue != "0")
            {
                sParameter = " AND StoreID=" + ddlStore.SelectedValue + OpenClosed();
                hfParameter.Value = sParameter;
            }
        }

        protected void BtnVendorClick(object Sender, EventArgs E)
        {
            if (ddlVendors.SelectedValue != "0")
            {
                sParameter = " AND VendorID=" + ddlVendors.SelectedValue + OpenClosed();
                hfParameter.Value = sParameter;
                hfVendor.Value = ddlVendors.SelectedValue;
            }
        }

        protected void BtnMarketClick(object Sender, EventArgs E)
        {
            if (ddlMarket.SelectedValue != "0")
            {
                sParameter = " AND MarketID=" + ddlMarket.SelectedValue + OpenClosed();
                hfParameter.Value = sParameter;
            }
        }

        protected void BtnNameClick(object Sender, EventArgs E)
        {
            sParameter = " AND fName LIKE ''" + txtFirst.Text.Trim() + "%'' AND lName LIKE ''%" + txtLast.Text.Trim() + "%''" + OpenClosed();
            hfParameter.Value = sParameter;
        }

        protected void BtnDatesClick(object Sender, EventArgs E)
        {
            if (rdpDate1.SelectedDate != null & rdpDate2.SelectedDate != null)
            {
                sParameter = " AND OrderDate>=''" + rdpDate1.SelectedDate.Value.ToShortDateString()
                             + "'' AND OrderDate<=''" + rdpDate2.SelectedDate.Value.ToShortDateString() + "'' " + OpenClosed();
                hfParameter.Value = sParameter;
            }
        }

        protected void BtnOptionsClick(object Sender, EventArgs E)
        {
            if(ddlOption.SelectedValue!="")
            {
                sParameter = " AND Options=''" + ddlOption.SelectedValue + "'' " + OpenClosed();
                hfParameter.Value = sParameter;
            }
        }

        string OpenClosed()
        {
            string result;
            if (rbOpen1.Checked)
            { result = " AND Status < 4 "; }
            else if (rbOpen2.Checked)
            { result = " AND Status > 3 "; }
            else
                result = " ";
            return result;
        }
    }
}