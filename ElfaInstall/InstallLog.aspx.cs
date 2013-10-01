using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class InstallLog : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        int userID, payType, orderID, orderStatus, _discountID, _vendorID;
        decimal _delivery, _ordPrice, _instPrice, _demoPrice, _milesPrice, _miscPrice, _tipPrice, _vendorDue, _promoPrice, _purchasePrice, _parkingPrice, _payProc, _tax;
        SqlDataReader drInfo;
        DateTime orderDate, vendorDate, paymentDay;
        string status, vendorName, customer, solution, comments, checkNumb, _state, _city, _zip, _storeCode;
        bool filter, _confirmed, _bySurvey;
        public bool Promo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                status = checkLog.UserStatus(userID).Trim();
                if (status != "Admin") Response.Redirect("Login.aspx");
                filter = false;
                FillInstList();
                FillInstalledDiscounts();
                BindDataGrid();
            }
            else
            {
                if (ddlVendors.SelectedIndex != 0 || txtOrderNumb.Text.Trim() != "")
                {
                    filter = true;
                }
            }
        }
        protected void GrdLogPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void GrdLogSelectedIndexChanged(object Sender, EventArgs E)
        {
            FillAddVendors();
            ddlProcent.SelectedIndex = 0;
            ddlVendProc.SelectedIndex = 0;
            txtVendAmt.Text = "0";
            txtOrgAmt.Text = "0";
            txtAddpayAmt.Text = "0";
            FillAddOrganizers();
            ddlOrgProc.SelectedIndex = 0;
            ddlOrgProc.SelectedIndex = 0;
            GetOrderInfo();
            ShowDetails();
            pnlGread.Visible = false;
            //btnReport.Visible = false;
            if (_state.Trim() == "NY" || _state.Trim() == "NJ" || _state.Trim() == "PA" || _state.Trim() == "OH" || _state.Trim() == "FL" || _state.Trim() == "MN" || _state.Trim() == "WA" || _state.Trim() == "TX")
            {
                btnTax.Visible = true;
            }
            if (payType == 1) rbCC.Checked = true; else rbCC.Checked = false;
            if (payType == 2) rbCheck.Checked = true; else rbCheck.Checked = false;
            if (payType == 3) rbNC.Checked = true; else rbNC.Checked = false;
            if (payType == 4) rbSQ.Checked = true; else rbSQ.Checked = false;
            if (payType == 5) rbInv.Checked = true; else rbInv.Checked = false;
            txtChkNumb.Text = checkNumb.Trim();
        }
        void ShowDetails()
        {
            orderID = int.Parse(grdLog.SelectedRow.Cells[0].Text);
            OrderData orderInfo = new OrderData();
            drInfo = orderInfo.GetAdditionalVendor(orderID);
            if (drInfo.HasRows)
            {
                drInfo.Read();
                lblVendorName.Text = drInfo[0].ToString();
                lblAmount.Text = decimal.Parse(drInfo[1].ToString()).ToString("c");
                pnlAdd1.Visible = false;
                pnlAdd2.Visible = true;
            }
            else
            {
                pnlAdd1.Visible = true;
                pnlAdd2.Visible = false;
            }
            drInfo.Close();
            drInfo = orderInfo.GetAdditionalOrganizer(orderID);
            if (drInfo.HasRows)
            {
                drInfo.Read();
                lblOrganizerName.Text = drInfo[0].ToString();
                lblOamount.Text = decimal.Parse(drInfo[1].ToString()).ToString("c");
                pnlAdd3.Visible = false;
                pnlAdd4.Visible = true;
            }
            else
            {
                pnlAdd3.Visible = true;
                pnlAdd4.Visible = false;
            }
            drInfo.Close();
            drInfo = orderInfo.GetAdditionalPayment(orderID, _vendorID);
            if(drInfo.HasRows)
            {
                drInfo.Read();
                lblAddAmount.Text = decimal.Parse(drInfo[1].ToString()).ToString("c");
                pnlAdd5.Visible = false;
                pnlAdd6.Visible = true;
            }
            else
            {
                pnlAdd5.Visible = true;
                pnlAdd6.Visible = false;
            }
            drInfo.Close();
            if (vendorDate < DateTime.Parse("01/01/2000"))
            { rdpVendorDate.SelectedDate = null; }
            else { rdpVendorDate.SelectedDate = vendorDate; }
            txtDemoPrice.Text = _demoPrice.ToString();
            txtPromoPrice.Text = _promoPrice.ToString();
            txtMilesPrice.Text = _milesPrice.ToString();
            txtMiscPrice.Text = _miscPrice.ToString();
            txtParking.Text = _parkingPrice.ToString();
            txtTipPrice.Text = _tipPrice.ToString();
            txtTax.Text = _tax.ToString();
            txtVendorDue.Text = _vendorDue.ToString();
            lblInvoice.Text = _ordPrice.ToString("c");
            //if (payType == 1) rbCC.Checked = true; else rbCC.Checked = false;
            //if (payType == 2) rbCheck.Checked = true; else rbCheck.Checked = false;
            //if (payType == 3) rbNC.Checked = true; else rbNC.Checked = false;
            //if (payType == 4) rbSQ.Checked = true; else rbSQ.Checked = false;
            //if (payType == 5) rbInv.Checked = true; else rbInv.Checked = false;
            //txtChkNumb.Text = checkNumb.Trim();
            lblVendor.Text = vendorName;
            lblVendor1.Text = vendorName;
            lblCustomer.Text = customer;
            lblElfaPrice.Text = _purchasePrice.ToString("c");
            pnlEdit.Visible = true;
            //if (orderStatus > 4) chkPayment.Checked = true;
            if (_payProc > 50)
            {
                ddlProcent.SelectedValue = (_payProc/100).ToString();
            }
            ddlReasons.SelectedValue = _discountID.ToString();
        }
        void BindDataGrid()
        {
            sdsLog.ConnectionString = ConString;
            if (filter == false)
            { sdsLog.SelectCommand = "sp_GetInstallLog"; }
            else
            {
                if (ddlVendors.SelectedIndex != 0)
                { sdsLog.SelectCommand = " EXEC sp_GetInstallLog_vendor " + ddlVendors.SelectedValue; }
                if (txtOrderNumb.Text.Trim() != "")
                { sdsLog.SelectCommand = "EXEC sp_GetInstallLog_order '" + txtOrderNumb.Text.Trim() + "'"; }
            }
            sdsLog.Dispose();
        }
        protected void GrdLogSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        void GetOrderInfo()
        {
            string result = grdLog.SelectedRow.Cells[0].Text;
            orderID = int.Parse(result);
            //orderID = int.Parse(grdOrders.SelectedRow.Cells[0].Text);
            var orderInfo = new OrderData();
            drInfo = orderInfo.NewOrderDetails(orderID);
            drInfo.Read();
            _vendorID = int.Parse(drInfo["VendorID"].ToString());
            lblOrderNumb.Text = drInfo["OrderNumb"].ToString();
            orderDate = DateTime.Parse(drInfo["OrderDate"].ToString());
            DateTime.Parse(drInfo["InstallDate"].ToString());
            DateTime.Parse(drInfo["PaymentDate"].ToString());
            _purchasePrice = decimal.Parse(drInfo["PurchasePrice"].ToString());
            _instPrice = decimal.Parse(drInfo["BaseInstallPrice"].ToString());
            _delivery = decimal.Parse(drInfo["DeliveryPrice"].ToString());
            _demoPrice = decimal.Parse(drInfo["DemoPrice"].ToString());
            _milesPrice = decimal.Parse(drInfo["MilesPrice"].ToString());
            _parkingPrice = decimal.Parse(drInfo["Parking"].ToString());
            _miscPrice = decimal.Parse(drInfo["MiscPrice"].ToString());
            _tipPrice = decimal.Parse(drInfo["TipPrice"].ToString());
            _parkingPrice = decimal.Parse(drInfo["Parking"].ToString());
            _promoPrice = decimal.Parse(drInfo["PromoPrice"].ToString());
            _tax = decimal.Parse(drInfo["Tax"].ToString());
            _ordPrice = decimal.Parse(drInfo["OrderPrice"].ToString());
            payType = int.Parse(drInfo["PayType"].ToString());
            _payProc = decimal.Parse(drInfo["PayProc"].ToString());
            _vendorDue = decimal.Parse(drInfo["VendorDue"].ToString());
            if (drInfo["VendorDate"].ToString().Trim() != "")
            { vendorDate = DateTime.Parse(drInfo["VendorDate"].ToString()); }
            else vendorDate = DateTime.Parse("01/01/1900");
            if (drInfo["PaymentDate"].ToString().Trim() != "")
            { paymentDay = DateTime.Parse(drInfo["PaymentDate"].ToString()); }
            else paymentDay = DateTime.Parse("01/01/1900");
            vendorName = drInfo["VendorName"].ToString();
            customer = drInfo["fName"].ToString().Trim() + " " + drInfo["lName"];
            orderStatus = int.Parse(drInfo["Status"].ToString());
            solution = drInfo["SolutionDescr"].ToString();
            comments = drInfo["Comments"].ToString();
            checkNumb = drInfo["CheckNumb"].ToString();
            _confirmed = bool.Parse(drInfo["Approved"].ToString());
            _bySurvey = bool.Parse(drInfo["bySurvey"].ToString());
            _state = drInfo["state"].ToString().Trim();
            _city = drInfo["city"].ToString().Trim();
            _zip = drInfo["zip"].ToString().Trim();
            _storeCode = drInfo["StoreCode"].ToString();
            _discountID = int.Parse(drInfo["DiscountID"].ToString().Trim());
            drInfo.Close();
            Promo = true;
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            GetOrderInfo();
            bool isDateSet = false;
            if (rdpVendorDate.SelectedDate == null)
            {
                vendorDate = DateTime.Parse("01/01/1900");
            }
            else
            {
                vendorDate = DateTime.Parse(rdpVendorDate.SelectedDate.ToString());
                isDateSet = true;
            }
            //paymentDay = DateTime.Parse("01/01/1900");
            payType = 0;
            if (rbCC.Checked) payType = 1;
            else
            {
                if (rbCheck.Checked) payType = 2;
                else
                {
                    if (rbNC.Checked) payType = 3;
                    else
                    {
                        if (rbSQ.Checked) payType = 4;
                        else if (rbInv.Checked) payType = 5;
                    }
                }
            }
            if (txtVendorDue.Text.Trim() == "" || txtVendorDue.Text.Trim() == "0.00")
            { _vendorDue = 0; }
            else
            { _vendorDue = decimal.Parse(txtVendorDue.Text); }
            //if (chkPayment.Checked)
            //{
            //    if (!isDateSet) vendorDate = GetNextThursday(DateTime.Today);
            //    orderStatus = 5;
            //    paymentDay = DateTime.Today;
            //    if (vendorDue == 0)
            //    {
            //        decimal minPrice = 150 * 100 / payProc;
            //        decimal usePrice = instPrice;
            //        if (minPrice > instPrice) usePrice = minPrice;
            //        decimal basePrice = (usePrice + delivery + demoPrice - promoPrice) * payProc / 100;
            //        vendorDue = basePrice + milesPrice + miscPrice + tipPrice + parkingPrice;
            //    }
            //}

            RecalculatePrice();
            var orderInfo = new OrderData();
            orderInfo.UpdateOrderPrice(orderID, _instPrice, _delivery, _demoPrice, _milesPrice, _miscPrice,
                _tipPrice, _promoPrice, _ordPrice, payType, _vendorDue, vendorDate, orderStatus);

            orderInfo.NewUpdateOrder(orderID, true, true,
                paymentDay, orderStatus, solution, "", _instPrice, _delivery, _demoPrice, _milesPrice,
                _miscPrice, _parkingPrice, _tipPrice, _promoPrice, _tax, _ordPrice, _confirmed, checkNumb,_bySurvey);
            orderInfo.SaveDiscount(orderID, int.Parse(ddlReasons.SelectedValue));

            //pnlEdit.Visible = false;
            BindDataGrid();
            GetOrderInfo();
            ShowDetails();
            pnlGread.Visible = true;
            pnlEdit.Visible = false;
            //btnReport.Visible = true;
        }
        protected void BtnCloseClick(object Sender, EventArgs E)
        {
            pnlEdit.Visible = false;
            //pnlPromo.Visible = false;
            BindDataGrid();
            pnlGread.Visible = true;
            //btnReport.Visible = true;
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
            if (txtMiscPrice.Text.Trim() == "" || txtMiscPrice.Text.Trim() == "0.00")
            { _miscPrice = 0; }
            else
            { _miscPrice = decimal.Parse(txtMiscPrice.Text); }
            if (txtTipPrice.Text.Trim() == "" || txtTipPrice.Text.Trim() == "0.00")
            { _tipPrice = 0; }
            else
            { _tipPrice = decimal.Parse(txtTipPrice.Text); }
            if (txtPromoPrice.Text.Trim() == "" || txtPromoPrice.Text.Trim() == "0.00")
            { _promoPrice = 0; }
            else
            { _promoPrice = decimal.Parse(txtPromoPrice.Text); }
            if (txtParking.Text.Trim() == "" || txtParking.Text.Trim() == "0.00")
            { _parkingPrice = 0; }
            else
            { _parkingPrice = decimal.Parse(txtParking.Text); }
            if(txtTax.Text.Trim()=="" || txtTax.Text.Trim()=="0.00")
            { _tax = 0; }
            else
            { _tax = decimal.Parse(txtTax.Text.Trim()); }
            _ordPrice = _delivery + _demoPrice + _instPrice + _milesPrice + _miscPrice + _tipPrice - _promoPrice + _parkingPrice + _tax;
        }
        protected void BtnFilterClick(object Sender, EventArgs E)
        {
            if (ddlVendors.SelectedIndex != 0 || txtOrderNumb.Text.Trim() != "")
            { filter = true; }
            else filter = false;
            BindDataGrid();
            txtOrderNumb.Text = "";
        }
        void FillInstList()
        {
            OrderData installers = new OrderData();
            drInfo = installers.ReportVendors();
            ddlVendors.DataSource = drInfo;
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataTextField = "Vendor";
            ddlVendors.DataBind();
            drInfo.Close();
        }
        void FillAddVendors()
        {
            OrderData installers = new OrderData();
            drInfo = installers.VendorList();
            ddlVendorAdd.DataSource = drInfo;
            ddlVendorAdd.DataValueField = "VendorID";
            ddlVendorAdd.DataTextField = "Vendor";
            ddlVendorAdd.DataBind();
            drInfo.Close();
        }
        void FillAddOrganizers()
        {
            OrderData organizer = new OrderData();
            drInfo = organizer.GetOrganizers();
            ddlOrganizerAdd.DataSource = drInfo;
            ddlOrganizerAdd.DataValueField = "SpecialistID";
            ddlOrganizerAdd.DataTextField = "Organizer";
            ddlOrganizerAdd.DataBind();
            drInfo.Close();
        }
        protected void DdlProcentSelectedIndexChanged(object Sender, EventArgs E)
        {
            GetOrderInfo();

            decimal minPrice = 150 * 100 / _payProc;
            decimal usePrice = _instPrice;
            if (minPrice > _instPrice) usePrice = minPrice;
            decimal basePrice = (usePrice + _delivery + _demoPrice - _promoPrice) * _payProc / 100;
            _vendorDue = basePrice + _milesPrice + _miscPrice + _tipPrice + _parkingPrice;

            //vendorDue = (instPrice + delivery + demoPrice + promoPrice) * decimal.Parse(ddlProcent.SelectedValue) + milesPrice +
            //            miscPrice + parkingPrice + tipPrice;
            txtVendorDue.Text = _vendorDue.ToString("n");
        }
        protected void BtnVaddClick(object Sender, EventArgs E)
        {
            if (ddlVendorAdd.SelectedIndex > 0 && ddlVendProc.SelectedIndex > 0)
            {
                GetOrderInfo();
                _vendorID = int.Parse(ddlVendorAdd.SelectedValue);
                orderID = int.Parse(grdLog.SelectedRow.Cells[0].Text);
                decimal amount = (_instPrice + _delivery + _demoPrice) * decimal.Parse(ddlVendProc.SelectedValue);
                var orderInfo = new OrderData();
                orderInfo.AddVendorBonus(_vendorID, orderID, amount);
            }
            if (ddlVendorAdd.SelectedIndex > 0 && decimal.Parse(txtVendAmt.Text.Trim()) > 0)
            {
                GetOrderInfo();
                _vendorID = int.Parse(ddlVendorAdd.SelectedValue);
                orderID = int.Parse(grdLog.SelectedRow.Cells[0].Text);
                decimal amount = decimal.Parse(txtVendAmt.Text.Trim());
                var orderInfo = new OrderData();
                orderInfo.AddVendorBonus(_vendorID, orderID, amount);
            }
            ShowDetails();
        }

        protected void BtnOaddClick(object Sender, EventArgs E)
        {
            if (ddlOrganizerAdd.SelectedIndex > 0 && ddlOrgProc.SelectedIndex > 0)
            {
                GetOrderInfo();
                orderID = int.Parse(grdLog.SelectedRow.Cells[0].Text);
                int organizerID = int.Parse(ddlOrganizerAdd.SelectedValue);
                decimal amount = _ordPrice * decimal.Parse(ddlOrgProc.SelectedValue);
                var orderInfo = new OrderData();
                orderInfo.AddOrganizerBonus(organizerID,orderID,amount);
            }
            if (ddlOrganizerAdd.SelectedIndex > 0 && decimal.Parse(txtOrgAmt.Text.Trim()) > 0)
            {
                GetOrderInfo();
                orderID = int.Parse(grdLog.SelectedRow.Cells[0].Text);
                int organizerID = int.Parse(ddlOrganizerAdd.SelectedValue);
                decimal amount = decimal.Parse(txtOrgAmt.Text.Trim());
                var orderInfo = new OrderData();
                orderInfo.AddOrganizerBonus(organizerID, orderID, amount);
            }
            ShowDetails();
        }

        protected void BtnPaddClick(object Sender, EventArgs E)
        {
            if (ddlAddpayProc.SelectedIndex > 0)
            {
                GetOrderInfo();
                orderID = int.Parse(grdLog.SelectedRow.Cells[0].Text);
                decimal amount = (_instPrice + _delivery + _demoPrice) * decimal.Parse(ddlAddpayProc.SelectedValue);
                var orderInfo = new OrderData();
                orderInfo.AddVendorBonus(_vendorID, orderID, amount);
            }
            if (txtAddpayAmt.Text.Trim()!="" && decimal.Parse(txtAddpayAmt.Text.Trim()) > 0)
            {
                GetOrderInfo();
                orderID = int.Parse(grdLog.SelectedRow.Cells[0].Text);
                decimal amount = decimal.Parse(txtAddpayAmt.Text.Trim());
                var orderInfo = new OrderData();
                orderInfo.AddVendorBonus(_vendorID, orderID, amount);
            }
            ShowDetails();
        }

        public void cbPayd_OnCheckedChanged(object Sender, EventArgs E)
        {
            CheckBox cbPayd = (CheckBox) Sender;
            GridViewRow row = (GridViewRow) cbPayd.NamingContainer;
            int sid = int.Parse(row.Cells[0].Text);
            bool payd = cbPayd.Checked;
            string mess = "";
            if(payd)
            {
                SetVendorDue(sid);
            }
            else
            {
                var orderInfo = new OrderData();
                SqlDataReader drOrder = orderInfo.NewOrderDetails(sid);
                drOrder.Read();
                solution = drOrder["SolutionDescr"].ToString();
                _instPrice = decimal.Parse(drOrder["BaseInstallPrice"].ToString());
                _delivery = decimal.Parse(drOrder["DeliveryPrice"].ToString());
                _demoPrice = decimal.Parse(drOrder["DemoPrice"].ToString());
                _promoPrice = decimal.Parse(drOrder["PromoPrice"].ToString());
                _milesPrice = decimal.Parse(drOrder["MilesPrice"].ToString());
                _miscPrice = decimal.Parse(drOrder["MiscPrice"].ToString());
                _parkingPrice = decimal.Parse(drOrder["Parking"].ToString());
                _tipPrice = decimal.Parse(drOrder["TipPrice"].ToString());
                _tax = decimal.Parse(drOrder["Tax"].ToString());
                _ordPrice = decimal.Parse(drOrder["OrderPrice"].ToString());
                checkNumb = drOrder["CheckNumb"].ToString();
                _confirmed = bool.Parse(drOrder["Approved"].ToString());
                _bySurvey = bool.Parse(drOrder["bySurvey"].ToString());
                drOrder.Close();

                orderInfo.NewUpdateOrder(sid, true, true,
                DateTime.Parse("1900-01-01"), 4, "", "", _instPrice, _delivery, _demoPrice,
                _milesPrice,_miscPrice,_parkingPrice,_tipPrice,_promoPrice,_tax,
                _ordPrice, _confirmed, checkNumb,_bySurvey);

                orderInfo.UpdateVendorDue(sid, 0, DateTime.Parse("1900-01-01"));
                BindDataGrid();
            }
        }

        private static DateTime GetNextThursday(DateTime CurrDate)
        {
            DateTime date = CurrDate.AddDays(6);
            while (date.DayOfWeek != DayOfWeek.Thursday)
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        private void SetVendorDue(int OrderID)
        {
            bool vPay, deduct;
            DateTime nextThursday = GetNextThursday(DateTime.Today);
            var orderInfo = new OrderData();
            SqlDataReader drOrder = orderInfo.NewOrderDetails(OrderID);
            drOrder.Read();
            _vendorID = int.Parse(drOrder["VendorID"].ToString());
            _instPrice = decimal.Parse(drOrder["BaseInstallPrice"].ToString());
            _delivery = decimal.Parse(drOrder["DeliveryPrice"].ToString());
            _demoPrice = decimal.Parse(drOrder["DemoPrice"].ToString());
            _promoPrice = decimal.Parse(drOrder["PromoPrice"].ToString());
            _milesPrice = decimal.Parse(drOrder["MilesPrice"].ToString());
            _miscPrice = decimal.Parse(drOrder["MiscPrice"].ToString());
            _parkingPrice = decimal.Parse(drOrder["Parking"].ToString());
            _tipPrice = decimal.Parse(drOrder["TipPrice"].ToString());
            _tax = decimal.Parse(drOrder["Tax"].ToString());
            _ordPrice = decimal.Parse(drOrder["OrderPrice"].ToString());
            _payProc = decimal.Parse(drOrder["PayProc"].ToString());
            solution = drOrder["SolutionDescr"].ToString();
            payType = int.Parse(drOrder["PayType"].ToString());
            checkNumb = drOrder["CheckNumb"].ToString();
            vPay = bool.Parse(drOrder["vPay"].ToString());
            deduct = bool.Parse(drOrder["Deduct"].ToString());
            _confirmed = bool.Parse(drOrder["Approved"].ToString());
            _bySurvey = bool.Parse(drOrder["bySurvey"].ToString());
            drOrder.Close();

            decimal minPrice = 150 * 100 / _payProc;
            decimal usePrice = _instPrice;
            if (minPrice > _instPrice) usePrice = minPrice;
            decimal basePrice = (usePrice + _delivery + _demoPrice - _promoPrice) * _payProc / 100;
            _vendorDue = basePrice + _milesPrice + _miscPrice + _tipPrice + _parkingPrice;
            if (deduct) _vendorDue = _vendorDue - 1.42m;

            string paymentType = "";
            switch (payType)
            {
                case 1:
                    paymentType = "CC";
                    break;
                case 2:
                    paymentType = "CHK";
                    break;
                case 3:
                    paymentType = "N/C";
                    break;
                case 4:
                    paymentType = "SQ";
                    break;
                case 5:
                    paymentType = "INV";
                    break;
                default:
                    paymentType = "";
                    break;
            }
            //paymentType = payType == 2 ? "CK" : "CC";
            paymentType = " payment by " + paymentType;
            comments = paymentType;
            if (!vPay)
            {
                comments = comments + "; payd date " + nextThursday.ToShortDateString() + "<br/>";
            }
            else comments = comments + "<br/>";

            orderInfo.NewUpdateOrder(OrderID, true, true,
                DateTime.Today, 5, solution, comments, _instPrice, _delivery, _demoPrice,
                _milesPrice,_miscPrice,_parkingPrice,_tipPrice,_promoPrice,_tax,_ordPrice, _confirmed, checkNumb,_bySurvey);
            if (!vPay)
            {
                orderInfo.UpdateVendorDue(OrderID, _vendorDue, nextThursday);
            }
            BindDataGrid();
            //Response.Redirect("InstallLog.aspx");
        }

        void CalculateTax()
        {
            GetOrderInfo();
            decimal taxRate = GetTaxRate();
            if (taxRate > 0)
            {
 //               instPrice = decimal.Parse(rntInstallPrice.Text);
                //if (rntDeliveryPrice.Text.Trim() == "")
                //{ _delivery = 0; rntDeliveryPrice.Text = "0.00"; }
                //else
                //{ _delivery = decimal.Parse(rntDeliveryPrice.Text); }
                if (txtDemoPrice.Text.Trim() == "")
                { _demoPrice = 0; txtDemoPrice.Text = "0.00"; }
                else
                { _demoPrice = decimal.Parse(txtDemoPrice.Text); }
                if (txtMilesPrice.Text.Trim() == "")
                { _milesPrice = 0; txtMilesPrice.Text = "0.00"; }
                else
                { _milesPrice = decimal.Parse(txtMilesPrice.Text); }
                if (txtMiscPrice.Text.Trim() == "")
                { _miscPrice = 0; txtMiscPrice.Text = "0.00"; }
                else
                { _miscPrice = decimal.Parse(txtMiscPrice.Text); }
                if (txtParking.Text.Trim() == "")
                { _parkingPrice = 0; txtParking.Text = "0.00"; }
                else
                { _parkingPrice = decimal.Parse(txtParking.Text); }
                if (txtTipPrice.Text.Trim() == "")
                { _tipPrice = 0; txtTipPrice.Text = "0.00"; }
                else
                { _tipPrice = decimal.Parse(txtTipPrice.Text); }
                if (txtPromoPrice.Text.Trim() == "")
                { _promoPrice = 0; txtPromoPrice.Text = "0.00"; }
                else
                { _promoPrice = decimal.Parse(txtPromoPrice.Text); }

                _tax = (_delivery + _demoPrice + _instPrice + _milesPrice + _miscPrice
                        + _parkingPrice + _tipPrice - _promoPrice) * taxRate;
                _tax = Math.Round(_tax, 2);
                txtTax.Text = _tax.ToString();
                _ordPrice = _delivery + _demoPrice + _instPrice + _milesPrice + _miscPrice
                + _parkingPrice + _tipPrice - _promoPrice + _tax;
                lblInvoice.Text = decimal.Parse(_ordPrice.ToString()).ToString("c");
            }
        }

        decimal GetTaxRate()
        {
            decimal taxRate = 0;
            if (_state == "OH")
            {
                if (_storeCode == "COL") taxRate = 0.0675m;
                if (_storeCode == "CIN") taxRate = 0.065m;
            }
            else
            {
                if (_state == "TX")
                {
                    taxRate = _storeCode == "ATX" ? 0.08m : 0.0825m;
                }
                else
                {
                    //if (_storeCode == "STL") taxRate = 0.09425m;
                    //else
                    //{
                        var tax = new OrderData();
                        taxRate = tax.GetTaxRate(_zip, _city);
                    //}
                }
            }
            return taxRate;
        }

        void FillInstalledDiscounts()
        {
            var reasons = new OrderData();
            drInfo = reasons.ShowInstalledDiscount();
            ddlReasons.DataSource = drInfo;
            ddlReasons.DataValueField = "DiscountID";
            ddlReasons.DataTextField = "DiscountReason";
            ddlReasons.DataBind();
            drInfo.Close();
        }

        protected void BtnTaxClick(object Sender, EventArgs E)
        {
            CalculateTax();
        }

        //public void cbVpay_OnCheckedChanged(object Sender, EventArgs E)
        //{
        //    CheckBox cbVpay = (CheckBox)Sender;
        //    GridViewRow row = (GridViewRow)cbVpay.NamingContainer;
        //    int sid = int.Parse(row.Cells[0].Text);
        //    bool payd = cbVpay.Checked;
        //    if(payd)
        //    {
        //        DateTime nextThursday = GetNextThursday(DateTime.Today);
        //        var orderInfo = new OrderData();
        //        SqlDataReader drOrder = orderInfo.NewOrderDetails(sid);
        //        drOrder.Read();
        //        vendorID = int.Parse(drOrder["VendorID"].ToString());
        //        instPrice = decimal.Parse(drOrder["BaseInstallPrice"].ToString());
        //        delivery = decimal.Parse(drOrder["DeliveryPrice"].ToString());
        //        demoPrice = decimal.Parse(drOrder["DemoPrice"].ToString());
        //        promoPrice = decimal.Parse(drOrder["PromoPrice"].ToString());
        //        milesPrice = decimal.Parse(drOrder["MilesPrice"].ToString());
        //        miscPrice = decimal.Parse(drOrder["MiscPrice"].ToString());
        //        parkingPrice = decimal.Parse(drOrder["Parking"].ToString());
        //        tipPrice = decimal.Parse(drOrder["TipPrice"].ToString());
        //        tax = decimal.Parse(drOrder["Tax"].ToString());
        //        ordPrice = decimal.Parse(drOrder["OrderPrice"].ToString());
        //        payProc = decimal.Parse(drOrder["PayProc"].ToString());
        //        solution = drOrder["SolutionDescr"].ToString();
        //        payType = int.Parse(drOrder["PayType"].ToString());
        //        checkNumb = drOrder["CheckNumb"].ToString();
        //        DateTime paymentDate = DateTime.Parse(drOrder["PaymentDate"].ToString());
        //        drOrder.Close();

        //        decimal minPrice = 150 * 100 / payProc;
        //        decimal usePrice = instPrice;
        //        if (minPrice > instPrice) usePrice = minPrice;
        //        decimal basePrice = (usePrice + delivery + demoPrice - promoPrice) * payProc / 100;
        //        vendorDue = basePrice + milesPrice + miscPrice + tipPrice + parkingPrice;

        //        comments = " Vendor payd date " + nextThursday.ToShortDateString() + "<br/>";

        //        orderInfo.NewUpdateOrder(sid, true, true,
        //        paymentDate, 4, solution, comments, instPrice, delivery, demoPrice, 
        //        milesPrice, miscPrice, parkingPrice, tipPrice,promoPrice,tax,
        //        ordPrice, true, checkNumb);

        //        orderInfo.UpdateVendorDue(sid, vendorDue, nextThursday);
        //        BindDataGrid();
        //    }
        //}
    }
}