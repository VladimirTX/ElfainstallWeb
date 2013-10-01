using System;

namespace ElfaInstall
{
    public partial class Login : System.Web.UI.Page
    {
        string _sPassword, _hashvalue, _sUser, _result, _encoded;
        int _userID;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                Session.RemoveAll();
                lblMessage.Text = "";
            }
        }
        protected void btnLogin_Click(object Sender, EventArgs E)
        {
            _sPassword = txtPassword.Text.Trim();
            _sUser = txtUser.Text.Trim();
            var secPass = new Classes.SecureData();
            _hashvalue = secPass.HashValue(_sPassword);
            _encoded = secPass.Encrypt(_sPassword);
            var checkLog = new Classes.SessionData();
            _userID = checkLog.CheckLogin(_sUser, _hashvalue);
            if (_userID == 0)
            {
                lblMessage.Text = "Your User Name or Password is invalid";
                lblMessage.Visible = true;
                txtUser.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                string page = "";
                _result = checkLog.UserStatus(_userID).Trim();
                checkLog.SetNewPassword(_userID, _encoded);
                SetCookie(_userID, _result);
                switch (_result)
                {
                    case "Admin":
                        page = "Orders.aspx";
                        break;
                    case "Store":
                        page = "StoreMenu.aspx";
                        break;
                    case "Vendor":
                        page = "VendorOrders.aspx";
                        break;
                    case "Region":
                        page = "RegionInfo.aspx";
                        break;
                    case "Organizer":
                        page = "OrdersO.aspx";
                        break;
                    default:
                        page = "Login.aspx";
                        break;
                }
                Response.Redirect(page);
            }
        }
        void SetCookie(int UserID, string Status)
        {
            Response.Cookies["UserID"].Value = UserID.ToString();
            Response.Cookies["status"].Value = Status;
        }
    }
}