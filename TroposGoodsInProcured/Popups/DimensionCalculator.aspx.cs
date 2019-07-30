using System;
using TroposWebClient;
using TroposUI.Transactions;
using System.Web.UI.WebControls;
using System.Web.UI;
using TroposUI.Common.LAL;
using System.Collections.Generic;
using TroposUI.Common.Exception;
using System.Diagnostics;
using TroposLookUp;
using TroposUI.Common.DataUpdater;
using System.Data;
using TroposUI.Common.Globalisation;
using TDK.Common;
using System.Globalization;


namespace TroposGoodsInProcured
{
    public partial class DimensionCalculator : TroposUI.Common.UI.TroposPage
    {
        const string ScreenCode = "2821";
        private Dictionary<string, string> GlobalFieldValues = new Dictionary<string, string>();

        private string TransactionControlViewState
        {
            get
            {
                if (ViewState["Transaction.ControlViewState"] != null)
                    return ViewState["Transaction.ControlViewState"].ToString();
                else
                    return String.Empty;
            }
            set { ViewState["Transaction.ControlViewState"] = value; }
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (this.Master is Site)
                ((Site)this.Master).OnClearFields += new EventHandler(ClearFields);
            TroposScreen DcalScreen = ScreenManager.GetScreen("DCAL", this.UserContext, false);
            UIScreenFactory Factory = new UIScreenFactory();
            if (!Page.IsPostBack)
            {
                object DcalFieldValues = Session["DcalFieldValues"];
                if (DcalFieldValues != null)
                    GlobalFieldValues = (Dictionary<string, string>)DcalFieldValues;
            }
            Panel panel = Factory.BuildScreenHTML(this.UserContext, DcalScreen, "", GlobalFieldValues);

            Factory.LalButtonClickEvent += new EventHandler<LalButtonClickEventArgs>(ShowLookup);

            TroposFields.Controls.Add(panel);
            foreach (Control control in UIScreenFactory.BuildButtons(DcalScreen))
            {
                updButtons.ContentTemplateContainer.Controls.Add(control);
                ((LinkButton)control).Command += TransactionButton_click;
            }
            if (!Page.IsPostBack)
            {
                DoTransaction("DCAL");
                TrimUnusedLines(panel);
            }
            if (!String.IsNullOrEmpty(TransactionControlViewState))
                CreateLookUp(TransactionControlViewState);

            string DefaultButtonIdScript = "var DefaultButtonId = '" + DefaultButton.ClientID + "';";
            ScriptManager.RegisterStartupScript(TroposFields, GetType(), "DefaultButtonId", DefaultButtonIdScript, true);
            string FocusScript = "$(document).ready(function(){FocusFirstField();});";
            ScriptManager.RegisterStartupScript(TroposFields, GetType(), "FocusScript", FocusScript.ToString(), true);
        }

        private void ClearFields(object sender, EventArgs e)
        {
            throw new NotImplementedException("ClearFields functionality has not been implemented");
        }
        private void TransactionButton_click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            string TransactionCode = button.Attributes["TransactionCode"];
            DoTransaction(TransactionCode);

        }
        protected void DefaultButton_Click(object sender, EventArgs e)
        {
            //They could not have clicked this with the mouse, so they must have pressed return.
            string TransactionCode = "DCAL";
            DoTransaction(TransactionCode);
        }

