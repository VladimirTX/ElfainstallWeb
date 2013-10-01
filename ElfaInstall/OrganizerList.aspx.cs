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
    public partial class OrganizerList : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        int _userID;
        string _status;

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
                 BindDataGrid();
             }
        }

        void BindDataGrid()
        {
            sdsOrganizers.ConnectionString = ConString;
            sdsOrganizers.SelectCommand = "SELECT SpecialistID,Name,City,State,Phone,active,adddate As Added FROM tblSpecialists ORDER BY active desc,State";
        }

        protected void GrvOrganizersSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }

        protected void GrvOrganizersPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }

        protected void BtnAddNewClick(object Sender, EventArgs E)
        {
            Response.Redirect("EditOrganizer.aspx?OrganizerID=0");
        }
    }
}