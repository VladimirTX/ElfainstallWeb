using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ElfaInstall.Reports
{
    public partial class InstallOrders : System.Web.UI.UserControl
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _connElfa = new SqlConnection(ConString);
        public string OrderList, OrderBy;
        public int NPage;

        protected void Page_Load(object Sender, EventArgs E)
        {
            BindDataGrid();
            lblPage.Text = NPage.ToString();
        }
        void BindDataGrid()
        {
            var cmdSelect = new SqlCommand();
            cmdSelect.Connection = _connElfa;
            cmdSelect.CommandType = CommandType.Text;
            cmdSelect.CommandText = "SELECT o.OrderDate,o.OrderNumb,c.fName + ' ' + c.lName AS Customer,s.StoreID,s.StoreName, m.MarketName, " +
                "v.VendorID,v.VendorName As Vendor,o.PurchasePrice,o.Actual,o.OrderPrice,o.InstallDate," +
                "CASE WHEN o.Status<4 THEN DateDiff(dd,OrderDate,GETDATE()) ELSE DateDiff(dd,OrderDate,InstallDate) END As Days, " +
                "CASE WHEN o.DeliveryPrice>0 THEN 'YES' ELSE 'NO' END As Delivery " + 
                "FROM tblOrders o INNER JOIN tblCustomers c ON o.CustomerID=c.CustomerID INNER JOIN tblStores s ON o.StoreID=s.StoreID " +
                "LEFT JOIN tblVendors v ON o.VendorID=v.VendorID INNER JOIN tblMarkets m ON s.MarketID=m.MarketID " +
                "WHERE o.OrderID IN " + OrderList;
            cmdSelect.CommandText = cmdSelect.CommandText + OrderBy;
            var dadOrders = new SqlDataAdapter(cmdSelect);
            var dtsOrders = new DataSet();
            dadOrders.Fill(dtsOrders);
            dgrOrders.DataSource = dtsOrders;
            dgrOrders.DataBind();
        }
    }
}