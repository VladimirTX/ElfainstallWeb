using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class ServiceOrder : System.Web.UI.Page
    {
        private SqlDataReader _drInfo, _drOrder;
        private int _customerID, _vendorID, _userID, _storeID, _orderID;
        private string _orderNumber, _instNotes, _userName;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            var userInfo = new SessionData();
            string status = userInfo.UserStatus(_userID);
            if (status != "Admin")
            {
                Response.Redirect("Login.aspx");
            }
            if (Request.QueryString["OrderID"] != null)
            {
                _orderID = int.Parse(Request.QueryString["OrderID"]);
            }
            else _orderID = 0;
            _userName = userInfo.UserName(_userID);
            if (!IsPostBack)
            {
                FillVendorList();
                FillStates();
                FillStoreList();
                FillReason();
                if(_orderID>0)
                {
                    FillOrderInfo();
                }
            }
        }
        void FillVendorList()
        {
            var vendors = new OrderData();
            _drInfo = vendors.VendorList();
            ddlVendors.DataSource = _drInfo;
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataTextField = "Vendor";
            ddlVendors.DataBind();
            _drInfo.Close();
        }
        void FillVendorAtHome()
        {
            var vendors = new SessionData();
            int storeID = vendors.StoreIDByCode("ATH");
            _drInfo = vendors.VendorListByStore(storeID);
            ddlVendors.DataSource = _drInfo;
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataTextField = "VendorName";
            ddlVendors.DataBind();
            _drInfo.Close();
        }
        void FillStates()
        {
            var states = new OrderData();
            _drInfo = states.StateList();
            ddlStates.DataSource = _drInfo;
            ddlStates.DataTextField = "name";
            ddlStates.DataValueField = "code";
            ddlStates.DataBind();
            _drInfo.Close();
        }
        void FillStoreList()
        {
            var installers = new OrderData();
            _drInfo = installers.StoreList();
            ddlStores.DataSource = _drInfo;
            ddlStores.DataValueField = "StoreID";
            ddlStores.DataTextField = "Store";
            ddlStores.DataBind();
            _drInfo.Close();
        }
        void FillReason()
        {
            var reasons = new OrderData();
            _drInfo = reasons.ShowServiceReasons();
            ddlReason.DataSource = _drInfo;
            ddlReason.DataTextField = "Description";
            ddlReason.DataValueField = "reasonCode";
            ddlReason.DataBind();
            _drInfo.Close();
        }

        void FillOrderInfo()
        {
            var orderInfo = new OrderData();
            _drOrder = orderInfo.NewOrderDetails(_orderID);
            if(_drOrder.HasRows)
            {
                _drOrder.Read();
                txtfName.Text = _drOrder["fName"].ToString();
                txtMi.Text = _drOrder["mi"].ToString();
                txtlName.Text = _drOrder["lName"].ToString();
                txtAddr1.Text = _drOrder["address1"].ToString();
                txtAddr2.Text = _drOrder["address2"].ToString();
                txtCity.Text = _drOrder["city"].ToString();
                ddlStates.SelectedValue = _drOrder["state"].ToString().Trim();
                txtZip.Text = _drOrder["zip"].ToString();
                txtPhone1.Text = _drOrder["hphone"].ToString();
                txtPhone2.Text = _drOrder["phone2"].ToString();
                ddlStores.SelectedValue = _drOrder["StoreID"].ToString();
                lblComments.Text = _drOrder["InstNotes"].ToString().Trim();
                hfOrderNumb.Value = _drOrder["OrderNumb"].ToString();
                hfCustID.Value = _drOrder["CustomerID"].ToString();
                hfOption.Value = _drOrder["Options"].ToString().Trim();
                if (hfOption.Value.Trim() != "" && hfOption.Value.IndexOf("ATH") != -1)
                {
                    FillVendorAtHome();
                }
            }
        }
        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            Response.Redirect("Orders.aspx");
        }
        protected void BtnSubmitClick(object Sender, EventArgs E)
        {
            if (_orderID > 0)
            {
                _customerID = int.Parse(hfCustID.Value);
            }
            else {_customerID = SaveCustomer();}
            SaveOrder();
            Response.Redirect("Orders.aspx");
        }
        int SaveCustomer()
        {
            var customer = new OrderData();
            int custID = customer.NewCustomer(txtfName.Text.Trim(), txtMi.Text, txtlName.Text.Trim(),
                                              txtAddr1.Text, txtAddr2.Text, txtCity.Text.Trim(), ddlStates.SelectedValue,
                                              txtZip.Text, txtPhone1.Text, txtPhone2.Text,"");
            return custID;
        }

        void SaveOrder()
        {
            string orderType = ddlOrderType.SelectedValue.Trim();
            string printType = ddlOrderType.SelectedItem.Text.Trim();
            int status = 0;
            _vendorID = int.Parse(ddlVendors.SelectedValue);
            if (_vendorID > 0) status = 1;
            _storeID = int.Parse(ddlStores.SelectedValue);
            if (_orderID > 0) _orderNumber = orderType + hfOrderNumb.Value.Trim();
            else _orderNumber = OrderNumber(orderType);
            decimal misc = decimal.Parse(txtPrice.Text);
            DateTime currdate;
            if (rdpEventDate.SelectedDate == null)
            {
                currdate = DateTime.Parse("01/01/1900");
            }
            else
            {
                currdate = DateTime.Parse(rdpEventDate.SelectedDate.ToString());
                if (status == 1) status = 2;
            }
            string comments = txtComments.Text.Trim();
            //if(comments!="")
            //{
            comments = comments + " by " + _userName + ". ";
            comments = lblComments.Text.Trim() + "<br>" + printType + " created " + DateTime.Today.ToShortDateString()
                       + " " + comments;
            //}
            var order = new OrderData();
            if (_orderID > 0 && orderType == "T")
            {
                order.UpdateTOrder(_orderID,_orderNumber,comments);
            }
            else
            {
                int ordID = order.NewOrder(_orderNumber, _customerID, _storeID, DateTime.Today,
                                           "11111", "ASAP", false, false,
                                           'B', 0.00M, misc,
                                           0.00M, 0.00M, misc,
                                           "", "", 0.00M);
                order.UpdateOrder(ordID, _vendorID, false, false, currdate, DateTime.Parse("01/01/1900"), status,
                                  printType, "",
                                  misc, 0.00M, 0.00M, 0.00M, 0.00M, misc, "", "", false);
                order.NewAppointment(ordID, currdate, currdate, comments);
                order.UpdateCustomer(ordID, txtPhone1.Text, txtPhone2.Text, "", comments, "");
                order.UpdateInstallName(ordID, txtfName.Text.Trim(), txtMi.Text, txtlName.Text.Trim());
                if (_vendorID > 0) order.AssignVendor(ordID, _vendorID);
                if (_orderID > 0 && hfOption.Value.Trim()!="" && hfOption.Value.IndexOf("ATH") != -1)
                {
                    order.UpdateOption(ordID, hfOption.Value);
                    order.CopyAtHomeData(_orderID, ordID);
                }
                _orderID = ordID;
            }
            if (orderType == "S")
            {
                order.SaveReason(_orderID, int.Parse(ddlReason.SelectedValue));
            }
        }

        static string OrderNumber(string OrderType)
        {
            string orderNumb = OrderType;
            orderNumb = orderNumb + DateTime.Today.Year.ToString().Substring(2, 2) +
                        DateTime.Today.Month.ToString().PadLeft(2, '0');
            orderNumb = orderNumb + DateTime.Today.Day.ToString().PadLeft(2, '0');
            orderNumb = orderNumb + DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                        DateTime.Now.Minute.ToString().PadLeft(2, '0');
            return orderNumb;
        }

        protected void DdlOrderTypeSelectedIndexChanged(object Sender, EventArgs E)
        {
            if(ddlOrderType.SelectedValue=="S")
            {
                ddlReason.Visible = true;
                lblReason.Visible = true;
            }
            else
            {
                ddlReason.Visible = false;
                lblReason.Visible = false;
            }
        }
    }
}