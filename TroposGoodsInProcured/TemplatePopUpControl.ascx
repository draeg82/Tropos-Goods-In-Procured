<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TemplatePopUpControl.ascx.cs" Inherits="TroposGoodsInProcured.TemplatePopUpControl" %>
<%@ Register assembly="Tropos.Web.UI" namespace="Tropos.Web.UI" tagprefix="cc1" %>
<!--User code all goes in the DIV below - do not alter code outside this section -->
<div class="popupCanvass" style="width: 100%; height: 800px; background: url(style/ui_theme/silver/ui_images/ui_modalLgt.png) no-repeat 0px 0px;"> 
    <cc1:TroposConnector ID="TroposConnector1" runat="server" 
        style="position: absolute; top: 61px; left: 746px;" />
    <cc1:TroposInput ID="TroposInput1" runat="server" 
        style="position: absolute; top: 66px; left: 49px;"></cc1:TroposInput>

    <asp:LinkButton ID="LinkButton1" runat="server" OnCommand="closePopup" 
        
        style="position: absolute; top: 68px; left: 304px; right: 819px; width: 107px;">Close Popup</asp:LinkButton>
</div>



<div id="designTimeFeatures">
<link href="style/ui_theme/silver/TroposUI.css" rel="stylesheet" type="text/css" media="all" />
<link href="style/ui_theme/silver/ui_core.css" rel="stylesheet" type="text/css" media="all" />
<link href="style/ui_theme/silver/ui_nav.css" rel="stylesheet" type="text/css" media="all" />
<link href="style/ui_theme/silver/ui_panels.css" rel="stylesheet" type="text/css" media="all" />
<link href="style/ui_theme/silver/ui_forms.css" rel="stylesheet" type="text/css" media="all" /></div>
