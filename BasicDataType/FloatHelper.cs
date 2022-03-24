using System;

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
