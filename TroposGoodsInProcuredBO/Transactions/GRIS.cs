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
    public class GRIS
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
        ///Uniquely defines a purchase order. Can be automatically
        ///assigned by the system. It consists of two parts, the first
        ///character is the company code for use when more than one
        ///business on the same site uses the TROPOS purchase order
        ///system, the second part of six characters is a numeric order
        ///number which can be automatically maintained by the system.
        ///It is the header purchase order number attached to the item
        ///number.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDER { get; set; }

        /// <summary>
        ///Purchase Order Line Number              
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDITM_ITEM { get; set; }

        /// <summary>
        ///The quantity advised in stock units. Is the conversion of
        ///the value in GRNADVISE. Conversion is done by UOMCONV and/or
        ///the factor UOMFACTOR.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vGRNADVSTK { get; set; }

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
        ///The number of marshalled or issued shortages for the item.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vSHORT { get; set; }

        /// <summary>
        ///Output field calculated as (GRNREC - GRNBIN - GRNREJ).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vGRNQTYOUT { get; set; }

        /// <summary>
        ///The quantity accepted as good stock, stated in stock
        ///units.
        ///Automatically set equal to GRNREC for non inventory
        ///goods orders.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vGRNBIN { get; set; }

        /// <summary>
        ///The Control Reference allows an order to be associated
        ///with a contract item. This field can also be used as a
        ///general reference if the order is not controlled. See also
        ///dataname text for CONTROLREF1.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDREF1 { get; set; }

        /// <summary>
        ///The control item to which the Purchase order line relates.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDREF2 { get; set; }

        /// <summary>
        ///The unique code by which any raw material, packaging,
        ///item, finished good, intermediate or product is known to the
        ///system. The field is used to access data associated with the
        ///item. Item Numbers are added using IADD.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPARTNO { get; set; }

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
        ///Provides an additional 50 characters of description to
        ///form an extended second line if required. It is created by
        ///IADD and ICHG or by ITDA and ITDC if item number selection
        ///is in use.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vDESCRIPTION2 { get; set; }

        /// <summary>
        ///Indicates the relative strength of a given stock item. It
        ///is usually applied to fluids. If entered then a potency unit
        ///of measure must be present. (See ITPOTUM).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPOTENCY { get; set; }

        /// <summary>
        ///Indicates that an item is held in potent units of measure
        ///and to detail the units in which the item is held. If a
        ///potent unit is specified then it must have been defined as a
        ///unit of measure with UMAD.
        /// </summary>
        [FieldName("vITPOTUM$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vITPOTUM_0 { get; set; }

        /// <summary>
        ///The unit of measure on an item is defined using IADD.
        ///Once a unit of measure has been defined against an item it
        ///can only be changed by deleting the item and re-adding it.
        ///An item may not be deleted if it has other data associated
        ///with it, e.g. formula, inventory parameters etc.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_ITM { get; set; }

        /// <summary>
        ///Represents the number of potent units in an entered stock
        ///quantity based on the entered potency.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPOTENTUNITS { get; set; }

        /// <summary>
        ///Indicates that an item is held in potent units of measure
        ///and to detail the units in which the item is held. If a
        ///potent unit is specified then it must have been defined as a
        ///unit of measure with UMAD.
        /// </summary>
        [FieldName("vITPOTUM$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vITPOTUM_1 { get; set; }

        /// <summary>
        ///The status of the lot. If entered it must be valid.
        ///Lot status processing is described under transaction LTSC
        ///(Lot Status Change).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vITSTATUS_LOT { get; set; }

        /// <summary>
        ///The grade of the lot.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vITGRADETG_LOT { get; set; }

        /// <summary>
        ///The first segment of a Tropos Ledger Reference. Combines
        ///with a cost code to form a Tropos Ledger Reference.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vCOSTCENTRE { get; set; }

        /// <summary>
        ///The cost code element of the ledger reference.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vCOSTCODE { get; set; }

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
        ///Allows a GRN number to be entered in the first field of a
        ///transaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNUMBERENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNUMBERENT { get; set; }

        /// <summary>
        ///GRN Quantity Good                       
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNQTYGOOD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNQTYGOOD { get; set; }

        /// <summary>
        ///Contains the value of the unit of advice.  It may be a
        ///standard abbreviation or a literal and can be the unit of
        ///purchase, a unit of stock or a unit which has a valid
        ///conversion from either of those.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_ADV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_ADV { get; set; }

        /// <summary>
        ///This is the value of the extra data required to perform a
        ///UOM conversion at the time a reading or movement occurred.
        ///For example: 1 ltre = 1 kg at 20 DEG C and increases by .01
        ///kg per DEG C P.I. reading of 120 ltre at 23 DEC C, then
        ///MOVEVAL is 23.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iMOVEVAL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiMOVEVAL { get; set; }

        /// <summary>
        ///The recorded density of a movement that has been recorded
        ///with temperature and density readings. A value will only be
        ///recorded if the item has a related STA (Standard Temperature
        ///Accounting) code.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iOILDENSITY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiOILDENSITY { get; set; }

        /// <summary>
        ///The automatic shortage clearance flag. When it is 'Y',
        ///stock received by the transaction will be used to clear
        ///shortages. It is defaulted from GRNSHORT-DEF, maintained by
        ///WIMD (Maintain Management Data).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNSHORT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNSHORT { get; set; }

        /// <summary>
        ///Identifies whether the GRN is complete or whether there
        ///are outstanding receipts.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_GRIS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_GRIS { get; set; }

        /// <summary>
        ///Along with Stock Location this field makes up the full
        ///location. It may also be used to hold quarantine stock. Good
        ///stock and quarantine stock may not be held at the same
        ///location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSTOR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSTOR { get; set; }

        /// <summary>
        ///Along with Store this field makes up the full location. It
        ///may also be used to hold quarantine stock. Good stock and
        ///quarantine stock may not be held at the same location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iBINLOCN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiBINLOCN { get; set; }

        /// <summary>
        ///Identifies a set of Inventory Parameters which are
        ///particular to a specific Warehouse. A Warehouse is used to
        ///identify a set of storage locations which are separate from
        ///the main Production Plant and which can be used to store
        ///stock items.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iWAREHOUSE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiWAREHOUSE { get; set; }

        /// <summary>
        ///The quantity currently on the pallet, in stock or potent
        ///units.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPALLQTY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPALLQTY { get; set; }

        /// <summary>
        ///The package unit of measure for this item.  This is used
        ///to allow stock movements to be made in whole number of
        ///packages rather than stock units.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_PACK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_PACK { get; set; }

        /// <summary>
        ///This indicates the size of container that the lot is
        ///delivered/stored in. If a pack size is present then a pack
        ///type must be present.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPACKSIZE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPACKSIZE { get; set; }

        /// <summary>
        ///This indicates the type of container the lot is
        ///delivered/stored in. This is an important data item wnhen
        ///pick-face allocation is used. Please refer to the HELP text
        ///for FALS (Order Progress).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPACKTPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPACKTPE { get; set; }

        /// <summary>
        ///Pallet Number from                      
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPALLET_FROM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPALLET_FROM { get; set; }

        /// <summary>
        ///Pallet Number TO                        
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPALLET_TO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPALLET_TO { get; set; }

        /// <summary>
        ///The number of pallets between which stock at a location is
        ///split.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iNUMPALLETS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiNUMPALLETS { get; set; }

        /// <summary>
        ///The position of the lot within the pallet.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPALLPOS_LOT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPALLPOS_LOT { get; set; }

        /// <summary>
        ///This allows the weight of an empty package to be recorded.
        ///If entered, the net weight of the contents of each package
        ///can be calculated.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPACKWEIGHT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPACKWEIGHT { get; set; }

        /// <summary>
        ///The unit of measure of the weight of an empty package.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_PWT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_PWT { get; set; }

        /// <summary>
        ///The default printer name to be used when printing a
        ///specific report. If required, the default name may be
        ///overridden on the BAPR transaction.
        /// </summary>
        [FieldName("iREPORTDEST$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iREPORTDEST_0 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiREPORTDEST_0 { get; set; }

        /// <summary>
        ///Indicates whether pallet tickets are required whenever
        ///stock is created for this item.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPALLTKT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPALLTKT { get; set; }

        /// <summary>
        ///The number of copies of documentation required.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iNUMCOPIES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiNUMCOPIES { get; set; }

        /// <summary>
        ///This is the lot prefix. The lot prefix can be defined for
        ///each item on the TIMT transaction. If the lot number is
        ///system generated a number range for each prefix used can be
        ///set up on the LNUM transaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLOTNOPRE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLOTNOPRE { get; set; }

        /// <summary>
        ///The status of the lot. If entered it must be valid.
        ///Lot status processing is described under transaction LTSC
        ///(Lot Status Change).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iITSTATUS_LOT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiITSTATUS_LOT { get; set; }

        /// <summary>
        ///The grade of the lot.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iITGRADETG_LOT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiITGRADETG_LOT { get; set; }

        /// <summary>
        ///Defines the approval standard for a given lot. It could
        ///also be Certificate of Conformity, supplier approval status,
        ///document or comment.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iQASPEC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiQASPEC { get; set; }

        /// <summary>
        ///This field indicates whether this lot is in Free
        ///Circulation i.e. the duty has been paid.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iFICIND { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiFICIND { get; set; }

        /// <summary>
        ///The default printer name to be used when printing a
        ///specific report. If required, the default name may be
        ///overridden on the BAPR transaction.
        /// </summary>
        [FieldName("iREPORTDEST$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iREPORTDEST_1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiREPORTDEST_1 { get; set; }

        /// <summary>
        ///Documentary only. For non-serial number controlled lots it
        ///holds the 'from-to' serial number range. There is no
        ///validation.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSERNORANGE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSERNORANGE { get; set; }

        /// <summary>
        ///This indicator will be used to determine if aquisition VAT
        ///needs to be declared when the goods are removed to home use
        ///in the UK.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iFIRSTACQIND { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiFIRSTACQIND { get; set; }

        /// <summary>
        ///The date on which the batch of items was manufactured. If
        ///purchased, this will be supplied by the supplier. If
        ///made-in, it will be the date they are booked into stores.
        ///Used to allocate lot controlled stocks on a FIFO basis. If
        ///amended with LQMT then ALL locations for that lot will be
        ///amended accordingly.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iBATCHDTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiBATCHDTE { get; set; }

        /// <summary>
        ///The date after which the whole lot will be regarded as
        ///available for use. The availability date will be applied to
        ///those items that have been identified as having a maturing
        ///time.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLOTAVADTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLOTAVADTE { get; set; }

        /// <summary>
        ///Where exisable goods may have been delivered from one
        ///country but has an origin of a different country, this field
        ///can be used to hold the actual country of origin of the
        ///goods.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iNATCODE_COO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiNATCODE_COO { get; set; }

        /// <summary>
        ///The date on which the whole 'lot' will be regarded as
        ///unavailable for use. The expiry date will be applied to only
        ///those items that have been identified as shelf life
        ///controlled. This is the field which will be used on online
        ///screens.
        ///The 'Lot Expiry start of day' parameter on the Inventory
        ///Management transaction (WIMD) identifies whether an expiry
        ///date of today means the lot is expired or whether it is
        ///available.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iEXPDTE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiEXPDTE { get; set; }

        /// <summary>
        ///Default Method of Calc Lot Expiry Date  
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iEXPCALC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiEXPCALC { get; set; }

        /// <summary>
        ///For an item subject to excise regulations, this return
        ///code will determine which box the movement will be reported
        ///upon the W1 Excise Warehouse Return form.
        ///The returns are split between Receipts for Period and
        ///Removals During Period. To allow for the introduction of any
        ///future values, values between 01 and 50 will be reserved for
        ///Receipts, and values 51 to 99 will be reserved for Removals.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iEXCRETURN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiEXCRETURN { get; set; }

        /// <summary>
        ///The Supplier's Batch Number.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSUPBATCH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSUPBATCH { get; set; }

        /// <summary>
        ///The date on which the movement took place as opposed to
        ///the date the transaction was entered. Cannot be backdated to
        ///before the last stock reconciliation date. This date may not
        ///be earlier than the earliest back-dating date (EBDATE).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iMOVDATE_ACT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiMOVDATE_ACT { get; set; }

        /// <summary>
        ///Y/N indicator to show if extra data is present
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iEXDATAIND_LOT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiEXDATAIND_LOT { get; set; }

        /// <summary>
        ///Comment field relating to G.R.N. data.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLOTCOMMQ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLOTCOMMQ { get; set; }

        /// <summary>
        ///Indicates whether there is text against a lot.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLOTXT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLOTXT { get; set; }

        /// <summary>
        ///The number of copies of this report to be printed on this
        ///print destination. In some cases, this value will be
        ///overridden by an application specific value (e.g. INPR
        ///(Invoice/Credit Print)).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iWPPCOPIES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiWPPCOPIES { get; set; }

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
        ///Used internally within the GROS transaction, this flag
        ///indicates that the GRIS transaction will execute on
        ///completion of the GROS transaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iAUTOGRIS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiAUTOGRIS { get; set; }

        /// <summary>
        ///Uniquely identifies a delivery from a supplier. Relates to
        ///the GRN data record of the GRN details file.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNUMBER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNUMBER { get; set; }

        /// <summary>
        ///Output field calculated as (GRNREC - GRNBIN - GRNREJ).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNQTYOUT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNQTYOUT { get; set; }

        /// <summary>
        ///The number of marshalled or issued shortages for the item.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSHORT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSHORT { get; set; }

        /// <summary>
        ///The quantity accepted as good stock, stated in stock
        ///units.
        ///Automatically set equal to GRNREC for non inventory
        ///goods orders.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNBIN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNBIN { get; set; }

        /// <summary>
        ///Represents the number of potent units in an entered stock
        ///quantity based on the entered potency.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPOTENTUNITS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPOTENTUNITS { get; set; }

        /// <summary>
        ///The quantity of the item being rejected by the associated
        ///REJNOTE. This is the original rejected quantity and is not
        ///reduced as items are disposed of.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iREJQTY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiREJQTY { get; set; }

        /// <summary>
        ///A generic screen input field denoting 'Y'es or 'N'o.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN { get; set; }

        /// <summary>
        ///Indicates the relative strength of a given stock item. It
        ///is usually applied to fluids. If entered then a potency unit
        ///of measure must be present. (See ITPOTUM).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPOTENCY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPOTENCY { get; set; }

        /// <summary>
        ///Indicates whether a new Process Number should be generated
        ///for an "O" type process.
        ///If a new process number is generated, the format of the
        ///number is the same as a production order and the next
        ///production order number is allocated. The new process can
        ///only be used on the production order of the same number
        ///which can only be added via the SOWA (Sales Production
        ///Order) transaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_GEN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_GEN { get; set; }

        /// <summary>
        ///The number of pallets in a range.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPALLETQTY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPALLETQTY { get; set; }

        /// <summary>
        ///The quantity advised in stock units. Is the conversion of
        ///the value in GRNADVISE. Conversion is done by UOMCONV and/or
        ///the factor UOMFACTOR.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNADVSTK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNADVSTK { get; set; }

        /// <summary>
        ///This field contains the quantity received so far for the
        ///order line. If the order contains scheduled/extended
        ///deliveries then this field will contain the sum of the
        ///receipts for all of them. The value will be in stock units
        ///for an inventory order or purchase units for other goods
        ///orders.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDREC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDREC { get; set; }
        public GRIS()
        {
            ClearFields();
        }

        public GRIS(Dictionary<string, string> fields)
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
            string action = "GRIS";
			TransactionDTO GRIS = getDTO(action);

#if TroposDesktop || TroposWebService
            using (Transactions trans = new Transactions())
            {
#else
            using (TDK.Common.Helper.Transactions trans = new TDK.Common.Helper.Transactions())
            {
#endif
                trans.DataValid += Trans_DataValid;

				trans.AddTransaction(GRIS);

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
		    TransactionDTO GRIS = new TransactionDTO(action);
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
                        GRIS.Fields.Add(name, property.GetValue(this, null));
                    else
                        GRIS.Fields.Add(name, string.Empty);
                }
            }
			GRIS.ScrollLines = MaxScrollLines;
			return GRIS;
		}

    }
}
