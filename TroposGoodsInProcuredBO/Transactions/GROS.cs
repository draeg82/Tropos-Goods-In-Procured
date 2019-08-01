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
    public class GROS
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
        ///Uniquely identifies a delivery from a supplier. Relates to
        ///the GRN data record of the GRN details file.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vGRNUMBER { get; set; }

        /// <summary>
        ///General Pic X field                     
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vGENPICX { get; set; }

        /// <summary>
        ///Allows the full lot number i.e. prefix and number to be
        ///entered in a single field.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vLOTNOENT { get; set; }

        /// <summary>
        ///This is the first 50 characters of the maximum 100
        ///character description. The full 100 character description is
        ///shown on screens and reports. It is created with IADD and
        ///changed with ICHG. If using item number selection it can be
        ///added and changed with ITDA and ITDC.
        /// </summary>
        [FieldName("vDESCRIPTION$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vDESCRIPTION_0 { get; set; }

        /// <summary>
        ///This is the first 50 characters of the maximum 100
        ///character description. The full 100 character description is
        ///shown on screens and reports. It is created with IADD and
        ///changed with ICHG. If using item number selection it can be
        ///added and changed with ITDA and ITDC.
        /// </summary>
        [FieldName("vDESCRIPTION$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vDESCRIPTION_1 { get; set; }

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
        ///Cross reference information held for a purchase order
        ///line.
        ///For a subcontract order this is the production order to
        ///which the PO relates. If a subcontract PO relates to more
        ///than one production order then 'MULTI' may be entered.
        ///Sub-contract lines are usually added with the super
        ///transaction WSSC (Select Subcontractor). WSSC creates a
        ///sub-contract cross reference record on the table MBF100.
        ///This must exist before POAD (Add a PO Line) can be executed
        ///to add a sub-contract line for an item other than the
        ///finished product of a production order.
        ///For contract or overhead orders this is an optional
        ///documentary reference.
        ///For inventory orders this is an optional user reference, the
        ///type of reference being indicated by the related 'Reference
        ///Type' (PORDREFTYPE).
        ///It can be any value for Asset, Contract or Overhead type
        ///Orders. It must be a valid production order or 'MULTI' for
        ///Subcontract PO's.
        ///Depending on 'Reference Type' can be either a Production
        ///Order, Sales Order, Customer ID or free format text for
        ///Inventory type Orders.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDCBC { get; set; }

        /// <summary>
        ///QA specification for the purchase order line.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vQASPEC_PORD { get; set; }

        /// <summary>
        ///Identifies the source of the Purchase Order. It may be a
        ///department, or person. It is carried forward from the
        ///requisition and may be used to determine where the goods are
        ///to be delivered.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vORIGIN_PORD { get; set; }

        /// <summary>
        ///To identify the estimated total cost of one stock unit of
        ///measure of the item (one potent unit if a potent item) at
        ///the time that inventory details are added. The value is used
        ///in costings and for inventory valuations.
        ///This field is NOT maintained. The unit cost of an item is
        ///maintained by the Product Costing transactions CPCH, CSCH
        ///and CSBD in the I01 (Item Costs file) field UNITCOST-CC.
        ///This field is only relevant from the time of the IPAD
        ///transaction to the time of the first cost change.
        ///There is a standard database view ssi_unitcost_view that
        ///incorporates this functionality.  If you join to the view on
        ///business (account15) and item_number (partno) then the
        ///item’s current effective cost is returned in unit_cost.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUNITCOST { get; set; }

        /// <summary>
        ///The field defines the unit of measure of all stock
        ///quantities, stock movements and inventory parameters. It is
        ///maintained with IPAD/IPCH. Once stock movements have
        ///occurred it is no longer possible to change the stock unit
        ///of measure.
        /// </summary>
        [FieldName("vUOM-STOCK$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_STOCK_0 { get; set; }

        /// <summary>
        ///To indicate whether or not this purchase order is
        ///controlled.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDCC { get; set; }

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
        ///The total original order quantity in purchase units of
        ///measure. If set to zero, then no receipts are allowed
        ///against this Purchase Order line.
        /// </summary>
        [FieldName("vPORDQTY$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDQTY_0 { get; set; }

        /// <summary>
        ///Qualifies the order quantity if the order is not in terms
        ///of the stock quantity.  For inventory orders, if this unit
        ///does not have a valid conversion from the stock unit, a
        ///factor must be entered.
        /// </summary>
        [FieldName("vUOM-PURCH$0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_PURCH_0 { get; set; }

        /// <summary>
        ///The total original order quantity in purchase units of
        ///measure. If set to zero, then no receipts are allowed
        ///against this Purchase Order line.
        /// </summary>
        [FieldName("vPORDQTY$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPORDQTY_1 { get; set; }

        /// <summary>
        ///Qualifies the order quantity if the order is not in terms
        ///of the stock quantity.  For inventory orders, if this unit
        ///does not have a valid conversion from the stock unit, a
        ///factor must be entered.
        /// </summary>
        [FieldName("vUOM-PURCH$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_PURCH_1 { get; set; }

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
        [FieldName("vUOM-STOCK$1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vUOM_STOCK_1 { get; set; }

        /// <summary>
        ///Along with Stock Location this field makes up the full
        ///location. It may also be used to hold quarantine stock. Good
        ///stock and quarantine stock may not be held at the same
        ///location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vSTOR { get; set; }

        /// <summary>
        ///Along with Store this field makes up the full location. It
        ///may also be used to hold quarantine stock. Good stock and
        ///quarantine stock may not be held at the same location.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vBINLOCN { get; set; }

        /// <summary>
        ///The required date of this item of the delivery schedule.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPOSCHREQD { get; set; }

        /// <summary>
        ///The promise date of this item of the delivery schedule.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vPOSCHDUE { get; set; }

        /// <summary>
        ///The number of marshalled or issued shortages for the item.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vSHORT { get; set; }

        /// <summary>
        ///A line of text on the text module. Depending on the type
        ///of text, this line will be located on either the MAA040,
        ///MAA200 or MAA500 text table. All programmer access of this
        ///text is through the subroutine TEFM.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string vNARRLINE { get; set; }

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
        ///Allows a purchase order number to be entered in the format
        ///Xmmmmmm/nnn.
        ///It is also used by some purchasing enquiry transactions as a
        ///reference field in which a reference related to an enterable
        ///reference type e.g. requisition, enquiry line or purchase
        ///order line, can be entered.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDNOENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDNOENT { get; set; }

        /// <summary>
        ///The Purchase Order line number that the GRN relates to.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDITM_GRN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDITM_GRN { get; set; }

        /// <summary>
        ///Allows a GRN number to be entered in the first field of a
        ///transaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNUMBERENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNUMBERENT { get; set; }

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
        ///To hold the item number by which the supplier recognises
        ///the item.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSPARTNO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSPARTNO { get; set; }

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
        ///The quantity advised on the GRN. The quantity can be in
        ///purchase units, stock units or a unit in either family.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNADVISE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNADVISE { get; set; }

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
        ///Relates the GRN to a supplier's document.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNDELADV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNDELADV { get; set; }

        /// <summary>
        ///Holds the quantity received from the supplier in the goods
        ///receipt transaction. The value is in stock units of measure
        ///for inventory orders otherwise purchase units.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNREC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNREC { get; set; }

        /// <summary>
        ///Contains a discrepancy note number.
        ///A Discrpancy Note must be entered when goods are being
        ///received in the following circumstances:
        ///- Discrepancy Notes are defined as required on the Goods
        ///Receiving Parameters (PGRM)
        ///- The quantity received does not match the quantity advised
        ///by the supplier
        ///- The discrepancy between the two quantities exceeds the
        ///discrepancy /reject tolerances defined in the Invoice
        ///Matching parameters (IMMT).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNDISCREF { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNDISCREF { get; set; }

        /// <summary>
        ///The quantity received so far for the order line in
        ///catchweight units. This field will only be maintained for
        ///catchweight items.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDREC_CW { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDREC_CW { get; set; }

        /// <summary>
        ///The Catchweight unit of measure.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUOM_CW { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUOM_CW { get; set; }

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
        ///Yes or No                               
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_REC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_REC { get; set; }

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
        ///Records the date that goods were received.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNDATE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNDATE { get; set; }

        /// <summary>
        ///Holds the onsite location at which the received goods are
        ///held.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iONSITELOC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiONSITELOC { get; set; }

        /// <summary>
        ///This holds a document number related to the goods. For a
        ///normal purchase this may be a certificate of conformity, a
        ///set of test results or some other document which confirms
        ///compliance of the goods.
        ///For a take-back receipt this column holds the credit note
        ///and line number with which the GRN has been cleared. This is
        ///set by CTBA (Add Take-Back Credit) when a credit note line
        ///is added.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNRELEASE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNRELEASE { get; set; }

        /// <summary>
        ///GRN Print                               
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN_PRGRN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN_PRGRN { get; set; }

        /// <summary>
        ///The default printer name to be used when printing a
        ///specific report. If required, the default name may be
        ///overridden on the BAPR transaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iREPORTDEST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiREPORTDEST { get; set; }

        /// <summary>
        ///To contain the printer to be used for the hazard and
        ///safety labels. If the Hazards module is present, labels will
        ///be produced for items with hazard and/or safety details
        ///according to the documentation requirements defined by the
        ///HZMG, Hazard Management Parameters, transaction.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iREPORTDEST_HAZ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiREPORTDEST_HAZ { get; set; }

        /// <summary>
        ///An optional free format field for holding general comments
        ///about the GRN.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNCOMMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNCOMMENT { get; set; }

        /// <summary>
        ///Uniquely identifies a delivery from a supplier. Relates to
        ///the GRN data record of the GRN details file.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iGRNUMBER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiGRNUMBER { get; set; }

        /// <summary>
        ///The required date of this item of the delivery schedule.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPOSCHREQD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPOSCHREQD { get; set; }

        /// <summary>
        ///The promise date of this item of the delivery schedule.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPOSCHDUE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPOSCHDUE { get; set; }

        /// <summary>
        ///The total original order quantity in purchase units of
        ///measure. If set to zero, then no receipts are allowed
        ///against this Purchase Order line.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDQTY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDQTY { get; set; }

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
        ///The number of marshalled or issued shortages for the item.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iSHORT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiSHORT { get; set; }

        /// <summary>
        ///To identify the estimated total cost of one stock unit of
        ///measure of the item (one potent unit if a potent item) at
        ///the time that inventory details are added. The value is used
        ///in costings and for inventory valuations.
        ///This field is NOT maintained. The unit cost of an item is
        ///maintained by the Product Costing transactions CPCH, CSCH
        ///and CSBD in the I01 (Item Costs file) field UNITCOST-CC.
        ///This field is only relevant from the time of the IPAD
        ///transaction to the time of the first cost change.
        ///There is a standard database view ssi_unitcost_view that
        ///incorporates this functionality.  If you join to the view on
        ///business (account15) and item_number (partno) then the
        ///item’s current effective cost is returned in unit_cost.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iUNITCOST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiUNITCOST { get; set; }

        /// <summary>
        ///To indicate whether or not this purchase order is
        ///controlled.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iPORDCC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiPORDCC { get; set; }

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
        ///A generic screen input field denoting 'Y'es or 'N'o.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iYORN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiYORN { get; set; }

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
        ///A number that uniquely identifies a lot for an item.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iLOTNO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiLOTNO { get; set; }

        /// <summary>
        ///Originating Transaction Code            
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public string iACTION_ORIG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")][System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        public bool hiACTION_ORIG { get; set; }
        public GROS()
        {
            ClearFields();
        }

        public GROS(Dictionary<string, string> fields)
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
            string action = "GROS";
			TransactionDTO GROS = getDTO(action);

#if TroposDesktop || TroposWebService
            using (Transactions trans = new Transactions())
            {
#else
            using (TDK.Common.Helper.Transactions trans = new TDK.Common.Helper.Transactions())
            {
#endif
                trans.DataValid += Trans_DataValid;

				trans.AddTransaction(GROS);

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
		    TransactionDTO GROS = new TransactionDTO(action);
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
                        GROS.Fields.Add(name, property.GetValue(this, null));
                    else
                        GROS.Fields.Add(name, string.Empty);
                }
            }
			GROS.ScrollLines = MaxScrollLines;
			return GROS;
		}

    }
}
