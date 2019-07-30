/*
 * $Log: SiteNewPage.Master.cs  $
 * Revision 1.7.1.8 2015/12/29 13:49:16GMT Charles D P Miller (cdpm) 
 * Add processing for electronic signatures
 * Revision 1.7.1.7 2015/09/03 12:12:43BST Charles D P Miller (cdpm) 
 * Set script manager to release mode in Page init routine to avoid problems caused by additional checks on update panel IDs in debug mode in .NET 4
 * Revision 1.7.1.6 2015/03/19 14:08:37GMT Charles D P Miller (cdpm) 
 * Correct LaunchMenuItem to match that for Site.Master, so that it sets up the ContentCode entry in the query string corectly
 * Revision 1.7.1.5 2014/03/24 15:19:15GMT Charles D P Miller (cdpm) 
 * Change to allow DAVL via webservice to use parameters to the SQL statement, and only use cached results if both SQL and parameters are unchanged.
 * Revision 1.7.1.4 2014/01/17 14:43:21GMT Charles D P Miller (cdpm) 
 * Add facility for callback when popped up page is closed.
 * Revision 1.7.1.3 2013/12/12 13:23:40GMT Charles D P Miller (cdpm) 
 * Add processing to remember last field focussed.
 * Revision 1.7.1.2 2013/11/27 12:04:46GMT Charles D P Miller (cdpm) 
 * Changes for the Tropos status bar
 * Revision 1.7.1.1 2013/11/13 14:54:05GMT Charles D P Miller (cdpm) 
 * Encode the encrypted Tropos login string so that it is not corrupted by inclusion in a URL
 * Revision 1.7 2013/03/15 09:38:09GMT Charles D P Miller (cdpm) 
 * Rebuild
 * Revision 1.6 2013/02/20 17:16:11GMT Charles D P Miller (cdpm) 
 * Bring site new page interface into line with main site master page.
 * Revision 1.5 2012/11/07 14:36:46GMT Charles D P Miller (cdpm) 
 * Apply code analysis changes
 * Revision 1.4 2012/09/21 13:40:26BST Charles D P Miller (cdpm) 
 * Add Change Log
 */
using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TroposUI.Common;
using TroposUI.Common.Context;
using TroposUI.Common.Globalisation;
using TroposUI.Common.Menu;
using TroposUI.Encryption;
using System.Web;
using System.Diagnostics;
using System.Collections.Generic;
using System.Globalization;

namespace TroposWebClient
{
    public partial class SiteNewPage : TroposUI.Common.UI.TroposMasterPage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible"), Obsolete("Set LaunchInDefault event to an EventHandler<LaunchInDefaultEventArgs> object instead")]
        public delegate void LaunchInDefaultEventHandler(AbstractMenuItem MenuItem, string QuickCode, string Url, bool InMenu);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1003:UseGenericEventHandlerInstances"), Obsolete("Set OnClearFields event to an EventHandler object instead")]
        public delegate void ClearFieldsEventHandler(object sender, EventArgs e);

        public event EventHandler<PopupClosedEventArgs> OnPopupClosed;

        [Obsolete("Use LaunchInDefault event instead")]
        public event LaunchInDefaultEventHandler LaunchInDefaultEvent;

        public event EventHandler<LaunchInDefaultEventArgs> LaunchInDefault;

        [Obsolete("Use OnClearFields event instead")]
        public event ClearFieldsEventHandler ClearFields;
        public event EventHandler OnClearFields;
        public event EventHandler OnTriggerPrint;

        private string TroposLogin = "";

        public PrintOptions printOptions;

        protected void Page_Init(object sender, EventArgs e)
        {
            TWCScriptManager.ScriptMode = ScriptMode.Release;
        }

        protected void Page_Load(object sender, EventArgs e) { }

