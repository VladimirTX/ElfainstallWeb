using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class PrintOrgInvoice : System.Web.UI.Page
    {
        private int _orderID, _userID, _organizerID;
        SqlDataReader _drOrder;
        string _status;
        private decimal _fees, _other, _adjustment, _total;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    _userID = int.Parse(Request.Cookies["UserID"].Value);
                }
                var userInfo = new SessionData();
                _status = userInfo.UserStatus(_userID);
                if (Request.QueryString["OrderID"] != null)
                {
                    _orderID = int.Parse(Request.QueryString["OrderID"]);
                }
                else
                {
                    if (_status == "Admin") Response.Redirect("Orders.aspx");
                    else if (_status == "Vendor") Response.Redirect("VendorOrders.aspx");
                    else if (_status == "Region") Response.Redirect("OrdersR.aspx");
                    else Response.Redirect("Login.aspx");
                }
                hfOrganizer.Value = "0";
                ShowOrderData();
            }
        }

        void ShowOrderData()
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(_orderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                lblOrderNumb.Text = _drOrder["OrderNumb"].ToString();
                lblStoreID.Text = _drOrder["StoreCode"].ToString();
                lblSaleDate.Text = DateTime.Parse(_drOrder["OrderDate"].ToString()).ToShortDateString();
                lblStartDate.Text = DateTime.Parse(_drOrder["StartTime"].ToString()).ToShortDateString();
                lblFinishDate.Text = DateTime.Parse(_drOrder["EndTime"].ToString()).ToShortDateString();
                lblCustomer.Text = (_drOrder["fName"] + " " + _drOrder["mi"]).Trim() + " " + _drOrder["lName"];
                lblAddress.Text = _drOrder["address1"] + " " + _drOrder["address2"];
                lblCityState.Text = _drOrder["city"] + " " + _drOrder["state"] + ", " + _drOrder["zip"];
                lblPhoneHome.Text = _drOrder["hphone"].ToString();
                lblPhoneCell.Text = _drOrder["phone2"].ToString();
                lblEmail.Text = _drOrder["email"].ToString();
            }
            _drOrder.Close();
            _drOrder = orderInfo.ShowAtHomeData(_orderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                lblOrganizer.Text = _drOrder["Specialist"].ToString();
                _organizerID = int.Parse(_drOrder["SpecialistID"].ToString());
                hfOrganizer.Value = _organizerID.ToString();
            }
            _drOrder.Close();
            _drOrder = orderInfo.ShowOrganizerInvoice(_orderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                _fees = decimal.Parse(_drOrder["Fees"].ToString());
                _other = decimal.Parse(_drOrder["Other"].ToString());
                _adjustment = decimal.Parse(_drOrder["Adjustment"].ToString());
                _total = _fees + _other - _adjustment;
                lblFees.Text = _fees.ToString();
                lblOther.Text = _other.ToString();
                lblAdjustment.Text = _adjustment.ToString();
                lblTotal.Text = _total.ToString();
                lblComments.Text = _drOrder["Comments"].ToString();
                if (hfOrganizer.Value == "0")
                {
                    lblOrganizer.Text = _drOrder["Organizer"].ToString();
                    _organizerID = int.Parse(_drOrder["OrganizerID"].ToString());
                    hfOrganizer.Value = _organizerID.ToString();
                }
            }
            _drOrder.Close();
        }

    }
}