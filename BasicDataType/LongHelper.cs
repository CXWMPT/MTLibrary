using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTLibrary
{
    public class LongHelper
    {

        #region 
        /// <summary>
        /// object转long
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static long Conversion(object o)
        {
            try
            {
                if (o==null) return 0;
                return Convert.ToInt64(o);
            }
            catch
            {
                return 0;
            }
        }
      
        #endregion

    }
}
