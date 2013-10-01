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
    public partial class OrganizerOrder : System.Web.UI.Page
    {
        private SqlDataReader _drInfo, _drOrder;
        private int _customerID, _vendorID, _userID, _storeID, _orderID, _organizerID;
        private string _orderNumber, _instNotes, _userName;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { _userID = int.Parse(Request.Cookies["UserID"].Value); }
            var userInfo = new SessionData();
            string status = userInfo.UserStatus(_userID).Trim();
            if (status != "Admin" && status != "Organizer")
            {
                Response.Redirect("Login.aspx");
            }
            _userName = userInfo.UserName(_userID);
            if (!IsPostBack)
            {
                FillStates();
                FillStoreList();
                FillOrganizers();
                if(status=="Organizer")
                {
                    _organizerID = userInfo.GetOrganizerID(_userID);
                    ddlOrganizers.SelectedValue = _organizerID.ToString();
                }
            }
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
        void FillOrganizers()
        {
            var specialists = new OrderData();
            _drInfo = specialists.ShowSpecialists();
            ddlOrganizers.DataSource = _drInfo;
            ddlOrganizers.DataValueField = "SpecialistID";
            ddlOrganizers.DataTextField = "Specialist";
            ddlOrganizers.DataBind();
            _drInfo.Close();
        }

        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            Response.Redirect("~/Orders.aspx");
        }

        protected void BtnSubmitClick(object Sender, EventArgs E)
        {
            _customerID = SaveCustomer();
            SaveOrder();
            Response.Redirect("~/OrganizerLog.aspx");
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
            int status = 3;
            _storeID = int.Parse(ddlStores.SelectedValue);
            _organizerID = int.Parse(ddlOrganizers.SelectedValue);
            _orderNumber = OrderNumber("ORG");
            DateTime currdate;
            currdate = DateTime.Parse(rdpEventDate.SelectedDate.ToString());
            string comments = txtComments.Text.Trim();
            var order = new OrderData();
            _orderID = order.NewOrder(_orderNumber, _customerID, _storeID, DateTime.Today,
                                           "11111", "ASAP", false, false,'B', 0.00M, 0.00M,
                                           0.00M, 0.00M, 0.00M,"",comments, 0.00M);
            order.UpdateOrder(_orderID, _vendorID, false, false, currdate, DateTime.Parse("01/01/1900"), status,
                                  "Organizer order", comments,
                                  0.00M, 0.00M, 0.00M, 0.00M, 0.00M, 0.00M, "", "", true);
            order.NewAppointment(_orderID, currdate, currdate, comments);
            order.UpdateCustomer(_orderID, txtPhone1.Text, txtPhone2.Text, "", "", "");
            order.UpdateOption(_orderID, "ATH");
            order.AddOrganizerInvoice(_orderID, _organizerID, comments);

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
    }
}