using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class UpdatePrice : System.Web.UI.Page
    {
        int _orderID, _userID, _storeID;
        decimal _purchasePrice, _deliveryPrice, _ordPrice, _instPrice, _demoPrice, _milesPrice, _miscPrice, _instMin, _proc, _tipPrice, _promoPrice, _tax;
        string _status;
        SqlDataReader _drOrder, _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            var checkLog = new SessionData();
            _status = checkLog.UserStatus(_userID).Trim();
            if (!IsPostBack)
            {
                if (Request.QueryString["OrderID"] != null)
                { _orderID = int.Parse(Request.QueryString["OrderID"]); }
                else
                { Response.Redirect("Orders.aspx"); };
                if (!(_status == "Admin" || _status == "Region")) Response.Redirect("Login.aspx");
                hfOrderID.Value = _orderID.ToString();
            }
            else
            { _orderID = int.Parse(hfOrderID.Value); }
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(_orderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                lblOrderID.Text = _drOrder["OrderNumb"].ToString();
                _storeID = int.Parse(_drOrder["StoreID"].ToString());
                _deliveryPrice = decimal.Parse(_drOrder["DeliveryPrice"].ToString());
                _purchasePrice = decimal.Parse(_drOrder["PurchasePrice"].ToString());
                lblPrice.Text = _purchasePrice.ToString("c");
                _instPrice = decimal.Parse(_drOrder["BaseInstallPrice"].ToString());
                _demoPrice = decimal.Parse(_drOrder["DemoPrice"].ToString());
                _milesPrice = decimal.Parse(_drOrder["MilesPrice"].ToString());
                _miscPrice = decimal.Parse(_drOrder["MiscPrice"].ToString());
                _tipPrice = decimal.Parse(_drOrder["TipPrice"].ToString());
                _promoPrice = decimal.Parse(_drOrder["PromoPrice"].ToString());
                _tax = decimal.Parse(_drOrder["Tax"].ToString());
                _ordPrice = decimal.Parse(_drOrder["OrderPrice"].ToString());
            }
            _drOrder.Close();
            var saleInfo = new SessionData();
            _drInfo = saleInfo.StoreInfo(_storeID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                _instMin = decimal.Parse(_drInfo["Inst_Min"].ToString());
                if (!IsPostBack)
                {
                    _proc = decimal.Parse(_drInfo["Inst_Proc"].ToString());
                    rntProc.Text = _proc.ToString();
                }
            }
            _drInfo.Close();
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            if (rntPrice.Text.Trim() != "" && rntProc.Text.Trim() != "")
            {
                try
                {
                    _purchasePrice = Decimal.Parse(rntPrice.Text);
                    _proc = Decimal.Parse(rntProc.Text);
                    _instPrice = _purchasePrice * _proc / 100;
                    if (_instPrice < _instMin)
                    {
                        _instPrice = _instMin;
                    }
                    _ordPrice = _deliveryPrice + _demoPrice + _instPrice + _milesPrice + _miscPrice + _tipPrice + _tax
                                - _promoPrice;
                    var orderInfo = new OrderData();
                    //_drOrder = orderInfo.NewOrderDetails(_orderID);
                    //_drOrder.Read();
                    //int customerID = int.Parse(_drOrder["CustomerID"].ToString());
                    //decimal oldPrice = decimal.Parse(_drOrder["BaseInstallPrice"].ToString());
                    //string invoiceNotes = _drOrder["InvoiceNotes"].ToString().Trim();
                    //_drOrder.Close();
                    //string newNotes = invoiceNotes + " Old price " + oldPrice.ToString("c");
                    //if (newNotes.Length > 300) newNotes = newNotes.Substring(0, 300);
                    //orderInfo.UpdateInvoiceNotes(customerID, newNotes);
                    orderInfo.UpdateBasePrice(_orderID, _purchasePrice, _instPrice, _deliveryPrice, _demoPrice,
                                              _ordPrice);
                    //Response.Redirect("~/OrderDetails.aspx?OrderID=" + this._orderID);
                }
                catch (Exception ex)
                {
                    var saveLog = new SessionData();
                    saveLog.AddErrorLog("Update Price", "SavePrice", _orderID + ": " + ex.Message);
                    return;
                }
            }
            else return;
            Response.Redirect("~/OrderDetails.aspx?OrderID=" + this._orderID);
        }
    }
}