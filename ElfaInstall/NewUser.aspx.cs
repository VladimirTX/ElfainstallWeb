using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class NewUser : System.Web.UI.Page
    {
        SqlDataReader _drList;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                FillStores();
                FillVendors();
                FillOrganizers();
            }
            rowStore.Visible = ddlStatus.SelectedValue == "Store";
            rowVendor.Visible = ddlStatus.SelectedValue == "Vendor";
            rowOrganizer.Visible = ddlStatus.SelectedValue == "Organizer";
        }
        void FillStores()
        {
            ddlStore.Items.Add(new ListItem("", "0"));
            var fillData = new OrderData();
            _drList = fillData.StoreList();
            ddlStore.DataSource = _drList;
            ddlStore.DataTextField = "Store";
            ddlStore.DataValueField = "StoreID";
            ddlStore.DataBind();
            _drList.Close();
        }
        void FillVendors()
        {
            var fillData = new OrderData();
            _drList = fillData.VendorList();
            ddlVendor.DataSource = _drList;
            ddlVendor.DataTextField = "Vendor";
            ddlVendor.DataValueField = "VendorID";
            ddlVendor.DataBind();
            _drList.Close();
        }
        void FillOrganizers()
        {
            var fillData = new OrderData();
            _drList = fillData.GetOrganizers();
            ddlOrganizer.DataSource = _drList;
            ddlOrganizer.DataTextField = "Organizer";
            ddlOrganizer.DataValueField = "SpecialistID";
            ddlOrganizer.DataBind();
            _drList.Close();
        }

        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            string status = ddlStatus.SelectedValue.Trim();
            int storeID = status == "Store" ? int.Parse(ddlStore.SelectedValue) : 0;
            int vendorID = status == "Vendor" ? int.Parse(ddlVendor.SelectedValue) : 0;
            int organizerID = status == "Organizer" ? int.Parse(ddlOrganizer.SelectedValue) : 0;
            var newPass = new SecureData();
            string hash = newPass.HashValue(txtPassword.Text.Trim());
            var newUser = new SessionData();
            int regionID = status == "Region" ? newUser.AddNewRegion(txtName.Text.Trim()) : 0;
            int userID = newUser.AddNewUser(txtName.Text.Trim(), txtLogin.Text.Trim(), hash, status, storeID, vendorID, regionID, txtEmail.Text.Trim(),txtPhone.Text.Trim(),organizerID);
            if (userID == 0)
            {
                string msg = "User with login '" + txtName.Text.Trim() + "' and password '" + txtPassword.Text.Trim() + "' already exists!";
                string page = "NewUser.aspx";
                Response.Redirect("ErrorMessage.aspx?Message=" + msg + "&Page=" + page);
            }
            else
            {
                switch (status)
                {
                    case "Region":
                        Response.Redirect("EditRegion.aspx?RegionID=" + regionID);
                        break;
                    case "Organizer":
                        Response.Redirect("EditOrganizer.aspx?OrganizerID=" + organizerID);
                        break;
                    case "Vendor":
                        Response.Redirect("EditVendor.aspx?VendorID=" + vendorID);
                        break;
                    default:
                        Response.Redirect("UserList.aspx");
                        break;
                }
            }
        }
    }
}