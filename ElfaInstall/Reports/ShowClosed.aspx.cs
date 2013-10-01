using System;
using System.Configuration;

namespace ElfaInstall.Reports
{
    public partial class ShowClosed : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _dateStart, _dateEnd, _target, _viewCommand;

        protected void Page_Load(object Sender, EventArgs E)
        {
            _target = Request.QueryString["Target"];
            _dateStart = Request.QueryString["DateStart"];
            _dateEnd = Request.QueryString["DateEnd"];
            lblDate1.Text = _dateStart;
            lblDate2.Text = _dateEnd;
            switch (_target)
            {
                case "Store":
                    _viewCommand = "sp_ReportByStore '" + _dateStart + "','" + _dateEnd + "'";
                    grdStores.Visible = true;
                    lblHeader.Text = "Orders by Store Completed&nbsp;&nbsp;from&nbsp;";
                    break;
                case "Market":
                    _viewCommand = "sp_ReportByMarket '" + _dateStart + "','" + _dateEnd + "'";
                    grdMarkets.Visible = true;
                    lblHeader.Text = "Orders by Market Completed&nbsp;&nbsp;from&nbsp;";
                    break;
                case "Vendor":
                    _viewCommand = "sp_ReportByVendor '" + _dateStart + "','" + _dateEnd + "'";
                    grdVendors.Visible = true;
                    lblHeader.Text = "Orders by Install. Completed&nbsp;&nbsp;from&nbsp;";
                    break;
                default:
                    _viewCommand = "";
                    break;
            }
            //if (_viewCommand == "")
            //    Response.Redirect("~/Reports/ReportCompleted.aspx");
            BindDataGrid();
        }
        void BindDataGrid()
        {
            sdsOrders.ConnectionString = ConString;
            sdsOrders.SelectCommand = _viewCommand;
        }
    }
}