using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TDK.Common.Helper;
using Tropos.Web.UI;
using TroposUI.Common;
using TroposWebClient;
using System.Data;
using TroposUI.Common.Menu;
using TroposGoodsInProcuredBO.DTO;
using TroposGoodsInProcuredBO.Transactions;
using System.Collections;

namespace TroposGoodsInProcured
{
    public partial class TroposGoodsInProcured : TroposUI.Common.UI.TroposPage
    {
        public string message;

        protected void Page_Init(object sender, EventArgs e)
        {
            tcv.ServerValidate += tcv_ServerValidate;
            iPURCHASE_ORDER.KeyChanged += iPURCHASE_ORDER_Keychanged;
            iPURCHASE_ORDER.DescriptionField.Visible = false;
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            tcboxINCLUDECOMPLETE.CheckedChanged += iPURCHASE_ORDER_Keychanged;

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

            TransAlert.Visible = false;

            try
            {
                UpdateGoodsInTable();
            }

            catch
            {
                grdGOODSIN.Visible = false;
            }


        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            TransAlert.Visible = false;
            updConfirm.Update();
            UpdateGoodsInTable();

        }

        private void BtnOK_Click(object sender, EventArgs e)
        {

            GROS gros = new GROS();
            gros.iPORDNOENT = ViewState["purchaseOrder"].ToString();
            gros.iPORDITM_GRN = ViewState["LineNo"].ToString();
            gros.iGRNADVISE = ViewState["receivedquantity"].ToString();
            gros.iGRNREC = ViewState["receivedquantity"].ToString();
            gros.iGRNDELADV = ViewState["advicenote"].ToString();
            gros.iREPORTDEST = "ERP_HP2300";
            gros.iGRNDATE = ViewState["receivedDate"].ToString();
            gros.iUOM_ADV = ViewState["uomSelected"].ToString();


            if (gros.Execute())
            {
                ShowMessage("Update Successful");
            }
            else
            {
                ShowMessage(gros.Errors);
            }

            TransAlert.Visible = false;
            updConfirm.Update();

            UpdateGoodsInTable();


        }

        private void PopupTxn(string action, bool autoExec, Dictionary<string, string> fields)
        {
            MenuItemFactory mif = new MenuItemFactory();
            AbstractMenuItem mi = mif.ItemForContentCode(this.UserContext, action);
            if (Page.Master is Site)
            {
                Site Master = (Site)Page.Master;
                Master.LaunchMenuItem(mi, action, string.Empty, true, false, false, autoExec, fields);
            }

            else
            {
                SiteNewPage Master = (SiteNewPage)Page.Master;
                Master.LaunchMenuItem(mi, action, string.Empty, true, false, false, autoExec, fields);
            }
        }