        protected void UserInfo_Load(object sender, EventArgs e)
        {
            //UserInfo.Text = UserContext.TroposUsername;
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
        public void LaunchMenuItem(AbstractMenuItem MenuItem, string QuickCode, string Reference, bool NewPage, bool InMenu, bool saveHistory, bool autoExecute)
        {
            LaunchMenuItem(MenuItem, QuickCode, Reference, NewPage, InMenu, saveHistory, autoExecute, new Dictionary<string, string>());
        }

        public void LaunchMenuItem(AbstractMenuItem MenuItem, string QuickCode, string Reference, bool NewPage, bool InMenu, bool saveHistory, bool autoExecute, Dictionary<string, string> fieldValues)
        {
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

                if (!string.IsNullOrEmpty(Reference))
                    url += "&Reference=" + Reference;

                if (autoExecute)
                {
                    url += "&AutoExecute=Y";
                }

                foreach (string Key in fieldValues.Keys)
                {
                    if (!string.IsNullOrEmpty(Key))
                        url += string.Format(CultureInfo.InvariantCulture, "&{0}={1}", Key, Uri.EscapeDataString(fieldValues[Key]));
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
            if (NewPage || MenuItem.LaunchInNewWindow)
            {
                if (MenuItem.Type != AbstractMenuItem.ContentType.URL)
                    url += "&IsNewWindow=True&Title=" + MenuItem.MenuText;
                StringBuilder script = new StringBuilder("$(document).ready(function() { ");
                script.Append("OpenInNewPage(\"" + url.Replace("\"", ""));
                script.Append("\",'" + MenuItem.ChildCode);
                script.Append("','" + MenuItem.LaunchInMaximisedWindow.ToString());
                script.Append("','" + MenuItem.WindowWidth);
                script.Append("','" + MenuItem.WindowHeight);
                script.Append("','" + MenuItem.WindowTop);
                script.Append("','" + MenuItem.WindowLeft);
                script.Append("');");
                script.Append("    });");

                //Register the script with the control QC which is in the update panel upQC. 
                //Thus, the script will then be run when the update panel reloads.
                ScriptManager.RegisterStartupScript(this.Page.Master.FindControl("upAppArea"), GetType(), "openInNewPage", script.ToString(), true);
                //                ((UpdatePanel)this.Page.Master.FindControl("upQC")).Update();
            }
            else
            {
                if (LaunchInDefault != null)
                {
                    if (saveHistory)
                        TWCScriptManager.AddHistoryPoint("TroposQuickCode", QuickCode, MenuItem.ContentName);
                    LaunchInDefault(this, new LaunchInDefaultEventArgs(MenuItem, QuickCode, url, InMenu));
                }
                else if (LaunchInDefaultEvent != null)
                {
                    if (saveHistory)
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
                        StringBuilder redirectTo = new StringBuilder(this.UserContext.ParentUrlPrefix + "Default.aspx");
                        redirectTo.Append("?FromApplication=" + HttpContext.Current.Request.ApplicationPath.ReplaceFirst("/", string.Empty));
                        redirectTo.Append("&QC=" + MenuItem.QuickCode);
                        redirectTo.Append("&InMenu=" + InMenu.ToString());
                        redirectTo.Append("&MenuUID=" + MenuItem.TroposUniqueMenuID);
                        if (MenuItem.Type == AbstractMenuItem.ContentType.Transaction)
                            redirectTo.Append("&Transactions=" + MenuItem.TransactionList);
                        Response.Redirect(redirectTo.ToString());
                    }
                }
            }


        }
        protected void Refresh(object sender, EventArgs e)
        {

            foreach (Control c in this.Page.Controls)
            {
                Debug.WriteLine(c.ID + ": " + c.GetType().ToString());
            }
        }

        protected void CallbackEvent_Click(object sender, EventArgs e)
        {
            string QC = Request["__EVENTARGUMENT"];
            if (OnPopupClosed != null)
            {
                OnPopupClosed(this, new PopupClosedEventArgs(QC));
            }
        }


        protected void Refresh_Clicked(object sender, EventArgs e)
        {
            foreach (Control c in this.Page.Controls)
            {
                Debug.WriteLine(c.ID + ": " + c.GetType().ToString());
            }
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
    }
}
