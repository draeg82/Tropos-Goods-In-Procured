using System;
using System.Collections;
using TroposUI.Common;
using TroposUI.Common.Context;

namespace TroposGoodsInProcuredBO.DTO
{
    public class Populate_davlCONSIGNMENT : ITroposQuery
    {
        private readonly UserContext _context;
        private ArrayList _parameters;
        private string _consignment_date;


        public Populate_davlCONSIGNMENT(UserContext context, String consignment_date)
        {
            _context = context;
            _consignment_date = consignment_date;
            _parameters = new ArrayList();
        }

        #region ITroposQuery Members
        public string SQLStatement
        {
            get
            {
                string SQL = @"SELECT DISTINCT porder_sch, 1 AS DUMMY
                                FROM mbd050, mbd070, mbb010
                                WHERE porder_sch = porder_item 
                                AND partno_pord = partno
                                AND porditm_sch = porditm_item
                                AND porder_item like 'P%'
                                AND poschreqd = TO_DATE(?, 'DD-MON-YY') 
                                ORDER BY porder_sch";

                return Helpers.ConvertSQL(_context, SQL);
            }
        }

        public object[] Parameters
        {
            get
            {
                _parameters = new ArrayList();
                _parameters.Add(_consignment_date);
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
