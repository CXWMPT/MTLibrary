using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class ReflectionHelper
    {

        #region 

        /// <summary>
        /// 根据实例文件名创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyName">程序集(当前项目生成的exe的前缀名)</param>
        /// <param name="fullName">命名空间.类型名</param>
        /// <returns></returns>
        public static bool CreateInstance<T>(string assemblyName, string fullName, out T t, out string msg)
        {
            t = default(T);
            msg = string.Empty;
            try
            {
                string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                Type o = Type.GetType(path);//加载类型
                object obj = Activator.CreateInstance(o);//根据类型创建实例
                t = (T)obj;
                return true;//类型转换并返回
            }
            catch (Exception ex)
            {
                msg += "创建对象实例出错:" + ex.Message;
                //发生异常，返回类型的默认值
                return false;
            }
        }

        #endregion
    }
}
