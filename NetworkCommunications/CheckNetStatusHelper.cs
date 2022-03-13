using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MTLibrary
{
    public class CheckNetStatusHelper
    {
        #region 检测本机网络是否连接
        private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);

        /// <summary>
        /// 检查本机是否连接网络
        /// </summary>
        /// <param name="ret">-1:未连网或出错，0:通过网卡连接网络，1:通过调制解调器连接网络</param>
        /// <returns></returns>
        public static bool CheckEnternet(out int ret)
        {
            try
            {
                System.Int32 dwFlag = new int();
                if (!InternetGetConnectedState(ref dwFlag, 0))
                {
                    ret = -1;
                    return false;
                }
                else if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0)
                {
                    ret = 1;
                    return true;
                }
                else if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
                {
                    ret = 0;
                    return true;
                }
                else
                {
                    ret = -1;
                    return false;
                }
            }
            catch
            {
                ret = -1;
                return false;
            }
        }
        #endregion
    }
}
