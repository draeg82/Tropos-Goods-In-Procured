using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDK.Common.Helper;
using Tropos.Web.UI;
using TroposWebClient;
using TroposGoodsInProcuredBO.DTO;
using TroposUI.Common;
using System.Data;


namespace TroposGoodsInProcured.Popups
{
    public partial class RejectNoteInformation : TroposUI.Common.UI.TroposPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            tcv.ServerValidate += tcv_ServerValidate;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            PrintOptions Options = new PrintOptions();
            Options.Indirect = true;
            Options.Fit = true;

            Site SiteMaster = this.Master as Site;
            if (SiteMaster != null)
            {
                SiteMaster.printOptions = Options;

                ((Site)this.Master).OnClearFields += new EventHandler(ClearFields);
            }
            else
            {
                SiteNewPage SiteNewPageMaster = this.Master as SiteNewPage;
                if (SiteNewPageMaster != null)
                {
                    SiteNewPageMaster.printOptions = Options;
                }
            }

            string RejectNoteNo = Request.QueryString["RejectNote"];
            ITroposQuery grdREJECTNOTEINFORMATION_Query = new Populate_grdREJECTNOTEINFORMATION(UserContext, RejectNoteNo);
            DataTable grd_data_table = Helpers.TroposQuery(grdREJECTNOTEINFORMATION_Query, UserContext);
            grdREJECTNOTEINFORMATION.DataSource = grd_data_table;
            grdREJECTNOTEINFORMATION.DataBind();

        }



        protected void TransactionCallbackButton_click(object sender, EventArgs e)
        {
            // Results from the dimension calculator:
            // DimensionResult Result = DimensionHelper.CalculationResult();
            // Full list of fields from the dimension calculator:
            // Dictionary<string, string> ResultFields = DimensionHelper.CalculatorFields();
        }

        #region Error Message Handlers

        void tcv_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (string.IsNullOrEmpty(tcv.ErrorMessage.Trim()))
                args.IsValid = true;
            else
                args.IsValid = false;
        }
        void ShowMessage()
        {
            tcv.Enabled = true;
            tcv.Validate();
        }
        void ShowMessage(string Message)
        {
            tcv.ErrorMessage = Message;
            ShowMessage();
        }
        void ShowMessage(IList<TransactionError> errors, string Transaction)
        {
            tcv.ErrorMessage = string.Empty;

            foreach (var error in errors)
            {
                tcv.ErrorMessage += Transaction + ": " + error.Message + " ";
            }

            ShowMessage();
        }

        void ShowMessage(IList<TransactionError> errors)
        {
            tcv.ErrorMessage = string.Empty;

            foreach (var error in errors)
                tcv.ErrorMessage += error.Message + " ";

            ShowMessage();
        }

        void AddToError(string ErrorMessage)
        {
            if (!tcv.ErrorMessage.Contains(ErrorMessage))
            {
                tcv.ErrorMessage += ErrorMessage + "<BR>";
            }
        }

        void SetControlError(ref bool FirstError, TroposInput iControl)
        {
            iControl.HighlightError = true;
            if (FirstError)
            {
                FirstError = false;
                iControl.Focus();
            }
        }
        void SetControlError(ref bool FirstError, TroposCheckbox iControl, bool highlight)
        {
            iControl.HighlightError = highlight;
            if (highlight)
            {
                if (FirstError)
                {
                    FirstError = false;
                    iControl.Focus();
                }
            }
        }
        void SetControlError(ref bool FirstError, TroposDAVL iControl)
        {
            iControl.HighlightError = true;
            if (FirstError)
            {
                FirstError = false;
                iControl.Focus();
            }
        }
        void SetControlError(ref bool FirstError, TroposBusinessCalendar iControl)
        {
            iControl.HighlightError = true;
            if (FirstError)
            {
                FirstError = false;
                iControl.Focus();
            }
        }

        void SetControlError(ref bool FirstError, TroposInput iControl, bool highlight)
        {
            iControl.HighlightError = highlight;
            if (highlight)
            {
                if (FirstError)
                {
                    FirstError = false;
                    iControl.Focus();
                }
            }
        }
        void SetControlError(ref bool FirstError, TroposDAVL iControl, bool highlight)
        {
            iControl.HighlightError = highlight;
            if (highlight)
            {
                if (FirstError)
                {
                    FirstError = false;
                    iControl.Focus();
                }
            }
        }
        void SetControlError(ref bool FirstError, TroposBusinessCalendar iControl, bool highlight)
        {
            iControl.HighlightError = highlight;
            if (highlight)
            {
                if (FirstError)
                {
                    FirstError = false;
                    iControl.Focus();
                }
            }
        }

        #endregion

        private void ClearFields(object sender, EventArgs e)
        {
            throw new NotImplementedException("ClearFields functionality has not been implemented");
        }
        private string[] GetLastFocusFieldDetails()
        {
            string[] ReturnArray = new string[2];
            Site SiteMaster = this.Master as Site;
            string Details = "";
            if (SiteMaster != null)
            {
                Details = SiteMaster.GetLastFocusField();
            }
            else
            {
                SiteNewPage SiteNewPageMaster = this.Master as SiteNewPage;
                if (SiteNewPageMaster != null)
                {
                    Details = SiteNewPageMaster.GetLastFocusField();
                }
            }
            if (string.IsNullOrEmpty(Details))
                Details = ":";  // Force return of an array with two empty strings
            ReturnArray = Details.Split(':');
            return ReturnArray;
        }

        private void ScriptDoFocus()
        {
            //Registers the script to start after Update panel was rendered
            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "ScriptDoFocus",
                "DoFocus();",
                true);
        }




    }
}