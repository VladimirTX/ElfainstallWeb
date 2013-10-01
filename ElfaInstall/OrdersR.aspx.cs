using System;
using System.Configuration;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class OrdersR : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _status;
        int _userID, _regionID;
        string _param = "";

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
                if (PreviousPage != null && PreviousPage.Title == "Region Orders Search")
                {
                    _param = ((HiddenField)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("hfParameter")).Value.Trim();
                    hfParameter.Value = _param;
                }
                else { hfParameter.Value = ""; }
                Response.Cookies["RegionID"].Value = _regionID.ToString();
                hfRegionID.Value = _regionID.ToString();
                BindDataGrid();
            }
            else
            { _regionID = int.Parse(hfRegionID.Value); }
        }
        void BindDataGrid()
        {
            sdsOrders.ConnectionString = ConString;
            if (hfParameter.Value == "")
            { sdsOrders.SelectCommand = "pGetRegionOrders " + _regionID; }
            else
            { sdsOrders.SelectCommand = "pGetRegionSearch " + _regionID + ",'" + hfParameter.Value + "'"; }
        }
        protected void GrdOrdersRowDataBound(object Sender, GridViewRowEventArgs E)
        {
            if (E.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = E.Row.Cells[3];
                string sValue = cell.Text.Trim();
                if (sValue == "D")
                {
                    E.Row.ForeColor = System.Drawing.Color.Blue;
                    E.Row.Font.Bold = true;
                }
            }
        }
        protected void GrdOrdersPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void GrdOrdersSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void BtnResetClick(object Sender, EventArgs E)
        {
            hfParameter.Value = "";
            txtOrderNumb.Text = "";
            txtCustName.Text = "";
            BindDataGrid();
        }

        protected void BtnSearch1Click(object Sender, EventArgs E)
        {
            if(txtOrderNumb.Text.Trim()!="")
            {
                _param = " AND OrderNumb LIKE ''%" + txtOrderNumb.Text.Trim() + "%''";
                hfParameter.Value = _param;
                BindDataGrid();
            }
        }

        protected void BtnSearch2Click(object Sender, EventArgs E)
        {
            if(txtCustName.Text.Trim()!="")
            {
                _param = " AND (fName LIKE ''" + txtCustName.Text.Trim() + "%'' OR lName LIKE ''%" + txtCustName.Text.Trim() + "%'') AND Status < 4 ";
                hfParameter.Value = _param;
                BindDataGrid();
            }
        }
    }
}