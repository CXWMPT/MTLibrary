﻿using System;

namespace MTLibrary
{
    public class DecimalHelper
    {

        #region

        /// <summary>
        /// 将object强转decimak
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static decimal Conversion(object o)
        {
            try
            {
                if (o==null) return 0;
                return Convert.ToDecimal(o);
            }
            catch
            {
                return 0;
            }
        }
        #endregion

    }
}
