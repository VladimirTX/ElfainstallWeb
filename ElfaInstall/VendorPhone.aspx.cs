using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class VendorPhone : System.Web.UI.Page
    {
        private SqlDataReader _drOrder;
        int _orderID, _vendorID;

        protected void Page_Load(object Sender, EventArgs E)
        {
            _orderID = int.Parse(Request.Cookies["OrderID"].Value);
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(_orderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                _vendorID = int.Parse(_drOrder["VendorID"].ToString());
                lblVendorName.Text = _drOrder["VendorName"].ToString();
                lblVendorEmail.Text = _drOrder["vemail"].ToString();
                _drOrder.Close();
                var sessionData = new SessionData();
                _drOrder = sessionData.VendorPhones(_vendorID);
                lblVendorPhone.Text = "";
                while(_drOrder.Read())
                {
                    if(_drOrder["phone"].ToString().Trim()!="")
                    {
                        lblVendorPhone.Text += _drOrder["phone"] + " - " + _drOrder["type"] + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                }
                _drOrder.Close();
            }
        }
    }
}