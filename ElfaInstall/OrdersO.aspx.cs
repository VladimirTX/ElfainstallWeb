using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class OrdersO : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _status;
        int _userID, _organizerID;
        string _param = "";

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
                if (_status == "Organizer")
                {
                    _organizerID = checkLog.GetOrganizerID(_userID);
                    if (_organizerID == 0)
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                //if (PreviousPage != null && PreviousPage.Title == "Region Orders Search")
                //{
                //    _param = ((HiddenField)PreviousPage.Master.FindControl("form1").FindControl("ContentPlaceHolder1").FindControl("hfParameter")).Value.Trim();
                //    hfParameter.Value = _param;
                //}
                //else { hfParameter.Value = ""; }
                Response.Cookies["OrganizerID"].Value = _organizerID.ToString();
                hfOrganizerID.Value = _organizerID.ToString();
                BindDataGrid();
            }
            else
            { _organizerID = int.Parse(hfOrganizerID.Value); }
        }

        void BindDataGrid()
        {
            sdsOrders.ConnectionString = ConString;
            sdsOrders.SelectCommand = "pGetOrganizerOrders " + _organizerID;
            //if (hfParameter.Value == "")
            //{ sdsOrders.SelectCommand = "pGetRegionOrders " + _organizerID; }
            //else
            //{ sdsOrders.SelectCommand = "pGetRegionSearch " + _organizerID + ",'" + hfParameter.Value + "'"; }
        }
        protected void GrdOrdersRowDataBound(object Sender, GridViewRowEventArgs E)
        {
            if (E.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell cell = E.Row.Cells[3];
                string sValue = cell.Text.Trim();
                if (sValue == "D")
                {
                    E.Row.ForeColor = System.Drawing.Color.Blue;
                    E.Row.Font.Bold = true;
                }
            }
        }
        protected void GrdOrdersPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void GrdOrdersSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
    }
}