        private void iPURCHASE_ORDER_Keychanged(object sender, EventArgs e)
        {
            grdGOODSIN.Visible = true;
            vSUPPLIER_LABEL.Visible = true;
            vSUPPLIER.Visible = true;
            vINCLUDECOMPLETE_LABEL.Visible = true;
            tcboxINCLUDECOMPLETE.Visible = true;

            PHDY phdy = new PHDY();
            phdy.iPORDNOENT = iPURCHASE_ORDER.Text;

            bool isOK = phdy.Execute(true);

            if (!isOK)
            {
                ShowMessage(phdy.Errors.ToString());
            }

            else
            {
                phdy.Execute();
                vSUPPLIER.Text = phdy.vSUPNAME_0.ToString();
                UpdateGoodsInTable();
            }
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

        protected void lnkGRN_Command(object sender, CommandEventArgs e)
        {
            if (sender is LinkButton)
            {
                string GRN = e.CommandArgument.ToString();
                LinkButton lb = (LinkButton)sender;
                Dictionary<string, string> Fields = new Dictionary<string, string>();
                Fields.Add("GRN", GRN);


                int width = (int)(Convert.ToDouble(screenWidth.Value) * 0.75);
                int height = (int)(Convert.ToDouble(screenHeight.Value) * 0.5);

                PopupPage pp = new PopupPage(updDummy, "Popups/OrderLineReceipts", "Order+Line+Receipts", false, 50, 50, width, height, Fields);
                updDummy.Update();



            }
        }

        public void imgEXECUTE_BTN_Command(object sender, CommandEventArgs e)
        {
            for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
            {
                try
                {


                    TroposInput receivedQty = (TroposInput)grdGOODSIN.Rows[i].Cells[6].FindControl("iRECEIVEDQTY");
                    ViewState["receivedQty_" + i.ToString()] = receivedQty.Text;
                    TroposInput adviceNote = (TroposInput)grdGOODSIN.Rows[i].Cells[8].FindControl("iADVICENOTE");
                    ViewState["adviceNote_" + i.ToString()] = adviceNote.Text;
                    TroposBusinessCalendar receivedDate = (TroposBusinessCalendar)grdGOODSIN.Rows[i].Cells[9].FindControl("tbcRECEIVEDDATE");
                    ViewState["receivedDate_" + i.ToString()] = receivedDate.Text;
                    DropDownList uomSelected = (DropDownList)grdGOODSIN.Rows[i].Cells[7].FindControl("iUOM");
                    ViewState["uomSelected_" + i.ToString()] = uomSelected.Text;


                }
                catch (Exception)
                {
                    // no exception                 
                }
            }


            int index = Convert.ToInt32(e.CommandArgument);

            ViewState["purchaseOrder"] = iPURCHASE_ORDER.Text;
            TroposInput ReceivedQty = (TroposInput)grdGOODSIN.Rows[index].Cells[3].FindControl("iRECEIVEDQTY");
            TroposInput AdviceNote = (TroposInput)grdGOODSIN.Rows[index].Cells[5].FindControl("iADVICENOTE");
            TroposBusinessCalendar ReceivedDate = (TroposBusinessCalendar)grdGOODSIN.Rows[index].Cells[6].FindControl("tbcRECEIVEDDATE");
            DropDownList UOMSelected = (DropDownList)grdGOODSIN.Rows[index].Cells[7].FindControl("iUOM");
            ViewState["LineNo"] = grdGOODSIN.Rows[index].Cells[0].InnerText().Replace("<center>", "").Replace("</center>", "").Trim();

            // Entry Validation
            int qty;

            if (!int.TryParse(ReceivedQty.Text, out qty))
            {
                ShowMessage("Received Quantity is not a number");
                ReceivedQty.HighlightError = true;
                return;
            }

            else
            {
                ShowMessage("");
                ReceivedQty.HighlightError = false;
            }

            if (int.Parse(ReceivedQty.Text) == 0)
            {
                ShowMessage("Please enter valid non-zero, quantity");
                ReceivedQty.HighlightError = true;
                return;
            }

            if (AdviceNote.Text.Trim() == string.Empty)
            {
                ShowMessage("Please enter an advice note");
                AdviceNote.HighlightError = true;
                return;
            }

            if (UOMSelected.Text == "")
            {
                ShowMessage("Please Select a valid UOM");
                return;
            }


            else
            {
                ShowMessage("");
                AdviceNote.HighlightError = false;
            }

            DateTime datetime;
            if (!DateTime.TryParse(ReceivedDate.Text, out datetime))
            {
                ShowMessage("Please enter a valid date");
                ReceivedDate.HighlightError = true;
                return;
            }

            else
            {
                ShowMessage("");
                ReceivedDate.HighlightError = false;
            }

            ViewState["receivedquantity"] = qty;
            ViewState["advicenote"] = AdviceNote.Text;
            ViewState["receivedDate"] = ReceivedDate.Text;
            ViewState["uomSelected"] = UOMSelected.Text;


            TransAlert.Visible = true;
            updConfirm.Update();

            // Refresh Grid Details
            ITroposQuery grdGOODSIN_Query = new Populate_grdGOODSIN(UserContext, iPURCHASE_ORDER.Text);
            DataTable grd_data_table = Helpers.TroposQuery(grdGOODSIN_Query, UserContext);
            grdGOODSIN.DataSource = grd_data_table;
            grdGOODSIN.DataBind();

            for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
            {
                try
                {

                    ReceivedQty = (TroposInput)grdGOODSIN.Rows[i].Cells[6].FindControl("iRECEIVEDQTY");
                    ReceivedQty.Text = ViewState["receivedQty_" + i.ToString()].ToString();
                    AdviceNote = (TroposInput)grdGOODSIN.Rows[i].Cells[8].FindControl("iADVICENOTE");
                    AdviceNote.Text = ViewState["adviceNote_" + i.ToString()].ToString();
                    ReceivedDate = (TroposBusinessCalendar)grdGOODSIN.Rows[i].Cells[9].FindControl("tbcRECEIVEDDATE");
                    ReceivedDate.Text = ViewState["receivedDate_" + i.ToString()].ToString();
                    UOMSelected = (DropDownList)grdGOODSIN.Rows[i].Cells[7].FindControl("iUOM");
                    UOMSelected.Text = ViewState["uomSelected_" + i.ToString()].ToString();

                }
                catch (Exception)
                {
                    // no error handling implemented
                }

            }

            ReceivedQty.HighlightError = false;
            AdviceNote.HighlightError = false;
            ReceivedDate.HighlightError = false;
            ShowMessage("");
        }

        protected void tcboxINCLUDECOMPLETE_CheckedChanged(object sender, EventArgs e)
        {

            PHDY phdy = new PHDY();
            phdy.iPORDNOENT = iPURCHASE_ORDER.Text;

            bool isOK = phdy.Execute(true);

            if (!isOK)
            {
                ShowMessage(phdy.Errors.ToString());
            }
            else
            {
                phdy.Execute();
                vSUPPLIER.Text = phdy.vSUPNAME_0.ToString();

                UpdateGoodsInTable();

                tab_Clear.Visible = true;
                vSUPPLIER.Visible = true;
                vSUPPLIER_LABEL.Visible = true;
                vINCLUDECOMPLETE_LABEL.Visible = true;
                tcboxINCLUDECOMPLETE.Visible = true;
            }
        }

        protected void tbcDATE_REQUIRED_DateSelected(object sender, EventArgs e)
        {
            tab_Clear_Click(new object(), new EventArgs());

            grdGOODSIN.Visible = false;

            string dateRequired = tbcDATE_REQUIRED.Text;
            ITroposQuery davlConsignmentQuery = new Populate_davlCONSIGNMENT(UserContext, dateRequired);
            DataTable davlConsignmentDT = Helpers.TroposQuery(davlConsignmentQuery, UserContext);
            if (davlConsignmentDT.Rows.Count == 0)
            {
                string code = "";
                string text = "";
                DAVLUserDefinedData davlData = new DAVLUserDefinedData(code, text);
                iPURCHASE_ORDER.UserDefined.Add(davlData);
            }
            else
            {
                for (int i = 0; i < davlConsignmentDT.Rows.Count; i++)
                {
                    string code = davlConsignmentDT.Rows[i][0].ToString();
                    string text = davlConsignmentDT.Rows[i][1].ToString();
                    DAVLUserDefinedData davlData = new DAVLUserDefinedData(code, text);
                    iPURCHASE_ORDER.UserDefined.Add(davlData);

                    try
                    {
                        UpdateGoodsInTable();
                    }
                    catch
                    {
                        grdGOODSIN.Visible = false;
                    }


                }
            }
        }

        protected void tab_Clear_main_Click(object sender, CommandEventArgs e)
        {
            int i = Convert.ToInt32(e.CommandArgument);


            TroposInput ReceivedQty = (TroposInput)grdGOODSIN.Rows[i].Cells[6].FindControl("iRECEIVEDQTY");
            ReceivedQty.Text = "";
            DropDownList UOM = (DropDownList)grdGOODSIN.Rows[i].Cells[7].FindControl("iUOM");
            UOM.ClearSelection();
            TroposInput AdviceNote = (TroposInput)grdGOODSIN.Rows[i].Cells[8].FindControl("iADVICENOTE");
            AdviceNote.Text = "";
            TroposBusinessCalendar ReceivedDate = (TroposBusinessCalendar)grdGOODSIN.Rows[i].Cells[10].FindControl("tbcRECEIVEDDATE");
            ReceivedDate.Text = DateTime.Today.ToShortDateString();

            ReceivedQty.HighlightError = false;
            AdviceNote.HighlightError = false;
            ShowMessage("");



        }

        protected void tab_Clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
            {
                try
                {


                    TroposInput ReceivedQty = (TroposInput)grdGOODSIN.Rows[i].Cells[6].FindControl("iRECEIVEDQTY");
                    ReceivedQty.Text = "";
                    DropDownList UOM = (DropDownList)grdGOODSIN.Rows[i].Cells[7].FindControl("iUOM");
                    UOM.Text = "";
                    TroposInput AdviceNote = (TroposInput)grdGOODSIN.Rows[i].Cells[8].FindControl("iADVICENOTE");
                    AdviceNote.Text = "";

                    ReceivedQty.HighlightError = false;
                    AdviceNote.HighlightError = false;
                    ShowMessage("");
                }

                catch (Exception)
                {
                    // no error handling required
                }
            }
        }

