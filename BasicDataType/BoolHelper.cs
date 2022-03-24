using System;


namespace MTLibrary
{
    public class BoolHelper
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
                if (string.IsNullOrEmpty(o == null ? "" : o.ToString())) return false;
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
