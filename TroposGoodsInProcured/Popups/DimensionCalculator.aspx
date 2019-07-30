<%@ Page Title="" Language="C#" MasterPageFile="~/SiteDesign.Master" AutoEventWireup="true"  EnableEventValidation="false"
    CodeBehind="DimensionCalculator.aspx.cs" Inherits="TroposGoodsInProcured.DimensionCalculator" %>

<%@ Register Assembly="Tropos.Web.UI" Namespace="Tropos.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="tlk" %>
<%@ Register Assembly="TroposLookUp" Namespace="TroposLookUp" TagPrefix="tlu" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonPlaceHolder" runat="server">
    <!--Action Butttons go in the PANEL below - do not alter code outside this section -->
    <asp:Panel id="developerButtons" CssClass="developerButtons" style="height: 50px; width: 100%; overflow: visible;" runat="server">
        <asp:UpdatePanel ID="updButtons" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                                <asp:Button ID="DefaultButton" runat="server" Width="1px" Height="1px" OnClick="DefaultButton_Click" style="visibility:hidden"/>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TroposContentPlaceHolder" runat="server">
    <!--User code all goes in the PANEL below - do not alter code outside this section -->
    <asp:Panel id="TroposFields" CssClass="developerCanvass" pagetitle="Dimension Calculator" style="width: 100%; height: 2000px;  background: url('style/ui_theme/silver/ui_images/ui_devCanvass.png') no-repeat 0px 0px;" runat="server">
        <div runat="server" id="SharedLookup" class="ui_fullscreenloader" visible="false">
            <tlu:LookUp ID="SharedLookup_TLU" runat="server" />
        </div>
    </asp:Panel>
    <script language="javascript">
        function FieldOnFocus(index)
        {
            return;
        }
        function KeyDown(TextField)
        {
            var e = window.event;
            if (e.keyCode != 13)
            {
                return;
            }

            TextField.blur();
            e.cancelBubble = true;
            // Date fields rely on the blur (above) to process the value the user has input.
            // Wait 10 milliseconds before clicking the button to allow the date field to finish its processing.
            window.setTimeout("_ClickDefaultButton()", 10);
        }

        function _ClickDefaultButton()
        {
            var DefaultButton = $get(DefaultButtonId);
            DefaultButton.click();
        }

        function DecideContext()
        {
            return false;
        }

        var FFFtimer;       // Allow the site master to reference the FocusFirstField timer so that it can cancel it if it wishes (if it pops up another page).
        var _FocusControl;
        function FocusFirstField()
        {
            //debugger;
            _FocusControl = null;
            var FirstControl = null;
            var allInputFields = document.getElementsByTagName("input");
            for (i = 0; i < allInputFields.length; i++)
            {
                var control = allInputFields[i];
                if (control.attributes.getNamedItem("FieldIndex") != null)
                {
                    if (FirstControl == null) FirstControl = control;
                    if (control.attributes["Highlight"] != null)
                    {
                        _FocusControl = control;
                        // Setting focus on a control here does not work, so set a timeout to do it after the
                        // page is complete and control returns to IE.
                        FFFtimer = window.setTimeout("_FocusFirstField()", 1);
                        return;
                    }
                }
            }
            // If we get here, then no control was highlit.
            _FocusControl = FirstControl;
            FFFtimer = window.setTimeout("_FocusFirstField()", 1);
        }
        function _FocusFirstField()
        {
            try
            {
                _FocusControl.focus();
                _FocusControl.select();
            }
            catch (e) { }
        }


</script>
</asp:Content>
