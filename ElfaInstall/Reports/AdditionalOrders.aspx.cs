using System;
using System.Configuration;

namespace ElfaInstall.Reports
{
    public partial class AdditionalOrders : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _dateStart, _dateEnd;

        protected void Page_Load(object Sender, EventArgs E)
        {

        }

        protected void RbShowClick(object Sender, EventArgs E)
        {
            if(rdpFrom.SelectedDate!=null && rdpTo.SelectedDate!=null && rdpFrom.SelectedDate.Value<=rdpTo.SelectedDate.Value)
            {
                BindDataGrid();
            }
        }
        private void BindDataGrid()
        {
            _dateStart = rdpFrom.SelectedDate.ToString();
            _dateEnd = rdpTo.SelectedDate.ToString();
            string command = "EXEC pGetAdditionalOrders '" + _dateStart + "','" + _dateEnd + "'";
            sdsOrders.SelectCommand = command;
            sdsOrders.ConnectionString = ConString;
        }
    }
}