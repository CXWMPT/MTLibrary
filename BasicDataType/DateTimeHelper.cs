using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTLibrary
{
    public class DateTimeHelper
    {
        #region window更改系统时间

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public ushort year;
            public ushort month;
            public ushort dayOfWeek;
            public ushort day;
            public ushort hour;
            public ushort minute;
            public ushort second;
            public ushort milliseconds;
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);


        /// <summary>
        /// 设置Window系统时间
        /// </summary>
        /// <param name="sysTime"></param>
        /// <returns></returns>
        public static bool SetSystemTime(SYSTEMTIME sysTime)
        {
            try
            {
                return SetSystemTime(ref sysTime);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置Window系统时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool SetSystemTime(DateTime dt)
        {
            try
            {
                SYSTEMTIME sysTime = new SYSTEMTIME();
                sysTime.year = (ushort)dt.Year;
                sysTime.month = (ushort)dt.Month;
                sysTime.dayOfWeek = (ushort)dt.DayOfWeek;
                sysTime.day = (ushort)dt.Day;
                sysTime.hour = (ushort)dt.Hour;
                sysTime.minute = (ushort)dt.Minute;
                sysTime.second = (ushort)dt.Second;
                return SetSystemTime(ref sysTime);
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool GetWebTime(string ntpServer, int port, out DateTime dt, out string msg)
        {
            dt = DateTime.Now;
            msg = string.Empty;
            try
            {
                ntpServer = "10.130.40.2 ";
                // default ntp server
                // const  string ntpServer ="ntp1.aliyun.com";
                // string ntpServer = "ntp1.aliyun.com";
                // NTP message size - 16 bytes of the digest (RFC 2030)
                byte[] ntpData = new byte[48];
                // Setting the Leap Indicator, Version Number and Mode values
                ntpData[0] = 0x1B; // LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

                IPEndPoint ipEndPoint = null;


                if (string.IsNullOrEmpty(ntpServer))
                {
                    throw new Exception("服务器地址为空：");
                }

                if (RegularCheckHelper.IPv6(ntpServer))
                {
                    ipEndPoint = new IPEndPoint(IPAddress.Parse(ntpServer), port);
                }
                else
                {
                    IPAddress[] addresses = Dns.GetHostEntry(ntpServer).AddressList;
                    ipEndPoint = new IPEndPoint(addresses[0], port);
                }
                // The UDP port number assigned to NTP is 123

                // NTP uses UDP
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                // socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);


                socket.Connect(ipEndPoint);

                /////////////////////////////////
                ManualResetEvent waitEvent = new ManualResetEvent(false);
                //socket.BeginConnect(
                //    ipEndPoint,
                //    new AsyncCallback(delegate(IAsyncResult ar)
                //    {
                //        waitEvent.Set();
                //    }),
                //    socket);
                //waitEvent.WaitOne(3000, false);
                //if (socket.Connected)
                //{
                //    // connected
                //}
                //else
                //{
                //    // timeout
                //    socket.Close();
                //    return false;
                //}
                ///////////////////////////////////

                //socket.Connect(ipEndPoint);
                // Stops code hang if NTP is blocked
                //socket.ReceiveTimeout = 3000;
                socket.Send(ntpData);
                waitEvent.Reset();
                bool received = false;
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        socket.Receive(ntpData);
                        received = true;
                        waitEvent.Set();
                    }
                    catch
                    { }
                });
                thread.Start();
                waitEvent.WaitOne(10000, false);
                if (received)
                {
                    // do received
                }
                else
                {
                    thread.Abort();
                    // timeout
                    socket.Close();
                    throw new Exception("接收数据超时：");

                }
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

                // Offset to get to the "Transmit Timestamp" field (time at which the reply 
                // departed the server for the client, in 64-bit timestamp format."
                const byte serverReplyTime = 40;
                // Get the seconds part
                ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
                // Get the seconds fraction
                ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);
                // Convert From big-endian to little-endian
                intPart = swapEndian(intPart);
                fractPart = swapEndian(fractPart);
                ulong milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000UL);

                // UTC time
                DateTime webTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(milliseconds);
                // Local time

                dt = webTime.ToLocalTime();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
            return true;
        }

        // 小端存储与大端存储的转换
        private static uint swapEndian(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
            ((x & 0x0000ff00) << 8) +
            ((x & 0x00ff0000) >> 8) +
            ((x & 0xff000000) >> 24));
        }

        #endregion
        #region 公共方法


        /// <summary>
        /// obeject强转日期类
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime Conversion(object o)
        {
            try
            {
                if (o==null) return DateTime.Now;
                return Convert.ToDateTime(o);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// string强转日期类
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime Conversion(string s)
        {
            try
            {
                if (string.IsNullOrEmpty(s)) return DateTime.Now;
                return Convert.ToDateTime(s);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// object强转日期类
        /// </summary>
        /// <param name="o"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool Conversion(object o, out DateTime dt)
        {
            try
            {
                if (o==null)
                {
                    dt = DateTime.Now;
                    return false;
                }
                dt = Convert.ToDateTime(o);
            }
            catch
            {
                dt = DateTime.Now;
                return false;
            }
            return true;
        }

        /// <summary>
        /// string强转日期类
        /// </summary>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool Conversion(string s, out DateTime dt)
        {
            try
            {
                if (string.IsNullOrEmpty(s))
                {
                    dt = DateTime.Now;
                    return false;
                }
                dt = Convert.ToDateTime(s);
            }
            catch
            {
                dt = DateTime.Now;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime Conversion(string s, string format)
        {
            try
            {
                if (string.IsNullOrEmpty(s)) return DateTime.Now;
                return DateTime.ParseExact(s, format, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 验证输入时分秒时间是否在指定时间段之间
        /// </summary>
        /// <param name="sNowTime"></param>
        /// <param name="sStartTime">08:30:01</param>
        /// <param name="sEndTime">17::59:59</param>
        /// <returns></returns>
        public static bool Between(string sNowTime, string sStartTime, string sEndTime)
        {
            //判断当前时间是否在工作时间段内
            try
            {
                TimeSpan dspWorkingDayAM = DateTime.Parse(sStartTime).TimeOfDay;
                TimeSpan dspWorkingDayPM = DateTime.Parse(sEndTime).TimeOfDay;

                DateTime t1 = Convert.ToDateTime(sNowTime);
                TimeSpan dspNow = t1.TimeOfDay;
                if (dspNow > dspWorkingDayAM && dspNow <= dspWorkingDayPM)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 时间是否到期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool NowTimeIsExpirationTime(string s)
        {
            try
            {
                return NowTimeIsExpirationTime(Convert.ToDateTime(s));
            }
            catch (Exception)
            {
                return false;
            }
       
        }

        /// <summary>
        /// 时间是否到期
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool NowTimeIsExpirationTime(DateTime dt)
        {
            try
            {
                //将日期字符串转换为日期对象
                //通过DateTIme.Compare()进行比较（）
                int compNum = DateTime.Compare(DateTime.Now, dt);
                if (compNum >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 比较两个时间， 参数1早于参数2返回-1 ，参数1等于参数2返回0，参数1大于参数2返回1
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static int CompanyDate(string s1, string s2)
        {
            try
            {
                //通过DateTIme.Compare()进行比较（）
                return CompanyDate(Convert.ToDateTime(s1), Convert.ToDateTime(s2));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static int CompanyDate(DateTime d1, DateTime d2)
        {
            try
            {
                //通过DateTIme.Compare()进行比较（）
                int compNum = DateTime.Compare(d1, d2);
                return compNum;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取当前日期的时间戳(秒)
        /// </summary>
        /// <returns></returns>
        public static long GetSecondTimeStamp()
        {
            return GetSecondTimeStamp(DateTime.Now);
        }

        /// <summary>
        /// 获取传入日期的时间戳(秒)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long GetSecondTimeStamp(string s)
        {
            return GetSecondTimeStamp(Conversion(s));
        }

        /// <summary>
        /// 获取传入日期的时间戳(秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetSecondTimeStamp(DateTime dt)
        {
           return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        /// <summary>
        /// 获取当前日期的时间戳(毫秒)
        /// </summary>
        /// <returns></returns>
        public static long GetMillisecondTimeStamp()
        {
            return GetMillisecondTimeStamp(DateTime.Now);
        }

        /// <summary>
        /// 获取传入日期的时间戳(毫秒)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long GetMillisecondTimeStamp(string s)
        {
            return GetMillisecondTimeStamp(Conversion(s));
        }

        /// <summary>
        /// 获取传入日期的时间戳(毫秒)
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetMillisecondTimeStamp(DateTime dt)
        {
            return (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }


       
        public static DateTime SecondTimeStampToDateTime(long l)
        {
            try
            {
                if(l.ToString().Length!=10) return DateTime.Now;
                System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                dateTime = dateTime.AddSeconds(Convert.ToInt64(l)).ToLocalTime();
                return dateTime;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        public static DateTime MillisecondTimeStampToDateTime(long l)
        {
            try
            {
                if (l.ToString().Length != 13) return DateTime.Now;
                System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                dateTime = dateTime.AddMilliseconds(Convert.ToInt64(l)).ToLocalTime();
                return dateTime;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        #endregion

    }
}
