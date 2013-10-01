using System;
using System.Data.SqlClient;
using System.Web.UI;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class YTDSumm : Page
    {
        int _userID;
        string _status;
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status != "Admin") Response.Redirect("Login.aspx");
                ShowYTD();
            }
        }
        void ShowYTD()
        {
            int currYear = DateTime.Today.Year;
            int nYear1 = currYear - 1;
            int nYear2 = currYear - 2;
            var ytd = new OrderData();
            _drInfo = ytd.ShowYTDvalues();
            _drInfo.Read();
            float openAmt = float.Parse(_drInfo["OpenAmt"].ToString());
            float openInv = float.Parse(_drInfo["OpenInv"].ToString());
            float instAmt = float.Parse(_drInfo["InstAmt"].ToString());
            float instInv = float.Parse(_drInfo["InstInv"].ToString());
            //float amtTot = openAmt + instAmt;
            //float invTot = openInv + instInv;
            int openQty = int.Parse(_drInfo["OpenQty"].ToString());
            int instQty = int.Parse(_drInfo["InstQty"].ToString());
            //int qtyTot = openQty + instQty;
            int qtyLast = int.Parse(_drInfo["InstQty1"].ToString());
            float amtLast = float.Parse(_drInfo["InstAmt1"].ToString());
            float invLast = float.Parse(_drInfo["InstInv1"].ToString());
            int qtyLast2 = int.Parse(_drInfo["InstQty2"].ToString());
            float amtLast2 = float.Parse(_drInfo["InstAmt2"].ToString());
            float invLast2 = float.Parse(_drInfo["InstInv2"].ToString());
            int ordersQty = int.Parse(_drInfo["OrdersQty"].ToString());
            int ordersQty1 = int.Parse(_drInfo["OrdersQty1"].ToString());
            int ordersQty2 = int.Parse(_drInfo["OrdersQty2"].ToString());
            float ordersAmt = float.Parse(_drInfo["OrdersAmt"].ToString());
            float ordersInv = float.Parse(_drInfo["OrdersInv"].ToString());
            float ordersAmt1 = float.Parse(_drInfo["OrdersAmt1"].ToString());
            float ordersInv1 = float.Parse(_drInfo["OrdersInv1"].ToString());
            float ordersAmt2 = float.Parse(_drInfo["OrdersAmt2"].ToString());
            float ordersInv2 = float.Parse(_drInfo["OrdersInv2"].ToString());
            _drInfo.Close();
            lblOpenQty.Text = openQty.ToString();
            lblOpenQtya.Text = openQty.ToString();
            lblOpenAmt.Text = openAmt.ToString("c");
            lblOpenAmta.Text = openAmt.ToString("c");
            lblOpenInv.Text = openInv.ToString("c");
            lblOpenInva.Text = openInv.ToString("c");
            lblInstQty.Text = instQty.ToString();
            lblInstAmt.Text = instAmt.ToString("c");
            lblInstInv.Text = instInv.ToString("c");
            lblTotQty.Text = ordersQty.ToString();
            lblTotAmt.Text = ordersAmt.ToString("c");
            lblTotInv.Text = ordersInv.ToString("c");
            lblQTYlast.Text = ordersQty1.ToString();
            lblAMTlast.Text = ordersAmt1.ToString("c");
            lblINVlast.Text = ordersInv1.ToString("c");
            lblQTYlast2.Text = ordersQty2.ToString();
            lblAMTlast2.Text = ordersAmt2.ToString("c");
            lblINVlast2.Text = ordersInv2.ToString("c");
            double qtyProc = (ordersQty - ordersQty1) * 100.0 / ordersQty1;
            float amtProc = (ordersAmt - ordersAmt1) * 100 / ordersAmt1;
            float invProc = (ordersInv - ordersInv1) * 100 / ordersInv1;
            lblQTYproc.Text = qtyProc.ToString("f") + "%";
            if (qtyProc >= 0) lblQTYproc.ForeColor = System.Drawing.Color.Blue;
            else lblQTYproc.ForeColor = System.Drawing.Color.Red;
            lblAMTproc.Text = amtProc.ToString("f") + "%";
            if (amtProc >= 0) lblAMTproc.ForeColor = System.Drawing.Color.Blue;
            else lblAMTproc.ForeColor = System.Drawing.Color.Red;
            lblINVproc.Text = invProc.ToString("f") + "%";
            if (invProc >= 0) lblINVproc.ForeColor = System.Drawing.Color.Blue;
            else lblINVproc.ForeColor = System.Drawing.Color.Red;
            lblInstLastQty.Text = qtyLast.ToString();
            lblInstLastAmt.Text = amtLast.ToString("c");
            lblInstLastInv.Text = invLast.ToString("c");
            qtyProc = (instQty - qtyLast) * 100.0 / qtyLast;
            amtProc = (instAmt - amtLast) * 100 / amtLast;
            invProc = (instInv - invLast) * 100 / invLast;
            lblInstLastQtyProc.Text = qtyProc.ToString("f") + "%";
            if (qtyProc >= 0) lblInstLastQtyProc.ForeColor = System.Drawing.Color.Blue;
            else lblInstLastQtyProc.ForeColor = System.Drawing.Color.Red;
            lblInstLastAmtProc.Text = amtProc.ToString("f") + "%";
            if (qtyProc >= 0) lblInstLastAmtProc.ForeColor = System.Drawing.Color.Blue;
            else lblInstLastAmtProc.ForeColor = System.Drawing.Color.Red;
            lblInstLastInvProc.Text = invProc.ToString("f") + "%";
            if (qtyProc >= 0) lblInstLastInvProc.ForeColor = System.Drawing.Color.Blue;
            else lblInstLastInvProc.ForeColor = System.Drawing.Color.Red;
            lblInstLast2Qty.Text = qtyLast2.ToString();
            lblInstLast2Amt.Text = amtLast2.ToString("c");
            lblInstLast2Inv.Text = invLast2.ToString("c");
            lblYear11.Text = nYear1.ToString();
            lblYear12.Text = nYear1.ToString();
            lblYear21.Text = nYear2.ToString();
            lblYear11a.Text = nYear1.ToString();
            lblYear12a.Text = nYear1.ToString();
            lblYear21a.Text = nYear2.ToString();
        }
    }
}