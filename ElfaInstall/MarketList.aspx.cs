using System;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class MarketList : System.Web.UI.Page
    {
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
            }
        }
        protected void BtnAddClick(object Sender, EventArgs E)
        {
            txtNewMarket.Visible = true;
            btnSave.Visible = true;
            lblNewMarket.Visible = true;
            btnCancel.Visible = true;
            btnAdd.Visible = false;
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            string marketName = txtNewMarket.Text.Trim().ToUpper();
            var info = new SessionData();
            info.AddMarket(marketName);
            Response.Redirect("MarketList.aspx");
        }
        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            txtNewMarket.Visible = false;
            btnSave.Visible = false;
            lblNewMarket.Visible = false;
            btnCancel.Visible = false;
            btnAdd.Visible = true;
        }
    }
}