using System;
using System.Data.SqlClient;
using System.Web.UI;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class OrderInfo : Page
    {
        public int OrderID, UserID;
        SqlDataReader _drOrder;
        string _status;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { UserID = int.Parse(Request.Cookies["UserID"].Value); }
            var userInfo = new SessionData();
            _status = userInfo.UserStatus(UserID);
            if (Request.QueryString["OrderID"] != null)
            { OrderID = int.Parse(Request.QueryString["OrderID"]); }
            else
            {
                if (_status == "Admin") Response.Redirect("~/Reports/DeletedOrders.aspx");
                else if (_status == "Vendor") Response.Redirect("~/VendorOrders.aspx");
                else Response.Redirect("Login.aspx");
            }
            hfOrderID.Value = OrderID.ToString();
            var orderInfo = new OrderData();
            _drOrder = orderInfo.DeletedDetails(OrderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                lblOrder.Text = _drOrder["OrderNumb"].ToString();
                lblDate.Text = DateTime.Parse(_drOrder["OrderDate"].ToString()).ToShortDateString();
                lblCustomer.Text = _drOrder["Customer"].ToString();
                lblAddress.Text = _drOrder["Address"].ToString();
                lblPhone.Text = _drOrder["hphone"].ToString();
                lblPrice.Text = decimal.Parse(_drOrder["PurchasePrice"].ToString()).ToString("c");
                lblInstallPrice.Text = decimal.Parse(_drOrder["OrderPrice"].ToString()).ToString("c");
                lblComments.Text = _drOrder["Comments"].ToString();
                lblVendor.Text = _drOrder["VendorName"].ToString();
                lblDelDate.Text = DateTime.Parse(_drOrder["DateDeleted"].ToString()).ToShortDateString();
                lblDelBy.Text = _drOrder["DeletedBy"].ToString();
            }
        }
    }
}