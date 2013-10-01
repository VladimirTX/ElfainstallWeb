using System;
using System.Configuration;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class UserList : System.Web.UI.Page
    {
        static readonly string ConString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        string _status;
        int _userID;
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
                BindDataGrid();
            }

        }
        void BindDataGrid()
        {
            sdsUsers.ConnectionString = ConString;
            sdsUsers.SelectCommand = "pGetAllUsers";
        }
        protected void GrdUsersPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }

        protected void GrdUsersSelectedIndexChanged(object Sender, EventArgs E)
        {
            //int pindex = grdUsers.PageIndex;
            int userID = int.Parse(grdUsers.SelectedRow.Cells[0].Text);
            pnlList.Visible = false;
            pnlEdit.Visible = true;
            var checkLog = new SessionData();
            _drInfo = checkLog.UserInfo(userID);
            _drInfo.Read();
            lblUserName.Text = _drInfo["UserName"].ToString();
            lblStatus.Text = _drInfo["status"].ToString();
            txtLogin.Text = _drInfo["userLogin"].ToString();
            chkActive.Checked = bool.Parse(_drInfo["active"].ToString());
            txtPhone.Text = _drInfo["userPhone"].ToString();
            txtEmail.Text = _drInfo["userEmail"].ToString();
            _drInfo.Close();
        }

        protected void GrdUsersSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }

        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            int userID = int.Parse(grdUsers.SelectedRow.Cells[0].Text);
            var user = new SessionData();
            user.UpdateUser(userID, txtLogin.Text.Trim(), chkActive.Checked, txtPhone.Text.Trim(), txtEmail.Text.Trim());
            BindDataGrid();
            pnlList.Visible = true;
            pnlEdit.Visible = false;
        }

        protected void BtnCancelClick(object Sender, EventArgs E)
        {
            BindDataGrid();
            pnlList.Visible = true;
            pnlEdit.Visible = false;
        }

        protected void BtnReserClick(object Sender, EventArgs E)
        {
            int userID = int.Parse(grdUsers.SelectedRow.Cells[0].Text);
            Response.Redirect("~/ResetPassword.aspx?UserID=" + userID);
        }


    }
}