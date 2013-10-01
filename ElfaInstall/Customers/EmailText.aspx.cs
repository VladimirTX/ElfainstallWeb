using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElfaInstall.Classes;

namespace ElfaInstall.Customers
{
    public partial class EmailText : System.Web.UI.Page
    {
        private string _orderID;
        private string _encoded;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                TextBox1.Text = "62625";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            _orderID = TextBox1.Text.Trim();
            var secData = new SecureData();
            _encoded = secData.Encrypt(_orderID);
            lbOrder.PostBackUrl = "ProjectInfo.aspx?" + _encoded;
            lbOrder.Visible = true;
        }
    }
}