        protected void UpdateGoodsInTable()
        {
            ITroposQuery grdGOODSIN_Query = new Populate_grdGOODSIN(UserContext, iPURCHASE_ORDER.Text);
            DataTable grd_data_table = Helpers.TroposQuery(grdGOODSIN_Query, UserContext);
            grdGOODSIN.DataSource = grd_data_table;
            grdGOODSIN.DataBind();


            if (tcboxINCLUDECOMPLETE.Checked == false)
            {
                for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
                {
                    int outstandingamount;
                    try
                    {
                        outstandingamount = int.Parse(grdGOODSIN.Rows[i].Cells[5].InnerText().Replace("<center>", "").Replace("</center>", "").Trim());
                    }
                    catch
                    {
                        outstandingamount = 0;
                    }
                    if (outstandingamount < 1 && grdGOODSIN.Rows[i].Cells[0].Controls.Count > 0)
                    {
                        grdGOODSIN.Rows[i].Visible = false;
                    }
                    else
                    {
                        grdGOODSIN.Rows[i].Visible = true;
                    }

                }
            }

            try
            {
                ArrayList rowsToClear = new ArrayList();

                for (int i = 1; i < grdGOODSIN.Rows.Count; i++)
                {
                    int j = i - 1;
                    string PrevLineNo = grdGOODSIN.Rows[j].Cells[0].InnerText().Replace("<center>", "").Replace("</center>", "").Trim();
                    string CurrentLineNo = grdGOODSIN.Rows[i].Cells[0].InnerText().Replace("<center>", "").Replace("</center>", "").Trim();
                    if (CurrentLineNo == PrevLineNo)
                    {
                        rowsToClear.Add(i);

                    }
                }

                foreach (int row in rowsToClear)
                {
                    grdGOODSIN.Rows[row].Cells[0].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[1].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[2].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[3].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[4].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[5].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[6].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[7].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[8].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[9].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[12].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[13].Controls.Clear();
                }

                if (tcboxINCLUDECOMPLETE.Checked == false)
                {
                    for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
                    {
                        int outstandingamount;
                        try
                        {
                            outstandingamount = int.Parse(grdGOODSIN.Rows[i].Cells[5].InnerText().Replace("<center>", "").Replace("</center>", "").Trim());
                        }
                        catch
                        {
                            outstandingamount = 0;
                        }
                        if (outstandingamount < 1 && grdGOODSIN.Rows[i].Cells[0].Controls.Count > 0)
                        {
                            grdGOODSIN.Rows[i].Visible = false;
                        }
                        else
                        {
                            grdGOODSIN.Rows[i].Visible = true;
                        }
                    }
                }


                // remove special case where all received <= 0 and where there are multiple rows with same line number.  Blank line numbers would still display otherwise.
                for (int i = 1; i < grdGOODSIN.Rows.Count; i++)
                {

                    if (grdGOODSIN.Rows[i].Cells[0].Controls.Count == 0 && tcboxINCLUDECOMPLETE.Checked == false)
                    {
                        grdGOODSIN.Rows[i].Visible = false;
                    }

                }
            }
            catch (Exception)
            {
                // do nothing here
            }
        }


    }
}

