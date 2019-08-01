using System;
using System.Collections;
using TroposUI.Common;
using TroposUI.Common.Context;

namespace TroposGoodsInProcuredBO.DTO
{
    public class Populate_davlPALLETS : ITroposQuery
    {
        private readonly UserContext _context;
        private ArrayList _parameters;

        public Populate_davlPALLETS(UserContext context)
        {
            _context = context;
            _parameters = new ArrayList();
        }

        #region ITroposQuery Members
        public string SQLStatement
        {
            get
            {
                string SQL = @"
                            SELECT ATTVALFROM
                            FROM MBT020
                            WHERE ATTRIBCODE_T02 = 'PTP'";

                return Helpers.ConvertSQL(_context, SQL);
            }
        }

        public object[] Parameters
        {
            get
            {
                _parameters = new ArrayList();
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
