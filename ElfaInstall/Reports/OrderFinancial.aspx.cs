using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class OrderFinancial : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        SqlDataReader _drInfo;
        int _userID;
        string _status;

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
                if (_status != "Admin") Response.Redirect("Login.aspx");
                FillInstList();
                FillStoreList();
                FillStates();
                FillDiscounts();
                //BindDataGrid();
            }
        }

        void FillInstList()
        {
            OrderData installers = new OrderData();
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
        void FillStates()
        {
            var states = new OrderData();
            _drInfo = states.StateList();
            ddlStates.DataSource = _drInfo;
            ddlStates.DataTextField = "Name";
            ddlStates.DataValueField = "code";
            ddlStates.DataBind();
            _drInfo.Close();
        }
        void FillDiscounts()
        {
            var reasons = new OrderData();
            _drInfo = reasons.ShowDiscountReasons();
            ddlReasons.DataSource = _drInfo;
            ddlReasons.DataValueField = "DiscountID";
            ddlReasons.DataTextField = "DiscountReason";
            ddlReasons.DataBind();
            _drInfo.Close();
        }
        private bool BindDataGrid()
        {
            if (rdpDate1.SelectedDate != null && rdpDate2.SelectedDate != null)
            {
                string param = "WHERE InstallDate>=''" + rdpDate1.SelectedDate.Value.ToShortDateString() +
                               "'' AND InstallDate<=''" + rdpDate2.SelectedDate.Value.ToShortDateString() + "''";
                if (ddlVendors.SelectedIndex > 0)
                {
                    param += " AND VendorID=" + ddlVendors.SelectedValue;
                }
                if (ddlStores.SelectedIndex > 0)
                {
                    param += " AND StoreID=" + ddlStores.SelectedValue;
                }
                if(ddlReasons.SelectedIndex>0)
                {
                    param += " AND DiscountID=" + ddlReasons.SelectedValue;
                }
                if(ddlOrderType.SelectedIndex>0)
                {
                    string sType;
                    string oType = ddlOrderType.SelectedValue.Trim();
                    switch (oType)
                    {
                        case "Service":
                            sType = " AND OrderNumb LIKE ''S%'' ";
                            break;
                        case "Additional":
                            sType = " AND OrderNumb LIKE ''F%'' ";
                            break;
                        case "Call In":
                            sType = " AND (OrderNumb LIKE ''%call%'' OR OrderNumb LIKE ''CAL%'' OR Options = ''CAL'') ";
                            break;
                        case "At Home":
                            sType = " AND (OrderNumb LIKE ''%home%'' OR OrderNumb LIKE ''ATH%'' OR Options = ''ATH'') ";
                            break;
                        case "Commercial":
                            sType = " AND (OrderNumb LIKE ''COM%'' OR Options = ''COM'') ";
                            break;
                        case "PR":
                            sType = " AND (OrderNumb LIKE ''PRM%'' OR Options = ''PRM'' )";
                            break;
                        case "elfa Preferred Customer":
                            sType = " AND Options = ''EPSP'' ";
                            break;
                        case "Business to Business":
                            sType = " AND Options = ''B2B'' ";
                            break;
                        case "Fall for elfa":
                            sType = " AND Options = ''FFE'' ";
                            break;
                        case "Just for You":
                            sType = " AND Options = ''JFU'' ";
                            break;
                        case "VIP":
                            sType = " AND Options = ''MID'' ";
                            break;
                        case "Ellen DeGeneres Show":
                            sType = " AND Options = ''EDS'' ";
                            break;
                        case "Store Comp":
                            sType = " AND Options = ''STC'' ";
                            break;
                        default:
                            sType = "";
                            break;
                    }
                    param += sType;
                }
                if(txtName.Text.Trim()!="")
                {
                    param += " AND Customer LIKE ''%" + txtName.Text.Trim() + "%'' ";
                }
                if(ddlStates.SelectedIndex>0)
                {
                    param += " AND [State] = ''" + ddlStates.SelectedValue.Trim() + "'' ";
                }
                param += OpenClosed();
                hfParam.Value = param;
                string command = "EXEC dbo.pOrderReport '" + param + " '";
                sdsOrders.SelectCommand = command;
                sdsOrders.ConnectionString = ConString;
                return true;
            }
            return false;
        }

        protected void BtnShowReportClick(object Sender, EventArgs E)
        {
            if (BindDataGrid())
            {
                pnlMain.Visible = false;
                pnlReport.Visible = true;
                txtName.Text = "";
            }
        }

        private string OpenClosed()
        {
            string result;
            if (ddlStatus.SelectedValue == "Opened")
            { result = " AND Status < 4 "; }
            else if (ddlStatus.SelectedValue == "Installed")
            { result = " AND Status > 3 "; }
            else
                result = " ";
            return result;
        }
    }
}