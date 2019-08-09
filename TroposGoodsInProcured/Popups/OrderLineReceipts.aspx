<%@ Page Title="" Language="C#" MasterPageFile="~/SiteDesign.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="OrderLineReceipts.aspx.cs" Inherits="TroposGoodsInProcured.Popups.OrderLineReceipts" %>

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
    <asp:Panel ID="developerCanvass" CssClass="developerCanvass" pagetitle="Order Line Receipts"
        Style="width: 100%; height: 2000px; background: url('style/ui_theme/silver/ui_images/ui_devCanvass.png') no-repeat 0px 0px;"
        runat="server">
        <asp:UpdatePanel ID="updContent" runat="server">
            <ContentTemplate>

                <cc1:TroposGridView ID="grdORDERLINERECEIPTS" runat="server" Style="position: relative; top: 50px; left: 50px; height: 100%;" AutoGenerateColumns="false" AllowPaging="true" PageSize="50">

                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlGRN" runat="server" Text="GRN" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Eval("GRN_No")%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlAdviseQty" runat="server" Text="Advised Quantity" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Order_Qty"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlReceivedQty" runat="server" Text="Received Quantity" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Received_qty"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlAcceptedQty" runat="server" Text="Accepted Quantity" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Accepted_Qty"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlRejectedQuantity" runat="server" Text="Rejected Quantity" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Convert.ToInt32(Eval("Rejected_Qty"))%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlAdviceNote" runat="server" Text="Advice Note" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Eval("Advice_Note")%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlReceivedDate" runat="server" Text="Received Date" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Eval("Received_Date", "{0:dd/MM/yyyy}")%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlStatus" runat="server" Text="Status" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <%# Eval("GRN_Status")%>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <center>
                                    <asp:Literal ID="ltlRejectNoteNo" runat="server" Text="Reject Note" />
                                </center>
                            </HeaderTemplate>
                            <HeaderStyle Width="30px" />
                            <ItemStyle />
                            <ItemTemplate>
                                <center>
                                    <asp:LinkButton ID="lnkRejectNoteNo" runat="server" ForeColor="Black" Font-Underline="true" OnCommand="lnkRejectNoteNo_Command" CommandArgument='<%# Eval("Reject_Note_No")%>'>
                                    <%# Eval("Reject_Note_No")%>
                                    </asp:LinkButton>
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

    <asp:UpdatePanel ID="updDummy" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>

    <input id="screenWidth" type="hidden" name="screenWidth" value="" runat="server" />
    <input id="screenHeight" type="hidden" name="screenHeight" value="" runat="server" />



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

        $(function () {
            document.getElementById('<%= screenWidth.ClientID %>').value = screen.width;
             document.getElementById('<%= screenHeight.ClientID %>').value = screen.height;
         });


    </script>
</asp:Content>
