using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        int _userID, _loginID;
        string _status;
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.QueryString["UserID"] != null)
            { _loginID = int.Parse(Request.QueryString["UserID"]); }
            else { Response.Redirect("UserList.aspx"); }
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status != "Admin") Response.Redirect("Login.aspx");
                _drInfo = checkLog.UserInfo(_loginID);
                _drInfo.Read();
                lblLogin.Text = _drInfo["userLogin"].ToString();
                lblUser.Text = _drInfo["UserName"].ToString();
                _drInfo.Close();
            }
        }
        protected void Button1_Click(object Sender, EventArgs E)
        {
            string sPassword = txtPass1.Text.Trim();
            var secPass = new SecureData();
            string hashvalue = secPass.HashValue(sPassword);
            string encoded = secPass.Encrypt(sPassword);
            var userPass = new SessionData();
            userPass.UpdatePassword(_loginID, hashvalue);
            userPass.SetNewPassword(_loginID, encoded);
            Response.Redirect("UserList.aspx");
        }
    }
}