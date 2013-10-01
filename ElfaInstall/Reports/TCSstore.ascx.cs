using System;
using System.Data.SqlClient;
using System.Web.UI;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class TCSstore : UserControl
    {
        public int StoreID;
        public DateTime StartDate, EndDate;
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            float qtyProc, amtProc, invProc;
            var orderInfo = new OrderData();
            _drInfo = orderInfo.TCSbyStoreF(StoreID, StartDate, EndDate);
            _drInfo.Read();
            float openAmt = float.Parse(_drInfo["OpenAmt"].ToString());
            float openInv = float.Parse(_drInfo["OpenInv"].ToString());
            float instAmt = float.Parse(_drInfo["InstAmt"].ToString());
            float instInv = float.Parse(_drInfo["InstInv"].ToString());
            //AmtTot = OpenAmt + InstAmt;
            //InvTot = OpenInv + InstInv;
            int openQty = int.Parse(_drInfo["OpenQty"].ToString());
            int instQty = int.Parse(_drInfo["InstQty"].ToString());
            //QTYTot = OpenQty + InstQty;
            int qtyLast = int.Parse(_drInfo["InstQty1"].ToString());
            float amtLast = float.Parse(_drInfo["InstAmt1"].ToString());
            float invLast = float.Parse(_drInfo["InstInv1"].ToString());
            float amtTot = float.Parse(_drInfo["SoldAmt"].ToString());
            float invTot = float.Parse(_drInfo["SoldInv"].ToString());
            int qtyTot = int.Parse(_drInfo["SoldQty"].ToString());
            lblStore.Text = _drInfo["Store"].ToString();
            lblOpenQty.Text = openQty.ToString();
            lblInstQty.Text = instQty.ToString();
            lblTotalQty.Text = qtyTot.ToString();
            lblOpenAmt.Text = openAmt.ToString("c");
            lblInstAmt.Text = instAmt.ToString("c");
            lblTotalAmt.Text = amtTot.ToString("c");
            lblOpenInv.Text = openInv.ToString("c");
            lblInstInv.Text = instInv.ToString("c");
            lblTotalInv.Text = invTot.ToString("c");
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
            if (qtyLast > 0)
            {
                lblQtyProc.Text = qtyProc.ToString("f") + "%";
                if (qtyProc >= 0) lblQtyProc.ForeColor = System.Drawing.Color.Blue;
                else lblQtyProc.ForeColor = System.Drawing.Color.Red;
            }
            else lblQtyProc.Text = "N/A";
            if (amtLast > 0)
            {
                lblAmtProc.Text = amtProc.ToString("f") + "%";
                if (amtProc >= 0) lblAmtProc.ForeColor = System.Drawing.Color.Blue;
                else lblAmtProc.ForeColor = System.Drawing.Color.Red;
            }
            else lblAmtProc.Text = "N/A";
            if (invLast > 0)
            {
                lblInvProc.Text = invProc.ToString("f") + "%";
                if (invProc >= 0) lblInvProc.ForeColor = System.Drawing.Color.Blue;
                else lblInvProc.ForeColor = System.Drawing.Color.Red;
            }
            else lblInvProc.Text = "N/A";
            _drInfo.Close();
        }
    }
}