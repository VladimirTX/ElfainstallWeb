using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class EditOrganizer : System.Web.UI.Page
    {
        int _userID, _organizerID;
        string _status;
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.QueryString["OrganizerID"] != null)
            { _organizerID = int.Parse(Request.QueryString["OrganizerID"]); }
            else { Response.Redirect("OrganizerList.aspx"); }
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
                if (_organizerID > 0) FillData(); 
                else
                {
                    txtProcent.Text = "0.00";
                    cbActive.Checked = true;
                }
            }
        }

         void FillData()
         {
             var organizer = new OrderData();
             _drInfo = organizer.OrganizerInfo(_organizerID);
             if (_drInfo.HasRows)
             {
                 _drInfo.Read();
                 txtName.Text = _drInfo["Name"].ToString().Trim();
                 txtPhone.Text = _drInfo["Phone"].ToString();
                 txtCity.Text = _drInfo["City"].ToString().Trim();
                 ddlStates.SelectedValue = _drInfo["state"].ToString();
                 txtEmail1.Text = _drInfo["Email"].ToString().Trim();
                 txtComments.Text = _drInfo["comments"].ToString().Trim();
                 txtProcent.Text = _drInfo["PayProc"].ToString().Trim();
                 cbActive.Checked = bool.Parse(_drInfo["active"].ToString());
                 txtContractor.Text = _drInfo["ContractorNumb"].ToString().Trim();
                 txtAddress.Text = _drInfo["Address"].ToString().Trim();
                 txtZip.Text = _drInfo["Zip"].ToString().Trim();
                 cbDeduct.Checked = bool.Parse(_drInfo["Deduct"].ToString());
             }
             else {txtProcent.Text = "0.00";}
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

        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            if (_organizerID > 0) { UpdateOrganizer(); }
            else { NewOrganizer(); }
            Response.Redirect("OrganizerList.aspx");
        }

        void NewOrganizer()
        {
            var organizer = new OrderData();
            _organizerID = organizer.NewOrganizer(txtName.Text.Trim(), txtPhone.Text, txtCity.Text, txtAddress.Text,
                                                  ddlStates.SelectedValue, txtZip.Text, txtEmail1.Text,
                                                  txtComments.Text.Trim(), decimal.Parse(txtProcent.Text),
                                                  cbDeduct.Checked, txtContractor.Text.Trim());
        }

        void UpdateOrganizer()
        {
            var organizer = new OrderData();
            organizer.UpdateOrganizer(_organizerID, txtName.Text.Trim(), txtPhone.Text, txtCity.Text, txtAddress.Text,
                                                  ddlStates.SelectedValue, txtZip.Text, txtEmail1.Text,
                                                  txtComments.Text.Trim(), cbActive.Checked, decimal.Parse(txtProcent.Text),
                                                  cbDeduct.Checked, txtContractor.Text.Trim());
        }
    }
}