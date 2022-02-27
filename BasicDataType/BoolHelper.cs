using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTDCommLib
{
    public class MTLibrary
    {
        #region 

        /// <summary>
        /// 将Object强转bool型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool Conversion(object o)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(o?.ToString())) return false;
                return Convert.ToBoolean(o);
            }
            catch
            {
                return false;
            }
        }
        #endregion 
    }
}
