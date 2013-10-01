using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class YTDStore : System.Web.UI.UserControl
    {
        public int StoreID;
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            float qtyProc, amtProc, invProc;
            float qtyProc1, amtProc1, invProc1;
            var orderInfo = new OrderData();
            _drInfo = orderInfo.YTDbyStore(StoreID);
            _drInfo.Read();
            float openAmt = float.Parse(_drInfo["OpenAmt"].ToString());
            float openInv = float.Parse(_drInfo["OpenInv"].ToString());
            float instAmt = float.Parse(_drInfo["InstAmt"].ToString());
            float instInv = float.Parse(_drInfo["InstInv"].ToString());
            float soldAmt = float.Parse(_drInfo["SoldAmt"].ToString());
            float soldInv = float.Parse(_drInfo["SoldInv"].ToString());
            float amtTot = openAmt + instAmt;
            float invTot = openInv + instInv;
            int openQty = int.Parse(_drInfo["OpenQty"].ToString());
            int instQty = int.Parse(_drInfo["InstQty"].ToString());
            int soldQty = int.Parse(_drInfo["SoldQty"].ToString());
            int qtyTot = openQty + instQty;
            int qtyLast = int.Parse(_drInfo["InstQty1"].ToString());
            float amtLast = float.Parse(_drInfo["InstAmt1"].ToString());
            float invLast = float.Parse(_drInfo["InstInv1"].ToString());
            int qtyLast1 = int.Parse(_drInfo["SoldQty1"].ToString());
            float amtLast1 = float.Parse(_drInfo["SoldAmt1"].ToString());
            float invLast1 = float.Parse(_drInfo["SoldInv1"].ToString());
            lblStore.Text = _drInfo["Store"].ToString();
            lblOpenQty.Text = openQty.ToString();
            lblInstQty.Text = instQty.ToString();
            lblTotalQty.Text = soldQty.ToString();
            lblOpenAmt.Text = openAmt.ToString("c");
            lblInstAmt.Text = instAmt.ToString("c");
            lblTotalAmt.Text = soldAmt.ToString("c");
            lblOpenInv.Text = openInv.ToString("c");
            lblInstInv.Text = instInv.ToString("c");
            lblTotalInv.Text = soldInv.ToString("c");
            lblLastQty.Text = qtyLast.ToString();
            lblLastAmt.Text = amtLast.ToString("c");
            lblLastInv.Text = invLast.ToString("c");
            lblTotalQty1.Text = qtyLast1.ToString();
            lblTotalAmt1.Text = amtLast1.ToString("c");
            lblTotalInv1.Text = invLast1.ToString("c");
            if (qtyLast > 0)
                qtyProc = (instQty - qtyLast) * 100 / qtyLast;
            else qtyProc = 100;
            if (amtLast > 0)
                amtProc = (instAmt - amtLast) * 100 / amtLast;
            else amtProc = 100;
            if (invLast > 0)
                invProc = (instInv - invLast) * 100 / invLast;
            else invProc = 100;
            if (qtyLast1 > 0)
                qtyProc1 = (soldQty - qtyLast1) * 100 / qtyLast1;
            else qtyProc1 = 100;
            if (amtLast1 > 0)
                amtProc1 = (soldAmt - amtLast1) * 100 / amtLast1;
            else amtProc1 = 100;
            if (invLast1 > 0)
                invProc1 = (soldInv - invLast1) * 100 / invLast1;
            else invProc1 = 100;
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
            if (qtyLast1 > 0)
            {
                lblQtyProc1.Text = qtyProc1.ToString("f") + "%";
                if (qtyProc1 >= 0) lblQtyProc1.ForeColor = System.Drawing.Color.Blue;
                else lblQtyProc1.ForeColor = System.Drawing.Color.Red;
            }
            else lblQtyProc1.Text = "N/A";
            if (amtLast1 > 0)
            {
                lblAmtProc1.Text = amtProc1.ToString("f") + "%";
                if (amtProc1 >= 0) lblAmtProc1.ForeColor = System.Drawing.Color.Blue;
                else lblAmtProc1.ForeColor = System.Drawing.Color.Red;
            }
            else lblAmtProc1.Text = "N/A";
            if (invLast1 > 0)
            {
                lblInvProc1.Text = invProc1.ToString("f") + "%";
                if (invProc1 >= 0) lblInvProc1.ForeColor = System.Drawing.Color.Blue;
                else lblInvProc1.ForeColor = System.Drawing.Color.Red;
            }
            else lblInvProc1.Text = "N/A";
            _drInfo.Close();
        }
    }
}