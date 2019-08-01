using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
#if TroposDesktop
using Tropos.Desktop;
using Tropos.Desktop.DataUpdater;
#elif TroposWebService
using Tropos.WebService;
using Tropos.WebService.DataUpdater;
#else
using TDK.Common.Helper;
using TroposUI.Common;
using TroposUI.Common.DataUpdater;
#endif

namespace TroposGoodsInProcuredBO.Transactions
{
    public class GRNQ
    {
        public event EventHandler<DataValidEventArgs> DataValid;

        private IList<TransactionError> _errors;
        /// <summary>
        /// Collection of TransactionErrors returned by transaction execution
        /// </summary>
       public IList<TransactionError> Errors
        {
            get { return _errors; }
        }
        private IList<TransactionResults> _results;
        /// <summary>
        /// Provides access to the objects returned by transaction execution.
        /// </summary>
        public IList<TransactionResults> Results
        {
            get { return _results; }
        }

        /// <summary>
        /// Maximum number of scrolling lines to be returned by the transaction.  Set to 0 to suppress scrolling data altogether.
        /// </summary>
        [ControlField(true)]
        public int MaxScrollLines { get; set; }

        private Collection<string> _ScrollLines;
        /// <summary>
        /// Collection of scrolling data lines returned by transaction execution.
        /// </summary>
        [ControlField(true)]
        public Collection<string> ScrollLines
        {
            get
            {
                return _ScrollLines;
            }
        }

        private Dictionary<string, string> _ActionFields;
		/// <summary>
		/// Provides direct access to the input fields for the Tropos transaction
		/// </summary>
        public Dictionary<string, string> ActionFields
        {
            get
            {
                return _ActionFields;
            }
        }

#if TroposWebService
        [ControlField(true)]
        public int WaitefReturn { get; set; }
#endif


        /// <summary>
        ///                                        
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLINECOUNT { get; set; }

        /// <summary>
        ///This is the first 50 characters of the maximum 100
        ///character description. The full 100 character description is
        ///shown on screens and reports. It is created with IADD and
        ///changed with ICHG. If using item number selection it can be
        ///added and changed with ITDA and ITDC.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vDESCRIPTION { get; set; }

        /// <summary>
        ///A unique code which identifies each supplier. Also used to
        ///identify standard delivery points : addresses within the
        ///user premises to which goods receipts are directed. These
        ///delivery points are set up as pseudo suppliers.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vSUPPLIER { get; set; }

        /// <summary>
        ///This is the name of the supplier.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vSUPNAME { get; set; }

