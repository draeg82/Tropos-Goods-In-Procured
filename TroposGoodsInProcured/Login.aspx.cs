using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using TroposUI.Common.Context;
using TroposUI.Common.Globalisation;
using TroposUI.Common.DAL;
using TroposUI.Common;
using TroposUI.Common.Exception;
using System.Text;

namespace TroposGoodsInProcured
{
    public partial class Login : TroposUI.Common.UI.TroposPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
        }

        protected void lblWindowsAuth_Load(object sender, EventArgs e)
        {
            bool Authenticated = HttpContext.Current.User.Identity.IsAuthenticated;
            chkWindowsAuth.Visible = Authenticated;
            lblWindowsAuth.Visible = Authenticated;
            if (!Authenticated)
            {
                chkWindowsAuth.Checked = false;
                ShowHideFields();
            }
        }

        protected void txtServer_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    UserContext context = (UserContext)Session["UserContext"];
                    if (context != null)
                    {
                        TroposCS.LogOff(context);
                    }
                    HttpCookie LoginCookie = Request.Cookies["TroposLogin"];
                    if (LoginCookie != null)
                    {
                        txtServer.Text = LoginCookie.Values["Server"];
                        txtDatabase.Text = LoginCookie.Values["Database"];
                        txtIdentity.Text = LoginCookie.Values["Identity"];
                        txtBusiness.Text = LoginCookie.Values["Business"];
                        chkManager.Checked = bool.Parse(LoginCookie.Values["Manager"]);
                        string WindowsAuth = LoginCookie.Values["WindowsAuth"];
                        if (WindowsAuth != null && chkWindowsAuth.Visible)
                        {
                            chkWindowsAuth.Checked = bool.Parse(WindowsAuth);
                        }
                        ShowHideFields();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return;

            }
        }

        protected void btnLogon_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chkSaveSettings.Checked)
                {
                    HttpCookie LoginCookie = new HttpCookie("TroposLogin");
                    //LoginCookie.Path = Request.ApplicationPath;
                    LoginCookie.Values.Add("Server", txtServer.Text);
                    LoginCookie.Values.Add("Database", txtDatabase.Text);
                    LoginCookie.Values.Add("Identity", txtIdentity.Text);
                    LoginCookie.Values.Add("Business", txtBusiness.Text);
                    LoginCookie.Values.Add("Manager", chkManager.Checked.ToString());
                    LoginCookie.Values.Add("WindowsAuth", chkWindowsAuth.Checked.ToString());
                    LoginCookie.Expires = DateTime.Now.AddYears(1); ;
                    Response.Cookies.Add(LoginCookie);
                }

                WindowsImpersonationContext Ctx = null;
                UserContext context;
                if (chkWindowsAuth.Checked)
                {
                    context = new UserContext(txtServer.Text, txtDatabase.Text, txtBusiness.Text);
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        WindowsIdentity WinId = (WindowsIdentity)HttpContext.Current.User.Identity;
                        Ctx = WinId.Impersonate();
                    }
                }
                else
                {
                    context = new UserContext(txtServer.Text, txtDatabase.Text, txtIdentity.Text, txtPassword.Text, txtBusiness.Text, chkManager.Checked);
                }

                //Store this applications url prefix for use when launching items from the menu
                context.ParentUrlPrefix = UrlHelper.UrlPrefix();

                bool attachedByMe = false;
                try
                {
                    context.Load(Session);
                    //This is a hack to load the resource management libraries for the 1st user to log in
                    if (!context.AttachedToTropos)
                    {
                        TroposCS.Attach(context);
                        attachedByMe = true;
                    }
                    TroposResourceProvider _trp = new TroposResourceProvider(context);
                    _trp.GetResource("TEST");
                }
                finally
                {
                    if (attachedByMe)
                        TroposCS.Detach(context);
                    if (Ctx != null)
                        Ctx.Undo();
                }

                // Create the user data
                string userDataString = string.Concat(txtServer.Text, "|"
                                                    , txtDatabase.Text, "|"
                                                    , txtIdentity.Text, "|"
                                                    , txtPassword.Text, "|"
                                                    , txtBusiness.Text, "|"
                                                    , chkManager.Checked.ToString(), "|"
                                                    , context.ParentUrlPrefix, "|"
                                                    , context.WindowsAuthentication.ToString());

                // Create the cookie that contains the forms authentication ticket
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(txtIdentity.Text, false);

                // Get the FormsAuthenticationTicket out of the encrypted cookie
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                // Create a new FormsAuthenticationTicket that includes our custom User Data
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, userDataString);

                // Update the authCookie's Value to use the encrypted version of newTicket
                authCookie.Value = FormsAuthentication.Encrypt(newTicket);

                // Manually add the authCookie to the Cookies collection
                Response.Cookies.Add(authCookie);

                // Determine redirect URL and send user there
                string redirUrl = FormsAuthentication.GetRedirectUrl(txtIdentity.Text, false);
                Response.Redirect(redirUrl);
                //FormsAuthentication.RedirectFromLoginPage(this.UserContext.TroposIdentity, false);

            }
            catch (TroposDbUnexpectedErrorException)
            {
                TroposResourceProvider trp = new TroposResourceProvider(this.UserContext);
                lblError.Text = trp.GetResource("MSG_DB_ERROR");
            }
            catch (TroposDbTableExistsException)
            {
                TroposResourceProvider trp = new TroposResourceProvider(this.UserContext);
                lblError.Text = trp.GetResource("MSG_DB_ERROR");
            }
            catch (TroposDbPermissionsException)
            {
                TroposResourceProvider trp = new TroposResourceProvider(this.UserContext);
                lblError.Text = trp.GetResource("MSG_DB_PERMISSIONS");
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return;
            }
        }

        protected void btnBypass_Click(object sender, EventArgs e)
        {
            FormsAuthentication.RedirectFromLoginPage("Admin", false);
        }

        protected void chkWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideFields();
        }

        private void ShowHideFields()
        {
            txtIdentity.Visible = !chkWindowsAuth.Checked;
            txtPassword.Visible = !chkWindowsAuth.Checked;
            lblIdentity.Visible = !chkWindowsAuth.Checked;
            lblPassword.Visible = !chkWindowsAuth.Checked;
            if (string.IsNullOrEmpty(txtServer.Text))
                txtServer.Focus();
            else if (string.IsNullOrEmpty(txtDatabase.Text))
                txtDatabase.Focus();
            else if (string.IsNullOrEmpty(txtIdentity.Text) && txtIdentity.Visible)
                txtIdentity.Focus();
            else if (txtPassword.Visible)
                txtPassword.Focus();
            else
                txtDatabase.Focus();
        }
    }
}
