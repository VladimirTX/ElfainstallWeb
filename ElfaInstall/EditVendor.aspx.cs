using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class EditVendor : System.Web.UI.Page
    {
        int _userID, _vendorID;
        string _status;
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.QueryString["VendorID"] != null)
            { _vendorID = int.Parse(Request.QueryString["VendorID"]); }
            else { Response.Redirect("VendorList.aspx"); }
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status != "Admin") Response.Redirect("Login.aspx");
                FillStates();
                if (_vendorID > 0)
                { FillData(); }
                else dpAdded.SelectedDate = DateTime.Today;
                //hlSchedule.NavigateUrl = "~/Calendar.aspx?" + _vendorID;
                FillVendors();
            }
            FillMembers();
        }
        void FillData()
        {
            var vendor = new SessionData();
            _drInfo = vendor.VendorInfo(_vendorID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                txtName.Text = _drInfo["VendorName"].ToString().Trim();
                txtCity.Text = _drInfo["City"].ToString().Trim();
                ddlStates.SelectedValue = _drInfo["state"].ToString();
                txtLocation.Text = _drInfo["location"].ToString().Trim();
                txtEmail1.Text = _drInfo["email"].ToString().Trim();
                txtEmail2.Text = _drInfo["email2"].ToString().Trim();
                txtComments.Text = _drInfo["comments"].ToString().Trim();
                txtProcent.Text = _drInfo["PayProc"].ToString().Trim();
                cbActive.Checked = bool.Parse(_drInfo["active"].ToString());
                txtContractor.Text = _drInfo["ContractorNumb"].ToString().Trim();
                dpAdded.SelectedDate = DateTime.Parse(_drInfo["adddate"].ToString());
                txtAddress.Text = _drInfo["Address"].ToString().Trim();
                txtZip.Text = _drInfo["Zip"].ToString().Trim();
                ddlVendors.SelectedValue = _drInfo["PayID"].ToString();
                cbDeduct.Checked = bool.Parse(_drInfo["Deduct"].ToString());
            }
            _drInfo.Close();
            _drInfo = vendor.VendorPhones(_vendorID);
            if (_drInfo.HasRows)
            {
                while (_drInfo.Read())
                {
                    string type = _drInfo["type"].ToString().Trim();
                    if (type == "Phone") txtPphone.Text = _drInfo["phone"].ToString().Trim();
                    if (type == "Office") txtOphone.Text = _drInfo["phone"].ToString().Trim();
                    if (type == "Cell") txtCphone.Text = _drInfo["phone"].ToString().Trim();
                    if (type == "Home") txtHphone.Text = _drInfo["phone"].ToString().Trim();
                    if (type == "Fax") txtFphone.Text = _drInfo["phone"].ToString().Trim();
                }
            }
            _drInfo.Close();
        }
        void FillStates()
        {
            var states = new OrderData();
            _drInfo = states.StateList();
            ddlStates.DataSource = _drInfo;
            ddlStates.DataTextField = "name";
            ddlStates.DataValueField = "code";
            ddlStates.DataBind();
            _drInfo.Close();
        }
        void FillVendors()
        {
            var vendors = new OrderData();
            _drInfo = vendors.VendorList();
            ddlVendors.DataSource = _drInfo;
            ddlVendors.DataTextField = "Vendor";
            ddlVendors.DataValueField = "VendorID";
            ddlVendors.DataBind();
            _drInfo.Close();
        }
        void FillMembers()
        {
            string teamMembers = "";
            int counter = 0;
            var members = new SessionData();
            _drInfo = members.InstallerInfo(_vendorID);
            string vend = _vendorID.ToString();
            while (_drInfo.Read())
            {
                if (counter > 0)
                {
                    string inst = _drInfo[0].ToString();
                    teamMembers = teamMembers + "<a href='DeleteMember.aspx?Inst=" + inst + "&Vend=" + vend + "'>" + _drInfo[1].ToString().Trim() + "</a><br>";
                }
                else
                { teamMembers = teamMembers + _drInfo[1].ToString().Trim() + "<br>"; }
                counter++;
            }
            _drInfo.Close();
            lblMembers.Text = teamMembers;
        }
        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            if (_vendorID > 0) { UpdateVendor(); }
            else { NewVendor(); }
            Response.Redirect("VendorList.aspx");
        }
        void NewVendor()
        {
            var vendor = new SessionData();
            _vendorID = vendor.NewVendor(txtName.Text, txtCity.Text, ddlStates.SelectedValue,
                txtLocation.Text, txtEmail1.Text, txtEmail2.Text, txtComments.Text.Trim(), 
                decimal.Parse(txtProcent.Text),txtContractor.Text.Trim(),dpAdded.SelectedDate,
                txtAddress.Text.Trim(),txtZip.Text.Trim(),int.Parse(ddlVendors.SelectedValue),
                cbDeduct.Checked);
            vendor.AddVendorPhone(_vendorID, txtPphone.Text.Trim(), "Phone");
            vendor.AddVendorPhone(_vendorID, txtOphone.Text.Trim(), "Office");
            vendor.AddVendorPhone(_vendorID, txtCphone.Text.Trim(), "Cell");
            vendor.AddVendorPhone(_vendorID, txtHphone.Text.Trim(), "Home");
            vendor.AddVendorPhone(_vendorID, txtFphone.Text.Trim(), "Fax");
        }
        void UpdateVendor()
        {
            var vendor = new SessionData();
            vendor.UpdateVendor(_vendorID, txtName.Text, txtCity.Text, ddlStates.SelectedValue,
                txtLocation.Text, txtEmail1.Text, txtEmail2.Text, txtComments.Text, cbActive.Checked, decimal.Parse(txtProcent.Text), 
                txtContractor.Text.Trim(),txtAddress.Text.Trim(),txtZip.Text,int.Parse(ddlVendors.SelectedValue),cbDeduct.Checked);
            vendor.UpdateVendorPhone(_vendorID, txtPphone.Text, "Phone");
            vendor.UpdateVendorPhone(_vendorID, txtOphone.Text, "Office");
            vendor.UpdateVendorPhone(_vendorID, txtCphone.Text, "Cell");
            vendor.UpdateVendorPhone(_vendorID, txtHphone.Text, "Home");
            vendor.UpdateVendorPhone(_vendorID, txtFphone.Text, "Fax");
        }
        protected void BtnDeleteClick(object Sender, EventArgs E)
        {
            var orders = new OrderData();
            int total = orders.OrdersbyVendor(_vendorID);
            if (total > 0)
            {
                const string page = "VendorList.aspx";
                string message = "Vendor " + txtName.Text.Trim() + " can not be deleted, because some " +
                                 "orders related to this vendor! Make Vendor Inactive.";
                Response.Redirect("ErrorMessage.aspx?Message=" + message + "&Page=" + page);
            }
            else
            {
                string connString = ConfigurationManager.ConnectionStrings["CommConnection"].ToString();
                var objConn = new SqlConnection(connString);
                var objComm = new SqlCommand();
                objComm.Connection = objConn;
                objComm.CommandType = CommandType.Text;
                objComm.CommandText = "DELETE FROM tblVendors WHERE VendorID=" + _vendorID;
                objConn.Open();
                objComm.ExecuteNonQuery();
                objConn.Close();
                Response.Redirect("VendorList.aspx");
            }
        }
        protected void BtnAddMemberClick(object Sender, EventArgs E)
        {
            pnlMember.Visible = true;
        }
        protected void BtnSaveMemberClick(object Sender, EventArgs E)
        {
            if (txtNewMember.Text.Trim() != "")
            {
                var newMember = new SessionData();
                newMember.NewInstaller(_vendorID, txtNewMember.Text.Trim());
                pnlMember.Visible = false;
                Response.Redirect("EditVendor.aspx?VendorID=" + _vendorID);
            }
        }
    }
}