        /// <summary>
        ///The total original order quantity in purchase units of
        ///measure. If set to zero, then no receipts are allowed
        ///against this Purchase Order line.
        /// </summary>
        [FieldName("vPORDQTY$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDQTY_0 { get; set; }

        /// <summary>
        ///This field contains the quantity received so far for the
        ///order line. If the order contains scheduled/extended
        ///deliveries then this field will contain the sum of the
        ///receipts for all of them. The value will be in stock units
        ///for an inventory order or purchase units for other goods
        ///orders.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDREC { get; set; }

        /// <summary>
        ///The total original order quantity in purchase units of
        ///measure. If set to zero, then no receipts are allowed
        ///against this Purchase Order line.
        /// </summary>
        [FieldName("vPORDQTY$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDQTY_1 { get; set; }

        /// <summary>
        ///If this has a value other than 'N', the order is a blanket
        ///order. If it is 'V', the order is a blanket order up to the
        ///specified value. If it is 'Q', the order is a blanket order
        ///up to the specified quantity. If it is 'A', the order is a
        ///blanket order up to the value specified on the agreement
        ///related to the order line.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vBLANKIND_PORD { get; set; }

        /// <summary>
        ///This is the limit of a blanket purchase order. It will
        ///either be a value or quantity depending on BLANKIND-PORD.
        /// </summary>
        [FieldName("vBLANKLIM-PORD$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vBLANKLIM_PORD_0 { get; set; }

        /// <summary>
        ///This is the limit of a blanket purchase order. It will
        ///either be a value or quantity depending on BLANKIND-PORD.
        /// </summary>
        [FieldName("vBLANKLIM-PORD$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vBLANKLIM_PORD_1 { get; set; }

        /// <summary>
        ///The field defines the unit of measure of all stock
        ///quantities, stock movements and inventory parameters. It is
        ///maintained with IPAD/IPCH. Once stock movements have
        ///occurred it is no longer possible to change the stock unit
        ///of measure.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_STOCK { get; set; }

        /// <summary>
        ///This redisplays the transaction code.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string fTRAN_HEAD { get; set; }

        /// <summary>
        ///This field is the first entered field on each screen It is
        ///a unique code which identifies the transaction the user
        ///requires to perform.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACTION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACTION { get; set; }

        /// <summary>
        ///The Purchase Order header to which the GRN relates.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDER_GRN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDER_GRN { get; set; }

        /// <summary>
        ///The Purchase Order line number that the GRN relates to.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDITM_GRN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDITM_GRN { get; set; }

        /// <summary>
        ///This identifies the item number on a purchase order.
        ///It must be a stock item if the Purchase Order is an
        ///Inventory type.
        ///It may or may not be a valid item for a non-Inventory P.O.
        ///It must not be a valid item for a Service type P.O.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPARTNO_PORD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPARTNO_PORD { get; set; }

        /// <summary>
        ///The field defines the unit of measure of all stock
        ///quantities, stock movements and inventory parameters. It is
        ///maintained with IPAD/IPCH. Once stock movements have
        ///occurred it is no longer possible to change the stock unit
        ///of measure.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_STOCK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_STOCK { get; set; }

        /// <summary>
        ///This field identifies that the data associated with it
        ///belongs to a particular business. The Business is subdivided
        ///into three parts to identify the Company, Division and Site.
        ///It can be displayed as either Company/Division/Site or as an
        ///abbreviated Mnemonic, depending on the value of the PAK flag
        ///PAKACCDIS (Business Display Type).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACCOUNTSHORT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACCOUNTSHORT { get; set; }

        /// <summary>
        ///Records the date that goods were received.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNDATE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNDATE { get; set; }

        /// <summary>
        ///The total original order quantity in purchase units of
        ///measure. If set to zero, then no receipts are allowed
        ///against this Purchase Order line.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDQTY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDQTY { get; set; }
        public GRNQ()
        {
            ClearFields();
        }

        public GRNQ(Dictionary<string, string> fields)
        {
            _ActionFields = fields;
            ClearFields();
            LoadFields();
        }

        private void ClearFields()
        {
            //Use reflection to set all string properties to empty string
            foreach (var property in this.GetType().GetProperties())
            {
                if (property.PropertyType == typeof(string))
                    property.SetValue(this, string.Empty, null);
            }
        }

        public void LoadFields()
        {
            //Use reflection to populate fields
            foreach (var property in this.GetType().GetProperties())
            {
                if (property.Name.StartsWith("i", System.StringComparison.Ordinal))
                {
                    string fieldValue;
                    string fieldName;

                    // Get the FieldName attribute which will be set for fields containing a dollar
                    FieldNameAttribute[] attribs = property.GetCustomAttributes(
                        typeof(FieldNameAttribute), false) as FieldNameAttribute[];

                    // Return the first if there was a match.
                    if (attribs.Length > 0)
                        fieldName = attribs[0].Text;
                    else
                        fieldName = property.Name.Replace("_", "-");

                    //If field found, set property to supplied value
                    if (_ActionFields.TryGetValue(fieldName, out fieldValue))
                        property.SetValue(this, fieldValue, null);
                }
            }
        }

        /// <summary>
        /// Execute the transaction using the "Single" execution mode.  If validation is successful, it will be executed immediately.
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            return Execute(false);
        }
        /// <summary>
        /// Allow the transaction to be executed in "Validate" mode.
        /// If the parameter is true, then "Validate" mode will be used and the transaction will not be updated.
        /// If the parameter is false, then "Single" mode will be used and the transaction will be updated if validation is successful.
        /// </summary>
        /// <param name="validateMode">true = Validate, false = Single</param>
        /// <returns></returns>
        public bool Execute(bool validateMode)
        {
			return Execute(validateMode ? ExecuteMode.Validate : ExecuteMode.Single);
		}

        /// <summary>
        /// Allow the transaction to be executed with any execute mode.
        /// </summary>
        /// <param name="mode">ExecuteMode: Single; Multiple; DataValid; Validate</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		public bool Execute(ExecuteMode mode)
		{
            string action = "GRNQ";
			TransactionDTO GRNQ = getDTO(action);

#if TroposDesktop || TroposWebService
            using (Transactions trans = new Transactions())
            {
#else
            using (TDK.Common.Helper.Transactions trans = new TDK.Common.Helper.Transactions())
            {
#endif
                trans.DataValid += Trans_DataValid;

				trans.AddTransaction(GRNQ);

				_results = trans.ExecuteTransaction(out _errors, mode);
				
				if (Results.Count > 0)
				{
					//Use reflection to populate each property with the transaction results
					Dictionary<string, object> f = Results[0].Fields;
					_ActionFields = new Dictionary<string, string>();
					foreach (string key in f.Keys)
					{
						string property = key.Replace("$", "_").Replace("-", "_");
						if (this.GetType().GetProperty(property) != null)
							this.GetType().GetProperty(property).SetValue(this, f[key], null);

						if (key.StartsWith("i", System.StringComparison.Ordinal) || key.StartsWith("v", System.StringComparison.Ordinal))
							_ActionFields.Add(key, f[key].ToString());
					}

				}
            }

            if (_results[0].ScrollLines.Rows.Count > 0)
            {
                _ScrollLines = new Collection<string>();
                foreach (DataRow Row in _results[0].ScrollLines.Rows)
                {
                    ScrollLines.Add(Row["ScrollLine"].ToString());
                }
            }
#if TroposWebService
            this.WaitefReturn = _results[0].WaitefReturn;
#endif

            int nonErrors = 0;
            if (_errors.Count > 0)
                //Count the non-errors
                foreach (TransactionError te in _errors)
                    if (te.Severity == TroposErrorSeverity.Severe
                     || te.Severity == TroposErrorSeverity.Validation)
                        break;
                    else
                        nonErrors++;
			//Return true if no errors or all the errors are not severe or validation (warnings, none, informational)
            return (_errors.Count == nonErrors);
        }

        private void Trans_DataValid(object sender, DataValidEventArgs e)
        {
            if (DataValid == null)
            {
#if TroposDesktop || TroposWebService
            using (Transactions trans = new Transactions())
            {
#else
                using (TDK.Common.Helper.Transactions trans = new TDK.Common.Helper.Transactions())
                {
#endif
                    trans.Reply(e.TransactionKey, TroposConfirmationResponse.No);
                }
                throw new InvalidOperationException("DataValid event unhandled");
            }
            else
            {
                DataValid(this, e);
            }
        }

        /// <summary>
        /// Create a TranasctionDTO object which can be added to a collection for execution of multiple transactions at once.
		/// </summary>
        /// <param name="action">Action code</param>
        /// <returns>TransactionDTO object representing the action code passed in</returns>
		public TransactionDTO getDTO(string action)
		{
		    TransactionDTO GRNQ = new TransactionDTO(action);
            foreach (var property in this.GetType().GetProperties())
            {
				bool IsControlField = false;
				ControlFieldAttribute[] ControlAttribs = property.GetCustomAttributes(
					typeof(ControlFieldAttribute), false) as ControlFieldAttribute[];
					
				if (ControlAttribs.Length > 0)
					IsControlField = ControlAttribs[0].Value;
					
                if (property.PropertyType != typeof(IList<TransactionError>)
                  && property.PropertyType != typeof(IList<TransactionResults>)
                  && property.PropertyType != typeof(Dictionary<string, string>)
                  && !property.Name.StartsWith("hi", System.StringComparison.Ordinal)
                  && !IsControlField)
                {
                    string name;

                    // Get the FieldName attribute which will be set for fields containing a dollar
                    FieldNameAttribute[] attribs = property.GetCustomAttributes(
                        typeof(FieldNameAttribute), false) as FieldNameAttribute[];

                    // Return the first if there was a match.
                    if (attribs.Length > 0)
                        name = attribs[0].Text;
                    else
                        name = property.Name.Replace("_", "-");

                    if (name.StartsWith("i", System.StringComparison.Ordinal) ||
                        name == "aText")
                        GRNQ.Fields.Add(name, property.GetValue(this, null));
                    else
                        GRNQ.Fields.Add(name, string.Empty);
                }
            }
			GRNQ.ScrollLines = MaxScrollLines;
			return GRNQ;
		}

    }
}
