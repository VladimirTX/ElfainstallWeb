using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class FakeOrder : System.Web.UI.Page
    {
        SqlDataReader _drInfo;
        private int _customerID, _vendorID, _userID;
        private string _orderNumber;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            var userInfo = new SessionData();
            string status = userInfo.UserStatus(_userID);
            if (status == "Admin")
            {
                if (Request.QueryString["VendorID"] != null)
                {
                    _vendorID = int.Parse(Request.QueryString["VendorID"]);
                }
                else
                {
                    Response.Redirect("Orders.aspx");
                }
            }
            else
            { Response.Redirect("Login.aspx"); }
            if (!IsPostBack)
            {
                _drInfo = userInfo.VendorInfo(_vendorID);
                _drInfo.Read();
                string vendorName = _drInfo["VendorName"].ToString();
                _drInfo.Close();
                lblInstaller.Text = vendorName;
                FillStates();
                FillReason();
            }
        }
        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            Response.Redirect("Orders.aspx");
        }
        protected void BtnSubmitClick(object Sender, EventArgs E)
        {
            _customerID = SaveCustomer();
            SaveOrder();
            Response.Redirect("InstallLog.aspx");
        }
        void FillReason()
        {
            var reasons = new OrderData();
            _drInfo = reasons.ShowFulFillReasons();
            ddlReason.DataSource = _drInfo;
            ddlReason.DataTextField = "Description";
            ddlReason.DataValueField = "reasonCode";
            ddlReason.DataBind();
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
        int SaveCustomer()
        {
            var customer = new OrderData();
            int custID = customer.NewCustomer(txtfName.Text.Trim(), txtMi.Text, txtlName.Text.Trim(),
                                              txtAddr1.Text, txtAddr2.Text, txtCity.Text.Trim(), ddlStates.SelectedValue,
                                              txtZip.Text, txtPhone1.Text, txtPhone2.Text, "");
            return custID;
        }
        void SaveOrder()
        {
            string comments = txtComments.Text.Trim();
            if (txtReason.Text.Trim() != "") comments = "Reason: " + txtReason.Text.Trim() + "; " + comments;
            _orderNumber = OrderNumber();
            decimal misc = decimal.Parse(txtVendorDue.Text);
            DateTime eventDate, vendorDate;
            if (rdpEventDate.SelectedDate != null)
            {
                eventDate = DateTime.Parse(rdpEventDate.SelectedDate.ToString());
            }
            else eventDate = DateTime.Parse("01/01/1900");
            if (rdpPayDate.SelectedDate != null)
            {
                vendorDate = DateTime.Parse(rdpPayDate.SelectedDate.ToString());
            }
            else vendorDate = DateTime.Parse("01/01/1900");
            var order = new OrderData();
            int ordID = order.NewOrder(_orderNumber, _customerID, 0, DateTime.Today,
                                       "22222", "ASAP", false, false,
                                       'B', 0.00M, 0.00M, 0.00M, 0.00M,
                                       0.00M, "", txtComments.Text, 0.00M);
            order.NewAppointment(ordID, eventDate, DateTime.Today, comments);
            order.AssignVendor(ordID, _vendorID);
            order.UpdateOrder(ordID, _vendorID, true, true, eventDate, DateTime.Today, 5, "", comments,
                              0.00M, 0.00M, 0.00M, 0.00M, misc, 0.00M, "", "", true);
            order.UpdateOrderPrice(ordID, 0.00M, 0.00M, 0.00M, 0.00M, misc, 0.00M, 0.00M, 0.00M, 0, misc, vendorDate, 5);
            order.CloseAppointment(ordID);
            order.SaveReason(ordID, int.Parse(ddlReason.SelectedValue));
        }

        static string OrderNumber()
        {
            string orderNumb = "F";
            orderNumb = orderNumb + DateTime.Today.Year.ToString().Substring(2, 2) +
                        DateTime.Today.Month.ToString().PadLeft(2, '0');
            orderNumb = orderNumb + DateTime.Today.Day.ToString().PadLeft(2, '0');
            orderNumb = orderNumb + DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                        DateTime.Now.Minute.ToString().PadLeft(2, '0');
            return orderNumb;
        }

        protected void DdlReasonSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReason.SelectedValue == "22")
                pnlReason.Visible = true;
            else
                pnlReason.Visible = false;
        }
    }
}