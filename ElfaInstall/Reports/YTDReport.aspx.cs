using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class YTDReport : Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _connElfa = new SqlConnection(ConString);
        YTDStore _ordersInfo = new YTDStore();
        int _storeID;
        SqlDataReader _drStores, _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            var cmdSelect = new SqlCommand();
            cmdSelect.Connection = _connElfa;
            cmdSelect.CommandType = CommandType.Text;
            cmdSelect.CommandText = "SELECT StoreID FROM tblStores";
            _connElfa.Open();
            _drStores = cmdSelect.ExecuteReader();
            while (_drStores.Read())
            {
                _storeID = int.Parse(_drStores["StoreID"].ToString());
                var tR1 = new TableRow();
                var tC1 = new TableCell();
                _ordersInfo = (YTDStore)LoadControl("~/Reports/YTDstore.ascx");
                _ordersInfo.StoreID = _storeID;
                tC1.Controls.Add(_ordersInfo);
                tR1.Cells.Add(tC1);
                tblMain.Rows.Add(tR1);
                _ordersInfo = null;
            }
            _drStores.Close();
            _connElfa.Close();
            ShowYTD();
        }
        void ShowYTD()
        {
            float qtyProc, amtProc, invProc, qtyProc2, amtProc2, invProc2;
            var ytd = new OrderData();
            _drInfo = ytd.ShowYTDvalues();
            _drInfo.Read();
            float openAmt = float.Parse(_drInfo["OpenAmt"].ToString());
            float openInv = float.Parse(_drInfo["OpenInv"].ToString());
            float instAmt = float.Parse(_drInfo["InstAmt"].ToString());
            float instInv = float.Parse(_drInfo["InstInv"].ToString());
            int openQty = int.Parse(_drInfo["OpenQty"].ToString());
            int instQty = int.Parse(_drInfo["InstQty"].ToString());
            int qtyLast = int.Parse(_drInfo["InstQty1"].ToString());
            float amtLast = float.Parse(_drInfo["InstAmt1"].ToString());
            float invLast = float.Parse(_drInfo["InstInv1"].ToString());
            int qtyTot = int.Parse(_drInfo["OrdersQty"].ToString());
            float amtTot = float.Parse(_drInfo["OrdersAmt"].ToString());
            float invTot = float.Parse(_drInfo["OrdersInv"].ToString());
            int qtyLast2 = int.Parse(_drInfo["OrdersQty1"].ToString());
            float amtLast2 = float.Parse(_drInfo["OrdersAmt1"].ToString());
            float invLast2 = float.Parse(_drInfo["OrdersInv1"].ToString());
            lblOpenQty.Text = openQty.ToString();
            lblOpenAmt.Text = openAmt.ToString("c");
            lblOpenInv.Text = openInv.ToString("c");
            lblInstQty.Text = instQty.ToString();
            lblInstAmt.Text = instAmt.ToString("c");
            lblInstInv.Text = instInv.ToString("c");
            lblTotalQty.Text = qtyTot.ToString();
            lblTotalAmt.Text = amtTot.ToString("c");
            lblTotalInv.Text = invTot.ToString("c");
            lblLastQty.Text = qtyLast.ToString();
            lblLastAmt.Text = amtLast.ToString("c");
            lblLastInv.Text = invLast.ToString("c");
            lblLastQty1.Text = qtyLast2.ToString();
            lblLastAmt1.Text = amtLast2.ToString("c");
            lblLastInv1.Text = invLast2.ToString("c");
            if (qtyLast > 0)
                qtyProc = (instQty - qtyLast) * 100 / qtyLast;
            else qtyProc = 100;
            if (amtLast > 0)
                amtProc = (instAmt - amtLast) * 100 / amtLast;
            else amtProc = 100;
            if (invLast > 0)
                invProc = (instInv - invLast) * 100 / invLast;
            else invProc = 100;
            if (qtyLast2 > 0)
                qtyProc2 = (qtyTot - qtyLast2) * 100 / qtyLast2;
            else qtyProc2 = 100;
            if (amtLast2 > 0)
                amtProc2 = (amtTot - amtLast2) * 100 / amtLast2;
            else amtProc2 = 100;
            if (invLast2 > 0)
                invProc2 = (invTot - invLast2) * 100 / invLast2;
            else invProc2 = 100;
            lblQtyProc.Text = qtyProc.ToString("f") + "%";
            if (qtyProc >= 0) lblQtyProc.ForeColor = System.Drawing.Color.Blue;
            else lblQtyProc.ForeColor = System.Drawing.Color.Red;
            lblAmtProc.Text = amtProc.ToString("f") + "%";
            if (amtProc >= 0) lblAmtProc.ForeColor = System.Drawing.Color.Blue;
            else lblAmtProc.ForeColor = System.Drawing.Color.Red;
            lblInvProc.Text = invProc.ToString("f") + "%";
            if (invProc >= 0) lblInvProc.ForeColor = System.Drawing.Color.Blue;
            else lblInvProc.ForeColor = System.Drawing.Color.Red;

            lblQtyProc1.Text = qtyProc2.ToString("f") + "%";
            if (qtyProc2 >= 0) lblQtyProc1.ForeColor = System.Drawing.Color.Blue;
            else lblQtyProc1.ForeColor = System.Drawing.Color.Red;
            lblAmtProc1.Text = amtProc2.ToString("f") + "%";
            if (amtProc2 >= 0) lblAmtProc1.ForeColor = System.Drawing.Color.Blue;
            else lblAmtProc1.ForeColor = System.Drawing.Color.Red;
            lblInvProc1.Text = invProc2.ToString("f") + "%";
            if (invProc2 >= 0) lblInvProc1.ForeColor = System.Drawing.Color.Blue;
            else lblInvProc1.ForeColor = System.Drawing.Color.Red;
            _drInfo.Close();
        }
    }
}