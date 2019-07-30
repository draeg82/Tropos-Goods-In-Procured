<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="Login.aspx.cs" Inherits="TroposGoodsInProcured.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tropos Login</title>
    <link href="http://localhost/troposro/style/ui_theme/silver/TroposUI.css" rel="stylesheet" type="text/css" media="all" />
    <link href="http://localhost/troposro/style/ui_theme/silver/TroposUIPrint.css" rel="stylesheet" type="text/css" media="print" />
    <link href="http://localhost/troposro/style/ui_theme/silver/ui_core.css" rel="stylesheet" type="text/css" />
    <link href="http://localhost/troposro/style/ui_theme/silver/ui_nav.css" rel="stylesheet" type="text/css" />
    <link href="http://localhost/troposro/style/ui_theme/silver/ui_panels.css" rel="stylesheet" type="text/css" />
    <link href="http://localhost/troposro/style/ui_theme/silver/ui_forms.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="~/images/favicon.ico" type="image/vnd.microsoft.icon" />
    <link rel="icon" href="~/images/favicon.ico" type="image/vnd.microsoft.icon" />
    <!--[if IE]><link href="style/ui_theme/silver/ui_ie.css" rel="stylesheet" type="text/css" /><![endif]-->
</head>
<body>
<form id="form1" runat="server" defaultbutton="btnLogon">
<div id="ui_header_bar">
  <div id="ui_brand"><span>Epicor</span></div>
</div>

<div id=ui_header_menu></div>

<div id="ui_workspace">


    <div id=ui_login>
        <div class="ui_ident tropos"></div>
        <div class="ui_modaldrk ui_formdefault">
            <div class=ui_modal_wrapper>
                <div class="ui_modalContent">
                    <div class=ui_inputGroup>
                        <label>Server</label> 
                        <asp:TextBox ID="txtServer" runat="server" OnLoad="txtServer_Load"></asp:TextBox>
                    </div>
                    <div class="ui_inputGroup">
                        <label>Database</label> 
                        <asp:TextBox ID="txtDatabase" runat="server"></asp:TextBox>
                    </div>
                    <div class="ui_lineBreak"></div>
                    <div class="ui_inputGroup">
                        <label> <asp:label ID="lblIdentity" runat="server">Identity</asp:label></label>
                         <asp:TextBox ID="txtIdentity" runat="server"></asp:TextBox>
                    </div>
                    <div class="ui_inputGroup">
                        <label> <asp:label ID="lblPassword" runat="server">Password</asp:label></label> 
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox> 
                    </div>
                    <div class="ui_inputGroup">
                        <label>Business</label> 
                        <asp:TextBox ID="txtBusiness" runat="server" ></asp:TextBox>
                    </div>
                    <div class="ui_inputGroup" >                           
                            <label> <asp:label ID="lblWindowsAuth" runat="server" OnLoad="lblWindowsAuth_Load">Use Windows Authentication</asp:label></label> 
                            <asp:CheckBox ID="chkWindowsAuth" runat="server"  AutoPostBack="true"
                                    oncheckedchanged="chkWindowsAuth_CheckedChanged" />
                    </div>
                    <div class="ui_inputGroup">
                        <label>Manager</label> 
                        <asp:CheckBox ID="chkManager" runat="server" />
                    </div>
                    <div class="ui_inputGroup">
                        <label>Save Settings</label> 
                        <asp:CheckBox ID="chkSaveSettings" runat="server" />
                    </div>
                    <div class="ui_submit">
                        <span class="ui_button" style="">
                            <asp:Button style="width: 100px"  ID="btnLogon" runat="server" Text="Logon" OnClick="btnLogon_Click" />
                        </span>
                        
                        
                    </div>
                    <div class="ui_error" id="loginError" style="display:none">
                        <asp:Literal ID="lblError" runat="server"></asp:Literal>
                    </div>
                </div>
            </div> <!--End of ui_modal_wrapper -->
            <div class=ui_modal_wrapperEnd>
                <div class=ui_modalContentEnd></div>
            </div>
        </div>
    </div> <!--End of ui_login -->
</div> <!--End of ui_workspace -->
                         
                           
</form>
<script language="javascript">

var errMessage=document.getElementById("loginError");
if (errMessage!=null)
{
    
    var errMessageText=errMessage.innerHTML;
    if (errMessageText.length>2)
        errMessage.style.display="block";
}

</script>
</body>
</html>
