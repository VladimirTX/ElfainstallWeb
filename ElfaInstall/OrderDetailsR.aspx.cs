using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class OrderDetailsR : System.Web.UI.Page
    {
        public int OrderID, UserID;
        SqlDataReader drOrder, drInfo;
        string Status, Duration;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (Request.Cookies["UserID"] == null)
            { Response.Redirect("Default.aspx"); }
            else
            { UserID = int.Parse(Request.Cookies["UserID"].Value); }
            SessionData UserInfo = new SessionData();
            Status = UserInfo.UserStatus(UserID);
            if (Request.QueryString["OrderID"] != null)
            { OrderID = int.Parse(Request.QueryString["OrderID"]); }
            else
            {
                if (Status == "Region") Response.Redirect("OrdersR.aspx");
                else Response.Redirect("Default.aspx");
            }
            hfOrderID.Value = OrderID.ToString();
            OrderData OrderInfo = new OrderData();
            drOrder = OrderInfo.OrderDetails(OrderID);
            if (drOrder.HasRows)
            {
                drOrder.Read();
                lblOrderID.Text = drOrder["OrderNumb"].ToString();
                lblStoreID.Text = drOrder["StoreCode"].ToString();
                lblSaleDate.Text = DateTime.Parse(drOrder["OrderDate"].ToString()).ToShortDateString();
                lblPlanner.Text = drOrder["Planner"].ToString();
                if (drOrder["Installer"].ToString().Trim() == "")
                { lblInstaller.Text = drOrder["VendorName"].ToString(); }
                else
                { lblInstaller.Text = drOrder["Installer"].ToString(); }
                if ((drOrder["InstallDate"].ToString().Trim() != "") && (DateTime.Parse(drOrder["InstallDate"].ToString()) > DateTime.Parse("01/01/2000")))
                {
                    lblInstallDate.Text = DateTime.Parse(drOrder["InstallDate"].ToString()).ToShortDateString();
                }
                lblInstallTime.Text = drOrder["InstallTime"].ToString();
                Duration = drOrder["Duration"].ToString().Trim();
                if (Duration != "")
                { lblInstallTime.Text = lblInstallTime.Text.Trim() + " / " + Duration; }
                lblInstallPref.Text = drOrder["InstallPref"].ToString();
                lblStatus.Text = drOrder["sCurrent"].ToString();
                lblcName.Text = (drOrder["fName"] + " " + drOrder["mi"]).Trim() + " " + drOrder["lName"];
                lblcAddress1.Text = drOrder["address1"].ToString();
                lblcAddress2.Text = drOrder["address2"].ToString();
                lblcCity.Text = drOrder["city"].ToString();
                lblcState.Text = drOrder["state"].ToString().Trim();
                lblcZip.Text = drOrder["zip"].ToString();
                lblHphone.Text = drOrder["hphone"].ToString();
                lblPhone2.Text = drOrder["phone2"].ToString();
                lblEmail.Text = drOrder["email"].ToString();
                cbDelivery.Checked = bool.Parse(drOrder["DeliveryOption"].ToString());
                cbDemolition.Checked = bool.Parse(drOrder["demolition"].ToString());
                if (drOrder["ScopeofDemo"].ToString() == "A")
                { lblDemo.Text = "Additional"; }
                else
                { lblDemo.Text = "Basic"; }
                lblDelivery.Text = drOrder["Delivery"].ToString();
                lblProc.Text = drOrder["Inst_Proc"].ToString();
                lblSolution.Text = drOrder["SolutionDescr"].ToString();
                lblPurchasePrice.Text = decimal.Parse(drOrder["PurchasePrice"].ToString()).ToString("c");
                lblInstallPrice.Text = decimal.Parse(drOrder["BaseInstallPrice"].ToString()).ToString("c");
                lblDeliveryPrice.Text = decimal.Parse(drOrder["DeliveryPrice"].ToString()).ToString("c");
                lblMilesPrice.Text = decimal.Parse(drOrder["MilesPrice"].ToString()).ToString("c");
                lblDemoPrice.Text = decimal.Parse(drOrder["DemoPrice"].ToString()).ToString("c");
                lblMisc.Text = decimal.Parse(drOrder["MiscPrice"].ToString()).ToString("c");
                lblTotalPrice.Text = decimal.Parse(drOrder["OrderPrice"].ToString()).ToString("c");
                lblComments.Text = drOrder["comments"].ToString();
                SecureData imgLink = new SecureData();
                string link = imgLink.GenerateHash(drOrder["OrderNumb"].ToString().Trim(), CodedDate(DateTime.Parse(drOrder["OrderDate"].ToString())), CodedDate(DateTime.Today));
                link = "http://www.containerstore.com/elfaInstallAdmin/orderSummary.htm?order="
                       + drOrder["OrderNumb"].ToString().Trim() + ":" +
                       CodedDate(DateTime.Parse(drOrder["OrderDate"].ToString())) + ":" + CodedDate(DateTime.Today)
                       + "&key=" + link;
                hlImages.NavigateUrl = link;
            }
            drOrder.Close();
            CheckCalls();
        }
        protected void btnEdit_Click(object Sender, EventArgs E)
        {
            Response.Redirect("EditROrder.aspx?OrderID=" + OrderID);
        }
        protected void btnPrint_Click(object Sender, EventArgs E)
        {
            Response.Redirect("PrintOrder.aspx?OrderID=" + OrderID);
        }
        protected void btnReturn_Click(object Sender, EventArgs E)
        {
            Response.Redirect("OrdersR.aspx");
        }
        void CheckCalls()
        {
            string lblText;
            OrderData CallsInfo = new OrderData();
            drInfo = CallsInfo.GetCallsInfo(OrderID);
            if (drInfo.HasRows)
            {
                drInfo.Read();
                if (bool.Parse(drInfo["Call1"].ToString()))
                {
                    lblText = "Call #1:&nbsp;&nbsp;" + DateTime.Parse(drInfo["Date1"].ToString()).ToShortDateString();
                    lblText = lblText + "&nbsp;&nbsp;" + drInfo["Result1"];
                    lblCall1.Text = lblText;
                }
                if (bool.Parse(drInfo["Call2"].ToString()))
                {
                    lblText = "Call #2:&nbsp;&nbsp;" + DateTime.Parse(drInfo["Date2"].ToString()).ToShortDateString();
                    lblText = lblText + "&nbsp;&nbsp;" + drInfo["Result2"];
                    lblCall2.Text = lblText;
                }
                if (bool.Parse(drInfo["Call3"].ToString()))
                {
                    lblText = "Call #3:&nbsp;&nbsp;" + DateTime.Parse(drInfo["Date3"].ToString()).ToShortDateString();
                    lblText = lblText + "&nbsp;&nbsp;" + drInfo["Result3"];
                    lblCall3.Text = lblText;
                }
            }
            drInfo.Close();
        }
        static string CodedDate(DateTime Calcdate)
        {
            string date = Calcdate.Year + "-" +
                          Calcdate.Month.ToString().Trim().PadLeft(2, '0') + "-" +
                          Calcdate.Day.ToString().Trim().PadLeft(2, '0');
            return date;
        }
    }
}