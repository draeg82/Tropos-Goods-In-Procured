/*
 * $Log: Site.Master.cs  $
 * Revision 1.32 2016/08/23 08:46:46BST Saovik Singh (saovik) 
 * 
 * Revision 1.31 2016/08/23 12:04:02IST Saovik Singh (saovik) 
 * 
 * Revision 1.30 2016/07/20 20:34:47IST Charles D P Miller (cdpm) 
 * Ensure that quick code is displayed in the quick code box (taken from the query string)
 * Revision 1.29 2015/12/29 13:49:16GMT Charles D P Miller (cdpm) 
 * Add processing for electronic signatures
 * Revision 1.28 2015/09/03 12:12:43BST Charles D P Miller (cdpm) 
 * Set script manager to release mode in Page init routine to avoid problems caused by additional checks on update panel IDs in debug mode in .NET 4
 * Revision 1.27 2014/12/05 09:36:26GMT Charles D P Miller (cdpm) 
 * Remove unnecessary code
 * Revision 1.26 2014/10/24 15:24:05BST Charles D P Miller (cdpm) 
 * If the TroposUI version is below 1.0.10, replace the Dsiplay:block style for the menu checkboxes with Display:inline, to better fit with the old TroposUI
 * Revision 1.26 2014/01/17 16:06:48GMT Charles D P Miller (cdpm) 
 * Rebuild
 * Revision 1.25 2014/01/17 14:58:59GMT Charles D P Miller (cdpm) 
 * Add facility for callback when popped up page is closed.
 * Revision 1.23 2013/12/19 15:12:16GMT Charles D P Miller (cdpm) 
 * Add processing to remember last field focussed.
 * Revision 1.21 2013/11/13 14:54:05GMT Charles D P Miller (cdpm) 
 * Encode the encrypted Tropos login string so that it is not corrupted by inclusion in a URL
 * Revision 1.20 2013/03/22 11:37:51GMT Charles D P Miller (cdpm) 
 * Correct icons.
 * Rebuild
 * Revision 1.19 2013/03/12 17:21:09GMT Charles D P Miller (cdpm) 
 * Reinstate changes dropped by last update, and provide Trigger option for printing by the server code
 * Revision 1.18 2012/11/07 14:31:26GMT Charles D P Miller (cdpm) 
 * Apply code analysis changes
 * Revision 1.16 2012/09/21 13:38:10BST Charles D P Miller (cdpm) 
 * Add Change Log
 */
using System;
using System.Configuration;
using System.Text;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TroposUI.Common.Context;
using TroposUI.Common.Globalisation;
using TroposUI.Common.Menu;
using TroposUI.Encryption;
using TroposUI.Common;
using System.Web;
using TroposUI.Common.Exception;
using System.Diagnostics;
using System.Collections.Generic;
using TDK.Common;
using System.Globalization;

namespace TroposWebClient
{
    public class PopupClosedEventArgs : EventArgs
    {
        public string QuickCode { get; set; }
        public PopupClosedEventArgs(string quickCode)
            : base()
        {
            QuickCode = quickCode;
        }
    }

    public class LaunchInDefaultEventArgs : EventArgs
    {
        public AbstractMenuItem MenuItem { get; set; }
        public string QuickCode { get; set; }
        public string Address { get; set; }
        public bool InMenu { get; set; }
        public LaunchInDefaultEventArgs(AbstractMenuItem menuItem, string quickCode, string address, bool inMenu)
            : base()
        {
            MenuItem = menuItem;
            QuickCode = quickCode;
            Address = address;
            InMenu = inMenu;
        }
    }

    public struct PrintOptions
    {
        public bool Trigger { get; set; }
        public bool Fit { get; set; }
        public bool Indirect { get; set; }
        public string trigger => Trigger.ToString().ToLower();
        public string fit => Fit.ToString().ToLower();
        public string indirect => Indirect.ToString().ToLower();
    }

