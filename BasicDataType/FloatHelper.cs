using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTLibrary
{
    public class FloatHelper
    {

        #region 

        /// <summary>
        /// object转float
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static float Conversion(object o)
        {
            try
            {
                if (o==null) return 0;

                return Convert.ToSingle(o);
            }
            catch
            {
                return 0;
            }
        }
       
        #endregion

     

    }
}