        private void DoTransaction(string TransactionCode)
        {
            TransactionExecution Execution = new TransactionExecution(this.UserContext);
            bool ReturnAfterDcal = false;

            if (this.UserContext.Status != TroposStatus.AwaitingTransaction)
            {
                Execution.DoReply("N", new ElectronicSignatureData(), int.Parse(this.UserContext.DataValidAttachKey, CultureInfo.InvariantCulture));
            }

            bool? NoUpdate = Session["DcalNoUpdate"] as bool?;
            if (NoUpdate != null && (bool)NoUpdate && TransactionCode == "UCAL")
            {
                // No Update flag is set, so do DCAL instead of UCAL
                TransactionCode = "DCAL";
                ReturnAfterDcal = true;
            }

            Session["CalculatorModeFieldValues"] = DimensionHelper.EvaluateFields(TroposFields.Controls[3].Controls);
            ExecuteReturn ReturnValue;
            ReturnValue = Execution.DoTransaction(TroposFields.Controls[3].Controls, TransactionCode, ScreenCode, false, false);
            {
                Dictionary<string, string> FieldValues = null;
                Dictionary<string, string> _fieldValues = null;
                object SessionFieldValues = null;
                DataTable FieldValuesTable = ReturnValue.FieldValuesTable;
                DataRow FieldValuesRow = FieldValuesTable.Rows[0];
                switch (ReturnValue.WaitefReturn)
                {
                    case "ExoutvarOk":
                    case "Exread":
                    case "Exread2":
                        // The DCAL transaction comes here - set up the fields in case we need to return them
                        FieldValuesTable = ReturnValue.FieldValuesTable;
                        FieldValuesRow = FieldValuesTable.Rows[0];
                        FieldValues = RowToDictionary(FieldValuesTable, FieldValuesRow);
                        if (ReturnAfterDcal)
                            TransactionReturn(FieldValues);
                        break;
                    case "ExdoneOk":
                        // UCAL comes here - set up the fields from the pre-update values, then add the result fields
                        SessionFieldValues = Session["CalculatorModeFieldValues"];
                        if (SessionFieldValues != null)
                        {
                            FieldValues = (Dictionary<string, string>)SessionFieldValues;
                            Session.Remove("CalculatorModeFieldValues");
                        }
                        FieldValuesTable = ReturnValue.FieldValuesTable;
                        FieldValuesRow = FieldValuesTable.Rows[0];
                        _fieldValues = RowToDictionary(FieldValuesTable, FieldValuesRow);
                        FieldValues["iATTVALFROM-DQ"] = _fieldValues["iATTVALFROM-DQ"];
                        FieldValues["iUOM-ATT1"] = _fieldValues["iUOM-ATT1"];
                        TransactionReturn(FieldValues);
                        break;
                    case "ExoutvarFail":
                        TroposResourceProvider Trp = new TroposResourceProvider(UserContext);
                        if (ReturnValue.Message.Trim() == Trp.GetResource("MSG_NO_CHANGES"))
                        {
                            switch (TransactionCode)
                            {
                                case "UCAL":
                                    // set up the fields from the pre-update values, then add the result fields
                                    SessionFieldValues = Session["CalculatorModeFieldValues"];
                                    if (SessionFieldValues != null)
                                    {
                                        FieldValues = (Dictionary<string, string>)SessionFieldValues;
                                        Session.Remove("CalculatorModeFieldValues");
                                    }
                                    FieldValuesTable = ReturnValue.FieldValuesTable;
                                    FieldValuesRow = FieldValuesTable.Rows[0];
                                    _fieldValues = RowToDictionary(FieldValuesTable, FieldValuesRow);
                                    FieldValues["iATTVALFROM-DQ"] = _fieldValues["iATTVALFROM-DQ"];
                                    FieldValues["iUOM-ATT1"] = _fieldValues["iUOM-ATT1"];
                                    TransactionReturn(FieldValues);
                                    break;
                                case "DCAL":
                                    if (ReturnAfterDcal)
                                    {
                                        FieldValuesTable = ReturnValue.FieldValuesTable;
                                        FieldValuesRow = FieldValuesTable.Rows[0];
                                        FieldValues = RowToDictionary(FieldValuesTable, FieldValuesRow);
                                        TransactionReturn(FieldValues);
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }

            ValidationResult MandatoryCheckResult = new ValidationResult();
            Execution.LoadScreenFields(
                    ReturnValue.FieldValuesTable,
                    ReturnValue.Message,
                    ReturnValue.ScrollTable,
                    ReturnValue.TextTable,
                    TroposFields.Controls[3].Controls,
                    TransactionCode,
                    MandatoryCheckResult);

            Label ErrorField = (Label)TroposFields.FindControl("eError");
            if (ErrorField != null)
            {
                if (ReturnValue.Success)
                {
                    ErrorField.Text = "";
                    ErrorField.CssClass = "TroposError TroposDisplay TroposField";
                }
                else
                {
                    ErrorField.Text = ReturnValue.Message;
                    if (string.IsNullOrEmpty(ReturnValue.Message))
                    {
                        ErrorField.CssClass = "TroposError TroposDisplay TroposField";
                    }
                    else
                    {
                        ErrorField.CssClass = "TroposErrorPopulated TroposDisplay TroposField";
                    }
                }
            }
        }

        void TransactionReturn(Dictionary<string, string> fieldValues)
        {
            Session["DcalFieldValues"] = fieldValues;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseScreen", "window.opener.TransactionCallback.click(); window.close();", true);
        }

        protected void ShowLookup(object Sender, LalButtonClickEventArgs e)
        {
            TroposLookup Lookup = null;
            foreach (TroposLookup _Lookup in e.Lookups)
            {
                if (string.IsNullOrEmpty(_Lookup.TransactionCode))
                {
                    Lookup = _Lookup;
                    break;
                }
            }
            if (Lookup == null)
                return;

            List<ListParameter> Parameters = new List<ListParameter>();
            Dictionary<int, string> LookupFields = new Dictionary<int, string>();
            foreach (TroposLookupParameter Parameter in Lookup.Parameters)
            {
                if (Parameter.Field.IndexOfAny(new char[] { 'i', 'v' }) != 0)
                {
                    //Assume it's a constant
                    Parameters.Add(new ListParameter(
                        Parameter.Number,
                        Parameter.Field,
                        "I"));
                    LookupFields.Add(Parameter.Number, "_dummy");
                }
                else
                {
                    string FieldValue = "";
                    Control SourceField = TroposFields.Controls[0].FindControl(Parameter.Field);
                    // The control named in the Lookup definition is not present on the screen.
                    if (SourceField == null)
                    {
                        // Set up a dummy parameter to keep the other parameters in order.  However, given that
                        // we are running from erroneous data, there is no guarantee it will work properly.
                        LookupFields.Add(Parameter.Number, "_dummy");
                        Parameters.Add(new ListParameter(Parameter.Number, "",
                                Parameter.Type == TroposLookupParameterType.Input ? "I" :
                                Parameter.Type == TroposLookupParameterType.Output ? "O" : "B"));
                        continue;
                    }
                    try
                    {
                        LookupFields.Add(Parameter.Number, SourceField.ID);
                    }
                    catch (ArgumentException)
                    {
                        throw new TroposScreenException(
                            string.Format(CultureInfo.InvariantCulture,
                            "Definition of Lookup {0} on field {2} has more than one parameter number {1}",
                            Lookup.Code,
                            Parameter.Number,
                            SourceField.ID), "DCAL", EventLogEntryType.Warning);
                    }
                    TextBox SourceBox = SourceField as TextBox;
                    if (SourceBox != null)
                    {
                        FieldValue = SourceBox.Text;
                    }
                    else
                    {
                        Label SourceLabel = SourceField as Label;
                        if (SourceLabel != null)
                            FieldValue = SourceLabel.Text.Replace("&nbsp;", " ").Trim();
                    }
                    Parameters.Add(new ListParameter(
                        Parameter.Number,
                        FieldValue,
                        Parameter.Type == TroposLookupParameterType.Input ? "I" :
                        Parameter.Type == TroposLookupParameterType.Output ? "O" : "B"));
                }
            }
            this.UserContext.LookupCode = Lookup.Code;
            this.UserContext.ListParameters = Parameters;
            this.UserContext.LookupFields = LookupFields;

            CreateLookUp(Lookup.Code);
        }
        protected void CreateLookUp(string lookUpCode)
        {
            if (String.IsNullOrEmpty(TransactionControlViewState))
                SharedLookup_TLU.Clean();

            SharedLookup_TLU.LookUpClosedEvent += new LookUp.LookUpClosedEventHandler(SharedLookup_TLU_LookUpClosedEvent);
            SharedLookup_TLU.LookUpDataSelectedEvent += new LookUp.LookUpDataSelectedEventHandler(SharedLookup_TLU_LookUpDataSelectedEvent);
            SharedLookup_TLU.LaunchInModalWindow = true;
            SharedLookup_TLU.Parameters = this.UserContext.ListParameters;
            SharedLookup_TLU.ID = lookUpCode;
            SharedLookup_TLU.Code = lookUpCode;

            SharedLookup_TLU.Visible = true;
            SharedLookup.Visible = true;
            //upLookUpContainer.Update();

            if (String.IsNullOrEmpty(TransactionControlViewState))
                TransactionControlViewState = lookUpCode;

        }
        void SharedLookup_TLU_LookUpDataSelectedEvent(List<ListParameter> Parameters)
        {
            Dictionary<int, string> LookupFields = this.UserContext.LookupFields;
            if (LookupFields == null)
                return;

            foreach (ListParameter Parameter in Parameters)
            {
                if (LookupFields.ContainsKey(Parameter.No))
                {
                    Control Field = TroposFields.Controls[0].FindControl(LookupFields[Parameter.No]);
                    TextBox InputField = Field as TextBox;
                    if (InputField != null)
                    {
                        InputField.Text = Parameter.Value.Replace("&nbsp;", " ");
                    }
                    else
                    {
                        Label DisplayField = Field as Label;
                        if (DisplayField != null)
                            DisplayField.Text = Parameter.Value.Replace("&nbsp;", " ");
                    }
                }
            }
            SharedLookup_TLU.Clean(); //Flushs before next use
            SharedLookup_TLU.Visible = false;
            SharedLookup.Visible = false;
            TransactionControlViewState = null;
            //upLookUpContainer.Update();
            //upTroposTrans.Update();
        }
        void SharedLookup_TLU_LookUpClosedEvent()
        {
            TransactionControlViewState = null;
            SharedLookup_TLU.Clean(); //Flushs before next use
            SharedLookup_TLU.Visible = false;
            SharedLookup.Visible = false;
            //upLookUpContainer.Update();
            //upTroposTrans.Update();
        }
        private static Dictionary<string, string> RowToDictionary(DataTable FieldValuesTable, DataRow FieldValuesRow)
        {
            Dictionary<string, string> FieldValues = new Dictionary<string, string>();
            foreach (DataColumn Column in FieldValuesTable.Columns)
            {
                string ColumnName = Column.ColumnName;
                if (ColumnName.StartsWith("i", StringComparison.Ordinal) ||
                    ColumnName.StartsWith("v", StringComparison.Ordinal) ||
                    ColumnName.StartsWith("f", StringComparison.Ordinal))
                {
                    if (FieldValuesRow[ColumnName] == System.DBNull.Value)
                        FieldValues.Add(ColumnName, " ");
                    else
                        FieldValues.Add(ColumnName, (string)FieldValuesRow[ColumnName]);
                }
            }
            return FieldValues;
        }
        private static void TrimUnusedLines(Panel troposControls)
        {
            int FieldsToBeHidden = 0;
            foreach (Control control in troposControls.Controls)
            {
                if (FieldsToBeHidden > 0)
                {
                    control.Visible = false;
                    FieldsToBeHidden--;
                }
                Label label = control as System.Web.UI.WebControls.Label;
                if (label == null)
                    continue;
                if (label.ID.StartsWith("vATTDESC_D", StringComparison.Ordinal) && string.IsNullOrEmpty(label.Text.Replace("&nbsp;", "")))
                {
                    // There is no description for the line, so the line can be removed.
                    label.Visible = false;
                    FieldsToBeHidden = 2;   // We know that for DCAL there are two more fields on the same line
                }
            }
        }

    }
}