    public delegate bool PageIsNavigatingDelegate(AbstractMenuItem fromMenuItem, AbstractMenuItem selectedMenuItem);

    public partial class Site : TroposUI.Common.UI.TroposMasterPage
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), Obsolete("Set LaunchInDefault event to an EventHandler<LaunchInDefaultEventArgs> object instead")]
        public delegate void LaunchInDefaultEventHandler(AbstractMenuItem MenuItem, string QuickCode, string Url, bool InMenu);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances"), Obsolete("Set OnClearFields event to an EventHandler object instead")]
        public delegate void ClearFieldsEventHandler(object sender, EventArgs e);

        public event EventHandler<PopupClosedEventArgs> OnPopupClosed;

        public PageIsNavigatingDelegate PageIsNavigating;

        [Obsolete("Use LaunchInDefault event instead")]
        public event LaunchInDefaultEventHandler LaunchInDefaultEvent;

        public event EventHandler<LaunchInDefaultEventArgs> LaunchInDefault;

        [Obsolete("Use OnClearFields event instead")]
        public event ClearFieldsEventHandler ClearFields;
        public event EventHandler OnClearFields;

        public event EventHandler OnTriggerPrint;

        private string TroposLogin = "";

        public ScriptManager MasterScriptManager => TWCScriptManager;
        public TextBox txtQuickCode => QC;
        public UpdatePanel upQuickCode => upQC;
        public override PlaceHolder TroposButtonRegion => TroposButtons;

        public PrintOptions printOptions;

        protected void Page_Init(object sender, EventArgs e)
        {
            TWCScriptManager.ScriptMode = ScriptMode.Release;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            phMenu.Controls.Clear();
            phMenu.Controls.Add(UserContext.Menus);

            // We have been called from a Tropos User Interface instance which has given us the login credentails.
            // Check that the credentials we have been passed are the same as those in the current context.
            // If not - something is seriously wrong so do not allow the user to continue.
            TroposLogin = HttpContext.Current.Request.QueryString["TroposLogin"];
            if (!string.IsNullOrEmpty(TroposLogin))
            {
                string TroposLoginClear = TroposEncryption.DecryptString(TroposLogin);
                string[] LoginDetails = TroposLoginClear.Split(new char[] { ':' });
                string TroposServer = LoginDetails[0];
                string TroposDatabase = LoginDetails[1];
                string TroposIdentity = LoginDetails[2];
                bool WindowsAuthentication = bool.Parse(LoginDetails[4]);
                if (this.UserContext.TroposServer == TroposServer &&
                    this.UserContext.TroposDatabase == TroposDatabase &&
                    this.UserContext.TroposIdentity == TroposIdentity &&
                    this.UserContext.WindowsAuthentication == WindowsAuthentication)
                {
                }
                else
                {
                    Session.Abandon();
                    throw new TroposConnectionException("Tropos login credentials from request do not match current Session", TroposUI.Common.DataUpdater.TroposError.Other, System.Diagnostics.EventLogEntryType.Error);
                }
            }

            TroposLogin = TroposEncryption.EncryptString(
                this.UserContext.TroposServer + ":" +
                this.UserContext.TroposDatabase + ":" +
                this.UserContext.TroposIdentity + ":" +
                (string.IsNullOrEmpty(UserContext.TroposPassword) ? "empty" : UserContext.TroposPassword) + ":" +
                this.UserContext.WindowsAuthentication.ToString() + ":" +
                this.UserContext.TroposBusiness + ":" +
                this.UserContext.TroposSession.SessionKey.TniSessionId);

            string Script = "var TroposLogin='" + TroposLogin + "';";
            Script += "var ParentUrlPrefix='" + this.UserContext.ParentUrlPrefix + "';";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "TroposLogin", Script, true);

            if (QC != null)
            {
                QC.Attributes.Add("onKeyPress", "doQuickCode('" + hdnQCEvent.ClientID + "',event)");
                QC.TabIndex = UserContext.QUICK_CODE_TAB_INDEX;
            }

            if (Session["menuStateArray"] != null)
            {
                if (!string.IsNullOrEmpty(menuStateControl.Value))
                {
                    Session["menuStateArray"] = menuStateControl.Value;
                }
                else
                {
                    menuStateControl.Value = Session["menuStateArray"].ToString();
                }
            }

            if (!Page.IsPostBack)
                SetQuickCode();

            if (!TWCScriptManager.IsInAsyncPostBack)
            {
                bool menuAutoOpen = false;
                bool menuAutoExpand = false;
                HttpCookie MenuCookie = Request.Cookies["TroposMenu"];
                if (MenuCookie != null)
                {
                    try
                    {
                        if (!bool.TryParse(MenuCookie.Values["AutoOpen"], out menuAutoOpen))
                            menuAutoOpen = false;
                        if (!bool.TryParse(MenuCookie.Values["AutoExpand"], out menuAutoExpand))
                            menuAutoExpand = false;
                    }
                    catch (Exception) { }
                }

                menuSingleOption.Checked = menuAutoOpen;
                menuAutoExpandOption.Checked = menuAutoExpand;
                UP_SingleMenu.Update();
            }
            if (new Version(Helpers.TroposUIVersion(this.UserContext)) < new Version("1.10.0"))
            {
                menuSingleOption.Style["display"] = "inline";
                menuAutoExpandOption.Style["display"] = "inline";
            }
        }

        protected void menuSingleOption_Changed(object sender, EventArgs e)
        {
            bool menuAutoExpand = menuAutoExpandOption.Checked;
            bool menuAutoOpen = menuSingleOption.Checked;
            HttpCookie MenuCookie = new HttpCookie("TroposMenu");
            MenuCookie.Values.Add("AutoExpand", menuAutoExpand.ToString());
            MenuCookie.Values.Add("AutoOpen", menuAutoOpen.ToString());
            MenuCookie.Expires = DateTime.Now.AddYears(1); ;
            Response.Cookies.Add(MenuCookie);
        }

        protected void menuAutoExpandOption_Changed(object sender, EventArgs e)
        {
            bool menuAutoExpand = menuAutoExpandOption.Checked;
            bool menuAutoOpen = menuSingleOption.Checked;
            HttpCookie MenuCookie = new HttpCookie("TroposMenu");
            MenuCookie.Values.Add("AutoExpand", menuAutoExpand.ToString());
            MenuCookie.Values.Add("AutoOpen", menuAutoOpen.ToString());
            MenuCookie.Expires = DateTime.Now.AddYears(1); ;
            Response.Cookies.Add(MenuCookie);
        }


        protected void UserInfo_Load(object sender, EventArgs e)
        {
            UserInfo.Text = string.Format("{0}&nbsp;&nbsp;({2} {3} {4})", UserContext.TroposUsername, ResourceProvider.GetResource("LBL_CONNECTED_TO"), UserContext.TroposDatabase, ResourceProvider.GetResource("LBL_ON"), UserContext.TroposServer);
            UserInfo.ToolTip = string.Format(@"{0}
{1} {2}", UserContext.TroposSession.UserEmail, ResourceProvider.GetResource("LBL_LANGUAGE"), UserContext.TroposSession.Language);
            ApplicationInfo.ToolTip = Request.ServerVariables["SERVER_NAME"];
        }
        protected void LogoutButton_Command(object sender, CommandEventArgs e)
        {
            ProcessLogout();
        }

        private void ProcessLogout()
        {
            string loginURL = string.Empty;
            if (this.UserContext != null)
            {
                loginURL = this.UserContext.ParentUrlPrefix + "login.aspx";
                this.UserContext.Unload(Session);
                Session.Abandon();
                Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
                // Clearing the ASP.Net session ID cookie causes the other applications to lose their session state,
                // so that they cannot carry on using the old Tropos connection
            }
            FormsAuthentication.SignOut();
            if (string.IsNullOrEmpty(loginURL))
                Response.Redirect("~/Login.aspx");
            else
                Response.Redirect(loginURL);
        }

        protected void CallbackEvent_Click(object sender, EventArgs e)
        {
            string QC = Request["__EVENTARGUMENT"];
            if (OnPopupClosed != null)
            {
                OnPopupClosed(this, new PopupClosedEventArgs(QC));
            }
        }

        protected void QCEvent_Click(object sender, EventArgs e)
        {
            DoQuickCode(QC.Text, false);
        }

        protected void QCNewPage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Reference.Text))
                DoQuickCode(QC.Text, true);
            else
            {
                DoQuickCode(QC.Text, Reference.Text, true, false, true);
                Reference.Text = "";
            }
        }

        protected void QCSamePage_Click(object sender, EventArgs e)
        {
            DoQuickCode(QC.Text, false);
        }

        public void DoQuickCode(string quickCode, bool newPage)
        {
            DoQuickCode(quickCode, "", newPage, true, false);
        }
        public void DoQuickCode(string quickCode, bool newPage, bool saveHistory)
        {
            DoQuickCode(quickCode, "", newPage, saveHistory, false);
        }

        public void DoQuickCode(string quickCode, string reference, bool newPage, bool saveHistory, bool autoExecute)
        {
            quickCode = quickCode.Trim();

            if (Array.Find(UserContext.ProhibitedActions, x => string.Compare(x.Trim(), quickCode.Trim(), StringComparison.OrdinalIgnoreCase) == 0) != null)
                // Some quick codes are just prohibited, so don't even try to process them.
                return;

            bool inMenu = false;
            TroposResourceProvider trp = new TroposResourceProvider(this.UserContext);
            if (quickCode == null || string.IsNullOrEmpty(quickCode.Trim()))
            {
                StringBuilder message = new StringBuilder();
                message.Append(trp.GetResource("MSG_QUICK_CODE_MISSING").Trim());
                message.Append(".");
                message.Append("<br/>");
                message.Append("<br/>");
                message.Append(trp.GetResource("MSG_ENTER_VALUE_AND_TRY_AGAIN").Trim());
                message.Append(" ");
                message.Append(trp.GetResource("MSG_OR_SELECT_FROM_MENU").Trim());
                message.Append(".");
                QCError(quickCode, message.ToString());
                return;
            }

            Session["menuStateArray"] = menuStateControl.Value;  //Save current menu state in session for cross page navigation

            AbstractMenuItem _mi;
            //Find the quick code in the user menu
            if (UserContext.QuickCodes.Find(x => string.Compare(x.Trim(), quickCode.Trim(), StringComparison.OrdinalIgnoreCase) == 0) == null)
            {
                inMenu = false;
                if (UserContext.BypassMenus ||
                    Array.Find(UserContext.PermittedActions, x => string.Compare(x.Trim(), quickCode.Trim(), StringComparison.OrdinalIgnoreCase) == 0) != null)
                {
                    //Not found in user menu, try to get details from Tropos
                    MenuItemFactory _mif = new MenuItemFactory();
                    _mi = _mif.ItemForQuickCode(this.UserContext, quickCode);
                    if (_mi == null)
                    {
                        StringBuilder message = new StringBuilder();
                        message.AppendFormat(trp.GetResource("MSG_QUICK_CODE_X_DOES_NOT_EXIST").Trim(), quickCode.Trim());
                        message.Append(".");
                        message.Append("<br/>");
                        message.Append("<br/>");
                        message.Append(trp.GetResource("MSG_ENTER_VALUE_AND_TRY_AGAIN").Trim());
                        message.Append(".");
                        QCError(quickCode, message.ToString());
                        return;
                    }
                }
                else
                {
                    StringBuilder message = new StringBuilder();
                    message.AppendFormat(trp.GetResource("MSG_QUICK_CODE_X_DOES_NOT_EXIST").Trim(), quickCode.Trim());
                    message.Append(" ");
                    message.Append(trp.GetResource("MSG_IN_YOUR_MENU").Trim());
                    message.Append(".");
                    message.Append("<br/>");
                    message.Append("<br/>");
                    message.Append(trp.GetResource("MSG_ENTER_VALUE_AND_TRY_AGAIN").Trim());
                    message.Append(" ");
                    message.Append(trp.GetResource("MSG_OR_SELECT_FROM_MENU").Trim());
                    message.Append(".");
                    QCError(quickCode, message.ToString());
                    return;
                }
            }
            else
            {
                inMenu = true;
                _mi = UserContext.TroposRoles.Find(x =>
                    ((x is TroposUI.Common.Menu.Transaction && x.TransactionList.IndexOf(quickCode.Trim(), StringComparison.OrdinalIgnoreCase) >= 0)
                    || x.TransactionList.ToUpper(CultureInfo.InvariantCulture) == quickCode.ToUpper(CultureInfo.InvariantCulture).Trim()));
                if (_mi == null)
                {
                    StringBuilder message = new StringBuilder();
                    message.AppendFormat(trp.GetResource("MSG_QUICK_CODE_X_DOES_NOT_EXIST").Trim(), quickCode.Trim());
                    message.Append(" ");
                    message.Append(trp.GetResource("MSG_IN_YOUR_MENU").Trim());
                    message.Append(".");
                    message.Append("<br/>");
                    message.Append("<br/>");
                    message.Append(trp.GetResource("MSG_REFRESH_PAGE_AND_TRY_AGAIN").Trim());
                    message.Append(".");
                    QCError(quickCode, message.ToString());
                    return;
                }
            }

            if (!(newPage || _mi.LaunchInNewWindow) || !_mi.AllowNewWindow)
            {
                //selectedNode.Value = _mi.ChildCode;
                selectedNode.Value = _mi.TroposUniqueMenuID.ToString(CultureInfo.InvariantCulture);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "reinstateMenu", "$(initialise);$(forceMenuSelection);", true);
            }

            LaunchMenuItem(_mi, quickCode, reference, newPage, inMenu, saveHistory, autoExecute);
        }

        private void QCError(string QuickCode, string ErrorMessage)
        {

            TroposAlert_QuickCode.Message = ErrorMessage;
            TroposAlert_QuickCode.Visible = true;
            upQCError.Update();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            TroposAlert_QuickCode.Visible = false;
            upQCError.Update();
        }

        protected void SM_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            TWCScriptManager.AsyncPostBackErrorMessage = e.Exception.Message;
        }
        public void LaunchMenuItem(AbstractMenuItem MenuItem, string QuickCode, bool NewPage, bool InMenu)
        {
            LaunchMenuItem(MenuItem, QuickCode, "", NewPage, InMenu, true, false);
        }

        public void LaunchMenuItem(AbstractMenuItem MenuItem, string QuickCode, bool NewPage, bool InMenu, bool saveHistory)
        {
            LaunchMenuItem(MenuItem, QuickCode, "", NewPage, InMenu, saveHistory, false);
        }

        public void LaunchMenuItem(AbstractMenuItem MenuItem, string QuickCode, string reference, bool NewPage, bool InMenu, bool saveHistory, bool autoExecute)
        {
            LaunchMenuItem(MenuItem, QuickCode, reference, NewPage, InMenu, saveHistory, autoExecute, new Dictionary<string, string>());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Web.UI.ScriptManager.#AddHistoryPoint(System.String,System.String,System.String)")]
        public void LaunchMenuItem(AbstractMenuItem MenuItem, string QuickCode, string reference, bool NewPage, bool InMenu, bool saveHistory, bool autoExecute, Dictionary<string, string> fieldValues)
        {
            Helpers.StoreQuickCode(this.UserContext, QuickCode);

            string ParentUrlPrefix = this.UserContext.ParentUrlPrefix;

            if (string.IsNullOrEmpty(menuAutoClose.Value))
                this.UserContext.IsMenuAutoClose = false;
            else
                this.UserContext.IsMenuAutoClose = bool.Parse(menuAutoClose.Value);
            this.UserContext.CurrentSelectedMenu = MenuItem.ChildCode;
            string url;
            if (string.IsNullOrEmpty(MenuItem.ContainerURL))
            {
                if (MenuItem.ContentURL.IndexOf(":", StringComparison.Ordinal) < 0)
                {
                    if (MenuItem.Type == AbstractMenuItem.ContentType.TWAHeaderPage || MenuItem.Type == AbstractMenuItem.ContentType.URL)
                        url = UrlHelper.UrlBasePrefix() + MenuItem.ContentURL;
                    else
                        url = UrlHelper.UrlPrefix() + MenuItem.ContentURL;
                }
                else
                {
                    url = MenuItem.ContentURL;
                }
                if (MenuItem.Type != AbstractMenuItem.ContentType.URL)
                {
                    if (url.Contains('?'))
                    {
                        if (url.EndsWith("?", StringComparison.Ordinal))
                            url += "ContentCode=" + MenuItem.ChildCode;
                        else
                            url += "&ContentCode=" + MenuItem.ChildCode;
                    }
                    else
                        url += "?ContentCode=" + MenuItem.ChildCode;

                    url += "&QC=" + QuickCode;
                }
            }
            else
            {
                url = this.UserContext.ParentUrlPrefix + MenuItem.ContainerURL;
                if (string.IsNullOrEmpty(MenuItem.ContentURL))
                {
                    if (url.Contains('?'))
                    {
                        if (url.EndsWith("?", StringComparison.Ordinal))
                            url += "ContentCode=" + MenuItem.ChildCode;
                        else
                            url += "&ContentCode=" + MenuItem.ChildCode;
                    }
                    else
                        url += "?ContentCode=" + MenuItem.ChildCode;
                }
                else
                {
                    if (url.Contains('?'))
                    {
                        if (url.EndsWith("?", StringComparison.Ordinal))
                            url += "ContentCode=" + MenuItem.ContentURL;
                        else
                            url += "&ContentCode=" + MenuItem.ContentURL;
                    }
                    else
                        url += "?ContentCode=" + MenuItem.ContentURL;
                }
                url += "&QC=" + QuickCode;

            }

            if (MenuItem.Type != AbstractMenuItem.ContentType.URL)
            {
                url += "&MenuUID=" + MenuItem.TroposUniqueMenuID;

                if (!string.IsNullOrEmpty(reference))
                    url += "&Reference=" + reference;

                foreach (string Key in fieldValues.Keys)
                {
                    if (!string.IsNullOrEmpty(Key))
                        url += string.Format(CultureInfo.InvariantCulture, "&{0}={1}", Key, Uri.EscapeDataString(fieldValues[Key]));
                }

                if (autoExecute)
                {
                    url += "&AutoExecute=Y";
                }

                if (MenuItem.Type == AbstractMenuItem.ContentType.Transaction)
                    url += "&Transactions=" + MenuItem.TransactionList.Trim();

                if (MenuItem.Type == AbstractMenuItem.ContentType.TWAHeaderPage)
                {
                    url += "&TroposLogin=" + Uri.EscapeDataString(TroposLogin) +
                        "&ParentUrlPrefix=" + UserContext.ParentUrlPrefix;
                }
            }

            //Force to open in new page if LaunchInNewWindow true
            if (MenuItem.AllowNewWindow && (NewPage || MenuItem.LaunchInNewWindow))
            {
                if (MenuItem.Type != AbstractMenuItem.ContentType.URL)
                    url += "&IsNewWindow=True&Title=" + MenuItem.MenuText;
                StringBuilder script = new StringBuilder("OpenInNewPage(\"" + url.Replace("\"", ""));
                script.Append("\",'" + MenuItem.ChildCode);
                script.Append("','" + MenuItem.LaunchInMaximisedWindow.ToString());
                script.Append("','" + MenuItem.WindowWidth);
                script.Append("','" + MenuItem.WindowHeight);
                script.Append("','" + MenuItem.WindowTop);
                script.Append("','" + MenuItem.WindowLeft);
                script.Append("');");

                //Register the script with the control QC which is in the update panel upQC. 
                //Thus, the script will then be run when the update panel reloads.
                ScriptManager.RegisterStartupScript(this.Page.Master.FindControl("QC"), GetType(), "openInNewPage", script.ToString(), true);
                ((UpdatePanel)this.Page.Master.FindControl("upQC")).Update();
            }
            else
            {
                if (!UrlsSameApplication(url, Request.Url.OriginalString))
                {
                    if (this.UserContext != null)
                    {
                        if (this.UserContext.IsSharedSession)
                            this.UserContext.UnloadShared(Session);
                        else
                            this.UserContext.Unload(Session);
                    }
                    Session.Abandon();
                }

                if (LaunchInDefault != null)
                {
                    if (saveHistory && TWCScriptManager.IsInAsyncPostBack)
                        TWCScriptManager.AddHistoryPoint("TroposQuickCode", QuickCode, MenuItem.ContentName);
                    LaunchInDefault(this, new LaunchInDefaultEventArgs(MenuItem, QuickCode, url, InMenu));
                }
                else if (LaunchInDefaultEvent != null)
                {
                    if (saveHistory && TWCScriptManager.IsInAsyncPostBack)
                        TWCScriptManager.AddHistoryPoint("TroposQuickCode", QuickCode, MenuItem.ContentName);
                    LaunchInDefaultEvent(MenuItem, QuickCode, url, InMenu);
                }
                else
                    //Are we just redisplaying the page we are currently on?
                    if (string.Compare(Request.Url.OriginalString, url, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    if (MenuItem.Type == AbstractMenuItem.ContentType.TWAHeaderPage
                     || MenuItem.Type == AbstractMenuItem.ContentType.RBAHeaderPage
                     || MenuItem.Type == AbstractMenuItem.ContentType.URL)
                        Response.Redirect(url);
                    else
                    {
                        StringBuilder redirectTo = new StringBuilder(ParentUrlPrefix + "Default.aspx");
                        redirectTo.Append("?FromApplication=" + HttpContext.Current.Request.ApplicationPath.ReplaceFirst("/", string.Empty));
                        redirectTo.Append("&QC=" + QuickCode);
                        redirectTo.Append("&InMenu=" + InMenu.ToString());
                        redirectTo.Append("&MenuUID=" + MenuItem.TroposUniqueMenuID);
                        if (MenuItem.Type == AbstractMenuItem.ContentType.Transaction)
                            redirectTo.Append("&Transactions=" + MenuItem.TransactionList);
                        Response.Redirect(redirectTo.ToString());
                    }
                }
            }


        }

        /// <summary>
        /// Do the two URLs supplied as parameters belong to the same application?
        /// </summary>
        /// <param name="url1"></param>
        /// <param name="url2"></param>
        /// <returns></returns>
        private static bool UrlsSameApplication(string url1, string url2)
        {
            int ApplicationNameLength = 0;
            for (int i = 0; i < 4; i++)
            {
                ApplicationNameLength = url1.IndexOf('/', ApplicationNameLength + 1);
                if (ApplicationNameLength == -1 || ApplicationNameLength >= url1.Length - 1)
                    return false;
            }
            return (url2.Length >= ApplicationNameLength &&
                url1.Substring(0, ApplicationNameLength) == url2.Substring(0, ApplicationNameLength));
        }

        protected void onMenuItemClientClick(object sender, EventArgs e)
        {
            int menuId = Int16.Parse(Request.Params["__EVENTARGUMENT"], CultureInfo.InvariantCulture);
            AbstractMenuItem _fromMenu = this.UserContext.TroposRoles.Find(a => a.ChildCode == this.UserContext.CurrentSelectedMenu);
            AbstractMenuItem _menu = this.UserContext.TroposRoles.Find(a => a.TroposUniqueMenuID == menuId);
            //selectedNode.Value = menuId;

            string QuickCode = _menu.QuickCode;
            if (_menu.TransactionList.Contains(QC.Text))
                QuickCode = QC.Text;        // Don't change the quickcode box if it already contains one of our tranasctions

            Session["menuStateArray"] = menuStateControl.Value;  //Save menu state in the session for cross page navigation

            if (_menu != null)
            {
                if (PageIsNavigating != null)
                {
                    if (PageIsNavigating(_fromMenu, _menu))
                    {
                        selectedNode.Value = menuId.ToString(CultureInfo.InvariantCulture);
                        LaunchMenuItem(_menu, QuickCode, false, true);
                    }
                }
                else
                {
                    selectedNode.Value = menuId.ToString(CultureInfo.InvariantCulture);
                    LaunchMenuItem(_menu, QuickCode, false, true);
                }
            }
        }

        protected void TWCScriptManager_Navigate(object sender, HistoryEventArgs e)
        {
            if (!((ScriptManager)sender).IsNavigating)
                return;

            // Following code removed because it was being triggered by "Save Field Defaults", etc.
            //if (e.State.AllKeys.Count() <= 0)
            //{
            //    Response.Redirect("default.aspx");
            //    return;
            //}

            string TroposTransaction = e.State["TroposTransaction"];
            if (!String.IsNullOrEmpty(TroposTransaction))
            {
                DoQuickCode(TroposTransaction, false, false);
                return;
            }
            string TroposQuickCode = e.State["TroposQuickCode"];
            if (!String.IsNullOrEmpty(TroposQuickCode))
            {
                DoQuickCode(TroposQuickCode, false, false);
            }

        }

        protected void Refresh(object sender, EventArgs e)
        {


        }

        protected void Refresh_Clicked(object sender, EventArgs e)
        {

        }
        protected void ClearFields_Clicked(object sender, EventArgs e)
        {
            if (ClearFields != null)
            {
                ClearFields(this, new EventArgs());
            }
            if (OnClearFields != null)
            {
                OnClearFields(this, new EventArgs());
            }
        }
        public void SetStatusMessage(string MessageString)
        {
            TroposStatusBarInfo.Text = MessageString;
            TroposStatusBarInfoPanel.ToolTip = MessageString;
            upTroposStatusBar.Update();
        }

        public void SetStatusError(string MessageString)
        {
            TroposStatusBarError.Text = MessageString;
            TroposStatusBarErrorPanel.ToolTip = MessageString;
            upTroposStatusBar.Update();
        }

        public void SetStatusAdditional(string MessageString)
        {
            TroposStatusBarAdditional.Text = MessageString;
            TroposStatusBarAdditionalPanel.ToolTip = MessageString;
            upTroposStatusBar.Update();
        }

        protected void TriggerPrint_Clicked(object sender, EventArgs e)
        {
            if (OnTriggerPrint != null)
                OnTriggerPrint(this, new EventArgs());
        }
        internal string GetLastFocusField() => hdnLastFocusInput.Text;

        private void SetQuickCode()
        {
            Control ctrlQC = FindControl("QC");

            //Control will be null if it's a new page
            if (ctrlQC != null)
            {
                string valQC = Request.QueryString["QC"];
                if (valQC != null)
                    ((TextBox)(ctrlQC)).Text = valQC.ToUpper();
            }
        }
    }
}
