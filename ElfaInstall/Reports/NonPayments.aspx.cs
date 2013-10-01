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
    public partial class NonPayments : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsExport = GetData();
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dsExport;

            //Report Header
            hw.WriteLine("<b><u><font size='5'>" + "Non-payment orders " + "</font></u></b>");
            //hw.WriteLine("<br>&mp;nbsp;");

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
            var da = new SqlDataAdapter("sp_NonPayments",ConString);
            da.TableMappings.Add("Table", "Orders");
            da.Fill(ds);
            return ds;
        }
    }
}