using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    using System.Data.SqlClient;

    public partial class OrganizerLog : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        int _userID, _organizerID, _payType, _orderID, _orderStatus;
        private string _status;
        SqlDataReader _drInfo;
        private decimal _fees, _other, _adjustment, _total;
        private bool _filter;

        protected void Page_Load(object Sender, EventArgs E)
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
            if (this._filter == false)
            {
                sdsLog.SelectCommand = "pGetOrganizerLog " + _organizerID;
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

        protected void cbDeduct_OnCheckedChanged(object Sender, EventArgs E)
        {
            CheckBox cbDeduct = (CheckBox)Sender;
            GridViewRow row = (GridViewRow)cbDeduct.NamingContainer;
            int sid = int.Parse(row.Cells[0].Text);
            bool deduct = cbDeduct.Checked;
            var orderInfo = new OrderData();
            orderInfo.UpdateDeductFlag(sid, deduct);
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
                    //E.Row.Cells[2].Enabled = false;
                }
                else
                {
                    E.Row.Cells[12].Controls.Clear();
                    E.Row.Cells[11].Enabled = false;
                }
            }
        }

        protected void GrdLogSelectedIndexChanged(object Sender, EventArgs E)
        {
            grdLog.Visible = false;
            pnlEdit.Visible = true;
            ShowDetals();
        }

        void ShowDetals()
        {
            GetOrderInfo();
        }

        void GetOrderInfo()
        {
            string result = grdLog.SelectedRow.Cells[0].Text;
            _orderID = int.Parse(result);
            var orderInfo = new OrderData();
            _drInfo = orderInfo.NewOrderDetails(_orderID);
            _drInfo.Read();
            lblOrderNumber.Text = _drInfo["OrderNumb"].ToString().Trim();
            string customer = _drInfo["fName"].ToString().Trim() + " " + _drInfo["lName"];
            lblCustomer.Text = customer;
            lblPhone.Text = _drInfo["hPhone"].ToString();
            _drInfo.Close();
            _drInfo = orderInfo.ShowOrganizerOrder(_orderID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                _fees = decimal.Parse(_drInfo["Fees"].ToString());
                _other = decimal.Parse(_drInfo["Other"].ToString());
                _adjustment = decimal.Parse(_drInfo["Adjustment"].ToString());
                _total = _fees + _other - _adjustment;
                lblFees.Text = _fees.ToString("c");
                lblOther.Text = _other.ToString("c");
                lblAdjustment.Text = _adjustment.ToString("c");
                lblTotal.Text = _total.ToString("c");
                lblComments.Text = _drInfo["Comments"].ToString();
            }
            _drInfo.Close();
        }

        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            //GetOrderInfo();
            _orderStatus = 4;
            _payType = 0;
            if (rbCC.Checked || rbCheck.Checked || rbNC.Checked || rbSQ.Checked || rbInv.Checked)
            {
                if (rbCC.Checked) _payType = 1;
                else
                {
                    if (rbCheck.Checked) _payType = 2;
                    else
                    {
                        if (rbNC.Checked) _payType = 3;
                        else
                        {
                            if (rbSQ.Checked) _payType = 4;
                            else if (rbInv.Checked) _payType = 5;
                        }
                    }
                }
            }
            else return;

            string result = grdLog.SelectedRow.Cells[0].Text;
            _orderID = int.Parse(result);
            var orderInfo = new OrderData();
            orderInfo.UpdateOrganizerOrder(_orderID, _orderStatus, _payType, txtCheckNumb.Text.Trim(), true);
            Response.Redirect("OrganizerLog.aspx");
        }

        protected void BtnCloseClick(object Sender, EventArgs E)
        {
            Response.Redirect("OrganizerLog.aspx");
        }
    }
}