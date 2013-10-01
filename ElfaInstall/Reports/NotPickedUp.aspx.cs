using System;
using System.Configuration;

namespace ElfaInstall.Reports
{
    public partial class NotPickedUp : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        private DateTime? _startDate, _endDate;

        protected void PageLoad(object Sender, EventArgs E)
        {
        }
        void BindDataGrid()
        {
            sdsNotPickedUp.ConnectionString = ConString;
            _startDate = rdpStart.SelectedDate;
            _endDate = rdpEnd.SelectedDate;
            string sCommand = "EXEC pNotPickedUp '" + _startDate + "','" + _endDate + "'";
            sdsNotPickedUp.SelectCommand = sCommand;
        }

        protected void Button1Click(object Sender, EventArgs E)
        {
            if (rdpStart.SelectedDate != null && rdpEnd.SelectedDate != null)
            {
                BindDataGrid();
            }
        }

        protected void GrdNotPickedUpPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }

        protected void GrdNotPickedUpSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
    }
}