using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTLibrary
{
    public class DoubleHelper
    {

        #region

        /// <summary>
        /// object转double
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static double Conversion(object o)
        {
            try
            {
                if (o==null) return 0;
                return Convert.ToDouble(o);
            }
            catch
            {
                return 0;
            }
        }

        #endregion


    }
}
