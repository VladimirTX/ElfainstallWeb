using System;
using System.Web.UI;

namespace ElfaInstall.Reports
{
    public partial class DeletedOrders : Page
    {
        //static string _conString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();

        protected void Page_Load(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        void BindDataGrid()
        {
            const string sCommand = "EXEC sp_DeletedOrders";
            sdsDeleted.SelectCommand = sCommand;
        }
    }
}