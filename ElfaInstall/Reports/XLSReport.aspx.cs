using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ElfaInstall.Reports
{
    public partial class XLSReport : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _viewCommand, _sHeader;
        string _selectBy, _orderBy, _target, _where, _targetName;
        string _dateStart, _dateEnd;
        bool _bOpen, _bInstalled;
        protected void Page_Load(object Sender, EventArgs E)
        {
            _sHeader = " Orders from ";
            if (PreviousPage != null && PreviousPage.Title == "elfa® Reports")
            {
                _target = ((HiddenField)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("hfTarget")).Value;
                _targetName = ((HiddenField)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("hfTargetName")).Value;
            }
            else { Response.Redirect("Reports.aspx"); }
            _dateStart =
                ((RadDatePicker)
                 PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("rdpOpStart")).
                    SelectedDate.ToString();
            _dateEnd =
                ((RadDatePicker)
                 PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("rdpOpEnd")).
                    SelectedDate.ToString();
            _bOpen = ((RadioButton)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("rbOpen")).Checked;
            _bInstalled = ((RadioButton)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("rbInstalled")).Checked;
            _where = " o.OrderDate BETWEEN '" + _dateStart + "' AND '" + _dateEnd + "'";
            switch (_target)
            {
                case "Store":
                    _selectBy = ((DropDownList)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("ddlStores")).Text;
                    if (_selectBy != "0")
                    { _where = _where + " AND s.StoreID=" + _selectBy + " "; }
                    _orderBy = "ORDER BY StoreName, OrderDate DESC ";
                    break;
                case "Vendor":
                    _selectBy = ((DropDownList)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("ddlVendors")).Text;
                    if (_selectBy != "0")
                    { _where = _where + " AND v.VendorID=" + _selectBy + " "; }
                    _orderBy = "ORDER BY VendorName, OrderDate DESC ";
                    break;
                case "Market":
                    _selectBy = ((DropDownList)PreviousPage.Master.FindControl("Form1").FindControl("ContentPlaceHolder1").FindControl("ddlMarkets")).Text;
                    if (_selectBy != "0")
                    { _where = _where + " AND m.MarketID=" + _selectBy + " "; }
                    _orderBy = "ORDER BY MarketName, OrderDate DESC ";
                    break;
            }
            if (_bOpen)
            {
                _where = _where + "AND o.Status<4 ";
                _sHeader = "Open " + _sHeader;
            }
            if (_bInstalled)
            {
                _where = _where + "AND o.Status>3 ";
                _sHeader = "Installed " + _sHeader;
            }
            _sHeader = _sHeader + _dateStart + "  to  " + _dateEnd;
            _sHeader = _sHeader + " " + _target + " - " + _targetName;
            _viewCommand = "SELECT CAST(o.OrderDate As varchar(11)) As [Order Date],o.OrderNumb As [Order #],RTRIM(c.fName) + ' ' + c.lName AS Customer," +
                "s.StoreName As [Store Name], m.MarketName As Market," +
                "v.VendorName As Vendor,o.PurchasePrice As [elfa Amt],o.OrderPrice As [Invoice Amt]," +
                "CASE WHEN o.Status<4 THEN DateDiff(dd,OrderDate,GETDATE()) ELSE DateDiff(dd,OrderDate,InstallDate) END As [Days Open] " +
                "FROM tblOrders o INNER JOIN tblCustomers c ON o.CustomerID=c.CustomerID " +
                "INNER JOIN tblStores s ON o.StoreID=s.StoreID " +
                "INNER JOIN tblVendors v ON o.VendorID=v.VendorID " +
                "INNER JOIN tblMarkets m ON s.MarketID=m.MarketID " +
                "WHERE ";
            _viewCommand = _viewCommand + _where + _orderBy;
            var cn = new SqlConnection(ConString);
            cn.Open();
            var da = new SqlDataAdapter(_viewCommand, cn);
            var dt = new DataTable();
            da.Fill(dt);
            cn.Close();

            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";

            string sep = "";
            Response.Write(_sHeader + "\n\n");
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(sep + dc.ColumnName);
                sep = "\t";
            }
            Response.Write("\n");

            int i;
            int count = 0;
            float elfaprice = 0;
            float invprice = 0;
            foreach (DataRow dr in dt.Rows)
            {
                sep = "";
                count++;
                elfaprice = elfaprice + float.Parse(dr[6].ToString());
                invprice = invprice + float.Parse(dr[7].ToString());
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 6 || i == 7)
                    { Response.Write(sep + float.Parse(dr[i].ToString()).ToString("c")); }
                    else Response.Write(sep + dr[i]);
                    sep = "\t";
                }
                Response.Write("\n");
            }
            Response.Write("\n");
            sep = "\t";
            Response.Write("Total: " + count + sep + sep + sep + sep + sep + sep + elfaprice.ToString("c") + sep + invprice.ToString("c"));
        }
    }
}