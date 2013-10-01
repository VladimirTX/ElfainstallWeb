using System;
using System.Configuration;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class VendorInstall : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        int _userID, _vendorID, _payType, _orderID, _orderStatus;
        decimal _delivery, _ordPrice, _instPrice, _demoPrice, _milesPrice, _miscPrice, _tipPrice, _promoPrice, _vendorDue, _tax;
        SqlDataReader _drInfo;
        DateTime _vendorDate, _installDate;
        string _vendorName, _customer, _orderNumber, _phone;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var userInfo = new SessionData();
                string status = userInfo.UserStatus(_userID);
                if (status == "Vendor")
                {
                    _vendorID = userInfo.GetVendorID(_userID);
                    if (_vendorID == 0)
                    {
                        userInfo = null;
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    userInfo = null;
                    Response.Redirect("Login.aspx");
                }
                hfVendorID.Value = _vendorID.ToString();
                _drInfo = userInfo.VendorInfo(_vendorID);
                _drInfo.Read();
                _vendorName = _drInfo["VendorName"].ToString();
                _drInfo.Close();
                lblHeader.Text = "Installation Log of completed jobs for " + _vendorName;
                BindDataGrid();
            }
            else _vendorID = int.Parse(hfVendorID.Value);
        }
        protected void GrdLogPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void GrdLogSelectedIndexChanged(object Sender, EventArgs E)
        {
            GetOrderInfo();
            ShowDetails();
            lblHeader.Text = "Completed Order # " + _orderNumber;
        }
        void ShowDetails()
        {
            txtDemoPrice.Text = _demoPrice.ToString();
            txtDelivery.Text = _delivery.ToString();
            txtMilesPrice.Text = _milesPrice.ToString();
            txtMiscPrice.Text = _miscPrice.ToString();
            txtTipPrice.Text = _tipPrice.ToString();
            txtVendorDue.Text = _vendorDue.ToString();
            lblInvoice.Text = _ordPrice.ToString("c");
            if (_payType == 1) rbCC.Checked = true;
            if (_payType == 2) rbCheck.Checked = true;
            if (_payType == 3) rbNC.Checked = true;
            if (_payType == 4) rbSQ.Checked = true;
            if (_payType == 5) rbInv.Checked = true;
            lblCustomer.Text = _customer;
            lblPhone.Text = _phone;
            lblInstallDate.Text = _installDate.ToShortDateString();
            pnlEdit.Visible = true;
            grdLog.Visible = false;
        }
        void BindDataGrid()
        {
            sdsLog.ConnectionString = ConString;
            sdsLog.SelectCommand = "sp_GetInstallVendor " + _vendorID;
        }
        protected void GrdLogSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        void GetOrderInfo()
        {
            string result = grdLog.SelectedRow.Cells[0].Text;
            _orderID = int.Parse(result);
            //OrderID = int.Parse(grdOrders.SelectedRow.Cells[0].Text);
            var orderInfo = new OrderData();
            _drInfo = orderInfo.NewOrderDetails(_orderID);
            _drInfo.Read();
            _orderNumber = _drInfo["OrderNumb"].ToString();
            _vendorName = _drInfo["VendorName"].ToString();
            _installDate = DateTime.Parse(_drInfo["InstallDate"].ToString());
            _instPrice = decimal.Parse(_drInfo["BaseInstallPrice"].ToString());
            _delivery = decimal.Parse(_drInfo["DeliveryPrice"].ToString());
            _demoPrice = decimal.Parse(_drInfo["DemoPrice"].ToString());
            _milesPrice = decimal.Parse(_drInfo["MilesPrice"].ToString());
            _miscPrice = decimal.Parse(_drInfo["MiscPrice"].ToString());
            _tipPrice = decimal.Parse(_drInfo["TipPrice"].ToString());
            _promoPrice = decimal.Parse(_drInfo["PromoPrice"].ToString());
            _tax = decimal.Parse(_drInfo["Tax"].ToString());
            _ordPrice = decimal.Parse(_drInfo["OrderPrice"].ToString());
            _payType = int.Parse(_drInfo["PayType"].ToString());
            _vendorDue = decimal.Parse(_drInfo["VendorDue"].ToString());
            _vendorDate = DateTime.Parse(_drInfo["VendorDate"].ToString().Trim() != "" ? _drInfo["VendorDate"].ToString() : "01/01/1900");
            _vendorName = _drInfo["VendorName"].ToString();
            _customer = _drInfo["fName"].ToString().Trim() + " " + _drInfo["lName"];
            _phone = _drInfo["hphone"].ToString();
            _orderStatus = int.Parse(_drInfo["Status"].ToString());
            _drInfo.Close();
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            GetOrderInfo();
            if (rbCC.Checked) _payType = 1;
            if (rbCheck.Checked) _payType = 2;
            if (rbNC.Checked) _payType = 3;
            if (rbSQ.Checked) _payType = 4;
            if (rbInv.Checked) _payType = 5;
            if (txtVendorDue.Text.Trim() == "" || txtVendorDue.Text.Trim() == "0.00")
            { _vendorDue = 0; }
            else
            { _vendorDue = decimal.Parse(txtVendorDue.Text); }
            RecalculatePrice();
            var orderInfo = new OrderData();
            orderInfo.UpdateOrderPrice(_orderID, _instPrice, _delivery, _demoPrice, _milesPrice, _miscPrice,
                _tipPrice, _promoPrice, _ordPrice, _payType, _vendorDue, _vendorDate, _orderStatus);
            BindDataGrid();
            GetOrderInfo();
            ShowDetails();
        }
        protected void BtnCloseClick(object Sender, EventArgs E)
        {
            Response.Redirect("VendorInstall.aspx");
        }
        void RecalculatePrice()
        {
            if (txtDemoPrice.Text.Trim() == "" || txtDemoPrice.Text.Trim() == "0.00")
            { _demoPrice = 0; }
            else
            { _demoPrice = decimal.Parse(txtDemoPrice.Text); }
            if (txtMilesPrice.Text.Trim() == "" || txtMilesPrice.Text.Trim() == "0.00")
            { _milesPrice = 0; }
            else
            { _milesPrice = decimal.Parse(txtMilesPrice.Text); }
            if (txtDelivery.Text.Trim() == "" || txtDelivery.Text.Trim() == "0.00")
            { _delivery = 0; }
            else
            { _delivery = decimal.Parse(txtDelivery.Text); }
            if (txtMiscPrice.Text.Trim() == "" || txtMiscPrice.Text.Trim() == "0.00")
            { _miscPrice = 0; }
            else
            { _miscPrice = decimal.Parse(txtMiscPrice.Text); }
            if (txtTipPrice.Text.Trim() == "" || txtTipPrice.Text.Trim() == "0.00")
            { _tipPrice = 0; }
            else
            { _tipPrice = decimal.Parse(txtTipPrice.Text); }
            _ordPrice = _delivery + _demoPrice + _instPrice + _milesPrice + _miscPrice + _tipPrice - _promoPrice + _tax;
        }
    }
}