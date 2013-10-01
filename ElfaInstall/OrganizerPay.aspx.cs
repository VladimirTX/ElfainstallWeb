using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    using System.Data.SqlClient;

    public partial class OrganizerPay : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        int _userID, _organizerID, _payType, _orderID, _orderStatus;
        private string _status;
        private bool _filter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status != "Admin" && _status != "Organizer") Response.Redirect("Login.aspx");
                if (_status == "Admin") _organizerID = 0;
                else _organizerID = checkLog.GetOrganizerID(_userID);
                hfOrganizerID.Value = _organizerID.ToString();
                this._filter = false;
                BindDataGrid();
            }
        }

        void BindDataGrid()
        {
            _organizerID = int.Parse(hfOrganizerID.Value);
            sdsLog.ConnectionString = ConString;
            if (_filter == false)
            {
                sdsLog.SelectCommand = "pGetOrganizerPay " + _organizerID;
            }
            sdsLog.Dispose();
        }

        protected void GrdLogPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }

        protected void GrdLogSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }

        protected void GrdOrdersRowDataBound(object Sender, GridViewRowEventArgs E)
        {
            if (E.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = E.Row.Cells[1];
                string sValue = cell.Text.Trim();
                if (sValue == "1")
                {
                    E.Row.Cells[2].Enabled = false;
                }
                else E.Row.Cells[12].Controls.Clear();
            }
        }

        public void cbPayd_OnCheckedChanged(object Sender, EventArgs E)
        {
            CheckBox cbPayd = (CheckBox)Sender;
            GridViewRow row = (GridViewRow)cbPayd.NamingContainer;
            int sid = int.Parse(row.Cells[0].Text);
            bool payd = cbPayd.Checked;
            string mess = "";
            if (payd)
            {
                SetVendorDue(sid);
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
            int organizerID;
            string comments;
            decimal payProc, fees, other, adjustment, total, organizerDue;
            DateTime nextThursday = GetNextThursday(DateTime.Today); 

            var orderInfo = new OrderData();
            SqlDataReader drOrder = orderInfo.NewOrderDetails(OrderID);
            drOrder.Read();
            _payType = int.Parse(drOrder["PayType"].ToString());
            vPay = bool.Parse(drOrder["vPay"].ToString());
            deduct = bool.Parse(drOrder["Deduct"].ToString());
            drOrder.Close();

            drOrder = orderInfo.ShowOrganizerOrder(OrderID);
            drOrder.Read();
            organizerID = int.Parse(drOrder["OrganizerID"].ToString());
            fees = decimal.Parse(drOrder["Fees"].ToString());
            other = decimal.Parse(drOrder["Other"].ToString());
            adjustment = decimal.Parse(drOrder["Adjustment"].ToString());
            drOrder.Close();

            drOrder = orderInfo.OrganizerInfo(organizerID);
            drOrder.Read();
            payProc = decimal.Parse(drOrder["PayProc"].ToString());
            drOrder.Close();

            total = fees + other - adjustment;
            organizerDue = total * payProc / 100;

            if (deduct) organizerDue = organizerDue - 1.42m;

            string paymentType = "";
            switch (_payType)
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
                default:
                    paymentType = "";
                    break;
            }

            paymentType = " payment by " + paymentType;
            comments = paymentType;
            if (!vPay)
            {
                comments = comments + "; payd date " + nextThursday.ToShortDateString() + "<br/>";
            }
            else comments = comments + "<br/>";

            orderInfo.UpdateOrganizerPay(OrderID, DateTime.Today, comments, organizerDue, nextThursday);
            BindDataGrid();
            //Response.Redirect("InstallLog.aspx");
        }
    }
}