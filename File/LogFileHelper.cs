using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    public class LogFileHelper
    {
        #region log操作部分
        private static object _logLock = new object();
        private static string _logInfo;
        public static  string LogInfo { get => _logInfo; set => _logInfo = value; }


        public static string LogPath => @".\log";//日志位置
        public static string LogFileNameDateFormat => @"yyyyMMdd";//log日志的命名格式
        public static string LogFileExt => @"log";//日志文件的文件后缀




        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="s"></param>
        public static void AddLog(string s)
        {
            AddLog(s, MTLogTypeEnum.Error);
        }

        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="s"></param>
        /// <param name="MTLogTypeEnum"></param>

        public static void AddLog(string s,MTLogTypeEnum MTLogTypeEnum)
        {
            string log = $"[{DateTime.Now}] [{MTLogTypeEnum}] {s}";
            lock (_logLock)
            {
                OutputLogToString(log);
                OutputLogToFile(log);
            }
        }

        /// <summary>
        /// 根据文件路径是否创建并写入
        /// </summary>
        /// <param name="s"></param>
        public static void OutputLogToFile(string s)
        {
            string logfile = $"{LogPath}\\{DateTime.Now.ToString(LogFileNameDateFormat)}.{LogFileExt}";
            
            Directory.CreateDirectory(LogPath);
            try
            {
                using (FileStream fs = new FileStream(logfile, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    var bts = Encoding.UTF8.GetBytes(s + "\r\n");
                    fs.Write(bts, 0, bts.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                OutputLogToString($"Write log file ({logfile}) failed: {ex.ToString()}");
            }
        }


        /// <summary>
        ///  清除多余称重记录[默认三十天]
        /// </summary>
        /// <param name="today"></param>
        public static void ClearLogFiles(DateTime today)
        {
            ClearLogFiles(today,30);
        }
        /// <summary>
        /// 清除多余称重记录
        /// </summary>
        /// <param name="today"></param>
        /// <param name="cleanDays"></param>
        public static void ClearLogFiles(DateTime today,int cleanDays)
        {
            if (!Directory.Exists(LogPath)) return;
            var files = Directory.GetFiles(LogPath, $"*.{LogFileExt}");
            foreach (var f in files)
            {
                string name = Path.GetFileNameWithoutExtension(f);
                if (DateTime.TryParseExact(name, LogFileNameDateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dt))
                {
                    //清除过期记录
                    if (dt.Date.AddDays(cleanDays) < today.Date)
                    {
                        AddLog(MTLogTypeEnum.Warning, $"Clear log file: {f}");
                        File.Delete(f);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 添加日志信息(已废弃)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="text"></param>
        public static void AddLog(MTLogTypeEnum type, string text)
        {
            string log = $"[{DateTime.Now}] [{type}] {text}";
            lock (_logLock)
            {
                OutputLogToString(log);
                OutputLogToFile(log);
            }
        }

        #region 内部变量方法
        /// <summary>
        /// 获取写入日志的内容
        /// </summary>
        /// <param name="log"></param>
        private static void OutputLogToString(string log)
        {
            log = $"{log}\r\n{LogInfo}";
            if (log.Length > 16 * 1024)
                log = log.Substring(0, log.Length / 2);
            LogInfo = log;
        }
        #endregion

    }
}
