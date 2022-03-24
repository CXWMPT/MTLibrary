using System;

namespace MTLibrary
{
    public class EnumHelper
    {
        #region 
        /// <summary>
        /// string强转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T Conversion<T>(string s)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), s,false);
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }
     
        /// <summary>
        /// int强转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T Conversion<T>(int i)
        {
            try
            {
                return (T)Enum.ToObject(typeof(T), i);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
     
        #endregion


    }
}
