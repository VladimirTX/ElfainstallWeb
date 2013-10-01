using System;
using System.Data.SqlClient;

using ElfaInstall.Classes;

namespace ElfaInstall
{
    using System.IO;

    public partial class OrganizerInvoice : System.Web.UI.Page
    {
        private int _orderID, _userID, _organizerID;
        SqlDataReader _drOrder;
        string _status, _fileName, _url;
        private decimal _fees, _other, _adjustment, _total;

        protected void Page_Load(object Sender, EventArgs E)
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
            btnPrint.NavigateUrl = "~/Reports/PrintOrgInvoice.aspx?OrderID=" + _orderID;
            //_url = "~/Invoices/" + _orderID + ".xlsx";
            _fileName = _orderID + ".xlsx";
            _url = Server.MapPath("Invoices") + "\\" + _fileName;
            if (File.Exists(_url))
                btnQuote.NavigateUrl = "~/Invoices/" + _fileName;
            else
                btnQuote.NavigateUrl = "~/Invoices/ATHinvoice.xlsx";
            if (!IsPostBack)
            {
                hfOrganizer.Value = "0";
                ShowOrderData();
            }
            else CalculateTotal();

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
            if(_drOrder.HasRows)
            {
                _drOrder.Read();
                _fees = decimal.Parse(_drOrder["Fees"].ToString());
                _other = decimal.Parse(_drOrder["Other"].ToString());
                _adjustment = decimal.Parse(_drOrder["Adjustment"].ToString());
                _total = _fees + _other - _adjustment;
                rntFees.Text = _fees.ToString();
                rntOther.Text = _other.ToString();
                rntAdjustment.Text = _adjustment.ToString();
                rntTotal.Text = _total.ToString();
                txtComments.Text = _drOrder["Comments"].ToString();
                if(hfOrganizer.Value=="0")
                {
                    lblOrganizer.Text = _drOrder["Organizer"].ToString();
                    _organizerID = int.Parse(_drOrder["OrganizerID"].ToString());
                    hfOrganizer.Value = _organizerID.ToString();
                }
            }
            else
            {
                rntFees.Value = 0.0;
                rntOther.Value = 0.0;
                rntAdjustment.Value = 0.0;
                rntTotal.Value = 0.0;
            }
            _drOrder.Close();
        }

        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            CalculateTotal();
            SaveInvoiceData();
            //Response.Redirect("OrderDetails.aspx?OrderID=" + _orderID);
        }

        void SaveInvoiceData()
        {
            _organizerID = int.Parse(hfOrganizer.Value);
            var orderInfo = new OrderData();
            orderInfo.UpdateOrganizerInvoice(_orderID,_organizerID,decimal.Parse(rntFees.Text),decimal.Parse(rntOther.Text),
                decimal.Parse(rntAdjustment.Text),txtComments.Text.Trim());
        }

        protected void BtnPrintClick(object Sender, EventArgs E)
        {
            CalculateTotal();
            SaveInvoiceData();

        }

        protected void RntFeesTextChanged(object Sender, EventArgs E)
        {
            CalculateTotal();
            SaveInvoiceData();
        }

        protected void RntOtherTextChanged(object Sender, EventArgs E)
        {
            CalculateTotal();
            SaveInvoiceData();
        }

        protected void RntAdjustmentTextChanged(object Sender, EventArgs E)
        {
            CalculateTotal();
            SaveInvoiceData();
        }

        void CalculateTotal()
        {
            _fees = decimal.Parse(rntFees.Text);
            _other = decimal.Parse(rntOther.Text);
            _adjustment = decimal.Parse(rntAdjustment.Text);
            _total = _fees + _other - _adjustment;
            rntTotal.Text = _total.ToString();
        }

        protected void BtnSaveQuoteClick(object Sender, EventArgs E)
        {
            rwQuoteSave.VisibleOnPageLoad = true;
        }

        protected void BtnSaveFileClick(object Sender, EventArgs E)
        {
            rwQuoteSave.VisibleOnPageLoad = false;

            string saveLocation = Server.MapPath("Invoices") + "\\" + _orderID + ".xlsx";
            uplFile.SaveAs(saveLocation);

            Response.Redirect("OrganizerInvoice.aspx?OrderID=" + _orderID);
        }

        protected void BtnSaveCancelClick(object Sender, EventArgs E)
        {
            rwQuoteSave.VisibleOnPageLoad = false;
        }
    }
}