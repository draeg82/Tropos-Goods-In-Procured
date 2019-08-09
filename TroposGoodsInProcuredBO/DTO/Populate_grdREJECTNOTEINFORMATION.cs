using System;
using System.Collections;
using TroposUI.Common;
using TroposUI.Common.Context;

namespace TroposGoodsInProcuredBO.DTO
{
    public class Populate_grdREJECTNOTEINFORMATION : ITroposQuery
    {
        private readonly UserContext _context;
        private ArrayList _parameters;
        private string _rejectNoteNumber;

        public Populate_grdREJECTNOTEINFORMATION(UserContext context, String rejectNoteNumber)
        {
            _context = context;
            _rejectNoteNumber = rejectNoteNumber;
            _parameters = new ArrayList();
        }


        #region ITroposQuery Members
        public string SQLStatement
        {
            get
            {
                string SQL = @"
                            SELECT 
                            MBC040.REJNOTE Reject_Note_No, MAA030.VALDESC Reject_Reason, MBC040.QRACCEPT Accepted_Planned, MBC040.QRACCEPT_DONE Accepted_Actual, MBC040.QRETURNSL Supplier_Liable_Planned, MBC040.QRETURNSL_DONE Supplier_Liable_Actual, MBC040.QRETURNCL Customer_Liable_Planned, MBC040.QRETURNCL_DONE Customer_Liable_Actual
                            FROM 
                            MAA030 MAA030, MBC040 MBC040
                            WHERE 
                            MBC040.REJECT = MAA030.VALVAL AND ((MAA030.VALDATA='REJECT') AND (MBC040.REJNOTE=?))";



                return TroposUI.Common.Helpers.ConvertSQL(_context, SQL);
            }
        }
        public object[] Parameters
        {
            get
            {
                _parameters = new ArrayList();
                _parameters.Add(_rejectNoteNumber);
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