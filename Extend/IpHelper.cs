using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MTLibrary
{
    /// <summary>
    /// 获取IP地址(Wince不止)
    /// </summary>
    public class IpHelper
    {
        #region 
        /// <summary>
        ///  获取本地IP地址信息
        /// </summary>
        /// <returns></returns>
        public static string GetAddressIP()
        {
            return GetAddressIP(AddressFamily.InterNetwork);
        }

        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public static string GetAddressIP(AddressFamily addressFamily)
        {
            ///获取本地的IP地址
            string addressIP = string.Empty;
            foreach (IPAddress ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                //IP4寻址协议
                if (ipAddress.AddressFamily == addressFamily)
                {
                    addressIP = ipAddress.ToString();
                }
            }
            return addressIP;
        }

        /// <summary>
        /// 获取Ip集合,返回集合
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIPAddressListStr()
        {
            List<string> listStr = new List<string>();
            string hostname = Dns.GetHostName();
            IPHostEntry ipadrlist = Dns.GetHostByName(hostname);
            foreach (var data in ipadrlist.AddressList)
            {
                IPAddress localaddr = data;
                listStr.Add(localaddr.ToString());
            }
            return listStr;
        }

        /// <summary>
        /// 获取Ip集合,返回数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetIPAddressArrayStr()
        {
            List<string> listStr = new List<string>();
            string hostname = Dns.GetHostName();
            IPHostEntry ipadrlist = Dns.GetHostByName(hostname);
            foreach (var data in ipadrlist.AddressList)
            {
                IPAddress localaddr = data;
                listStr.Add(localaddr.ToString());
            }
            //list转到数组
            string[] array = listStr.ToArray();

            return array;
        }

        #endregion




    }
}
