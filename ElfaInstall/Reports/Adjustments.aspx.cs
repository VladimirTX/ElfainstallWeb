using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElfaInstall.Reports
{
    public partial class Adjustments : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();

        protected void Page_Load(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        void BindDataGrid()
        {
            sdsAdjustments.ConnectionString = ConString;
            string command = "EXEC pRepAdjustments";
            sdsAdjustments.SelectCommand = command;
        }
    }
}