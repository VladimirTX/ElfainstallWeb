using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ElfaInstall.Reports
{
    public partial class ShowOpen : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _connElfa = new SqlConnection(ConString);
        OpenOrders _report1=new OpenOrders();
        InstallOrders _report2 = new InstallOrders();
        string _selectBy, _orderBy, _target, _where, _orderList, _targetName;
        string _dateStart, _dateEnd;
        SqlDataReader _drOrders;
        int _nPage;
        bool _bOpen, _bInstalled, _bClosed;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (PreviousPage != null && PreviousPage.Title == "elfa® Reports")
            {
                _target = ((HiddenField)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("hfTarget")).Value;
                _targetName = ((HiddenField)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("hfTargetName")).Value;
            }
            else { Response.Redirect("SummaryReport.aspx"); }
            _dateStart =
                ((RadDatePicker)
                 PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("rdpOpStart")).
                    SelectedDate.ToString();
            _dateEnd =
                ((RadDatePicker)
                 PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("rdpOpEnd")).
                    SelectedDate.ToString();
            _bOpen = ((RadioButton)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("rbOpen")).Checked;
            _bInstalled = ((RadioButton)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("rbInstalled")).Checked;
            _bClosed = ((RadioButton)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("rbClosed")).Checked;
            switch (_target)
            {
                case "Store":
                    _selectBy = ((DropDownList)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("ddlStores")).Text;
                    _where = " s.StoreID=" + _selectBy + " AND ";
                    _orderBy = " ORDER BY StoreName, OrderDate DESC ";
                    break;
                case "Vendor":
                    _selectBy = ((DropDownList)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("ddlVendors")).Text;
                    _where = " v.VendorID=" + _selectBy + " AND ";
                    _orderBy = " AND v.active=1 ORDER BY VendorName, OrderDate DESC ";
                    break;
                case "Market":
                    _selectBy = ((DropDownList)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("ddlMarkets")).Text;
                    _where = " m.MarketID=" + _selectBy + " AND ";
                    _orderBy = "ORDER BY MarketName, OrderDate DESC ";
                    break;
            }
            if (_selectBy == "0")
            {
                _where = " ";
            }
            if (_bOpen)
            {
                _where = _where + " o.OrderDate BETWEEN '" + _dateStart + "' AND '" + _dateEnd + "' AND o.Status<4 ";
                lblHeader.Text = "Open " + lblHeader.Text;
            }
            if (_bInstalled)
            {
                _where = _where + "  o.InstallDate BETWEEN '" + _dateStart + "' AND '" + _dateEnd + "' AND o.Status>3 ";
                lblHeader.Text = "Installed " + lblHeader.Text;
            }
            if (_bClosed)
            {
                _where = _where + "  o.InstallDate BETWEEN '" + _dateStart + "' AND '" + _dateEnd + "' AND o.Status>5 ";
                lblHeader.Text = "Closed Orders. Installed from ";
            }
            lblDate.Text = _dateStart + "&nbsp;&nbsp;to&nbsp;&nbsp;" + _dateEnd;
            lblTarget.Text = _target + " - " + _targetName;
            MakeSelection();
        }
        void MakeSelection()
        {
            decimal totalPurchase = 0.00M;
            decimal totalInstall = 0.00M;
            SqlCommand objComm = new SqlCommand();
            objComm.Connection = _connElfa;
            objComm.CommandType = CommandType.Text;
            string sqLstring = "SELECT o.OrderID,o.PurchasePrice,o.OrderPrice FROM tblOrders o INNER JOIN tblCustomers c ON o.CustomerID=c.CustomerID " +
                               "INNER JOIN tblStores s ON o.StoreID=s.StoreID LEFT JOIN tblVendors v ON o.VendorID=v.VendorID " +
                               "INNER JOIN tblMarkets m ON s.MarketID=m.MarketID WHERE ";
            sqLstring = sqLstring + _where + _orderBy;
            objComm.CommandText = sqLstring;
            _connElfa.Open();
            _drOrders = objComm.ExecuteReader();
            _orderList = "(";
            int count = 0;
            _nPage = 0;
            int total = 0;
            if (_drOrders.HasRows)
            {
                while (_drOrders.Read())
                {
                    total++;
                    string orderID = _drOrders["OrderID"].ToString().Trim();
                    totalPurchase = totalPurchase + decimal.Parse(_drOrders["PurchasePrice"].ToString());
                    totalInstall = totalInstall + decimal.Parse(_drOrders["OrderPrice"].ToString());
                    _orderList = _orderList + orderID + ",";
                    count++;
                    if (count > 25)
                    {
                        _orderList = _orderList + "0)";
                        count = 0;
                        _nPage++;
                        PrintPage();
                        _orderList = "(";
                    }
                }
                if (count > 0)
                {
                    _orderList = _orderList + "0)";
                    _nPage++;
                    PrintPage();
                }
            }
            _drOrders.Close();
            _connElfa.Close();
            lblTotPurchase.Text = totalPurchase.ToString("c");
            lblTotInstall.Text = totalInstall.ToString("c");
            lblTotOrders.Text = total.ToString();
        }
        void PrintPage()
        {
            var tR1 = new TableRow();
            var tC1 = new TableCell();
            if (_bInstalled || _bClosed)
            {
                _report2 = (InstallOrders)LoadControl("~/Reports/InstallOrders.ascx");
                _report2.OrderList = _orderList;
                _report2.OrderBy = _orderBy;
                _report2.NPage = _nPage;
                tC1.Controls.Add(_report2);
            }
            else
            {
                _report1 = (OpenOrders)LoadControl("~/Reports/OpenOrders.ascx");
                _report1.OrderList = _orderList;
                _report1.OrderBy = _orderBy;
                _report1.NPage = _nPage;
                tC1.Controls.Add(_report1);
            }
            tR1.Cells.Add(tC1);
            tblMain.Rows.Add(tR1);
            _report1 = null;
            _report2 = null;
        }
    }
}