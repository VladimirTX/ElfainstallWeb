using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall.Reports
{
    public partial class AtHomeCheckList : System.Web.UI.Page
    {
        public int OrderID;
        SqlDataReader _drOrder, _drInfo;
        private ShowSpace _oneSpace = new ShowSpace();

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.QueryString["OrderID"] != null)
            { OrderID = int.Parse(Request.QueryString["OrderID"]); }
            else
            {
                Response.Redirect("Orders.aspx");
            }
            if (!IsPostBack)
            {
                var orderInfo = new OrderData();
                _drOrder = orderInfo.NewOrderDetails(OrderID);
                _drOrder.Read();
                lblCustomer.Text = _drOrder["fName"] + " " + _drOrder["lName"];
                lblProductAmt.Text = decimal.Parse(_drOrder["PurchasePrice"].ToString()).ToString("c");
                lblInstallAmt.Text = decimal.Parse(_drOrder["OrderPrice"].ToString()).ToString("c");
                if (DateTime.Parse(_drOrder["StartTime"].ToString()) > DateTime.Parse("01/01/2000"))
                    lblInstallDateStart.Text = DateTime.Parse(_drOrder["StartTime"].ToString()).ToShortDateString() + " "+
                                               DateTime.Parse(_drOrder["StartTime"].ToString()).ToShortTimeString();
                if (DateTime.Parse(_drOrder["EndTime"].ToString()) > DateTime.Parse("01/01/2000"))
                    lblInstallDateEnd.Text = DateTime.Parse(_drOrder["EndTime"].ToString()).ToShortDateString() + " "+
                                               DateTime.Parse(_drOrder["EndTime"].ToString()).ToShortTimeString();
                lblInstaller1.Text = _drOrder["VendorName"].ToString();
                //txtComments.Text = _drOrder["InstNotes"].ToString();
                lblComments.Text = _drOrder["InstNotes"].ToString();
                _drOrder.Close();
                _drOrder = orderInfo.ShowAtHomeData(OrderID);
                if (_drOrder.HasRows)
                {
                    _drOrder.Read();
                    lblSpecialist.Text = _drOrder["Specialist"].ToString();
                    lblPickUp.Text = _drOrder["PickUp"].ToString().Trim();
                    lblLocation.Text = _drOrder["Location"].ToString();
                    lblStaging.Text = _drOrder["Staging"].ToString().Trim();
                    lblStyling.Text = _drOrder["Styling"].ToString().Trim();
                    if (_drOrder["StagingDate"].ToString().Trim()!="") 
                        lblStagingDate.Text = DateTime.Parse(_drOrder["StagingDate"].ToString()).ToShortDateString();
                    if (_drOrder["StylingDate"].ToString().Trim()!="") 
                        lblStylingDate.Text = DateTime.Parse(_drOrder["StylingDate"].ToString()).ToShortDateString();
                    lblInstaller2.Text = _drOrder["VendorName"].ToString();
                    //txtSpecial.Text = _drOrder["Special"].ToString();
                    lblSpecial.Text = _drOrder["Special"].ToString();
                }
                _drOrder.Close();
                ShowSpaces();
            }
        }

        void ShowSpaces()
        {
            var details = new OrderData();
            _drInfo = details.GetSpacesInfo(OrderID);
            while(_drInfo.Read())
            {
                var tR1 = new TableRow();
                var tC1 = new TableCell();
                _oneSpace = (ShowSpace)LoadControl(("~/ShowSpace.ascx"));
                _oneSpace.OrderID = int.Parse(_drInfo["OrderID"].ToString());
                _oneSpace.SpaceID = int.Parse(_drInfo["SpaceID"].ToString());
                _oneSpace.SpaceName = _drInfo["SpaceName"].ToString();
                _oneSpace.SpaceNumber = _drInfo["SpaceNumber"].ToString();
                _oneSpace.Texture = bool.Parse(_drInfo["Texture"].ToString());
                _oneSpace.Description = _drInfo["Description"].ToString();
                _oneSpace.Color = _drInfo["ColorOption"].ToString();
                _oneSpace.NonElfa = _drInfo["NonElfa"].ToString();
                _oneSpace.Instruction = _drInfo["Instruction"].ToString();
                _oneSpace.Removal = _drInfo["Removal"].ToString();
                _oneSpace.ColorName = _drInfo["ColorName"].ToString();
                if (_oneSpace.SpaceID > 0)
                {
                    tC1.Controls.Add(_oneSpace);
                    tR1.Cells.Add(tC1);
                    tblSpaces.Rows.Add(tR1);
                }
                _oneSpace = null;
            }
        }
    }
}