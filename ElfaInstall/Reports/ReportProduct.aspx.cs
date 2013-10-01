using System;
using System.Configuration;
using System.Web.UI;

namespace ElfaInstall.Reports
{
    public partial class ReportProduct : Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _dateStart, _dateEnd;

        protected void Page_Load(object Sender, EventArgs E)
        {
            _dateStart = Request.QueryString["DateStart"];
            _dateEnd = Request.QueryString["DateEnd"];
            lblDate1.Text = _dateStart;
            lblDate2.Text = _dateEnd;
            lblDate11.Text = _dateStart;
            lblDate21.Text = _dateEnd;
            lblDate12.Text = _dateStart;
            lblDate22.Text = _dateEnd;
            BindDataGrid();
        }
        void BindDataGrid()
        {
            sdsMissing.ConnectionString = ConString;
            string command = "EXEC sp_GetMissingProduct '" + _dateStart + "','" + _dateEnd + "'";
            sdsMissing.SelectCommand = command;
            sdsDamaged.ConnectionString = ConString;
            command = "EXEC sp_GetDamagedProduct '" + _dateStart + "','" + _dateEnd + "'";
            sdsDamaged.SelectCommand = command;
            sdsFlaw.ConnectionString = ConString;
            command = "EXEC sp_GetFlawProduct '" + _dateStart + "','" + _dateEnd + "'";
            sdsFlaw.SelectCommand = command;
            int counter = grdFlaw.Rows.Count;
            if (counter == 0)
            {
                lblEmpty.Visible = true;
            }
        }

        protected void DdlReportSelectedIndexChanged(object Sender, EventArgs E)
        {
            if(ddlReport.SelectedValue=="1")
            {
                tblMissing.Visible = true;
                grdMissing.Visible = true;
                tblDamaged.Visible = false;
                grdDamaged.Visible = false;
                tblFlaw.Visible = false;
                grdFlaw.Visible = false;
            }
            if(ddlReport.SelectedValue=="2")
            {
                tblMissing.Visible = false;
                grdMissing.Visible = false;
                tblDamaged.Visible = true;
                grdDamaged.Visible = true;
                tblFlaw.Visible = false;
                grdFlaw.Visible = false;
            }
            if(ddlReport.SelectedValue=="3")
            {
                tblMissing.Visible = false;
                grdMissing.Visible = false;
                tblDamaged.Visible = false;
                grdDamaged.Visible = false;
                tblFlaw.Visible = true;
                grdFlaw.Visible = true;
            }
        }
    }
}