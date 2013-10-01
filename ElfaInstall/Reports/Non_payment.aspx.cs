using System;
using System.Configuration;

namespace ElfaInstall.Reports
{
    public partial class Non_payment : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();

        protected void Page_Load(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        void BindDataGrid()
        {
            sdsOrders.ConnectionString = ConString;
            sdsOrders.SelectCommand = "sp_Non_Payment";
        }
    }
}