<%@ Page Title="" Language="C#" MasterPageFile="~/SiteDesign.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="RejectNoteInformation.aspx.cs" Inherits="TroposGoodsInProcured.Popups.RejectNoteInformation" %>

<%@ Register Assembly="Tropos.Web.UI" Namespace="Tropos.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="tlk" %>
<%@ Register Assembly="TroposLookUp" Namespace="TroposLookUp" TagPrefix="tlu" %>
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
    <asp:Panel ID="developerCanvass" CssClass="developerCanvass" pagetitle="Reject Note Information"
        Style="width: 100%; height: 2000px; background: url('style/ui_theme/silver/ui_images/ui_devCanvass.png') no-repeat 0px 0px;"
        runat="server">
        <asp:UpdatePanel ID="updContent" runat="server">
            <ContentTemplate>

                <cc1:TroposGridView ID="grdREJECTNOTEINFORMATION" runat="server" Style="position: relative; top: 50px; left: 50px; height: 100%;" AutoGenerateColumns="false" AllowPaging="true" PageSize="50">

                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlRejectNotNumber" runat="server" Text="Reject Note Number" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Eval("Reject_Note_No")%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlRejectReason" runat="server" Text="Reject Reason" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Eval("Reject_Reason")%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlAccepted_Planned" runat="server" Text="Accepted Planned" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Accepted_Planned"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlAcceptedActual" runat="server" Text="Accepted Actual" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Accepted_Actual"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlSupplierLiablePlanned" runat="server" Text="Supplier Liable Planned" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Supplier_Liable_Planned"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlSupplierLiableActual" runat="server" Text="Supplier Liable Actual" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Supplier_Liable_Actual"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlCustomerLiablePlanned" runat="server" Text="Customer Liable Planned" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Customer_Liable_Planned"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlCustomerLiableActual" runat="server" Text="Customer Liable Actual" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Customer_Liable_Actual"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                </cc1:TroposGridView>


                <cc1:TroposCustomValidator ID="tcv" runat="server" ErrorMessage="" Style="position: absolute; top: 0px; left: 5px;" Enabled="false" Display="None"></cc1:TroposCustomValidator>
                <div style="bottom: 0px; position: absolute">
                    <cc1:TroposErrorSummary ID="TroposErrorSummary" runat="server" />
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
        function forceClick(e, elemId) {
            var elem = document.getElementById(elemId);
            var evt = (e) ? e : window.event;
            var intKey = (evt.which) ? evt.which : evt.keyCode;

            if (intKey == 13) {
                elem.click();
                return false;
            }
        }


    </script>
</asp:Content>
