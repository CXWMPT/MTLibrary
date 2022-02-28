using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    public class IpHelper
    {
        /// <summary>
        /// 获取本地IP地址信息
        /// </summary>
        public static string GetAddressIP(AddressFamily addressFamily=AddressFamily.InterNetwork)
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
      
        public static List<string> GetIPAddressListStr()
        {
            List<string> listStr = new List<string>();
            string hostname = Dns.GetHostName();
            IPHostEntry ipadrlist = Dns.GetHostByName(hostname);
            foreach (var data in ipadrlist.AddressList) {
                IPAddress localaddr = data;
                listStr.Add(localaddr.ToString());
            }
            return listStr;
        }
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
     

}
}
