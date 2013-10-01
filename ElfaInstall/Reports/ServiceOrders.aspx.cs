using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElfaInstall.Reports
{
    public partial class ServiceOrders : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        private string _reportDate;

        protected void Page_Load(object Sender, EventArgs E)
        {

        }

        protected void BtnShowClick(object Sender, EventArgs E)
        {
            if(rdpDate.SelectedDate!=null)
            {
                BindDataGrid();
            }
        }

        private void BindDataGrid()
        {
            _reportDate = rdpDate.SelectedDate.ToString();
            string command = "EXEC pRepServiceOrders '" + _reportDate + "'";
            sdsOrders.SelectCommand = command;
            sdsOrders.ConnectionString = ConString;
        }
    }
}