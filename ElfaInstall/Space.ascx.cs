using System;
using System.Data.SqlClient;
using ElfaInstall.Classes;

namespace ElfaInstall
{
    public partial class Space : System.Web.UI.UserControl
    {
        public int OrderID;
        public int SpaceID;
        public string SpaceName;
        public string SpaceNumber;
        public bool Texture;
        public string Description;
        public int Color;
        public string NonElfa;
        public string Instruction;
        public string Removal;
        public string ColorName;
        SqlDataReader _drInfo;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                FillColor();
                txtSpaceName.Text = SpaceName.Trim();
                txtSpaceID.Text = SpaceNumber.Trim();
                bool texture = bool.Parse(Texture.ToString());
                string bText = texture.ToString().Trim();
                ddlTexture.SelectedValue = bText;
                if (texture) ddlDescription.SelectedValue = Description;
                else ddlDescription.Enabled = false;
                ddlColors.SelectedValue = Color.ToString();
                txtNonElfa.Text = NonElfa;
                txtInstructions.Text = Instruction;
                txtRemoval.Text = Removal;
                txtColorName.Text = ColorName;
                if (cbDeleted.Checked) cbDeleted.Enabled = false;
            }
            if(Color==0) FillColor();
        }

        protected void BtnSaveClick(object Sender, EventArgs E)
        {
            var space = new OrderData();
            space.UpdateSpace(OrderID, SpaceID, txtSpaceName.Text, txtSpaceID.Text,bool.Parse(ddlTexture.SelectedValue),
                ddlDescription.SelectedValue, int.Parse(ddlColors.SelectedValue), txtNonElfa.Text, txtInstructions.Text,
                txtRemoval.Text.Trim(),txtColorName.Text.Trim());
        }

        void FillColor()
        {
            var color = new OrderData();
            _drInfo = color.GetColors();
            ddlColors.DataSource = _drInfo;
            ddlColors.DataTextField = "ColorName";
            ddlColors.DataValueField = "ColorID";
            ddlColors.DataBind();
            _drInfo.Close();
        }

        protected void BtnRemoveClick(object Sender, EventArgs E)
        {
            var space = new OrderData();
            space.RemoveSpace(OrderID,SpaceID);
        }

        protected void CbDeletedCheckedChanged(object Sender, EventArgs E)
        {
            if(cbDeleted.Checked)
            {
                var space = new OrderData();
                space.RemoveSpace(OrderID, SpaceID);
                cbDeleted.Enabled = false;
            }
        }

        protected void DdlTextureSelectedIndexChanged(object Sender, EventArgs E)
        {
            if (ddlTexture.SelectedValue == "True") ddlDescription.Enabled = true;
            else
            {
                ddlDescription.SelectedIndex = 0;
                ddlDescription.Enabled = false;
            }
        }
    }
}