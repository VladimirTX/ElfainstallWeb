using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarlosAg.ExcelXmlWriter;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class PayByType : System.Web.UI.Page
    {
        SqlDataReader _drInfo;
        string _dateStart, _dateEnd;
        static readonly string ConnString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConnString);

        protected void Page_Load(object Sender, EventArgs E)
        {

        }

        protected void RdpStartSelectedDateChanged(object Sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs E)
        {
            lblResult.Visible = false;
        }

        protected void RdpEndSelectedDateChanged(object Sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs E)
        {
            lblResult.Visible = false;
        }

        protected void BtnCompletedClick(object Sender, EventArgs E)
        {
            if (rdpStart.SelectedDate != null && rdpEnd.SelectedDate != null)
            {
                _dateStart = rdpStart.SelectedDate.Value.ToString();
                _dateEnd = rdpEnd.SelectedDate.Value.ToString();
                var objComm = new SqlCommand();
                objComm.Connection = _objConn;
                objComm.CommandType = CommandType.StoredProcedure;
                objComm.CommandText = "pRepPayByType";
                objComm.Parameters.AddWithValue("@StartDate", _dateStart);
                objComm.Parameters.AddWithValue("@EndDate", _dateEnd);
                _objConn.Open();
                _drInfo = objComm.ExecuteReader(CommandBehavior.CloseConnection);
                if (_drInfo.HasRows) ShowReport();
                else
                    lblResult.Text = "No result for selected date range.";
            }
        }

        private void ShowReport()
        {
            var monthInfo = new List<MonthInfo>();
            int vendor = 0;
            int month = 0;
            string sMonth = "";
            string sVendor = "";
            string pType = "";
            try
            {
                var book = new Workbook();
                book.ExcelWorkbook.ActiveSheetIndex = 1;

                WorksheetStyle style = book.Styles.Add("HeaderStyle");
                style.Font.FontName = "Tahoma";
                style.Font.Size = 12;
                style.Font.Bold = true;

                style = book.Styles.Add("Top");
                style.Font.Size = 10;
                style.Font.Bold = true;
                style.Alignment.Horizontal = StyleHorizontalAlignment.Center;
                style.Font.Color = "Black";
                style.Interior.Color = "LightGray";
                style.Interior.Pattern = StyleInteriorPattern.Solid;

                style = book.Styles.Add("Money");
                style.Alignment.Horizontal = StyleHorizontalAlignment.Right;

                style = book.Styles.Add("Total");
                style.Alignment.Horizontal = StyleHorizontalAlignment.Right;
                style.Font.Bold = true;

                style = book.Styles.Add("Yellow");
                style.Interior.Color = "Yellow";
                style.Interior.Pattern = StyleInteriorPattern.Solid;

                style = book.Styles.Add("Center");
                style.Alignment.Horizontal = StyleHorizontalAlignment.Center;


                style = book.Styles.Add("Default");
                style.Font.FontName = "Tahoma";
                style.Font.Bold = false;
                style.Font.Size = 10;

                WorksheetStyle dateStyle = book.Styles.Add("DateStyle");
                dateStyle.NumberFormat = "Dd Mmm Yyyy";

                Worksheet sheet = book.Worksheets.Add("Report");
                sheet.Table.Columns.Add(new WorksheetColumn(100));
                sheet.Table.Columns.Add(new WorksheetColumn(80));
                sheet.Table.Columns.Add(new WorksheetColumn(100));
                sheet.Table.Columns.Add(new WorksheetColumn(70));
                sheet.Table.Columns.Add(new WorksheetColumn(70));
                sheet.Table.Columns.Add(new WorksheetColumn(70));
                sheet.Table.Columns.Add(new WorksheetColumn(70));
                sheet.Table.Columns.Add(new WorksheetColumn(70));
                sheet.Table.Columns.Add(new WorksheetColumn(70));
                sheet.Table.Columns.Add(new WorksheetColumn(50));

                WorksheetRow row = sheet.Table.Rows.Add();
                WorksheetCell cellH = row.Cells.Add("Pay By Payment Type Report");
                cellH.MergeAcross = 4;
                cellH.StyleID = "HeaderStyle";

                int j = 2;

                double invoice = 0.00;
                double payment = 0.00;
                double totInvoice = 0.00;
                double totPayment = 0.00;
                double totType = 0.00;

                while (_drInfo.Read())
                {
                    int cVendor = int.Parse(_drInfo[1].ToString());
                    int cMonth = int.Parse(_drInfo[6].ToString());
                    string cType = _drInfo[12].ToString().Trim();
                    if (cType != pType)
                    {
                        if(cType != pType && month!=0)
                        {
                            j++;
                            row = sheet.Table.Rows.Add();
                            row.Index = j;
                            WorksheetCell cell = row.Cells.Add(pType + " Total:");
                            cell.MergeAcross = 3;
                            cell.StyleID = "Total";
                            row.Cells.Add(totInvoice.ToString("c"), DataType.String, "Total");
                            row.Cells.Add(totPayment.ToString("c"), DataType.String, "Total");
                            j++;
                            row = sheet.Table.Rows.Add();
                            row.Index = j;
                            WorksheetCell cellB = row.Cells.Add();
                            cellB.MergeAcross = 9;
                            cellB.StyleID = "Yellow";
                            totInvoice = 0.00;
                            totPayment = 0.00;
                        }
                        j++;
                        row = sheet.Table.Rows.Add();
                        row.Index = j;
                        row.Cells.Add(new WorksheetCell("Installer/Coordinator", "Top"));
                        row.Cells.Add(new WorksheetCell("Order #", "Top"));
                        row.Cells.Add(new WorksheetCell("Customer", "Top"));
                        row.Cells.Add(new WorksheetCell("Install Date", "Top"));
                        row.Cells.Add(new WorksheetCell("Total Invoice", "Top"));
                        row.Cells.Add(new WorksheetCell("Vendor Due", "Top"));
                        row.Cells.Add(new WorksheetCell("Pay Date", "Top"));
                        row.Cells.Add(new WorksheetCell("CGS%", "Top"));
                        row.Cells.Add(new WorksheetCell("Closing Date", "Top"));
                        row.Cells.Add(new WorksheetCell("Pay Type", "Top"));
                        invoice = 0.00;
                        payment = 0.00;
                    }
                    j++;
                    string vendorName = _drInfo[2].ToString();
                    sVendor = _drInfo[2].ToString();
                    //if (invoice != 0) vendorName = "";
                    double cInvoice = double.Parse(_drInfo[7].ToString());
                    double cPayment = double.Parse(_drInfo[8].ToString());
                    string proc = (cPayment / cInvoice).ToString("p");
                    if (_drInfo[4].ToString() == "Addition") cInvoice = 0;
                    if (_drInfo[3].ToString().Substring(0, 1) == "S" || _drInfo[3].ToString().Substring(0, 1) == "F")
                        cInvoice = 0;
                    invoice = invoice + cInvoice;
                    totInvoice = totInvoice + cInvoice;
                    payment = payment + cPayment;
                    totPayment = totPayment + cPayment;
                    row = sheet.Table.Rows.Add();
                    row.Index = j;
                    row.Cells.Add(vendorName);
                    row.Cells.Add(_drInfo[3].ToString());
                    row.Cells.Add(_drInfo[4].ToString());
                    string val = DateTime.Parse(_drInfo[5].ToString()).ToString("s");
                    row.Cells.Add(val, DataType.DateTime, "DateStyle");
                    row.Cells.Add((new WorksheetCell(cInvoice.ToString("c"), "Money")));
                    row.Cells.Add((new WorksheetCell(cPayment.ToString("c"), "Money")));
                    val = DateTime.Parse(_drInfo[9].ToString()).ToString("s");
                    row.Cells.Add(val, DataType.DateTime, "DateStyle");
                    row.Cells.Add(proc, DataType.String, "Money");
                    val = DateTime.Parse(_drInfo[11].ToString()).ToString("s");
                    row.Cells.Add(val, DataType.DateTime, "DateStyle");
                    row.Cells.Add(_drInfo[12].ToString(), DataType.String, "Center");

                    vendor = cVendor;
                    month = cMonth;
                    pType = cType;
                    sMonth = _drInfo[10].ToString().Trim();
                }
                j++;
                row = sheet.Table.Rows.Add();
                row.Index = j;
                WorksheetCell cellTv = row.Cells.Add(pType + " Total:");
                cellTv.MergeAcross = 3;
                cellTv.StyleID = "Total";
                row.Cells.Add(totInvoice.ToString("c"), DataType.String, "Total");
                row.Cells.Add(totPayment.ToString("c"), DataType.String, "Total");
                j++;
                row = sheet.Table.Rows.Add();
                row.Index = j;
                WorksheetCell cellY = row.Cells.Add();
                cellY.MergeAcross = 9;
                cellY.StyleID = "Yellow";

                Response.ContentType = "application/vnd.ms-excel";
                book.Save(Response.OutputStream);
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                lblResult.Text = result;
                lblResult.Visible = true;
            }
        }

        private static int CheckMonth(List<MonthInfo> MonthSet, string MonthName)
        {
            int result = -1;
            for (int i = 0; i < MonthSet.Count; i++)
            {
                if (MonthSet[i].MonthName == MonthName)
                { result = i; }
            }
            return result;
        }

    }
}