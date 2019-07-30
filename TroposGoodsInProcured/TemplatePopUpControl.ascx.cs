using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TroposGoodsInProcured
{
    public partial class TemplatePopUpControl : TroposUI.Common.UI.TroposUserControl
    {
        public event EventHandler onTroposPopupClosed;

        protected new void Page_Load(object sender, EventArgs e)
        {

        }

        protected void closePopup(object sender, CommandEventArgs e)
        {
            if (onTroposPopupClosed != null) onTroposPopupClosed(this, new EventArgs());
        }

    }
}