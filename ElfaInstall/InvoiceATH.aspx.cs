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
    public partial class InvoiceATH : System.Web.UI.Page
    {
        int _orderID, _userID;
        SqlDataReader _drOrder;
        string _status;
        private decimal painting, parking, additional;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            SessionData userInfo = new SessionData();
            _status = userInfo.UserStatus(_userID);
            if (Request.QueryString["OrderID"] != null)
            { _orderID = int.Parse(Request.QueryString["OrderID"]); }
            else
            {
                if (_status == "Admin") Response.Redirect("Orders.aspx");
                else if (_status == "Vendor") Response.Redirect("VendorOrders.aspx");
                else if (_status == "Region") Response.Redirect("OrdersR.aspx");
                else Response.Redirect("Login.aspx");
            }
            //OrderID = 40430;
            OrderData orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(_orderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                lblOrderID.Text = _drOrder["OrderNumb"].ToString();
                lblStoreID.Text = _drOrder["StoreCode"].ToString();
                lblSaleDate.Text = DateTime.Parse(_drOrder["OrderDate"].ToString()).ToShortDateString();
                lblPlanner.Text = _drOrder["Planner"].ToString();
                if (_drOrder["Installer"].ToString().Trim() == "")
                { lblInstaller.Text = _drOrder["VendorName"].ToString(); }
                else
                { lblInstaller.Text = _drOrder["Installer"].ToString(); }
                if ((_drOrder["StartTime"].ToString().Trim() != "") && (DateTime.Parse(_drOrder["StartTime"].ToString()) > DateTime.Parse("01/01/2000")))
                {
                    lblInstallDate.Text = DateTime.Parse(_drOrder["StartTime"].ToString()).ToShortDateString();
                }
                lblInstallTime.Text = _drOrder["InstallTime"].ToString();
                //lblInstallPref.Text = _drOrder["InstallPref"].ToString();
                lblStatus.Text = _drOrder["sCurrent"].ToString();
                lblcName.Text = (_drOrder["fName"] + " " + _drOrder["mi"]).Trim() + " " + _drOrder["lName"];
                lblcAddress1.Text = _drOrder["address1"].ToString();
                lblcAddress2.Text = _drOrder["address2"].ToString();
                lblcCity.Text = _drOrder["city"].ToString();
                lblcState.Text = _drOrder["state"].ToString().Trim();
                lblcZip.Text = _drOrder["zip"].ToString();
                lblHphone.Text = _drOrder["hphone"].ToString();
                lblPhone2.Text = _drOrder["phone2"].ToString();
                lblEmail.Text = _drOrder["email"].ToString();
                cbDelivery.Checked = bool.Parse(_drOrder["DeliveryOption"].ToString());
                lblInstNotes.Text = "Installation Notes:&nbsp;&nbsp;&nbsp;&nbsp;" + _drOrder["InvoiceNotes"].ToString().Trim();
                lblDelivery.Text = _drOrder["Delivery"].ToString();
                lblPurchasePrice.Text = decimal.Parse(_drOrder["PurchasePrice"].ToString()).ToString("c");
                lblActual.Text = decimal.Parse(_drOrder["Actual"].ToString()).ToString("c");
                lblInstallPrice.Text = decimal.Parse(_drOrder["BaseInstallPrice"].ToString()).ToString("c");
                lblDeliveryPrice.Text = decimal.Parse(_drOrder["DeliveryPrice"].ToString()).ToString("c");
                lblMilesPrice.Text = decimal.Parse(_drOrder["MilesPrice"].ToString()).ToString("c");
                additional = decimal.Parse(_drOrder["DemoPrice"].ToString());
                parking = decimal.Parse(_drOrder["Parking"].ToString());
                painting = decimal.Parse(_drOrder["MiscPrice"].ToString());
                lblDemoPrice.Text = (additional + parking + painting).ToString("c");
                //lblDemoPrice.Text = decimal.Parse(_drOrder["DemoPrice"].ToString()).ToString("c");
                //lblMiscPrice.Text = decimal.Parse(_drOrder["MiscPrice"].ToString()).ToString("c");
                //lblParking.Text = decimal.Parse(_drOrder["Parking"].ToString()).ToString("c");
                lblOther.Text = decimal.Parse(_drOrder["TipPrice"].ToString()).ToString("c");
                lblDiscount.Text = (-1 * decimal.Parse(_drOrder["PromoPrice"].ToString())).ToString("c");
                lblTotalPrice.Text = decimal.Parse(_drOrder["OrderPrice"].ToString()).ToString("c");
                decimal tax = decimal.Parse(_drOrder["Tax"].ToString());
                lblTax.Text = tax.ToString("c");
                lblTaxRate.Text = GetTaxRate().ToString("n");
                if (tax > 0) pnlTax.Visible = true;
                string delivOption = _drOrder["vemail2"].ToString().Trim();
                if (delivOption != "") lblDelivOption.Text = delivOption;
            }
            _drOrder.Close();
        }

        decimal GetTaxRate()
        {
            decimal taxRate = 0;
            string state = lblcState.Text.Trim();
            string city = lblcCity.Text.Trim();
            string zip = lblcZip.Text.Trim();
            string storeCode = lblStoreID.Text.Trim();
            if (state == "OH")
            {
                if (storeCode == "COL") taxRate = 0.0675m;
                if (storeCode == "CIN") taxRate = 0.065m;
            }
            else
            {
                if (state == "TX")
                {
                    taxRate = storeCode == "ATX" ? 0.08m : 0.0825m;
                }
                else
                {
                    //if (storeCode == "STL") taxRate = 0.09425m;
                    //else
                    //{
                        var tax = new OrderData();
                        taxRate = tax.GetTaxRate(zip, city);
                    //}
                }
            }
            return taxRate * 100;
        }
    }
}