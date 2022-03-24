using System;
using System.Security.Cryptography;
using System.Text;

namespace MTLibrary
{
    public class MD5Helper
    {
        #region MD5 加密

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt16(string str)
        {
            try
            {
                var md5 = new MD5CryptoServiceProvider();
                return BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(str)), 4, 8).Replace("-", "");
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            return Encrypt32(str);
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt32(string str)
        {
            try
            {
                MD5 md5 = MD5.Create(); //实例化一个md5对像
                byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));  // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                string pwd = "";
                // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                for (int i = 0; i < s.Length; i++)
                {
                    // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                    pwd = pwd + s[i].ToString("x2");
                }
                return pwd;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt64(string str)
        {
            try
            {
                string cl = str;
                //string pwd = "";
                MD5 md5 = MD5.Create(); //实例化一个md5对像
                return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(str))); // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }
}
