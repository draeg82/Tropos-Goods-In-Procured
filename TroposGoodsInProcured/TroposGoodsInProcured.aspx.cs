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
using System.Linq;
using System.Drawing;

namespace TroposGoodsInProcured
{
    public partial class TroposGoodsInProcured : TroposUI.Common.UI.TroposPage
    {
        public string message;

        protected void Page_Init(object sender, EventArgs e)
        {
            tcv.ServerValidate += tcv_ServerValidate;
            //iPURCHASE_ORDER.KeyChanged += iPURCHASE_ORDER_Keychanged;
            //iPURCHASE_ORDER.DescriptionField.Visible = false;
            iPURCHASE_ORDER.TroposLookUpDataSelected += iPURCHASE_ORDER_Keychanged;
            btnOK.Click += BtnOK_Click;
            btnCancel.Click += BtnCancel_Click;
            btnSuccessOK.Click += BtnSuccessOk_Click;
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

            if (!IsPostBack)
            {
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


        protected void BtnSuccessOk_Click(object sender, EventArgs e)
        {
            AlertSUCCESS.Visible = false;
            updSuccessMessage.Update();
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
            gros.iREPORTDEST = "ERP_HP2300";
            gros.iPORDNOENT = ViewState["purchaseOrder"].ToString();
            gros.iPORDITM_GRN = ViewState["LineNo"].ToString();
            gros.iGRNADVISE = ViewState["receivedquantity"].ToString();
            gros.iGRNREC = ViewState["receivedquantity"].ToString();
            gros.iGRNDELADV = ViewState["advicenote"].ToString();
            gros.iGRNDATE = ViewState["receivedDate"].ToString();
            gros.iSTOR = ViewState["store"].ToString();
            gros.iBINLOCN = ViewState["location"].ToString();



            if (gros.Execute())
            {
                ShowMessage("GROS Update Successful");

            }
            else
            {
                ShowMessage("GROS Failed: " + gros.Errors[0].Message.ToString());
                return;
            }


            #region LEGACY CODE - GRIS TRANSACTION 
            //// GRIS = Store and Location
            //GRIS gris = new GRIS();
            //gris.iGRNUMBERENT = gros.Results[0].Fields["vGRNUMBER"].ToString();
            //gris.iSTOR = ViewState["store"].ToString();
            //gris.iBINLOCN = ViewState["location"].ToString();
            //gris.iREPORTDEST_1 = "ERP_HP2300";


            //if (gris.Execute())
            //{
            //    ShowMessage("GRIS Update Successful");
            //}
            //else
            //{
            //    ShowMessage(gris.Errors);
            //    return;
            //}
            #endregion


            // ATVM = country of origin, container type, number of containers, pallet type, number of pallets, 
            ATVM atvm = new ATVM();
            atvm.iATTRIBTYPE = "L";
            atvm.iATTREFTYP = "RP";
            atvm.iATTKEYVAL = "G" + gros.Results[0].Fields["vGRNUMBER"].ToString();
            atvm.iATTVALFROM_T10_0 = ViewState["numberofpallets"].ToString();
            atvm.iATTVALFROM_T10_1 = ViewState["pallettype"].ToString();
            atvm.iATTVALFROM_T10_2 = ViewState["numberofcontainers"].ToString();
            atvm.iATTVALFROM_T10_3 = ViewState["containertype"].ToString();
            atvm.iATTVALFROM_T10_4 = ViewState["receivedquantity"].ToString();
            atvm.iATTVALFROM_T10_5 = ViewState["countryoforigin"].ToString();

            if (atvm.Execute())
            {
                // ALL TRANSACTIONS EXECUTED, TRIGGER SUCCESS MESSAGE
                AlertSUCCESS.Visible = true;
                updSuccessMessage.Update();
            }
            else
            {
                ShowMessage("GROS succeeded, ATVM Failed: " + atvm.Errors[0]);
                return;
            }

            ShowMessage(string.Empty);
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
            string Details = string.Empty;
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
                    // Put data entry in gridview into viewstate

                    TroposInput receivedQty = (TroposInput)grdGOODSIN.Rows[i].Cells[5].FindControl("iRECEIVEDQTY");
                    ViewState["receivedQty_" + i.ToString()] = receivedQty.Text;

                    TroposInput adviceNote = (TroposInput)grdGOODSIN.Rows[i].Cells[6].FindControl("iADVICENOTE");
                    ViewState["adviceNote_" + i.ToString()] = adviceNote.Text;

                    TroposBusinessCalendar receivedDate = (TroposBusinessCalendar)grdGOODSIN.Rows[i].Cells[7].FindControl("tbcRECEIVEDDATE");
                    ViewState["receivedDate_" + i.ToString()] = receivedDate.Text;

                    TroposInput store = (TroposInput)grdGOODSIN.Rows[i].Cells[8].FindControl("iSTORE");
                    ViewState["store_" + i.ToString()] = store.Text;

                    TroposInput location = (TroposInput)grdGOODSIN.Rows[i].Cells[9].FindControl("iLOCATION");
                    ViewState["location_" + i.ToString()] = location.Text;

                    DropDownList countryOfOrigin = (DropDownList)grdGOODSIN.Rows[i].Cells[10].FindControl("iCOUNTRYOFORIGIN");
                    ViewState["countryOfOrigin_" + i.ToString()] = countryOfOrigin.Text;

                    DropDownList containerType = (DropDownList)grdGOODSIN.Rows[i].Cells[11].FindControl("iCONTAINERTYPE");
                    ViewState["containerType_" + i.ToString()] = containerType.Text;

                    TroposInput numberOfContainers = (TroposInput)grdGOODSIN.Rows[i].Cells[12].FindControl("iNUMBEROFCONTAINERS");
                    ViewState["numberOfContainers_" + i.ToString()] = numberOfContainers.Text;

                    DropDownList palletType = (DropDownList)grdGOODSIN.Rows[i].Cells[13].FindControl("iPALLETTYPE");
                    ViewState["palletType_" + i.ToString()] = palletType.Text;

                    TroposInput numberOfPallets = (TroposInput)grdGOODSIN.Rows[i].Cells[14].FindControl("iNUMBEROFPALLETS");
                    ViewState["numberOfPallets_" + i.ToString()] = numberOfPallets.Text;



                }
                catch (Exception)
                {
                    // no exception                 
                }
            }

            // e.command arguement is the row index to be written to file.  Get data from input fields and validate
            int index = Convert.ToInt32(e.CommandArgument);

            ViewState["purchaseOrder"] = iPURCHASE_ORDER.Text;
            TroposInput ReceivedQty = (TroposInput)grdGOODSIN.Rows[index].Cells[5].FindControl("iRECEIVEDQTY");
            TroposInput AdviceNote = (TroposInput)grdGOODSIN.Rows[index].Cells[6].FindControl("iADVICENOTE");
            TroposBusinessCalendar ReceivedDate = (TroposBusinessCalendar)grdGOODSIN.Rows[index].Cells[7].FindControl("tbcRECEIVEDDATE");
            TroposInput Store = (TroposInput)grdGOODSIN.Rows[index].Cells[8].FindControl("iSTORE");
            TroposInput Location = (TroposInput)grdGOODSIN.Rows[index].Cells[9].FindControl("iLOCATION");
            DropDownList CountryOfOrigin = (DropDownList)grdGOODSIN.Rows[index].Cells[10].FindControl("iCOUNTRYOFORIGIN");
            DropDownList ContainerType = (DropDownList)grdGOODSIN.Rows[index].Cells[11].FindControl("iCONTAINERTYPE");
            TroposInput NumberOfContainers = (TroposInput)grdGOODSIN.Rows[index].Cells[12].FindControl("iNUMBEROFCONTAINERS");
            DropDownList PalletType = (DropDownList)grdGOODSIN.Rows[index].Cells[13].FindControl("iPALLETTYPE");
            TroposInput NumberOfPallets = (TroposInput)grdGOODSIN.Rows[index].Cells[14].FindControl("iNUMBEROFPALLETS");
            ViewState["LineNo"] = grdGOODSIN.Rows[index].Cells[0].InnerText().Replace("<center>", string.Empty).Replace("</center>", string.Empty).Trim();

            if (!Validation(index))
            {
                return;
            }


            ViewState["receivedquantity"] = ReceivedQty.Text;
            ViewState["advicenote"] = AdviceNote.Text;
            ViewState["receivedDate"] = ReceivedDate.Text;
            ViewState["store"] = Store.Text;
            ViewState["location"] = Location.Text;
            ViewState["countryoforigin"] = CountryOfOrigin.Text;
            ViewState["containertype"] = ContainerType.Text;
            ViewState["numberofcontainers"] = NumberOfContainers.Text;
            ViewState["pallettype"] = PalletType.Text;
            ViewState["numberofpallets"] = NumberOfPallets.Text;


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

                    ReceivedQty = (TroposInput)grdGOODSIN.Rows[i].Cells[5].FindControl("iRECEIVEDQTY");
                    ReceivedQty.Text = ViewState["receivedQty_" + i.ToString()].ToString();

                    AdviceNote = (TroposInput)grdGOODSIN.Rows[i].Cells[6].FindControl("iADVICENOTE");
                    AdviceNote.Text = ViewState["adviceNote_" + i.ToString()].ToString();

                    ReceivedDate = (TroposBusinessCalendar)grdGOODSIN.Rows[i].Cells[7].FindControl("tbcRECEIVEDDATE");
                    ReceivedDate.Text = ViewState["receivedDate_" + i.ToString()].ToString();

                    Store = (TroposInput)grdGOODSIN.Rows[i].Cells[8].FindControl("iSTORE");
                    Store.Text = ViewState["store_" + i.ToString()].ToString();

                    Location = (TroposInput)grdGOODSIN.Rows[i].Cells[9].FindControl("iLOCATION");
                    Location.Text = ViewState["location_" + i.ToString()].ToString();

                    CountryOfOrigin = (DropDownList)grdGOODSIN.Rows[i].Cells[10].FindControl("iCOUNTRYOFORIGIN");
                    CountryOfOrigin.Text = ViewState["countryOfOrigin_" + i.ToString()].ToString();

                    ContainerType = (DropDownList)grdGOODSIN.Rows[i].Cells[11].FindControl("iCONTAINERTYPE");
                    ContainerType.Text = ViewState["containerType_" + i.ToString()].ToString();

                    NumberOfContainers = (TroposInput)grdGOODSIN.Rows[i].Cells[12].FindControl("iNUMBEROFCONTAINERS");
                    NumberOfContainers.Text = ViewState["numberOfContainers_" + i.ToString()].ToString();

                    PalletType = (DropDownList)grdGOODSIN.Rows[i].Cells[13].FindControl("iPALLETTYPE");
                    PalletType.Text = ViewState["palletType_" + i.ToString()].ToString();

                    NumberOfPallets = (TroposInput)grdGOODSIN.Rows[i].Cells[14].FindControl("iNUMBEROFPALLETS");
                    NumberOfPallets.Text = ViewState["numberOfPallets_" + i.ToString()].ToString();


                }
                catch (Exception)
                {
                    // no error handling implemented
                }

            }

