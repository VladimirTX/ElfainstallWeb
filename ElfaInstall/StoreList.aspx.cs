using System;
using System.Configuration;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class StoreList : System.Web.UI.Page
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
            sdsStores.ConnectionString = ConString;
            sdsStores.SelectCommand = "SELECT s.StoreID,s.StoreNumb,s.StoreCode,s.StoreName,m.MarketName,s.State FROM tblStores s INNER JOIN tblMarkets m on s.MarketID=m.MarketID ORDER BY MarketName, StoreName";
        }
        protected void BtnAddClick(object Sender, EventArgs E)
        {
            Response.Redirect("EditStore.aspx?StoreID=0");
        }
        protected void GrvStoresPageIndexChanged(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
        protected void GrvStoresSorted(object Sender, EventArgs E)
        {
            BindDataGrid();
        }
    }
}