using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;

namespace ElfaInstall.Classes
{
    public class CreateExcel
    {
        public string PayReport(SqlDataReader DrInfo, string FileName)
        {
            string result = "";
            var monthInfo = new List<MonthInfo>();
            if (DrInfo.HasRows)
            {
                int vendor = 0;
                int month = 0;
                string sMonth = "";
                string sVendor = "";
                try
                {
                    Range oRng;
                    var oXl = new Application();
                    oXl.Visible = false;
                    var oWB = (_Workbook)(oXl.Workbooks.Add(XlWBATemplate.xlWBATWorksheet));
                    var oSheet = (_Worksheet)oWB.ActiveSheet;
                    oSheet.Range["A1", "H1"].Merge(null);
                    oSheet.Cells[1, 1] = "Weekly Installation Log Pay Report";
                    oSheet.Range["A1", "A1"].Font.Bold = true;
                    oSheet.Range["A1", "A1"].Font.Size = 12;
                    double invoice = 0.00;
                    double payment = 0.00;
                    double totInvoice = 0.00;
                    double totPayment = 0.00;
                    int j = 2;
                    while (DrInfo.Read())
                    {
                        int cVendor = int.Parse(DrInfo[1].ToString());
                        int cMonth = int.Parse(DrInfo[6].ToString());
                        if (month != cMonth || vendor != cVendor)
                        {
                            if (vendor != 0)
                            {
                                j++;
                                oRng = oSheet.Range["A" + j, "D" + j];
                                oRng.Cells.Merge(null);
                                oRng.HorizontalAlignment = XlHAlign.xlHAlignRight;
                                oRng.Font.Bold = true;
                                oSheet.Cells[j, 1] = sMonth + " Total:";
                                oSheet.Range["E" + j, "F" + j].Font.Bold = true;
                                oSheet.Cells[j, 5] = invoice.ToString("c");
                                oSheet.Cells[j, 6] = payment.ToString("c");
                            }
                            if ((vendor != cVendor) && (vendor != 0))
                            {
                                j++;
                                oRng = oSheet.Range["A" + j, "D" + j];
                                oRng.Cells.Merge(null);
                                oRng.HorizontalAlignment = XlHAlign.xlHAlignRight;
                                oRng.Font.Bold = true;
                                oSheet.Cells[j, 1] = sVendor + " Total:";
                                oSheet.Range["E" + j, "F" + j].Font.Bold = true;
                                oSheet.Cells[j, 5] = totInvoice.ToString("c");
                                oSheet.Cells[j, 6] = totPayment.ToString("c");
                                j++;
                                oRng = oSheet.Range["A" + j, "H" + j];
                                oRng.Cells.Merge(null);
                                oRng.Interior.ColorIndex = 22;
                                totInvoice = 0.00;
                                totPayment = 0.00;
                            }
                            j++;
                            oRng = oSheet.Range["A" + j, "H" + j];
                            oRng.Font.Bold = true;
                            oRng.Interior.ColorIndex = 15;
                            oSheet.Cells[j, 1] = "Installer";
                            oSheet.Cells[j, 2] = "Order #";
                            oSheet.Cells[j, 3] = "Customer";
                            oSheet.Cells[j, 4] = "Install Date";
                            oSheet.Cells[j, 5] = "Total Invoice";
                            oSheet.Cells[j, 6] = "Vendor Due";
                            oSheet.Cells[j, 7] = "Vendor Paid Day";
                            oSheet.Cells[j, 8] = "CGS %";
                            invoice = 0.00;
                            payment = 0.00;
                        }
                        j++;
                        string vendorName = DrInfo[2].ToString();
                        sVendor = DrInfo[2].ToString();
                        if (invoice != 0) vendorName = "";
                        double cInvoice = double.Parse(DrInfo[7].ToString());
                        double cPayment = double.Parse(DrInfo[8].ToString());
                        invoice = invoice + cInvoice;
                        payment = payment + cPayment;
                        totInvoice = totInvoice + cInvoice;
                        totPayment = totPayment + cPayment;
                        oSheet.Cells[j, 1] = vendorName;
                        oSheet.Cells[j, 2] = DrInfo[3].ToString();
                        oSheet.Cells[j, 3] = DrInfo[4].ToString();
                        oSheet.Cells[j, 4] = DateTime.Parse(DrInfo[5].ToString()).ToShortDateString();
                        oSheet.Cells[j, 5] = cInvoice.ToString("c");
                        oSheet.Cells[j, 6] = cPayment.ToString("c");
                        oSheet.Cells[j, 7] = DateTime.Parse(DrInfo[9].ToString()).ToShortDateString();
                        oSheet.Cells[j, 8] = (cPayment / cInvoice).ToString("p");

                        vendor = cVendor;
                        month = cMonth;
                        sMonth = DrInfo[10].ToString().Trim();
                        int index = -1;
                        if (monthInfo.Count > 0)
                        {
                            index = CheckMonth(monthInfo, sMonth);
                        }
                        if (index >= 0)
                        {
                            monthInfo[index].Invoice = monthInfo[index].Invoice + cInvoice;
                            monthInfo[index].Payment = monthInfo[index].Payment + cPayment;
                        }
                        else
                        {
                            var newMonth = new MonthInfo();
                            newMonth.MonthName = sMonth;
                            newMonth.Invoice = cInvoice;
                            newMonth.Payment = cPayment;
                            monthInfo.Add(newMonth);
                        }
                    }
                    j++;
                    oRng = oSheet.Range["A" + j, "D" + j];
                    oRng.Cells.Merge(null);
                    oRng.HorizontalAlignment = XlHAlign.xlHAlignRight;
                    oRng.Font.Bold = true;
                    oSheet.Cells[j, 1] = sMonth + "Total:";
                    oSheet.Range["E" + j, "F" + j].Font.Bold = true;
                    oSheet.Cells[j, 5] = invoice.ToString("c");
                    oSheet.Cells[j, 6] = payment.ToString("c");
                    j++;
                    oRng = oSheet.Range["A" + j, "D" + j];
                    oRng.Cells.Merge(null);
                    oRng.HorizontalAlignment = XlHAlign.xlHAlignRight;
                    oRng.Font.Bold = true;
                    oSheet.Cells[j, 1] = sVendor + " Total:";
                    oSheet.Range["E" + j, "F" + j].Font.Bold = true;
                    oSheet.Cells[j, 5] = totInvoice.ToString("c");
                    oSheet.Cells[j, 6] = totPayment.ToString("c");
                    j++;
                    oRng = oSheet.Range["A" + j, "H" + j];
                    oRng.Cells.Merge(null);
                    oRng.Interior.ColorIndex = 22;
                    j++;

                    foreach (var curMonth in monthInfo)
                    {
                        j++;
                        oRng = oSheet.Range["A" + j, "D" + j];
                        oRng.Cells.Merge(null);
                        oRng.HorizontalAlignment = XlHAlign.xlHAlignRight;
                        oRng.Font.Bold = true;
                        oSheet.Cells[j, 1] = "Total for " + curMonth.MonthName;
                        oSheet.Cells[j, 5] = curMonth.Invoice.ToString("c");
                        oSheet.Cells[j, 6] = curMonth.Payment.ToString("c");
                    }


                    oRng = oSheet.Range["A1", "H1"];
                    oRng.EntireColumn.AutoFit();
                    oWB.SaveAs(FileName, XlFileFormat.xlWorkbookNormal,
                                null, null, false, false, XlSaveAsAccessMode.xlShared,
                                false, false, null, null, null);
                    oWB.Close(null, null, null);
                    oXl.Workbooks.Close();
                    oXl.Quit();
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(oRng);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oXl);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oWB);
                    GC.Collect(); // force final cleanup!
                    result = "Success";
                }
                catch (Exception ex)
                { result = ex.Message; }
            }
            DrInfo.Close();

            return result;
        }
        public int CheckMonth(List<MonthInfo> MonthSet, string MonthName)
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