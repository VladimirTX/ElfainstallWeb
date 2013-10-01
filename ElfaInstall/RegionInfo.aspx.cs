using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class RegionInfo : System.Web.UI.Page
    {
        int _userID, _regionID;
        string _status;
        private SqlDataReader _drInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserID"] == null)
                { Response.Redirect("Login.aspx"); }
                else
                { _userID = int.Parse(Request.Cookies["UserID"].Value); }
                var checkLog = new SessionData();
                _status = checkLog.UserStatus(_userID).Trim();
                if (_status == "Region")
                {
                    _regionID = checkLog.GetRegionID(_userID);
                    if (_regionID == 0)
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                Response.Cookies["RegionID"].Value = _regionID.ToString();
                hfRegionID.Value = _regionID.ToString();
                if (!GetRegionInfo()) Response.Redirect("~/OrdersR.aspx");
            }
        }

        bool GetRegionInfo()
        {
            bool result;
            var orderInfo = new OrderData();
            _drInfo = orderInfo.RegionAttention(_regionID);
            if (_drInfo.HasRows)
            {
                _drInfo.Read();
                int confirmed = int.Parse(_drInfo["Confirmed"].ToString());
                int byPhone = int.Parse(_drInfo["ByPhone"].ToString());
                int bySurvey = int.Parse(_drInfo["BySurvey"].ToString());
                int unConfirmed = int.Parse(_drInfo["UnApproved"].ToString());
                int noContact = int.Parse(_drInfo["NoContact"].ToString());
                int unScheduled = int.Parse(_drInfo["UnScheduled"].ToString());
                int total = int.Parse(_drInfo["Total"].ToString());
                int uConstruct = int.Parse(_drInfo["Status2"].ToString());
                int backordered = int.Parse(_drInfo["Status1"].ToString());
                int cRescheduling = int.Parse(_drInfo["Status3"].ToString());
                lblHeader.Text = _drInfo["RegionName"].ToString();
                _drInfo.Close();
                lblConfirmed.Text = confirmed.ToString();
                lblConfirmedP.Text = (confirmed * 100.00 / total).ToString("##.#");
                lblByPhone.Text = byPhone.ToString();
                lblBySurvey.Text = bySurvey.ToString();
                lblUnconfirmed.Text = unConfirmed.ToString();
                lblUnconfirmedP.Text = (unConfirmed * 100.00 / total).ToString("##.#");
                lblNoContact.Text = noContact.ToString();
                lblNoContactP.Text = (noContact * 100.00 / total).ToString("##.#");
                lblUnscheduled.Text = unScheduled.ToString();
                lblUnscheduledP.Text = (unScheduled * 100.00 / total).ToString("##.#");
                lblTotal.Text = total.ToString();
                if (unScheduled > 0)
                {
                    lblUnderConstruction.Text = uConstruct.ToString();
                    //lblUnderConstructionP.Text = (uConstruct * 100.00 / unScheduled).ToString("##.#");
                    lblBackorders.Text = backordered.ToString();
                    //lblBackordersP.Text = (backordered * 100.00 / unScheduled).ToString("##.#");
                    lblRescheduling.Text = cRescheduling.ToString();
                    //lblReschedulingP.Text = (cRescheduling * 100.00 / unScheduled).ToString("##.#");
                    pnlNotScheduled.Visible = true;
                }
                else pnlNotScheduled.Visible = false;
                result = true;
            }
            else result = false;
            _drInfo.Close();
            return result;
        }

        protected void BtnContinueClick(object Sender, EventArgs E)
        {
            Response.Redirect("~/OrdersR.aspx");
        }
    }
}