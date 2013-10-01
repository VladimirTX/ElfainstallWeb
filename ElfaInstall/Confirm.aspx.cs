using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class Confirm : System.Web.UI.Page
    {
        public int OrderID, UserID;
        SqlDataReader _drOrder;
        string _status, _orderNumb;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { UserID = int.Parse(Request.Cookies["UserID"].Value); }
            if (!IsPostBack)
            {
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(UserID).Trim();
                if (_status != "Admin") Response.Redirect("Login.aspx");
            }
            if (Request.QueryString["OrderID"] != null)
            { OrderID = int.Parse(Request.QueryString["OrderID"]); }
            else { Response.Redirect("Orders.aspx"); }
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(OrderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                _orderNumb = _drOrder["OrderNumb"].ToString();
            }
            _drOrder.Close();
            lblOrderNumb.Text = _orderNumb;
        }
        protected void BtnYesClick(object Sender, EventArgs E)
        {
            string reason = "";
            if (ddlReason.SelectedIndex == 8)
                reason = txtReason.Text.Trim();
            else
                reason = ddlReason.SelectedValue.Trim();
            var userInfo = new SessionData();
            string userName = userInfo.UserName(UserID);
            var orderInfo = new OrderData();
            orderInfo.DeleteOrder(OrderID, reason, userName);
            Response.Redirect("Orders.aspx");
        }
        protected void BtnNoClick(object Sender, EventArgs E)
        {
            Response.Redirect("EditOrder.aspx?OrderID=" + OrderID);
        }

        protected void DdlReasonSelectedIndexChanged(object Sender, EventArgs E)
        {
            if (ddlReason.SelectedIndex == 8)
                txtReason.Visible = true;
            else
                txtReason.Visible = false;
        }
    }
}