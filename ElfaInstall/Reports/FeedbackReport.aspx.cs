using System;
using System.Configuration;
using System.Web.UI;

namespace ElfaInstall.Reports
{
    public partial class FeedbackReport : Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();

        protected void Page_Load(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        void BindDataGrid()
        {
            string sCommand = "EXEC sp_FeedBackReport";
            sdsReport.ConnectionString = ConString;
            sdsReport.SelectCommand = sCommand;
        }
    }
}