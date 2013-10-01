using System;
using System.Data.SqlClient;
using System.Web.UI;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class TCSSummary : Page
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
                FillDateList();
            }
        }
        void FillDateList()
        {
            var dateList = new OrderData();
            _drInfo = dateList.FiscalDates();
            ddlStart.DataSource = _drInfo;
            ddlStart.DataValueField = "StartDate";
            ddlStart.DataTextField = "StartDate";
            ddlStart.DataBind();
            _drInfo.Close();
            _drInfo = dateList.FiscalDates();
            ddlEnd.DataSource = _drInfo;
            ddlEnd.DataValueField = "EndDate";
            ddlEnd.DataTextField = "EndDate";
            ddlEnd.DataBind();
            _drInfo.Close();
        }
        void ShowYTD(DateTime Start, DateTime End)
        {
            var ytd = new OrderData();
            float qtyProc, amtProc, invProc;
            _drInfo = ytd.ShowFiscalByDate(Start, End);
            _drInfo.Read();
            float openAmt = float.Parse(_drInfo["OpenAmt"].ToString());
            float openInv = float.Parse(_drInfo["OpenInv"].ToString());
            float instAmt = float.Parse(_drInfo["InstAmt"].ToString());
            float instInv = float.Parse(_drInfo["InstInv"].ToString());
            float soldAmt = float.Parse(_drInfo["SoldAmt"].ToString());
            float soldInv = float.Parse(_drInfo["SoldInv"].ToString());
            //float amtTot = openAmt + instAmt;
            //float invTot = openInv + instInv;
            int openQty = int.Parse(_drInfo["OpenQty"].ToString());
            int instQty = int.Parse(_drInfo["InstQty"].ToString());
            int soldQty = int.Parse(_drInfo["SoldQty"].ToString());
            //int qtyTot = openQty + instQty;
            int qtyLast = int.Parse(_drInfo["InstQty1"].ToString());
            float amtLast = float.Parse(_drInfo["InstAmt1"].ToString());
            float invLast = float.Parse(_drInfo["InstInv1"].ToString());
            int qtyLast2 = int.Parse(_drInfo["InstQty2"].ToString());
            float amtLast2 = float.Parse(_drInfo["InstAmt2"].ToString());
            float invLast2 = float.Parse(_drInfo["InstInv2"].ToString());
            lblOpenQty.Text = openQty.ToString();
            lblOpenAmt.Text = openAmt.ToString("c");
            lblOpenInv.Text = openInv.ToString("c");
            lblInstQty.Text = instQty.ToString();
            lblInstAmt.Text = instAmt.ToString("c");
            lblInstInv.Text = instInv.ToString("c");
            lblTotQty.Text = soldQty.ToString();
            lblTotAmt.Text = soldAmt.ToString("c");
            lblTotInv.Text = soldInv.ToString("c");
            lblQTYlast.Text = qtyLast.ToString();
            lblAMTlast.Text = amtLast.ToString("c");
            lblINVlast.Text = invLast.ToString("c");
            lblQTYlast2.Text = qtyLast2.ToString();
            lblAMTlast2.Text = amtLast2.ToString("c");
            lblINVlast2.Text = invLast2.ToString("c");
            if (qtyLast > 0)
            { qtyProc = (instQty - qtyLast) * 100 / qtyLast; }
            else qtyProc = 0;
            if (amtLast > 0)
            { amtProc = (instAmt - amtLast) * 100 / amtLast; }
            else amtProc = 0;
            if (invLast > 0)
            { invProc = (instInv - invLast) * 100 / invLast; }
            else invProc = 0;
            lblQTYproc.Text = qtyProc.ToString("f") + "%";
            if (qtyProc >= 0) lblQTYproc.ForeColor = System.Drawing.Color.Blue;
            else lblQTYproc.ForeColor = System.Drawing.Color.Red;
            lblAMTproc.Text = amtProc.ToString("f") + "%";
            if (amtProc >= 0) lblAMTproc.ForeColor = System.Drawing.Color.Blue;
            else lblAMTproc.ForeColor = System.Drawing.Color.Red;
            lblINVproc.Text = invProc.ToString("f") + "%";
            if (invProc >= 0) lblINVproc.ForeColor = System.Drawing.Color.Blue;
            else lblINVproc.ForeColor = System.Drawing.Color.Red;
            lblYear11.Text = _drInfo["LastYear"].ToString();
            lblYear12.Text = _drInfo["LastYear"].ToString();
            lbYear21.Text = _drInfo["Last2Year"].ToString();
            _drInfo.Close();
        }
        protected void BtnCalculateClick(object Sender, EventArgs E)
        {
            ShowYTD(DateTime.Parse(ddlStart.SelectedValue), DateTime.Parse(ddlEnd.SelectedValue));
            hlByStore.NavigateUrl = "~/Reports/TCSbyStore.aspx?Start=" + DateTime.Parse(ddlStart.SelectedValue).ToShortDateString() + "&End=" + DateTime.Parse(ddlEnd.SelectedValue).ToShortDateString();
            hlByStore.Visible = true;
            lblByStore.Visible = true;
        }
    }
}