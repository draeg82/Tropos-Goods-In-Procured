using System;
using System.Collections;
using TroposUI.Common;
using TroposUI.Common.Context;

namespace TroposGoodsInProcuredBO.DTO
{
    public class Populate_grdGOODSIN : ITroposQuery
    {
        private readonly UserContext _context;
        private ArrayList _parameters;
        private string _purchase_order_num;

        public Populate_grdGOODSIN(UserContext context, String purchase_order_num)
        {
            _context = context;
            _purchase_order_num = purchase_order_num;
            _parameters = new ArrayList();
        }

        #region ITroposQuery Members
        public string SQLStatement
        {
            get
            {
                string SQL = @"
                            SELECT  round(A.PORDQTY) PORDQTY, B.PORDER_SCH, B.PORDITM_SCH, A.PARTNO_PORD, C.DESCRIPTION, round(A.PORDREC) PORDREC, TO_CHAR(B.POSCHREQD, 'DD-MON-YY') ReqDate,  D.STOR, D.BINLOCN, E.GRNUMBER, A.UOM_PURCH, (A.PORDQTY - A.PORDREC) OUTSTANDING_QTY, TO_CHAR(SYSDATE, 'DD-MON-YY') TODAY
                            FROM MBD070 A LEFT OUTER JOIN SSI_MBD020_VIEW E ON  A.ACCOUNT15_ITEM = E.ACCOUNT15_GRN
                            AND A.PORDER_ITEM = E.PORDER_GRN
                            AND A.PORDITM_ITEM = E.PORDITM_GRN,
                            MBD050 B, MBB010 C, MBC010 D
                            WHERE A.ACCOUNT15_ITEM = B.ACCOUNT15_POSCH
                            AND A.PORDER_ITEM = B.PORDER_SCH
                            AND A.PORDITM_ITEM = B.PORDITM_SCH
                            AND A.ACCOUNT15_ITEM = C.ACCOUNT15
                            AND A.PARTNO_PORD = C.PARTNO
                            AND A.PARTNO_PORD = D.PARTNO_INV
                            AND D.WAREHOUSE = ' '
                            AND (D.LOCSTOR_INV=' ')
                            AND B.PORDER_SCH  = ?
                            ORDER BY B.PORDITM_SCH";

                return Helpers.ConvertSQL(_context, SQL);
            }
        }

        public object[] Parameters
        {
            get
            {
                _parameters = new ArrayList();
                _parameters.Add(_purchase_order_num);
                return _parameters.ToArray();
            }
        }

        public bool TableFunction
        {
            get
            {
                return false;
            }
        }

        #endregion
    }
}
