using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class StorePhone : System.Web.UI.Page
    {
        private SqlDataReader _drOrder;
        int _orderID, _storeID;

        protected void Page_Load(object Sender, EventArgs E)
        {
            _orderID = int.Parse(Request.Cookies["OrderID"].Value);
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(_orderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                _storeID = int.Parse(_drOrder["StoreID"].ToString());
                _drOrder.Close();
                var sessionData = new SessionData();
                _drOrder = sessionData.StoreInfo(_storeID);
                _drOrder.Read();
                lblStoreName.Text = _drOrder["StoreName"].ToString();
                lblStorePhone.Text = _drOrder["phone"].ToString();
                _drOrder.Close();
            }

        }
    }
}