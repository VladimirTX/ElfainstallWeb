using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class RegionList : System.Web.UI.Page
    {
        static readonly string ConnString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
        readonly SqlConnection _objConn = new SqlConnection(ConnString);
        int _userID;
        string _status;
        SqlDataReader _drInfo, _drStores;

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
            FillRegions();
        }
        void FillRegions()
        {
            var regInfo = new SessionData();
            _drInfo = regInfo.RegionList();
            if (_drInfo.HasRows)
            {
                int regionID;
                string storeList = "";
                while (_drInfo.Read())
                {
                    regionID = int.Parse(_drInfo[0].ToString());
                    string regionName = _drInfo[1].ToString();
                    var objComm = new SqlCommand();
                    objComm.Connection = _objConn;
                    objComm.CommandType = CommandType.Text;
                    objComm.CommandText = "SELECT rs.StoreID,s.StoreCode FROM tblRegionStores rs INNER JOIN tblStores s ON rs.StoreID = s.StoreID WHERE RegionID=" + regionID;
                    _objConn.Open();
                    _drStores = objComm.ExecuteReader(CommandBehavior.CloseConnection);
                    if (_drStores.HasRows)
                    {
                        int i = 0;
                        while (_drStores.Read())
                        {
                            if (i == 0)
                            { storeList = _drStores[1].ToString().Trim(); }
                            else
                            { storeList = storeList + ", " + _drStores[1].ToString().Trim(); }
                            i++;
                        }
                    }
                    _drStores.Close();
                    var tR = new TableRow();
                    var tC1 = new TableCell();
                    var tC2 = new TableCell();
                    var tC3 = new TableCell();
                    tC1.Text = "<a href='EditRegion.aspx?RegionID=" + regionID + "'>" + regionID + "</a>";
                    tC1.CssClass = "tdmain";
                    tC1.HorizontalAlign = HorizontalAlign.Center;
                    tC2.Text = regionName;
                    tC2.CssClass = "tdmain";
                    tC3.Text = storeList;
                    tC3.CssClass = "tdmain";
                    tR.Cells.Add(tC1);
                    tR.Cells.Add(tC2);
                    tR.Cells.Add(tC3);
                    tR.BorderStyle = BorderStyle.Solid;
                    tR.BorderWidth = Unit.Pixel(1);
                    tblRegions.Rows.Add(tR);
                }
            }
            _drInfo.Close();
        }
    }
}