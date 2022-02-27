using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    /// <summary>
    /// AES加密解密类
    /// </summary>
    public sealed class AESHelper
    {
        #region  加密/解密

        private static string KEY ="12345678876543211234567887654NTD";

        /// <summary>
        /// AES 加密
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            return Encrypt(str, KEY);
        }

        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string Encrypt(string str, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(str)|| string.IsNullOrEmpty(key)) return "";
                Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

                RijndaelManaged rm = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform cTransform = rm.CreateEncryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">明文（待解密）</param>
        /// <returns></returns>
        public static string Decrypt(string str)
        {
            return Decrypt(str, KEY);
        }

        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">明文（待解密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string Decrypt(string str, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(key)) return "";
                Byte[] toEncryptArray = Convert.FromBase64String(str);

                RijndaelManaged rm = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(key),
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                ICryptoTransform cTransform = rm.CreateDecryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception)
            {
                return "";
            }
          
        }
        #endregion
    }

}
