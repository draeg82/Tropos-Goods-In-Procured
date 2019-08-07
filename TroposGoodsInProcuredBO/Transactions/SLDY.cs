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
    public class SLDY
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
        ///NOT USED - REDUNDANT
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLOCCAT { get; set; }

        /// <summary>
        ///The type of storage location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vSLTYPE { get; set; }

        /// <summary>
        ///This field is documentary.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vSLSTATUS { get; set; }

        /// <summary>
        ///Current Compatibility Code              
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vITCOMPCDE_CUR { get; set; }

        /// <summary>
        ///Defines how stock can be allocated from this location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vALLOCCAT { get; set; }

        /// <summary>
        ///Contains a code which relates to the storage conditions of
        ///this store/location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vITSTORCOND_SL { get; set; }

        /// <summary>
        ///The unit in which the storage location capacity SLCAPACITY
        ///and the safety margin SLSAFETYLEV are measured.
        /// </summary>
        [FieldName("vUOM-SLC$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_SLC_0 { get; set; }

        /// <summary>
        ///The unit in which the storage location capacity SLCAPACITY
        ///and the safety margin SLSAFETYLEV are measured.
        /// </summary>
        [FieldName("vUOM-SLC$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_SLC_1 { get; set; }

        /// <summary>
        ///This shows the loading/unloading status of a lorry (truck)
        ///location. This is use by the lorry loading and unloading
        ///transactions (LRYL etc.). A lorry is a location in the store
        ///defined by Transport Store on WHMN (Warehouse Maintenance).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vTRANSTAT { get; set; }

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
        ///Warehouse                               
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iWAREHOUSE_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiWAREHOUSE_SL { get; set; }

        /// <summary>
        ///Store for the Storage Location          
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSTOR_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSTOR_SL { get; set; }

        /// <summary>
        ///Defines the storage location within the factory for the
        ///storage of the item.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iBINLOCN_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiBINLOCN_SL { get; set; }

        /// <summary>
        ///Storage Location Description            
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iDESCRIPTION_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiDESCRIPTION_SL { get; set; }

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
        ///Relates the Storage Location to a Cost Centre for cost
        ///accounting purposes. NB. this is only relevant if the
        ///location is defined as a resource.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iCOSTCENTRE_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiCOSTCENTRE_SL { get; set; }

        /// <summary>
        ///To identify the Storage Location with a Cost Code for cost
        ///accounting purposes.
        ///N.B. This is only relevant if the location is defined as a
        ///resource.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iCOSTCODE_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiCOSTCODE_SL { get; set; }

        /// <summary>
        ///The Resource Number by which this storage location is
        ///identified. By defining a storage location as a resource
        ///this allows the location to be used as a critical resource
        ///within the Scheduling module.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iRESOURCENO_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiRESOURCENO_SL { get; set; }

        /// <summary>
        ///If this indicator is changed to Y then stock at this
        ///location will be ignored by material call-off unless the
        ///Store matches the Primary MCO Store associated with the
        ///lineside location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iMCORESTRICT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiMCORESTRICT { get; set; }

        /// <summary>
        ///Defines the way that the location is used. Some values
        ///invoke specific server processing while others are used by
        ///user interface aplications.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLOCCAT_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLOCCAT_SL { get; set; }

        /// <summary>
        ///Printer for reorder point tickets; used when stock at a
        ///location falls below a specified reorder point.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iREORDERDEST_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiREORDERDEST_SL { get; set; }

        /// <summary>
        ///The type of storage location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLTYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLTYPE { get; set; }

        /// <summary>
        ///The unit in which the storage location capacity SLCAPACITY
        ///and the safety margin SLSAFETYLEV are measured.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_SLC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_SLC { get; set; }

        /// <summary>
        ///This field is documentary.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLSTATUS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLSTATUS { get; set; }

        /// <summary>
        ///Current Compatibility Code              
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iITCOMPCDE_CUR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiITCOMPCDE_CUR { get; set; }

        /// <summary>
        ///Indicates whether the location's Compatibility Code should
        ///be automatically blanked out once the location is empty,
        ///used by the STMV transaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iITRESETCCODE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiITRESETCCODE { get; set; }

        /// <summary>
        ///Indicates the type of checking reqired when attempting to
        ///place stock using STMV. 'N' indicates that no checking of
        ///the item and location compatability codes is required. 'M'
        ///indicates that checking is Mandatory and an error will be
        ///displayed if different. 'O' indicates that checks should be
        ///performed but only a warning issued if different.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iITCOMPATCHK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiITCOMPATCHK { get; set; }

        /// <summary>
        ///The material restrictions relating to the storage location
        ///may be entered as a text string for documentation purposes.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iMATLREST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiMATLREST { get; set; }

        /// <summary>
        ///Defines how stock can be allocated from this location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iALLOCCAT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiALLOCCAT { get; set; }

        /// <summary>
        ///Contains a code which relates to the storage conditions of
        ///this store/location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iITSTORCOND_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiITSTORCOND_SL { get; set; }

        /// <summary>
        ///The flow rate of material into the storage location. This
        ///is measured in its associated unit of measure field
        ///UOM-SLFI.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iFLOWIN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiFLOWIN { get; set; }

        /// <summary>
        ///The units in which the input flow rate to a storage
        ///location FLOWIN, is measured.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_SLFI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_SLFI { get; set; }

        /// <summary>
        ///The unit of time associated with the flow rate of material
        ///into the storage location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iITPRDRTUNS_SLFI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiITPRDRTUNS_SLFI { get; set; }

        /// <summary>
        ///Storage Rule                            
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSTORAGERULE_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSTORAGERULE_SL { get; set; }

        /// <summary>
        ///This is the flow rate of material out of the storage
        ///location in its associated unit of measure field UOM-SLFO.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iFLOWOUT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiFLOWOUT { get; set; }

        /// <summary>
        ///The unit in which the output flow rate of a storage
        ///location FLOWOUT is measured.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_SLFO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_SLFO { get; set; }

        /// <summary>
        ///The unit of time associated with the flow rate of material
        ///out of the storage location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iITPRDRTUNS_SLFO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiITPRDRTUNS_SLFO { get; set; }

        /// <summary>
        ///Indicates which is the location's critical dimension. Used
        ///to determine how much stock can be placed in the location by
        ///the STMV transaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iITCRITICLDIM_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiITCRITICLDIM_SL { get; set; }

        /// <summary>
        ///Indicates whether a location's Critical Dimension Capacity
        ///can be exceeded. 'N' means the location cannot be overloaded
        ///and an error will be dislayed. 'Y' means a warning will be
        ///dislayed when overloading the location during the STMV trans
        ///action.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLOVERFILL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLOVERFILL { get; set; }

        /// <summary>
        ///The height of the storage location in its associated unit
        ///of measure UOM-SLH.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLHEIGHT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLHEIGHT { get; set; }

        /// <summary>
        ///It is connected with dataname SLHEIGHT and is the unit in
        ///which the storage location height is measured.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_SLH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_SLH { get; set; }

        /// <summary>
        ///The width of the storage location in its associated unit
        ///of measure UOM-SLW.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLWIDTH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLWIDTH { get; set; }

        /// <summary>
        ///It is connected with dataname SLWIDTH and is the unit in
        ///which the storage location width is measured.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_SLW { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_SLW { get; set; }

        /// <summary>
        ///The depth of the storage location in its associated unit
        ///of measure UOM-SLD.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLDEPTH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLDEPTH { get; set; }

        /// <summary>
        ///It is the unit in which the SLDEPTH, storage location
        ///depth is measured.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_SLD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_SLD { get; set; }

        /// <summary>
        ///The maximum weight of the storage location in its
        ///associated unit of measure UOM-SLWT.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLWEIGHT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLWEIGHT { get; set; }

        /// <summary>
        ///It is connected with dataname SLWEIGHT and is the unit in
        ///which the storage location weight is measured.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_SLWT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_SLWT { get; set; }

        /// <summary>
        ///The capacity of the storage location measured in UOM- SLC.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLCAPACITY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLCAPACITY { get; set; }

        /// <summary>
        ///The safety margin in the unit of measure field normally
        ///associated with the storage location capacity SLCAPACITY.
        ///The Unit of measure field is UOM-SLC.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLSAFETYLEV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLSAFETYLEV { get; set; }

        /// <summary>
        ///This shows the loading/unloading status of a lorry (truck)
        ///location. This is use by the lorry loading and unloading
        ///transactions (LRYL etc.). A lorry is a location in the store
        ///defined by Transport Store on WHMN (Warehouse Maintenance).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iTRANSTAT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiTRANSTAT { get; set; }

        /// <summary>
        ///This holds the despatch location from which a lorry
        ///(truck) was loaded. This used by the lorry loading and
        ///unloading transactions (LRYL etc.). A despatch location is a
        ///location in the despatch store. The despatch store is
        ///defined on WHMN (Maintain Warehouse Data) for a warehouse or
        ///the main plant.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLOCBIN_DESP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLOCBIN_DESP { get; set; }

        /// <summary>
        ///Update Balance indicator                
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_UPDBAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_UPDBAL { get; set; }

        /// <summary>
        ///Indicates whether Materials Planning should consider the
        ///stock held at this location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iMRPINCIND_SL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiMRPINCIND_SL { get; set; }

        /// <summary>
        ///This field indicates whether the contents of the location
        ///are to be included in the Planned Perpetual Inventory
        ///Counting process.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iINCLPIND { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiINCLPIND { get; set; }

        /// <summary>
        ///A YN flag indicating whether related text is to be
        ///maintained or retrieved.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_TEXT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_TEXT { get; set; }

        /// <summary>
        ///The number of lines of text associated with this
        ///storage location.
        ///POSITIVE NUMERIC
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSLTEXTLINES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSLTEXTLINES { get; set; }
        public SLDY()
        {
            ClearFields();
        }

        public SLDY(Dictionary<string, string> fields)
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
            string action = "SLDY";
			TransactionDTO SLDY = getDTO(action);

#if TroposDesktop || TroposWebService
            using (Transactions trans = new Transactions())
            {
#else
            using (TDK.Common.Helper.Transactions trans = new TDK.Common.Helper.Transactions())
            {
#endif
                trans.DataValid += Trans_DataValid;

				trans.AddTransaction(SLDY);

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
		    TransactionDTO SLDY = new TransactionDTO(action);
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
                        SLDY.Fields.Add(name, property.GetValue(this, null));
                    else
                        SLDY.Fields.Add(name, string.Empty);
                }
            }
			SLDY.ScrollLines = MaxScrollLines;
			return SLDY;
		}

    }
}
