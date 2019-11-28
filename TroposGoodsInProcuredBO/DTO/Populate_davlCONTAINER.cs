﻿using System;
using System.Collections;
using TroposUI.Common;
using TroposUI.Common.Context;

namespace TroposGoodsInProcuredBO.DTO
{
    public class Populate_davlCONTAINER : ITroposQuery
    {
        private readonly UserContext _context;
        private ArrayList _parameters;

        public Populate_davlCONTAINER(UserContext context)
        {
            _context = context;
            _parameters = new ArrayList();
        }

        #region ITroposQuery Members

        public string SQLStatement
        {
            get
            {
                string SQL = @"SELECT ATTVALFROM
                                FROM MBT020
                                WHERE ATTRIBCODE_T02='TTP'
                                ORDER BY ATTVALFROM";

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
