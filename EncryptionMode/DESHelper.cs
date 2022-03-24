using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MTLibrary
{
    /// <summary>
    /// DES加密解密类
    /// </summary>
    public sealed class DESHelper
    {
        #region DES 加密/解密

        private static string KEY = "uiertysd";
        private static string IV = "99008855";




        /// <summary>
        /// DES加密。
        /// </summary>
        /// <param name="str">输入字符串。</param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            return Encrypt(str, KEY);
        }


        /// <summary>
        /// DES加密。
        /// </summary>
        /// <param name="str">输入字符串。</param>
        /// <returns>加密后的字符串。</returns>
        public static string Encrypt(string str, string key)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StreamWriter sw = null;
            try
            {
                if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(key)) return "";
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(ASCIIEncoding.ASCII.GetBytes(key), ASCIIEncoding.ASCII.GetBytes(IV)), CryptoStreamMode.Write);
                sw = new StreamWriter(cs);
                sw.Write(str);
                sw.Flush();
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
            catch (Exception)
            {

                return "";
            }
            finally
            {
                if (sw != null) sw.Close();
                if (cs != null) cs.Close();
                if (ms != null) ms.Close();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Decrypt(string str)
        {
            return Decrypt(str, KEY);
        }



        /// <summary>
        /// DES解密。
        /// </summary>
        /// <param name="inputString">输入字符串。</param>
        /// <returns>解密后的字符串。</returns>
        public static string Decrypt(string str, string key)
        {
            MemoryStream ms = null;
            CryptoStream cs = null;
            StreamReader sr = null;
            try
            {
                if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(key)) return "";
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                ms = new MemoryStream(Convert.FromBase64String(str));
                cs = new CryptoStream(ms, des.CreateDecryptor(ASCIIEncoding.ASCII.GetBytes(key), ASCIIEncoding.ASCII.GetBytes(IV)), CryptoStreamMode.Read);
                sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                if (sr != null) sr.Close();
                if (cs != null) cs.Close();
                if (ms != null) ms.Close();
            }
        }

        #endregion
    }

}
