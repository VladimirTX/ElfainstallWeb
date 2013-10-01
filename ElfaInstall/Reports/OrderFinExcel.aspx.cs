using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ElfaInstall.Reports
{
    public partial class OrderFinExcel : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        private string _param;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (PreviousPage != null && PreviousPage.Title == "Financial Report")
            {
                _param = ((HiddenField)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("hfParam")).Value.Trim();
            }
            DataSet dsExport = GetData();
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dsExport;

            //Report Header
            hw.WriteLine("<b><u><font size='5'>" + "Order Financial Details " + "</font></u></b>");

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

        private DataSet GetData()
        {
            var ds = new DataSet("Orders");
            var da = new SqlDataAdapter("dbo.pOrderReport '" + _param + "'", ConString);
            da.TableMappings.Add("Table", "Orders");
            da.Fill(ds);
            return ds;
        }
    }
}