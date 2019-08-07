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
    public class ATVM
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
        ///This field identifies the type of attribute being updated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBTYPE { get; set; }

        /// <summary>
        ///Used to redisplay a prompt if a type is required to
        ///qualify the transaction code against which extra data is
        ///held.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vEDKEYPROMPT { get; set; }

        /// <summary>
        ///Fields making up the prime Keys of the file on which the
        ///dataname occurs. May include a group level dataname to which
        ///this dataname is Subordinate, although the group level
        ///dataname is not a key.
        ///A dataname defined as a main key will have the datanames
        ///which are its Sub-keys in these fields.
        /// </summary>
        [FieldName("vDATAKEY$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vDATAKEY_0 { get; set; }

        /// <summary>
        ///Fields making up the prime Keys of the file on which the
        ///dataname occurs. May include a group level dataname to which
        ///this dataname is Subordinate, although the group level
        ///dataname is not a key.
        ///A dataname defined as a main key will have the datanames
        ///which are its Sub-keys in these fields.
        /// </summary>
        [FieldName("vDATAKEY$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vDATAKEY_1 { get; set; }

        /// <summary>
        ///Fields making up the prime Keys of the file on which the
        ///dataname occurs. May include a group level dataname to which
        ///this dataname is Subordinate, although the group level
        ///dataname is not a key.
        ///A dataname defined as a main key will have the datanames
        ///which are its Sub-keys in these fields.
        /// </summary>
        [FieldName("vDATAKEY$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vDATAKEY_2 { get; set; }

        /// <summary>
        ///Fields making up the prime Keys of the file on which the
        ///dataname occurs. May include a group level dataname to which
        ///this dataname is Subordinate, although the group level
        ///dataname is not a key.
        ///A dataname defined as a main key will have the datanames
        ///which are its Sub-keys in these fields.
        /// </summary>
        [FieldName("vDATAKEY$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vDATAKEY_3 { get; set; }

        /// <summary>
        ///This field contains the component ingredient number on
        ///the B02 table and is associated with the component
        ///ingredient number on the B01 table.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPARTNO_B02COM { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_0 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_0 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_0 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_0 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_1 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_1 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_1 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_1 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_2 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_2 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_2 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_2 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_3 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_3 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_3 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_3 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_4 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_4 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_4 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_4 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_5 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_5 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_5 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_5 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_6 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_6 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_6 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_6 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$7")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_7 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$7")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_7 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$7")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_7 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$7")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_7 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$8")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_8 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$8")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_8 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$8")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_8 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$8")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_8 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$9")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_9 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$9")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_9 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$9")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_9 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$9")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_9 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$10")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_10 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$10")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_10 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$10")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_10 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$10")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_10 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$11")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_11 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$11")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_11 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$11")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_11 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$11")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_11 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$12")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_12 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$12")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_12 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$12")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_12 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$12")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_12 { get; set; }

        /// <summary>
        ///Attribute Code                          
        /// </summary>
        [FieldName("vATTRIBCODE-T03$13")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTRIBCODE_T03_13 { get; set; }

        /// <summary>
        ///This field contains the description of the attribute.
        /// </summary>
        [FieldName("vATTDESC$13")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vATTDESC_13 { get; set; }

        /// <summary>
        ///Unit of Measure 1                       
        /// </summary>
        [FieldName("vUOM-ATT1$13")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ATT1_13 { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [FieldName("vLEADTM$13")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLEADTM_13 { get; set; }

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
        ///This field identifies the type of attribute being updated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBTYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBTYPE { get; set; }

        /// <summary>
        ///This field identifies the type of attribute value being
        ///searched or updated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTREFTYP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTREFTYP { get; set; }

        /// <summary>
        ///Show Calculated                         
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_SHCALC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_SHCALC { get; set; }

        /// <summary>
        ///Indicates the action to be performed as follows:
        ///     A - Add
        ///     C - Change
        ///     D - Delete
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND { get; set; }

        /// <summary>
        ///Attribute key value                     
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTKEYVAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTKEYVAL { get; set; }

        /// <summary>
        ///Attribute key value                     
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTKEYVAL_2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTKEYVAL_2 { get; set; }

        /// <summary>
        ///Attribute key value                     
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTKEYVAL_3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTKEYVAL_3 { get; set; }

        /// <summary>
        ///Attribute key value                     
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTKEYVAL_4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTKEYVAL_4 { get; set; }

        /// <summary>
        ///First Attribute Code                    
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_FIR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_FIR { get; set; }

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
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_0 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_0 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_0 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_0 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_0 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_0 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_1 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_1 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_1 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_2 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_2 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_2 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_3 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_3 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_3 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_4 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_4 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_4 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_5 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_5 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_5 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_6 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_6 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_6 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$7")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_7 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$7")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_7 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$7")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_7 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$8")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_8 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_8 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$8")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_8 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_8 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$8")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_8 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_8 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$9")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_9 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_9 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$9")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_9 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_9 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$9")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_9 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_9 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$10")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_10 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_10 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$10")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_10 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_10 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$10")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_10 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_10 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$11")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_11 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_11 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$11")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_11 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_11 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$11")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_11 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_11 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$12")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_12 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_12 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$12")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_12 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_12 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$12")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_12 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_12 { get; set; }

        /// <summary>
        ///Attribute Value From                    
        /// </summary>
        [FieldName("iATTVALFROM-T10$13")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALFROM_T10_13 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALFROM_T10_13 { get; set; }

        /// <summary>
        ///Attribute Value To                      
        /// </summary>
        [FieldName("iATTVALTO-T10$13")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTVALTO_T10_13 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTVALTO_T10_13 { get; set; }

        /// <summary>
        ///Add/Chg/Del Indicator                   
        /// </summary>
        [FieldName("iACDIND-ENT$13")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACDIND_ENT_13 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACDIND_ENT_13 { get; set; }

        /// <summary>
        ///Used by various transactions usually to indicate whether
        ///the user wishes to continue processing data for the same key
        ///details. If 'Y' is entered, the key data remains on the
        ///screen and the cursor is positioned to continue adding
        ///variable data. If 'N' is entered, the screen is cleared and
        ///the cursor positioned to allow different key data to be
        ///entered.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_CONT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_CONT { get; set; }

        /// <summary>
        ///Yes or No                               
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_UPDSTK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_UPDSTK { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_0 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_0 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_0 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_0 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_1 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_1 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_2 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$2")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_2 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_3 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$3")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_3 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_4 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_4 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_4 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_5 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$5")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_5 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_5 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_6 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$6")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_6 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_6 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$7")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_7 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$7")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_7 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$8")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_8 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_8 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$8")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_8 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_8 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$9")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_9 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_9 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$9")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_9 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_9 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$10")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_10 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_10 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$10")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_10 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_10 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$11")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_11 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_11 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$11")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_11 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_11 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$12")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_12 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_12 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$12")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_12 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_12 { get; set; }

        /// <summary>
        ///This field contains the identifier by which an attribute
        ///is referenced. It is a user-defined value.
        /// </summary>
        [FieldName("iATTRIBCODE$13")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTRIBCODE_13 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTRIBCODE_13 { get; set; }

        /// <summary>
        ///This field identifies the sequence in which an attribute
        ///is presented for display or for update.
        /// </summary>
        [FieldName("iATTSEQ$13")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTSEQ_13 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTSEQ_13 { get; set; }

        /// <summary>
        ///Structures are broken down by Process Stage, which
        ///represent a standard operation or set of operations in the
        ///production process, to which Ingredient/Component Items and
        ///Resources can be allocated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPROCSTAGE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPROCSTAGE { get; set; }

        /// <summary>
        ///The sales order line number. Can be entered, or generated
        ///by the system. Will default to one greater than the highest
        ///item number that already exists. The order line must not
        ///already exist.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSORDITEM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSORDITEM { get; set; }

        /// <summary>
        ///The Purchase Order line number.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDITM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDITM { get; set; }

        /// <summary>
        ///Lead Time is used to hold a number of days; generally the
        ///number of days required to produce or deliver an item.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLEADTM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLEADTM { get; set; }

        /// <summary>
        ///Transaction Code                        
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iATTACTION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiATTACTION { get; set; }

        /// <summary>
        ///Key to the Parent Data                  
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iEXDATAKEY2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiEXDATAKEY2 { get; set; }

        /// <summary>
        ///15 Character Account Code               
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACCOUNT15 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACCOUNT15 { get; set; }

        /// <summary>
        ///Yes or No                               
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_LOAD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_LOAD { get; set; }

        /// <summary>
        ///Internal sequence number, for an ingredient or component,
        ///within a stage.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iFORMSEQ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiFORMSEQ { get; set; }

        /// <summary>
        ///This identifies a shipment of goods. It may relate to one
        ///customer or to more than one customer e.g. if despatching by
        ///the same method to the same delivery area. A shipment can
        ///contain several order consignments.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSHIPNUM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSHIPNUM { get; set; }

        /// <summary>
        ///Package Number                          
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPACKNUM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPACKNUM { get; set; }

        /// <summary>
        ///Sequence number used to determine the sequence in which
        ///actions are to be performed.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACTIONSEQ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACTIONSEQ { get; set; }

        /// <summary>
        ///System maintained sequence number that uniquely identifies
        ///the activities logged against an action.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLOGSEQ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLOGSEQ { get; set; }

        /// <summary>
        ///Used to distinguish between different addresses for a
        ///customer/supplier.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iNADSEQNO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiNADSEQNO { get; set; }

        /// <summary>
        ///System generated number that identifies a loading bay
        ///activity within the TROPOS database.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLBACTIVITYNO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLBACTIVITYNO { get; set; }
        public ATVM()
        {
            ClearFields();
        }

        public ATVM(Dictionary<string, string> fields)
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
            string action = "ATVM";
			TransactionDTO ATVM = getDTO(action);

#if TroposDesktop || TroposWebService
            using (Transactions trans = new Transactions())
            {
#else
            using (TDK.Common.Helper.Transactions trans = new TDK.Common.Helper.Transactions())
            {
#endif
                trans.DataValid += Trans_DataValid;

				trans.AddTransaction(ATVM);

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
		    TransactionDTO ATVM = new TransactionDTO(action);
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
                        ATVM.Fields.Add(name, property.GetValue(this, null));
                    else
                        ATVM.Fields.Add(name, string.Empty);
                }
            }
			ATVM.ScrollLines = MaxScrollLines;
			return ATVM;
		}

    }
}
