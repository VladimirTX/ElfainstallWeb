using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class TCSbyStore : Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _connElfa = new SqlConnection(ConString);
        TCSstore _ordersInfo = new TCSstore();
        //Fiscal.YTDfiscal OrdersInfo = new Fiscal.YTDfiscal();
        int _storeID;
        SqlDataReader _drStores, _drInfo;
        DateTime _dateStart, _dateEnd;
        protected void Page_Load(object Sender, EventArgs E)
        {
            _dateStart = DateTime.Parse(Request.QueryString["Start"]);
            _dateEnd = DateTime.Parse(Request.QueryString["End"]);
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
                _ordersInfo = (TCSstore)LoadControl("~/Reports/TCSstore.ascx");
                _ordersInfo.StoreID = _storeID;
                _ordersInfo.StartDate = _dateStart;
                _ordersInfo.EndDate = _dateEnd;
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
            float qtyProc, amtProc, invProc;
            var ytd = new OrderData();
            _drInfo = ytd.ShowFiscalByDate(_dateStart, _dateEnd);
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
            float soldAmt = float.Parse(_drInfo["SoldAmt"].ToString());
            float soldInv = float.Parse(_drInfo["SoldInv"].ToString());
            int soldQty = int.Parse(_drInfo["SoldQty"].ToString());
            //int qtyTot = openQty + instQty;
            //float amtTot = openAmt + instAmt;
            //float invTot = openInv + instInv;
            lblOpenQty.Text = openQty.ToString();
            lblOpenAmt.Text = openAmt.ToString("c");
            lblOpenInv.Text = openInv.ToString("c");
            lblInstQty.Text = instQty.ToString();
            lblInstAmt.Text = instAmt.ToString("c");
            lblInstInv.Text = instInv.ToString("c");
            lblTotalQty.Text = soldQty.ToString();
            lblTotalAmt.Text = soldAmt.ToString("c");
            lblTotalInv.Text = soldInv.ToString("c");
            lblLastQty.Text = qtyLast.ToString();
            lblLastAmt.Text = amtLast.ToString("c");
            lblLastInv.Text = invLast.ToString("c");
            if (qtyLast > 0)
                qtyProc = (instQty - qtyLast) * 100 / qtyLast;
            else qtyProc = 100;
            if (amtLast > 0)
                amtProc = (instAmt - amtLast) * 100 / amtLast;
            else amtProc = 100;
            if (invLast > 0)
                invProc = (instInv - invLast) * 100 / invLast;
            else invProc = 100;
            lblQtyProc.Text = qtyProc.ToString("f") + "%";
            if (qtyProc >= 0) lblQtyProc.ForeColor = System.Drawing.Color.Blue;
            else lblQtyProc.ForeColor = System.Drawing.Color.Red;
            lblAmtProc.Text = amtProc.ToString("f") + "%";
            if (amtProc >= 0) lblAmtProc.ForeColor = System.Drawing.Color.Blue;
            else lblAmtProc.ForeColor = System.Drawing.Color.Red;
            lblInvProc.Text = invProc.ToString("f") + "%";
            if (invProc >= 0) lblInvProc.ForeColor = System.Drawing.Color.Blue;
            else lblInvProc.ForeColor = System.Drawing.Color.Red;
            lblFiscal.Text = "(" + DateTime.Parse(_drInfo["StartDate"].ToString()).ToShortDateString() + " - " + DateTime.Parse(_drInfo["EndDate"].ToString()).ToShortDateString() + ")";
            _drInfo.Close();
        }
    }
}