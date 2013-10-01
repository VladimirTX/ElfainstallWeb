using System;
using System.Configuration;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ElfaInstall.Reports
{
    public partial class ShowCreated : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _dateStart, _dateEnd, _target, _viewCommand;

        protected void Page_Load(object Sender, EventArgs E)
        {
            _target = ((DropDownList)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("ddlSelection")).Text;
            _dateStart =
                ((RadDatePicker)
                 PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("rdpDateStart"))
                    .SelectedDate.ToString();
            _dateEnd =
                ((RadDatePicker)
                 PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("rdpDateEnd")).
                    SelectedDate.ToString();
            lblDate1.Text = _dateStart;
            lblDate2.Text = _dateEnd;
            switch (_target)
            {
                case "Store":
                    _viewCommand = "sp_CreatedByStore '" + _dateStart + "','" + _dateEnd + "'";
                    grdStores.Visible = true;
                    lblHeader.Text = "Orders by Store Created&nbsp;&nbsp;from&nbsp;";
                    break;
                case "Market":
                    _viewCommand = "sp_CreatedByMarket '" + _dateStart + "','" + _dateEnd + "'";
                    grdMarkets.Visible = true;
                    lblHeader.Text = "Orders by Market Created&nbsp;&nbsp;from&nbsp;";
                    break;
                case "Vendor":
                    _viewCommand = "sp_CreatedByVendor '" + _dateStart + "','" + _dateEnd + "'";
                    grdVendors.Visible = true;
                    lblHeader.Text = "Orders by Vendor Created&nbsp;&nbsp;from&nbsp;";
                    break;
                default:
                    Response.Redirect("Report_Created.aspx");
                    break;
            }
            BindDataGrid();
        }
        void BindDataGrid()
        {
            sdsOrders.ConnectionString = ConString;
            sdsOrders.SelectCommand = _viewCommand;
        }
    }
}