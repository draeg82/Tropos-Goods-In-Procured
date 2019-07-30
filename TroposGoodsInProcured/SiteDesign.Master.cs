using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TroposGoodsInProcured
{
    public partial class SiteDesign : TroposUI.Common.UI.TroposMasterPage//System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e) { }

        public override PlaceHolder TroposButtonRegion => TroposButtons;
    }
}
