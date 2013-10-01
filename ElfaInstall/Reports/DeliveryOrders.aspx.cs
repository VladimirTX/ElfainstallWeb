using System;
using System.Configuration;

namespace ElfaInstall.Reports
{
    public partial class DeliveryOrders : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _dateStart, _dateEnd;

        protected void PageLoad(object Sender, EventArgs E)
        {

        }

        private void BindDataGrid()
        {
            _dateStart = rdpFrom.SelectedDate.ToString();
            _dateEnd = rdpTo.SelectedDate.ToString();
            string command = "EXEC pOrdersWithDelivery '" + _dateStart + "','" + _dateEnd + "'";
            sdsOrders.SelectCommand = command;
            sdsOrders.ConnectionString = ConString;
        }

        protected void RbShowClick(object Sender, EventArgs E)
        {
            if (rdpFrom.SelectedDate != null && rdpTo.SelectedDate != null && rdpFrom.SelectedDate.Value <= rdpTo.SelectedDate.Value)
            {
                BindDataGrid();
                pnlReport.Visible = true;
            }
        }

        protected void GrdOrdersSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
    }
}