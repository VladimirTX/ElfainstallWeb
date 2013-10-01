using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElfaInstall.Reports
{
    public partial class Accomodation : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        private string _startDate, _endDate;

        protected void Page_Load(object Sender, EventArgs E)
        {

        }

        protected void BtnViewReportClick(object Sender, EventArgs E)
        {
            if (rdpDate1.SelectedDate != null && rdpDate2.SelectedDate != null)
            {
                _startDate = rdpDate1.SelectedDate.ToString();
                _endDate = rdpDate2.SelectedDate.ToString();

                string command;

                sdsService.ConnectionString = ConString;
                command = "EXEC pRepAccomodationGrid '" + _startDate + "','" + _endDate + "','Service Orders'";
                sdsService.SelectCommand = command;

                //sdsRefunds.ConnectionString = ConString;
                //command = "EXEC pRepAccomodationGrid '" + _startDate + "','" + _endDate + "','Refunds'";
                //sdsRefunds.SelectCommand = command;

                sdsAdjustments.ConnectionString = ConString;
                command = "EXEC pRepAccomodationGrid '" + _startDate + "','" + _endDate + "','Adjustments'";
                sdsAdjustments.SelectCommand = command;

                sdsTrip.ConnectionString = ConString;
                command = "EXEC pRepAccomodationGrid '" + _startDate + "','" + _endDate + "','Trip to Store'";
                sdsTrip.SelectCommand = command;

                sdsFulFillment.ConnectionString = ConString;
                command = "EXEC pRepAccomodationGrid '" + _startDate + "','" + _endDate + "','Fulfillment Orders'";
                sdsFulFillment.SelectCommand = command;

                pnlGrids.Visible = true;
            }
        }

        protected void BntExcelClick(object Sender, EventArgs E)
        {
            if(rdpDate1.SelectedDate!=null && rdpDate2.SelectedDate!=null)
            {
                _startDate = rdpDate1.SelectedDate.ToString();
                _endDate = rdpDate2.SelectedDate.ToString();

                DataSet dsExport = GetData();
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dsExport;

                //Report Header
                hw.WriteLine(
                    "<b><u><font size='4'>" + "Accomodation Report " + rdpDate1.SelectedDate.Value.ToShortDateString()
                    + "-" + rdpDate2.SelectedDate.Value.ToShortDateString() + "</font></u></b>");

                // Get the HTML for the control.
                dgGrid.HeaderStyle.Font.Bold = true;
                dgGrid.DataBind();
                dgGrid.RenderControl(hw);

                // Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }

        private DataSet GetData()
        {
            var ds = new DataSet("Orders");
            var da = new SqlDataAdapter("dbo.pRepAccomodation '" + _startDate + "','" + _endDate + "'", ConString);
            da.TableMappings.Add("Table", "Orders");
            da.Fill(ds);
            return ds;
        }
    }
}