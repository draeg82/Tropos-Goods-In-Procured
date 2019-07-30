using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using System.Security.Principal;
using System.Threading;
using TroposUI.Common;
using TroposUI.Common.Context;
using TroposUI.Common.DAL;
using TroposUI.Common.Security;
using TroposUI.Encryption;
using System.Diagnostics;
using TDK.Validators;
using StructureMap;
using TDK.Common;
using System.Globalization;

namespace TroposGoodsInProcured
{
    public class Global : System.Web.HttpApplication
    {
        void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {

            // Get a reference to the current User
            IPrincipal usr = HttpContext.Current.User;

            // If we are dealing with an authenticated forms authentication request
            if (usr.Identity.IsAuthenticated && usr.Identity.AuthenticationType == "Forms")
            {
                FormsIdentity fIdent = usr.Identity as FormsIdentity;

                // Create a CustomIdentity based on the FormsAuthenticationTicket           
                CustomIdentity ci = new CustomIdentity(fIdent.Ticket);

                // Create the CustomPrincipal
                CustomPrincipal p = new CustomPrincipal(ci);

                // Attach the CustomPrincipal to HttpContext.User and Thread.CurrentPrincipal
                HttpContext.Current.User = p;
                Thread.CurrentPrincipal = p;
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            ObjectFactory.Initialize(x =>
                {
                    x.AddRegistry<CommonRegistry>();
                    x.AddRegistry<ValidatorRegistry>();
                    x.For<UserContext>().Use(() => (UserContext)HttpContext.Current.Session["UserContext"]);
                }
            );
        }

        protected void Session_Start(object sender, EventArgs e)
        {

            string Message;
            try
            {
                WindowsIdentity CurrentIdentity = WindowsIdentity.GetCurrent();
                string strUser;
                WindowsIdentity CallerWindowsIdentity =
                        HttpContext.Current.User.Identity as WindowsIdentity;
                if (CallerWindowsIdentity == null)
                {
                    strUser = "AnonymousUser";
                }
                else if (CallerWindowsIdentity.IsAuthenticated)
                {
                    strUser = CallerWindowsIdentity.Name;
                }
                else
                {
                    strUser = "Anonymous user";
                }
                Message = "  Connected as " + strUser + " ( " + CurrentIdentity.Name + " )";
            }
            catch (Exception ex)
            {
                Message = "  Failed to get identity data : " + ex.Message;
            }

            // Create an EventLog instance and assign its source.
            EventLog appLog = new EventLog();
            appLog.Source = "TroposWebClient";
            appLog.WriteEntry("TroposUIAdmin.Global.Session_Start" + Environment.NewLine +
                "Starting session" + Environment.NewLine +
                Message, EventLogEntryType.Information);

            UserContext context = null;
            string TroposLogin = HttpContext.Current.Request.QueryString["TroposLogin"];
            if (!string.IsNullOrEmpty(TroposLogin))
            {
                string TroposLoginClear = TroposEncryption.DecryptString(TroposLogin);
                string[] LoginDetails = TroposLoginClear.Split(new char[] { ':' });
                string TroposServer = LoginDetails[0];
                string TroposDatabase = LoginDetails[1];
                string TroposIdentity = LoginDetails[2];
                string TroposPassword = LoginDetails[3];
                bool WindowsAuthentication = bool.Parse(LoginDetails[4]);
                string TroposBusiness = "";
                try
                {
                    TroposBusiness = LoginDetails[5];
                }
                catch
                {
                    // No business code passed - just ignore it.
                }
                int TniSessionId = -1;
                if (LoginDetails.GetUpperBound(0) >= 6)
                {
                    TniSessionId = int.Parse(LoginDetails[6], CultureInfo.InvariantCulture);
                }
                if (WindowsAuthentication)
                {
                    WindowsImpersonationContext Ctx = null;
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        context = new UserContext(TroposServer, TroposDatabase, TroposBusiness);
                        WindowsIdentity WinId = (WindowsIdentity)HttpContext.Current.User.Identity;
                        Ctx = WinId.Impersonate();
                    }
                    else
                    {
                        throw new Exception("Windows authentication not set up correctly");
                    }
                    try
                    {
                        if (TniSessionId >= 0)
                            context.LoadShared(Session, TniSessionId);
                        else
                            context.Load(Session);
                    }
                    finally
                    {
                        if (Ctx != null)
                            Ctx.Undo();
                    }
                }
                else
                {
                    context = new UserContext(TroposServer, TroposDatabase, TroposIdentity, TroposPassword, TroposBusiness);
                    if (TniSessionId >= 0)
                        context.LoadShared(Session, TniSessionId);
                    else
                        context.Load(Session);
                }
                context.ParentUrlPrefix = HttpContext.Current.Request.QueryString["ParentUrlPrefix"];
            }
            else
            {
                CustomIdentity ident = User.Identity as CustomIdentity;
                if (ident != null)
                {
                    context = new UserContext(ident.TroposServer, ident.TroposDatabase, ident.TroposUserName, ident.TroposPassword, ident.TroposBusiness, ident.IsTroposManager);
                    context.ParentUrlPrefix = ident.ParentUrlPrefix;
                    context.Load(Session);
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

#if (!DEBUG)
        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (Server.GetLastError() != null)
                    {

                        if (Request.Path.Contains(".aspx"))
                        {
                            UserContext Context = (UserContext)Session["UserContext"];
                            string _errMessage = Server.GetLastError().Message.HtmlSpecialEntitiesEncode().NewLineToBreak();
                            string _innerMessage = Server.GetLastError().InnerException.Message.HtmlSpecialEntitiesEncode().NewLineToBreak();
                            string _stack = Server.GetLastError().InnerException.StackTrace.HtmlSpecialEntitiesEncode().NewLineToBreak();
                            string _source = Server.GetLastError().InnerException.Source.HtmlSpecialEntitiesEncode().NewLineToBreak();
                            Response.Redirect(Context.ParentUrlPrefix + "Error.aspx?Message=" + _errMessage + "&Source=" + _source + "&Stack=" + _stack + "&InnerException=" + _innerMessage, false);
                        }
                    }
                }
                catch (HttpException)
                {
                    // This is thrown with the message "Request is not available in this context" if we are
                    // called after an error in the Session_End routine.  Just ignore it.
                    ;
                }
            }
            finally
            {
                // Clear the error on server.
                Server.ClearError();
            }
        }
#endif

        protected void Session_End(object sender, EventArgs e)
        {
            UserContext Context = (UserContext)Session["UserContext"];
            if (Context != null)
            {
                if (Context.IsSharedSession)
                    Context.UnloadShared(Session);
                else
                    Context.Unload(Session);
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
