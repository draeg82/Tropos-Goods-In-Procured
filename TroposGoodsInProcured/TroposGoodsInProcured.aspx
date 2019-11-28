<%@ Page Title="" Language="C#" MasterPageFile="~/SiteDesign.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="TroposGoodsInProcured.aspx.cs" Inherits="TroposGoodsInProcured.TroposGoodsInProcured" %>

<%@ Register Assembly="Tropos.Web.UI" Namespace="Tropos.Web.UI" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="tlk" %>
<%@ Register Assembly="TroposLookUp" Namespace="TroposLookUp" TagPrefix="tlu" %>
<%@ Register Assembly="TroposAlertDialog" Namespace="TroposAlertDialog" TagPrefix="cc2" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ButtonPlaceHolder" runat="server">
    <!--Action Butttons go in the PANEL below - do not alter code outside this section -->
    <asp:Panel ID="developerButtons" CssClass="developerButtons" Style="height: 50px; width: 100%; overflow: visible;" runat="server">
        <asp:UpdatePanel ID="updButtons" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cc1:TroposActionButton ID="tab_Clear" runat="server" Style='position: relative;' Text="Clear Details" ButtonStyle="TopMenu" OnClick="tab_Clear_Click"></cc1:TroposActionButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="TroposContentPlaceHolder" runat="server">
    <!--User code all goes in the PANEL below - do not alter code outside this section -->
    <asp:Panel ID="developerCanvass" CssClass="developerCanvass" pagetitle="Procured Raw Material Intake"
        Style="width: 100%; height: 2000px; background: url('style/ui_theme/silver/ui_images/ui_devCanvass.png') no-repeat 0px 0px;"
        runat="server">
        <asp:UpdatePanel ID="updConfirm" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cc2:TroposAlert ID="TransAlert" runat="server" IconClass="ui_question" Message="You are about to book in this line.  Are you sure?"
                    Visible="false" Title="">
                    <buttons> 
                                <asp:Button ID="btnOK" runat="server" Text="Ok"/>  
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />                         
                            </buttons>
                </cc2:TroposAlert>
            </ContentTemplate>
        </asp:UpdatePanel>

        <%--Update panel holding confirmation message for successful booking in of goods to a line--%>
        <asp:UpdatePanel ID="updSuccessMessage" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <cc2:TroposAlert ID="AlertSUCCESS" runat="server" IconClass="ui_info" Message="Update Successful" Visible="false" Title="">
                    <buttons>
                        <asp:Button ID="btnSuccessOK" runat="server" Text="Ok" />
                    </buttons>
                </cc2:TroposAlert>
            </ContentTemplate>
        </asp:UpdatePanel>




        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <%--                <cc1:TroposLabel ID="vREQUIRED_DATE" Text="Required Date" runat="server" Style='position: absolute; top: 25px; left: 25px; width: 100px;'></cc1:TroposLabel>
                <cc1:TroposBusinessCalendar ID="tbcDATE_REQUIRED" runat="server" Style='position: absolute; top: 25px; left: 140px; width: 100px;' Width="120px" OnDateSelected="tbcDATE_REQUIRED_DateSelected"></cc1:TroposBusinessCalendar>--%>

                <cc1:TroposLabel ID="vPURCHASE_ORDER_DETAIL" Text="Purchase Order" runat="server" Style='position: absolute; top: 50px; left: 25px; width: 100px;'></cc1:TroposLabel>
                <%-- <cc1:TroposDAVL ID="iPURCHASE_ORDER" DavlDropDownType="CodeOnly" DavlType="UserDefined" runat="server" Style='position: absolute; top: 50px; left: 140px; width: 100px;' Width="100px" Text=""></cc1:TroposDAVL>--%>
                <asp:Panel ID="panel1" runat="server" DefaultButton="EnterPO">
                    <cc1:TroposInput ID="iPURCHASE_ORDER" runat="server" Style='position: absolute; top: 50px; left: 140px; width: 200px;' Width="100px" LookUpCode="PORDER_PROCURED_OUTSTANDING" loo></cc1:TroposInput>
                    <asp:Button ID="EnterPO" runat="server" Text="Enter PO" Style="position: absolute; top: 50px; left: 250px; display: none;" />
                </asp:Panel>
                <cc1:TroposLabel ID="vSUPPLIER_LABEL" Text="Grower" runat="server" Style='position: absolute; top: 75px; left: 25px; width: 75px;' Width="75px" Visible="false"></cc1:TroposLabel>
                <cc1:TroposLabel ID="vSUPPLIER" runat="server" Style='position: absolute; top: 75px; left: 140px; width: 250px;' Visible="false"></cc1:TroposLabel>

                <cc1:TroposLabel ID="vINCLUDECOMPLETE_LABEL" Text="Include Completed Lines" runat="server" Style='position: absolute; top: 100px; left: 25px; width: 150px;' Width="150px" Visible="false"></cc1:TroposLabel>
                <cc1:TroposCheckbox ID="tcboxINCLUDECOMPLETE" runat="server" Style='position: absolute; top: 105px; left: 225px;' Visible="false" Checked="false" OnCheckedChanged="tcboxINCLUDECOMPLETE_CheckedChanged" AutoPostBack="true" ToolTip="Toggles display of lines where there is no outstanding items to receive." />

                <cc1:TroposGridView ID="grdGOODSIN" runat="server" Style="position: relative; top: 150px; left: 25px; height: 100%; width: 95%" AutoGenerateColumns="false" AllowPaging="false" PageSize="1000" Width="100%">

                    <AlternatingRowStyle CssClass="alt" />
                    <Columns>

                        <%-- Column 0 --%>
                        <asp:TemplateField HeaderStyle-Width="25px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Line No</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center><%# Eval("PORDITM_SCH")%> </center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 1 --%>
                        <asp:TemplateField HeaderStyle-Width="40px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Item No</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center><%# Eval("PARTNO_PORD")%></center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 2 --%>
                        <asp:TemplateField HeaderStyle-Width="300px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Description</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center><%# Eval("DESCRIPTION")%></center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 3 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Ordered Qty</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center><%# Eval("PORDQTY")%> <%# Eval("UOM_PURCH") %></center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 4 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Outstanding Qty</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center><%# Eval("OUTSTANDING_QTY")%> <%# Eval("UOM_PURCH") %></center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 5 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Receiving Qty</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <cc1:TroposInput ID="iRECEIVEDQTY" runat="server" Style="align-content: center; text-align: center; align-self: center;" Width="60px"></cc1:TroposInput>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 6 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Advice Note</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <cc1:TroposInput ID="iADVICENOTE" runat="server" Style="align-content: center; text-align: center; align-self: center;" Width="60px"></cc1:TroposInput>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 7 --%>
                        <asp:TemplateField HeaderStyle-Width="80px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Receied Date</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <cc1:TroposBusinessCalendar ID="tbcRECEIVEDDATE" runat="server" Text='<%#Eval("TODAY")%>' Width="90px"></cc1:TroposBusinessCalendar>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 8 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Store</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <cc1:TroposInput ID="iSTORE" runat="server" Style="align-content: center; text-align: center; align-self: center;" Width="60px"></cc1:TroposInput>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 9 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Location</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <cc1:TroposInput ID="iLOCATION" runat="server" Style="align-content: center; text-align: center; align-self: center;" Width="60px"></cc1:TroposInput>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 10 --%>
                        <asp:TemplateField HeaderStyle-Width="100px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Country of Origin</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="iCOUNTRYOFORIGIN" Style="align-content: center; text-align: center; align-self: center;" Width="100px"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- column 11 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Container Type</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="iCONTAINERTYPE" Style="align-content: center; text-align: center; align-self: center;" Width="60px">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 12 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Number of Containers</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <cc1:TroposInput ID="iNUMBEROFCONTAINERS" runat="server" Style="align-content: center; text-align: center; align-self: center;" Width="60px"></cc1:TroposInput>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- column 13 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Pallet Type</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="iPALLETTYPE" Style="align-content: center; text-align: center; align-self: center;" Width="60px">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 14 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Number of Pallets</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <cc1:TroposInput ID="iNUMBEROFPALLETS" runat="server" Style="align-content: center; text-align: center;" Width="60px"></cc1:TroposInput>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- Column 15 --%>
                        <asp:TemplateField HeaderStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Delivered Qty to Date</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center><%# Eval("PORDREC")%> <%# Eval("UOM_PURCH") %></center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- column 16 --%>
                        <asp:TemplateField HeaderStyle-Width="40px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>GRN</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center>
                                    <asp:LinkButton ID="lnkGRN" runat="server" ForeColor="Black" Font-Underline="true" OnCommand="lnkGRN_Command" CommandArgument='<%# Eval("GRNUMBER")%>'>
                                    <%# Eval("GRNUMBER")%></center>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- column 17 --%>
                        <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Write Details</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="imgEXECUTE_BTN" runat="server" onblur="pressUpdateButton(this.id)" OnCommand="imgEXECUTE_BTN_Command" CommandArgument="<%# Container.DataItemIndex%>" ImageUrl="~/images/icons/save.png" Height="20px" Width="20px" /></center>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-- column 18 --%>
                        <asp:TemplateField HeaderStyle-Width="30px" ItemStyle-VerticalAlign="Middle">
                            <HeaderTemplate>
                                <center>Clear Details</center>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <center>
                                    <asp:ImageButton ID="tab_Clear_main" runat="server" OnCommand="tab_Clear_main_Click" CommandArgument="<%# Container.DataItemIndex%>" ImageUrl="~/images/icons/Clear.png" Height="20px" Width="20px"></asp:ImageButton>
                                </center>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                </cc1:TroposGridView>


                <cc1:TroposCustomValidator ID="tcv" runat="server" ErrorMessage="" Style="position: absolute; top: 0px; left: 5px;" Enabled="false" Display="None"></cc1:TroposCustomValidator>
                <div style="bottom: 0px; position: absolute">
                    <cc1:TroposErrorSummary ID="TroposErrorSummary1" runat="server" />
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

        function updateRow() {
            __doPostBack('imgEXECUTE_BTN', 'OnClick');

        }

        function pressUpdateButton(id) {
            document.getElementById(id).click();
        }



    </script>
</asp:Content>
