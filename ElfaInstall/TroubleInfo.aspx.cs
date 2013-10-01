using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class TroubleInfo : System.Web.UI.Page
    {
        public int OrderID, UserID;
        SqlDataReader _drOrder;
        public string OrderNumb = "";
        private OrderData _orderInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Login.aspx"); }
            else
            { UserID = int.Parse(Request.Cookies["UserID"].Value); }
            if (Request.QueryString["OrderID"] != null)
            { OrderID = int.Parse(Request.QueryString["OrderID"]); }
            else
            { Response.Redirect("Orders.aspx"); }
            if(!IsPostBack)
            {
                FillOrderData();
                FillTroubleData();
            }
        }

        private void FillOrderData()
        {
            _orderInfo = new OrderData();
            _drOrder = _orderInfo.GetOrderProblems(OrderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                OrderNumb = _drOrder["OrderNumb"].ToString();
                lblOrder.Text = OrderNumb;
                lblCustomer.Text = _drOrder["Customer"].ToString();
                lblStore.Text = _drOrder["StoreCode"].ToString();
                lblInstaller.Text = _drOrder["VendorName"].ToString();
                string delivered = Boolean.Parse(_drOrder["DeliveryOption"].ToString()) ? "Yes" : "No";
                lblDelivered.Text = delivered;
                DateTime installDate = DateTime.Parse(_drOrder["InstallDate"].ToString());
                if (installDate > DateTime.Parse("01/01/2000"))
                {
                    lblInstDate.Text = installDate.ToShortDateString();
                }
                else lblInstDate.Text = "";
                string installed = Boolean.Parse(_drOrder["Installed"].ToString()) ? "Yes" : "No";
                lblInstalled.Text = installed;
                if (_drOrder["Ddetails"] != null && _drOrder["Ddetails"].ToString().Trim()!="")
                {
                    pnlDamaged.Visible = true;
                    lbldDetails.Text = _drOrder["Ddetails"].ToString();
                    lbldResolution.Text = _drOrder["Dresolution"].ToString();
                }
                if (_drOrder["Mdetails"] != null && _drOrder["Mdetails"].ToString().Trim() != "")
                {
                    pnlMissing.Visible = true;
                    lblmDetails.Text = _drOrder["Mdetails"].ToString();
                    lblmResolution.Text = _drOrder["Mresolution"].ToString();
                }
                if(_drOrder["Fdetails"]!=null && _drOrder["Fdetails"].ToString().Trim()!="")
                {
                    pnlFlaw.Visible = true;
                    lblFlaw.Text = _drOrder["Fdetails"].ToString();
                }
            }
            _drOrder.Close();
        }
        private void FillTroubleData()
        {
            _orderInfo = new OrderData();
            _drOrder = _orderInfo.GetTroubleLog(OrderID);
            if(_drOrder.HasRows)
            {
                string logData = "";
                while(_drOrder.Read())
                {
                    logData = logData + _drOrder["LogData"] +"</br>";
                }
                lblTroubleData.Text = logData;
            }
            _drOrder.Close();
        }

        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            if (txtTrouble.Text.Trim()!="") { SaveLogData();}
            Response.Redirect("OrderDetails.aspx?OrderID=" + OrderID);
        }

        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            Response.Redirect("OrderDetails.aspx?OrderID=" + OrderID);
        }

        private void SaveLogData()
        {
            var userInfo = new SessionData();
            //UserID = int.Parse(Request.Cookies["UserID"].Value);
            string userName = userInfo.UserName(UserID);
            _orderInfo = new OrderData();
            try
            {
                //_orderInfo.AddTroubleLog(OrderID, txtTrouble.Text.Trim(), userName);
            }
            catch (Exception ex)
            {
                userInfo.AddErrorLog("Trouble Info", "SaveLogData", OrderID + ": " + ex.Message);
            }
        }
    }
}