            ReceivedQty.HighlightError = false;
            AdviceNote.HighlightError = false;
            ReceivedDate.HighlightError = false;
            Store.HighlightError = false;
            Location.HighlightError = false;
            NumberOfContainers.HighlightError = false;
            NumberOfPallets.HighlightError = false;

            ShowMessage(string.Empty);
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

        //protected void tbcDATE_REQUIRED_DateSelected(object sender, EventArgs e)
        //{
        //    tab_Clear_Click(new object(), new EventArgs());

        //    grdGOODSIN.Visible = false;

        //    string dateRequired = tbcDATE_REQUIRED.Text;
        //    ITroposQuery davlConsignmentQuery = new Populate_davlCONSIGNMENT(UserContext, dateRequired);
        //    DataTable davlConsignmentDT = Helpers.TroposQuery(davlConsignmentQuery, UserContext);
        //    if (davlConsignmentDT.Rows.Count == 0)
        //    {
        //        string code = string.Empty;
        //        string text = string.Empty;
        //        DAVLUserDefinedData davlData = new DAVLUserDefinedData(code, text);
        //        //iPURCHASE_ORDER.UserDefined.Add(davlData);
        //    }
        //    else
        //    {
        //        for (int i = 0; i < davlConsignmentDT.Rows.Count; i++)
        //        {
        //            string code = davlConsignmentDT.Rows[i][0].ToString();
        //            string text = davlConsignmentDT.Rows[i][1].ToString();
        //            DAVLUserDefinedData davlData = new DAVLUserDefinedData(code, text);
        //            //iPURCHASE_ORDER.UserDefined.Add(davlData);

