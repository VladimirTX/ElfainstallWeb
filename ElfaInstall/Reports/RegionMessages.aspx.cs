using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElfaInstall.Reports
{
    using System.Configuration;

    public partial class RegionMessages : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConString);
        private int total;
        private decimal amount, invoice;

        protected void Page_Load(object Sender, EventArgs E)
        {
            BindDataGrid();
            GetTotal();
            lblTotal.Text = total.ToString();
            lblAmount.Text = amount.ToString("c");
            lblInvoice.Text = invoice.ToString("c");
        }
        void BindDataGrid()
        {
            sdsMessages.ConnectionString = ConString;
            string command = "EXEC pRepRegMessages " + total + "," + amount + "," + invoice;
            sdsMessages.SelectCommand = command;
        }

        protected void GrdMessagesRowDataBound(object Sender, GridViewRowEventArgs E)
        {
            if (E.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = E.Row.Cells[0];
                string sValue = cell.Text.Trim();
                if (sValue == "Coordinators")
                {
                    E.Row.BackColor = System.Drawing.Color.LightGray;
                    E.Row.Font.Bold = true;
                    E.Row.Cells[1].Text = "";
                    E.Row.Cells[2].Text = "";
                    E.Row.Cells[3].Text = "";
                    E.Row.Cells[4].Text = "";
                    E.Row.Cells[5].Text = "";
                    E.Row.Cells[6].Text = "";
                    E.Row.Cells[7].Text = "";
                    E.Row.Cells[8].Text = "";
                    E.Row.Cells[9].Text = "";
                }
                if (sValue == "Regionals")
                {
                    E.Row.BackColor = System.Drawing.Color.LightGray;
                    E.Row.Font.Bold = true;
                    E.Row.Cells[1].Text = "";
                    E.Row.Cells[2].Text = "";
                    E.Row.Cells[3].Text = "";
                    E.Row.Cells[4].Text = "";
                    E.Row.Cells[5].Text = "";
                    E.Row.Cells[6].Text = "";
                    E.Row.Cells[7].Text = "";
                    E.Row.Cells[8].Text = "";
                    E.Row.Cells[9].Text = "";
                }
            }
        }

        public void GetTotal()
        {
            var objComm = new SqlCommand();
            objComm.Connection = _objConn;
            objComm.CommandType = CommandType.StoredProcedure;
            objComm.CommandText = "pRepRegMessages";
            SqlParameter parmResult1 = objComm.Parameters.Add("@Summary", SqlDbType.Int);
            SqlParameter parmResult2 = objComm.Parameters.Add("@Amount", SqlDbType.Decimal);
            SqlParameter parmResult3 = objComm.Parameters.Add("@Invoice", SqlDbType.Decimal);
            parmResult1.Direction = ParameterDirection.Output;
            parmResult2.Direction = ParameterDirection.Output;
            parmResult3.Direction = ParameterDirection.Output;
            _objConn.Open();
            objComm.ExecuteNonQuery();
            total = int.Parse(objComm.Parameters["@Summary"].Value.ToString());
            amount = decimal.Parse(objComm.Parameters["@Amount"].Value.ToString());
            invoice = decimal.Parse(objComm.Parameters["@Invoice"].Value.ToString());
            _objConn.Close();
        }
    }
}