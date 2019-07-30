<%@ Page Title="" Language="C#" MasterPageFile="~/SiteDesign.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="DefaultWithElecSign.aspx.cs" Inherits="TroposGoodsInProcured.DefaultWithElecSign" %>

<%@ Register Assembly="Tropos.Web.UI" Namespace="Tropos.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="tlk" %>
<%@ Register Assembly="TroposLookUp" Namespace="TroposLookUp" TagPrefix="tlu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="TroposAlertDialog" Namespace="TroposAlertDialog" TagPrefix="cc1" %>
<%@ Register Assembly="TroposModalPopup" Namespace="TroposModalPopup" TagPrefix="cc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ButtonPlaceHolder" runat="server">
    <!--Action Butttons go in the PANEL below - do not alter code outside this section -->
    <asp:Panel ID="developerButtons" CssClass="developerButtons" Style="height: 50px; width: 100%; overflow: visible;" runat="server">
        <asp:UpdatePanel ID="updButtons" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TroposContentPlaceHolder" runat="server">
    <!--User code all goes in the PANEL below - do not alter code outside this section -->
    <asp:Panel ID="developerCanvass" CssClass="developerCanvass" pagetitle="My RBA 1"
        Style="width: 100%; height: 2000px; background: url('style/ui_theme/silver/ui_images/ui_devCanvass.png') no-repeat 0px 0px;"
        runat="server">
        <asp:UpdatePanel ID="updContent" runat="server">
            <ContentTemplate>
                <cc1:TroposCustomValidator ID="tcv" runat="server" ErrorMessage="" Style="position: absolute; top: 0px; left: 5px;" Enabled="false" Display="None"></cc1:TroposCustomValidator>
                <div style="bottom: 0px; position: absolute">
                    <cc1:TroposErrorSummary ID="TroposErrorSummary" runat="server" />
                </div>
                <!-- Panel area -->
                <div class="ui_panel ui_form">
                    <asp:LinkButton runat="server" ID="showModalPopupServerOperatorButton" Text="" Style="visibility: hidden" />
                    <asp:Panel ID="pnlPopupDataValid" runat="server" Style="display: none;" CssClass="pnlPopupDataValid">
                        <div class="ui_fullscreenloader">
                            <div id="ui_Modal">
                                <!--  ui_Modal for standard Modal ui_ModalWide for extra large Modal -->
                                <div class="ui_modallgt ui_formdefault">
                                    <div class="ui_modal_wrapper">
                                        <div class="ui_modalContent">
                                            <div class="ui_question">
                                                <!-- alternatives ui_error, ui_warning, ui_question, ui_info -->
                                                <h2>
                                                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources : LBL_DATA_VALID %>" /></h2>
                                            </div>
                                            <div class="ui_fieldset" runat="server" id="DVSignature">
                                                <div class="ui_inputGroup">
                                                    <label class="pnlPopupDataValidItem">
                                                        <asp:Literal ID="litIdentity" runat="server" Text="<%$ Resources: LBL_Identity%>" />
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtDvIdentity" />
                                                </div>
                                                <div class="ui_inputGroup">
                                                    <label class="pnlPopupDataValidItem">
                                                        <asp:Literal ID="litPassword" runat="server" Text="<%$ Resources: LBL_Password%>" />
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtDvPassword" TextMode="Password" />
                                                </div>
                                                <div class="ui_inputGroup">
                                                    <label class="pnlPopupDataValidItem">
                                                        <asp:Literal ID="litESReason" runat="server" Text="<%$ Resources: LBL_ESReason%>" />
                                                    </label>
                                                    <asp:ListBox runat="server" ID="lstDvReason" />
                                                </div>
                                                <div class="ui_inputGroup">
                                                    <label class="pnlPopupDataValidItem">
                                                        <asp:Literal ID="litESComment" runat="server" Text="<%$ Resources: LBL_ESComment%>" />
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtDvComment" />
                                                </div>
                                                <asp:Label runat="server" ID="lblDvError" CssClass="pnlPopupDataValidItem" />
                                            </div>
                                            <div class="ui_modalaction">
                                                <span class="ui_buttonAlt">
                                                    <asp:LinkButton ID="btnBackground" OnClick="btnBackground_Click" runat="server" Text="<%$ Resources : BTN_BACKGROUND %>" />
                                                </span><span class="ui_buttonAlt">
                                                    <asp:LinkButton ID="btnDefer" OnClick="btnDefer_Click" runat="server" Text="<%$ Resources : BTN_DEFER %>" />
                                                </span><span class="ui_buttonAlt cancelButton">
                                                    <asp:LinkButton ID="btnNo" OnClick="btnNo_Click" runat="server" Text="<%$ Resources : BTN_NO %>" />
                                                </span><span class="ui_button" runat="server" id="spanYes">
                                                    <asp:LinkButton ID="btnYes" OnClick="btnYes_Click" runat="server" Text="<%$ Resources : BTN_YES %>" />
                                                </span>
                                                <br />
                                                <div id="DVError" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ui_modal_wrapperEnd">
                                        <div class="ui_modalContentEnd">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:ModalPopupExtender ID="pnlPopup_DataValid" runat="server" TargetControlID="showModalPopupServerOperatorButton"
                        BehaviorID="mdlPopupDataValid" PopupControlID="pnlPopupDataValid" BackgroundCssClass="modalBackground2">
                    </asp:ModalPopupExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="TransactionCallbackButton" runat="server" Width="1px" Height="1px"
            OnClick="TransactionCallbackButton_click" Style="visibility: hidden" />
        <span id="TransactionCallback" onclick='javascript: __doPostBack("<%= this.TransactionCallbackButton.UniqueID %>", "");'
            style="visibility: hidden"><a href="#"></a></span>
        <div runat="server" id="SharedLookup" class="ui_fullscreenloader" visible="false">
            <tlu:LookUp ID="SharedLookup_TLU" runat="server" />
        </div>
    </asp:Panel>
    <script type="text/javascript">
        function forceClick(e, elemId)
        {
            var elem = document.getElementById(elemId);
            var evt = (e) ? e : window.event;
            var intKey = (evt.which) ? evt.which : evt.keyCode;

            if (intKey == 13)
            {
                elem.click();
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        var AltDown = false;

        function ModalLoaded()
        {
            $("#ui_Modal").keydown(function (event)
            {
                var ModalArea = $("#ui_Modal");
                switch (event.keyCode)
                {
                    case 13:
                        __doPostBack("<%= this.btnYes.UniqueID %>", "");
                        ModalUnloaded();
                        break;
                    case 27:
                        __doPostBack("<%= this.btnNo.UniqueID %>", "");
                        ModalUnloaded();
                        break;
                    case 68:        // D
                        if (AltDown)
                        {
                            __doPostBack("<%= this.btnDefer.UniqueID %>", "");
                            ModalUnloaded();
                            AltDown = false;
                        }
                        break;
                    case 66:        // B
                        if (AltDown)
                        {
                            __doPostBack("<%= this.btnBackground.UniqueID %>", "");
                        ModalUnloaded();
                        AltDown = false;
                    }
                    break;
                case 17:        // AltGr
                case 18:        // Alt
                    AltDown = true;
                    break;
            }
            }
        )
        $("#ui_Modal").keyup(function (event)
        {
            var ModalArea = $("#ui_Modal");
            switch (event.keyCode)
            {
                case 17:        // AltGr
                case 18:        // Alt
                    AltDown = false;
                    break;
            }
        }
    )
    }
    function ModalUnloaded()
    {
        $("#ui_Modal").unbind("keydown");
        $("#ui_Modal").unbind("keyup");
    }


    </script>


</asp:Content>
