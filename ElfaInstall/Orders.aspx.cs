using System;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class Orders : Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _status;
        int _userID;
        string _param = "";

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] != null) _userID = int.Parse(Request.Cookies["UserID"].Value);
                else Response.Redirect("Login.aspx");
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status != "Admin") Response.Redirect("Login.aspx");
                if (PreviousPage != null && PreviousPage.Title == "Order Search")
                {
                    _param = ((HiddenField)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("hfParameter")).Value.Trim();
                    hfParameter.Value = _param;
                }
                else { hfParameter.Value = ""; }
                BindDataGrid();
                if (_param.IndexOf("o.status = 6") > 0)
                { lblHeader.Text = "Closed Orders"; }
            }
            _param = hfParameter.Value.Trim();
        }
        void BindDataGrid()
        {
            sdsOrders.ConnectionString = ConString;
            if (_param.Trim() != "")
            {
                sdsOrders.SelectCommand = "pOrderSearch '" + _param + "'";
            }
            else
            {
                sdsOrders.SelectCommand = "pGetAllOrders";
            }
            sdsOrders.Dispose();
        }
        protected void GrdOrdersRowDataBound(object Sender, GridViewRowEventArgs E)
        {
            if (E.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = E.Row.Cells[4];
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

        public void cbImage_OnCheckedChanged(object Sender, EventArgs E)
        {
            CheckBox cbImage = (CheckBox)Sender;
            GridViewRow row = (GridViewRow)cbImage.NamingContainer;
            int sid = int.Parse(row.Cells[0].Text);
            bool viewed = cbImage.Checked;
            var orderInfo = new OrderData();
            orderInfo.UpdateImageView(sid,viewed);
            BindDataGrid();
        }

        protected void cbDeduct_OnCheckedChanged(object Sender, EventArgs E)
        {
            CheckBox cbDeduct = (CheckBox) Sender;
            GridViewRow row = (GridViewRow) cbDeduct.NamingContainer;
            int sid = int.Parse(row.Cells[0].Text);
            bool deduct = cbDeduct.Checked;
            var orderInfo = new OrderData();
            orderInfo.UpdateDeductFlag(sid,deduct);
            BindDataGrid();
        }
    }
}