using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElfaInstall
{
    public partial class TroubleLogReport : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();

        protected void Page_Load(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        void BindDataGrid()
        {
            const string sCommand = "SELECT o.OrderNumb,o.OrderDate,RTRIM(c.fName)+' '+RTRIM(c.lName) As Customer,t.Description "
                                    + "FROM tblOrders o INNER JOIN tblCustomers c ON o.CustomerID=c.CustomerID INNER JOIN tblTroubleLog t ON o.OrderID=t.OrderID "
                                    + "WHERE o.status < 4 ORDER BY OrderDate ";
            sdsOrders.ConnectionString = ConString;
            sdsOrders.SelectCommand = sCommand;
        }
    }
}