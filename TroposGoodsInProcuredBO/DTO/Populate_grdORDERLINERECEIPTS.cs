using System;
using System.Collections;
using TroposUI.Common;
using TroposUI.Common.Context;

namespace TroposGoodsInProcuredBO.DTO
{
    public class Populate_grdORDERLINERECEIPTS : ITroposQuery
    {
        private readonly UserContext _context;
        private ArrayList _parameters;
        private string _grnNumber;

        public Populate_grdORDERLINERECEIPTS(UserContext context, String grnNumber)
        {
            _context = context;
            _grnNumber = grnNumber;
            _parameters = new ArrayList();
        }


        #region ITroposQuery Members
        public string SQLStatement
        {
            get
            {
                string SQL = @"
                            SELECT sq_grns.GRNUMBER GRN_No, sq_grns.GRNADVISE Order_Qty, sq_grns.GRNREC Received_qty, sq_grns.GRNBIN Accepted_Qty, sq_grns.GRNREJ Rejected_Qty, sq_grns.GRNDATE Received_Date, sq_grns.GRNDELADV Advice_Note, sq_grns.VALDESC GRN_Status, 
                            MBC040.REJNOTE Reject_Note_No
                            FROM (SELECT MBD020.GRNUMBER, MBD020.GRNADVISE, MBD020.GRNREC, MBD020.GRNBIN, MBD020.GRNREJ, MBD020.GRNDATE, MBD020.GRNDELADV, MAA030.VALDESC
                            FROM MAA030 MAA030, MBD020 MBD020  
                            WHERE MAA030.VALVAL = MBD020.GRNSTATUS AND (MBD020.GRNUMBER = ?) AND (MAA030.VALDATA = 'GRNSTATUS') AND (MAA030.VALLANG = 'E')) sq_grns left outer join MBC040
                            ON sq_grns.GRNUMBER = MBC040.REJGRN";



                return TroposUI.Common.Helpers.ConvertSQL(_context, SQL);
            }
        }
        public object[] Parameters
        {
            get
            {
                _parameters = new ArrayList();
                _parameters.Add(_grnNumber);
                return _parameters.ToArray();
            }
        }
        public bool TableFunction
        {
            get
            {
                // The value of TableFunction is normally set to false.Only set to true if selecting from a table function
                return false;
            }
        }
        #endregion
    }
}