        //            try
        //            {
        //                UpdateGoodsInTable();
        //            }
        //            catch
        //            {
        //                grdGOODSIN.Visible = false;
        //            }
        //        }
        //    }
        //}

        protected void tab_Clear_main_Click(object sender, CommandEventArgs e)
        {
            int i = Convert.ToInt32(e.CommandArgument);

            TroposInput ReceivedQty = (TroposInput)grdGOODSIN.Rows[i].Cells[6].FindControl("iRECEIVEDQTY");
            ReceivedQty.Text = string.Empty;
            DropDownList UOM = (DropDownList)grdGOODSIN.Rows[i].Cells[7].FindControl("iUOM");
            UOM.ClearSelection();
            TroposInput AdviceNote = (TroposInput)grdGOODSIN.Rows[i].Cells[8].FindControl("iADVICENOTE");
            AdviceNote.Text = string.Empty;
            TroposBusinessCalendar ReceivedDate = (TroposBusinessCalendar)grdGOODSIN.Rows[i].Cells[10].FindControl("tbcRECEIVEDDATE");
            ReceivedDate.Text = DateTime.Today.ToShortDateString();

            ReceivedQty.HighlightError = false;
            AdviceNote.HighlightError = false;
            ShowMessage(string.Empty);
        }

        protected void tab_Clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
            {
                try
                {


                    TroposInput ReceivedQty = (TroposInput)grdGOODSIN.Rows[i].Cells[6].FindControl("iRECEIVEDQTY");
                    ReceivedQty.Text = string.Empty;
                    DropDownList UOM = (DropDownList)grdGOODSIN.Rows[i].Cells[7].FindControl("iUOM");
                    UOM.Text = string.Empty;
                    TroposInput AdviceNote = (TroposInput)grdGOODSIN.Rows[i].Cells[8].FindControl("iADVICENOTE");
                    AdviceNote.Text = string.Empty;

                    ReceivedQty.HighlightError = false;
                    AdviceNote.HighlightError = false;
                    ShowMessage(string.Empty);
                }

                catch (Exception)
                {
                    // no error handling required
                }
            }
        }

        protected void UpdateGoodsInTable()
        {

            // Get goods in table data
            ITroposQuery grdGOODSIN_Query = new Populate_grdGOODSIN(UserContext, iPURCHASE_ORDER.Text);
            DataTable grd_data_table = Helpers.TroposQuery(grdGOODSIN_Query, UserContext);
            grdGOODSIN.DataSource = grd_data_table;
            grdGOODSIN.DataBind();

            for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
            {
                TroposInput Store = (TroposInput)grdGOODSIN.Rows[i].FindControl("iSTORE");
                Store.Text = grd_data_table.Rows[i]["STOR"].ToString();

                TroposInput Location = (TroposInput)grdGOODSIN.Rows[i].FindControl("iLOCATION");
                Location.Text = grd_data_table.Rows[i]["BINLOCN"].ToString();
            }
            ITroposQuery countryoforigin = new Populate_davlCOUNTRYOFORIGIN(UserContext);
            String ddlName = "iCOUNTRYOFORIGIN";
            PopulateDDLfromQuery(countryoforigin, ddlName);



            ITroposQuery containers = new Populate_davlCONTAINER(UserContext);
            ddlName = "iCONTAINERTYPE";
            PopulateDDLfromQuery(containers, ddlName);

            ITroposQuery pallets = new Populate_davlPALLETS(UserContext);
            ddlName = "iPALLETTYPE";
            PopulateDDLfromQuery(pallets, ddlName);

            if (tcboxINCLUDECOMPLETE.Checked == false)
            {
                for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
                {
                    int outstandingamount;
                    try
                    {
                        outstandingamount = int.Parse(grdGOODSIN.Rows[i].Cells[4].InnerText().Replace("<center>", string.Empty).Replace("</center>", string.Empty).Split(' ')[0].Trim());
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
                    string PrevLineNo = grdGOODSIN.Rows[j].Cells[0].InnerText().Replace("<center>", string.Empty).Replace("</center>", string.Empty).Trim();
                    string CurrentLineNo = grdGOODSIN.Rows[i].Cells[0].InnerText().Replace("<center>", string.Empty).Replace("</center>", string.Empty).Trim();
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
                    grdGOODSIN.Rows[row].Cells[10].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[11].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[12].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[13].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[14].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[13].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[17].Controls.Clear();
                    grdGOODSIN.Rows[row].Cells[18].Controls.Clear();
                }

                if (tcboxINCLUDECOMPLETE.Checked == false)
                {
                    for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
                    {
                        int outstandingamount;
                        try
                        {
                            outstandingamount = int.Parse(grdGOODSIN.Rows[i].Cells[4].InnerText().Replace("<center>", string.Empty).Replace("</center>", string.Empty).Split(' ')[0].Trim());
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

        protected void PopulateDDLfromQuery(ITroposQuery itroposQuery, String dropdowncontrolName)
        {
            try
            {
                DataTable dt = Helpers.TroposQuery(itroposQuery, UserContext);

                for (int i = 0; i < grdGOODSIN.Rows.Count; i++)
                {
                    DropDownList ddl = (DropDownList)grdGOODSIN.Rows[i].FindControl(dropdowncontrolName);
                    ddl.Items.Add(string.Empty);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        ddl.Items.Add(dt.Rows[j][0].ToString());
                    }
                }
            }
            catch
            {
                // Don't throw         
            }

        }

        protected bool Validation(int index)
        {
            ViewState["purchaseOrder"] = iPURCHASE_ORDER.Text;
            TroposInput ReceivedQty = (TroposInput)grdGOODSIN.Rows[index].Cells[5].FindControl("iRECEIVEDQTY");
            TroposInput AdviceNote = (TroposInput)grdGOODSIN.Rows[index].Cells[6].FindControl("iADVICENOTE");
            TroposBusinessCalendar ReceivedDate = (TroposBusinessCalendar)grdGOODSIN.Rows[index].Cells[7].FindControl("tbcRECEIVEDDATE");
            TroposInput Store = (TroposInput)grdGOODSIN.Rows[index].Cells[8].FindControl("iSTORE");
            TroposInput Location = (TroposInput)grdGOODSIN.Rows[index].Cells[9].FindControl("iLOCATION");
            DropDownList CountryOfOrigin = (DropDownList)grdGOODSIN.Rows[index].Cells[10].FindControl("iCOUNTRYOFORIGIN");
            DropDownList ContainerType = (DropDownList)grdGOODSIN.Rows[index].Cells[11].FindControl("iCONTAINERTYPE");
            TroposInput NumberOfContainers = (TroposInput)grdGOODSIN.Rows[index].Cells[12].FindControl("iNUMBEROFCONTAINERS");
            DropDownList PalletType = (DropDownList)grdGOODSIN.Rows[index].Cells[13].FindControl("iPALLETTYPE");
            TroposInput NumberOfPallets = (TroposInput)grdGOODSIN.Rows[index].Cells[14].FindControl("iNUMBEROFPALLETS");
            ViewState["LineNo"] = grdGOODSIN.Rows[index].Cells[0].InnerText().Replace("<center>", string.Empty).Replace("</center>", string.Empty).Trim();


            // Entry Validation
            float floatValidation;
            int intValidation;


            // RECEIVED QUANTITY / TROPOSINPUT - FLOAT
            if (!float.TryParse(ReceivedQty.Text, out floatValidation))
            {
                ShowMessage("Received Quantity is not a number");
                ReceivedQty.HighlightError = true;
                return false;
            }

            if (float.Parse(ReceivedQty.Text) == 0)
            {
                ShowMessage("Please enter valid, non-zero value for received quantity");
                ReceivedQty.HighlightError = true;
                return false;
            }

            if (float.Parse(ReceivedQty.Text) < 0)
            {
                ShowMessage("Please enter a valid, non-zero value for received quantity");
                ReceivedQty.HighlightError = true;
                return false;
            }
            // VALIDATION PASSED
            ShowMessage(string.Empty);
            ReceivedQty.HighlightError = false;



            // ADVICE NOTE / TROPOSINPUT - NON BLANK          
            if (AdviceNote.Text.Trim() == string.Empty)
            {
                ShowMessage("Please enter an advice note");
                AdviceNote.HighlightError = true;
                return false;
            }
            // VALIATION PASSED
            ShowMessage(string.Empty);
            AdviceNote.HighlightError = false;


            // RECEIVED DATE - TROPOSBUSINESSCALENDAR / VALID DATE, NOT BLANK
            DateTime datetime;
            if (!DateTime.TryParse(ReceivedDate.Text, out datetime))
            {
                ShowMessage("Please enter a valid date");
                ReceivedDate.HighlightError = true;
                return false;
            }
            //VALIDATION PASSED
            ShowMessage(string.Empty);
            ReceivedDate.HighlightError = false;



            // STORE & LOCATION / DEFAULT FOR ITEM PULLED THROUGH FROM SQL QUERY (Populate_grdGOODSIN.cs) - MUST BE VALID STORE AND LOCATION COMBINATION
            SLDY sldy = new SLDY();
            sldy.iSTOR_SL = Store.Text;
            sldy.iBINLOCN_SL = Location.Text;
            if (!sldy.Execute(true))
            {
                ShowMessage("Please enter a valid Store and Location");
                Store.HighlightError = true;
                Location.HighlightError = true;
                return false;
            }
            // VALIDATION PASSED
            ShowMessage(string.Empty);
            Store.HighlightError = false;
            Location.HighlightError = false;



            // COUNTRY OF ORIGIN / DROPDOWN - NON BLANK
            if (CountryOfOrigin.Text.Trim() == string.Empty)
            {
                ShowMessage("Please enter a country of origin");
                CountryOfOrigin.BackColor = ColorTranslator.FromHtml("#ea542e");
                CountryOfOrigin.ForeColor = Color.White;
                return false;
            }
            // VALIDATION PASSED
            ShowMessage(string.Empty);
            CountryOfOrigin.BackColor = Color.Transparent;
            CountryOfOrigin.ForeColor = Color.Black;



            // CONTAINER TYPE / DROPDOWN - NON BLANK
            if (ContainerType.Text == string.Empty)
            {
                ShowMessage("Please select a container type");
                ContainerType.BackColor = ColorTranslator.FromHtml("#ea542e");
                ContainerType.ForeColor = Color.White;
                return false;
            }
            // VALIDATION PASSED
            ShowMessage(string.Empty);
            ContainerType.BackColor = Color.Transparent;
            ContainerType.ForeColor = Color.Black;



            // NUMBER OF CONTAINERS / TEXT FIELD - INTEGER
            if (!int.TryParse(NumberOfContainers.Text, out intValidation))
            {
                ShowMessage("Please enter a valid, non-zero value for the number of containers");
                NumberOfContainers.HighlightError = true;
                return false;
            }

            if (int.Parse(NumberOfContainers.Text) == 0)
            {
                ShowMessage("Please enter a valid, non-zero value for the number of containers");
                NumberOfContainers.HighlightError = true;
                return false;
            }
            if (int.Parse(NumberOfContainers.Text) < 0)
            {
                ShowMessage("Please enter a valid, non-zero value for the number of containers");
                NumberOfContainers.HighlightError = true;
                return false;
            }
            // VALIDATION PASSED
            ShowMessage(string.Empty);
            NumberOfContainers.HighlightError = false;



            //PALLET TYPE - DROPDOWN - NON BLANK
            if (PalletType.Text == string.Empty)
            {
                ShowMessage("Please select pallet type");
                PalletType.BackColor = ColorTranslator.FromHtml("#ea542e");
                PalletType.ForeColor = Color.White;
                return false;
            }
            // VALIDATION PASSED
            ShowMessage(string.Empty);
            PalletType.BackColor = Color.Transparent;
            PalletType.ForeColor = Color.Black;



            // NUMBER OF PALLETS / TEXT FIELD - INTEGER
            if (!int.TryParse(NumberOfPallets.Text, out intValidation))
            {
                ShowMessage("Please enter a valid, non-zero value for number of pallets");
                NumberOfPallets.HighlightError = true;
                return false;
            }

            if (int.Parse(NumberOfPallets.Text) <= 0)
            {
                ShowMessage("Please enter a valid, non-zero value for number of pallets");
                NumberOfPallets.HighlightError = true;
                return false;
            }
            // VALIDATION PASSED
            ShowMessage(string.Empty);
            NumberOfPallets.HighlightError = false;



            // ALL VALIDATION PASSED
            return true;
        }
    }
}

