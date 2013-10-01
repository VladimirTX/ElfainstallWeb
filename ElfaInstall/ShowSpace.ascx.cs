using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElfaInstall
{
    public partial class ShowSpace : System.Web.UI.UserControl
    {
        public int OrderID;
        public int SpaceID;
        public string SpaceName;
        public string SpaceNumber;
        public bool Texture;
        public string Description;
        public string Color;
        public string NonElfa;
        public string Instruction;
        public string Removal;
        public string ColorName;

        protected void Page_Load(object Sender, EventArgs E)
        {
            if (!IsPostBack)
            {
                lblSpaceName.Text = SpaceName.Trim();
                lblSpaceID.Text = SpaceNumber.Trim();
                bool texture = bool.Parse(Texture.ToString());
                if (texture) lblTexture.Text = "Y";
                else lblTexture.Text = "N";
                lblDescription.Text = Description;
                lblColor.Text = Color;
                lblNonElfa.Text = NonElfa;
                lblInstructions.Text = Instruction;
                lblRemoval.Text = Removal;
                lblColorName.Text = ColorName;
            }
        }
    }
}