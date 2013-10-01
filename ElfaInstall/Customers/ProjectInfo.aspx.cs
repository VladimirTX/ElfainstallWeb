using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web;
using ElfaInstall.Classes;

namespace ElfaInstall.Customers
{
    public partial class ProjectInfo : System.Web.UI.Page
    {
        private static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        private int _orderID;
        private string _encoded;
        private bool _isDelivery;
        SqlDataReader _drOrder, _drInfo;
        private string _phone1, _phone2, _email, _instNotes, _invoiceNotes, _orderNumber;
        private readonly OrderData _orderInfo = new OrderData();
        private string _store;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                var secData = new SecureData();
                HttpRequest q = Request;
                _encoded = q.QueryString.ToString();
                string orderID = secData.Decrypt(_encoded);
                //string orderID = "66624";
                hfOrderID.Value = orderID;
                _orderID = int.Parse(orderID);
                FillStates();
            }
            else
            { _orderID = int.Parse(hfOrderID.Value);}
            ShowData();
            if (_isDelivery) pnlDelivery.Visible = true;
        }

        void ShowData()
        {
            _drOrder = _orderInfo.NewOrderDetails(_orderID);
            if (_drOrder.HasRows)
            {
                _drOrder.Read();
                _orderNumber = _drOrder["OrderNumb"].ToString();
                _store = _drOrder["StoreCode"].ToString().Trim();
                lblCustomerNameShow.Text = (_drOrder["fName"] + " " + _drOrder["mi"]).Trim() + " " + _drOrder["lName"];
                txtCustomerFirstName.Text = _drOrder["fName"].ToString();
                txtCustomerMi.Text = _drOrder["mi"].ToString();
                txtCustomerLastName.Text = _drOrder["lName"].ToString();
                lblAddressShow.Text = _drOrder["address1"] + " " + _drOrder["address2"] + " " +
                                      _drOrder["city"] + ", " + _drOrder["state"] + " " + _drOrder["zip"];
                txtAddress1.Text = _drOrder["address1"].ToString();
                txtAddress2.Text = _drOrder["address2"].ToString();
                txtCity.Text = _drOrder["city"].ToString();
                txtZip.Text = _drOrder["zip"].ToString();
                string state = _drOrder["state"].ToString().Trim();
                ddlStates.SelectedValue = state;
                _phone1 = _drOrder["hphone"].ToString().Trim();
                lblContactNumber.Text = _phone1;
                _phone2 = _drOrder["phone2"].ToString().Trim();
                lblAlternateNumber.Text = _phone2;
                _email = _drOrder["email"].ToString().Trim();
                _instNotes = _drOrder["InstNotes"].ToString().Trim();
                _invoiceNotes = _drOrder["InvoiceNotes"].ToString().Trim();
                txtContactNumber.Text = _drOrder["hphone"].ToString().Trim();
                txtAlternativeNumber.Text = _drOrder["phone2"].ToString().Trim();
                if (decimal.Parse(_drOrder["Delivery"].ToString()) > 0) _isDelivery = true;
                DateTime instDate = DateTime.Parse(_drOrder["StartTime"].ToString());
                lblInstallDate.Text = instDate.ToLongDateString() + " " + instDate.ToShortTimeString();
                lblPrice.Text = _drOrder["OrderPrice"].ToString();
                if (bool.Parse(_drOrder["DeliveryOption"].ToString())) rbDeliveryYes.Checked = true;
            }
            _drOrder.Close();
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

        protected void BtnEditNameClick(object Sender, EventArgs E)
        {
            pnlNameShow.Visible = false;
            pnlNameEdit.Visible = true;
        }

        protected void BtnSaveNameClick(object Sender, EventArgs E)
        {
            _orderInfo.UpdateInstallName(_orderID, txtCustomerFirstName.Text.Trim(), txtCustomerMi.Text.Trim(),
                                        txtCustomerLastName.Text.Trim());
            ShowData();
            pnlNameShow.Visible = true;
            pnlNameEdit.Visible = false;
        }

        protected void BtnNameCancelClick(object Sender, EventArgs E)
        {
            ShowData();
            pnlNameShow.Visible = true;
            pnlNameEdit.Visible = false;
        }

        protected void BtnEditAddressClick(object Sender, EventArgs E)
        {
            pnlAddressEdit.Visible = true;
            pnlAddressShow.Visible = false;
        }

        protected void BtnSaveAddressClick(object Sender, EventArgs E)
        {
            _orderInfo.UpdateInstallAddress(_orderID, txtAddress1.Text.Trim(), txtAddress2.Text.Trim(),
                                           txtCity.Text.Trim(), ddlStates.SelectedValue.Trim(), txtZip.Text.Trim());
            ShowData();
            pnlAddressEdit.Visible = false;
            pnlAddressShow.Visible = true;
        }

        protected void BtnCancelAddressClick(object Sender, EventArgs E)
        {
            ShowData();
            pnlAddressEdit.Visible = false;
            pnlAddressShow.Visible = true;
        }

        protected void BtnEditContactNumbersClick(object Sender, EventArgs E)
        {
            pnlContactEdit.Visible = true;
            pnlContactChow.Visible = false;
        }

        protected void BtnSaveContactNumberClick(object Sender, EventArgs E)
        {
            _orderInfo.UpdateCustomer(_orderID, txtContactNumber.Text.Trim(), txtAlternativeNumber.Text.Trim(), _email,
                                     _instNotes, _invoiceNotes);
            ShowData();
            pnlContactEdit.Visible = false;
            pnlContactChow.Visible = true;
        }

        protected void BtnCancelContactNumberClick(object Sender, EventArgs E)
        {
            ShowData();
            pnlContactEdit.Visible = false;
            pnlContactChow.Visible = true;
        }

        protected void BtnSubmitClick(object Sender, EventArgs E)
        {
            if (ValidateInput() == false) return;
            if (pnlNameEdit.Visible || pnlAddressEdit.Visible || pnlContactEdit.Visible)
            {
                //RadAjaxManager1.Alert("Save information you changed or cancel.");
                return;
            }
            SubmitInformation();
            //RadAjaxManager1.Alert("Your Information has been Submitted.");
            btnSubmit.Enabled = false;
        }
        private bool ValidateInput()
        {
            if (cbCustomerInfo.Checked == false)
            {
                //RadAjaxManager1.Alert("Check Customer Information");
                return false;
            }
            if (_isDelivery && (!rbDeliveryYes.Checked && !rbDeliveryNo.Checked))
            {
                //RadAjaxManager1.Alert("Select Product Delivery Option");
                return false;
            }
            if(!rbDemolition1.Checked && !rbDemolition2.Checked && !rbDemolition3.Checked)
            {
                //RadAjaxManager1.Alert("Select Demolition option");
                rbDemolition3.Focus();
                return false;
            }
            if (!rbMilage1.Checked && !rbMilage2.Checked)
            {
                //RadAjaxManager1.Alert("Select Your Mileage Option");
                rbMilage2.Focus();
                return false;
            }
            if (!rbOther1.Checked && !rbOther2.Checked)
            {
                //RadAjaxManager1.Alert("Select Other Considerations Option");
                rbOther1.Focus();
                return false;
            }
            if (cbInstallFee.Checked == false)
            {
                //RadAjaxManager1.Alert("Check Installation Fee Information");
                cbInstallFee.Focus();
                return false;
            }
            if (!rbInstallDate1.Checked && !rbInstallDate2.Checked)
            {
                //RadAjaxManager1.Alert("Check Installation Schedule");
                rbInstallDate2.Focus();
                return false;
            }
            return true;
        }
        private void SubmitInformation()
        {
            if(txtAdditional.Text.Trim()!="")
            {
                _orderInfo.UpdateCustomer(_orderID, _phone1, _phone2, _email, _instNotes + " " + txtAdditional.Text.Trim(), _invoiceNotes);
            }
            _orderInfo.UpdateResponse(_orderID);
            SendConfirmationEmail();
        }
        private void SendConfirmationEmail()
        {
            var mailSeverName = MailServer; 
            const string from = "confirmation@elfainstall.com";
            string to = "tom@elfainstall.com";
            //const string to = "vlad@tomatex.com";
            string subject = "Customer confirmation for Order " + _orderNumber.Trim() + " installation.";
            string body = "";
            body += "Customer: " + lblCustomerNameShow.Text + "<br>";
            body += "Address: " + lblAddressShow.Text + "<br>";
            body += "Best contact number: " + lblContactNumber.Text + "<br>";
            body += "Product Delivery: ";
            if (rbDeliveryYes.Checked) body += "EIS is delivering my product.<br>";
            else body += "I have arranged to have the product here by install date myself.<br>";
            body += "Mileage fees: ";
            if (rbMilage1.Checked) body += "My installation is within 20 miles from store.<br>";
            else
                body +=
                    "Install is located more than 20 miles away from store – I understand the additional mileage fee.<br>";
            body += "Other Considerations: ";
            if (rbOther1.Checked) body += "There are no issues of this nature on my project.<br>";
            else body += "EIS will need to address with appropriate association or superintendant.<br>";
            body += "Installation Date and Time: " + lblInstallDate.Text;
            if (rbInstallDate1.Checked) body += " - is correct.<br>";
            else body += " - is not correct, please call.<br>";
            if (txtAdditional.Text.Trim() != "")
                body += "Additional Info: " + txtAdditional.Text.Trim() + "<br>";
            body += "<br><i>This is an automated e-mail. Do not reply.</i>";
            try
            {
                var copy = new MailAddress("nodailey@containerstore.com");
                var copy1 = new MailAddress("vlad@tomatex.com");
                var message = new MailMessage(from, to, subject, body);
                message.IsBodyHtml = true;
                message.CC.Add(copy);
                message.Bcc.Add(copy1);
                var mailClient = new SmtpClient(mailSeverName);
                mailClient.UseDefaultCredentials = true;
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                var saveLog = new SessionData();
                saveLog.AddErrorLog("ProjectInfor", "SendConfirmationEmail", _orderID + ": " + ex.Message);
            }
        }
    }
}