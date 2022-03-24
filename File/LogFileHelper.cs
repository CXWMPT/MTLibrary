using System;
using System.Text;
using System.IO;
using System.Globalization;

namespace MTLibrary
{
    public class LogFileHelper
    {
        #region log操作部分
        public static string LogInfo = string.Empty;
        static object LogLock = new object();

        public static void AddLog(MTLogTypeEnum type, string text)
        {
            string log = string.Format("[{0}] [{1}] {2}", DateTime.Now, type, text);
            lock (LogLock)
            {
                OutputLogToString(log);
                OutputLogToFile(log);
            }
        }
        public static void AddLog(string text)
        {
            string log = string.Format("[{0}] [{1}] {2}", DateTime.Now, MTLogTypeEnum.Error, text);
            lock (LogLock)
            {
                OutputLogToString(log);
                OutputLogToFile(log);
            }
        }
        public static void OutputLogToString(string log)
        {
            log = string.Format("{0}\r\n{1}", log, LogInfo);
            if (log.Length > 16 * 1024)
                log = log.Substring(0, log.Length / 2);
            LogInfo = log;
        }
        //日志路径
        private static string _logPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\log";
        public static string LogPath { get { return _logPath; } set { _logPath = value; } }
        //日志文件的名称
        private static string _logFileNameDateFormat = @"yyyyMMdd";
        public static string LogFileNameDateFormat { get { return _logFileNameDateFormat; } set { _logFileNameDateFormat = value; } }
        //日志文件的名称后缀
        private static string _logFileExt = @"log";
        public static string LogFileExt { get { return _logFileExt; } set { _logFileExt = value; } }

        public static void OutputLogToFile(string log)
        {
            string logfile = string.Format("{0}\\{1}.{2}", LogPath, DateTime.Now.ToString(LogFileNameDateFormat), LogFileExt);
            Directory.CreateDirectory(LogPath);
            try
            {
                using (FileStream fs = new FileStream(logfile, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    var bts = Encoding.UTF8.GetBytes(log + "\r\n");
                    fs.Write(bts, 0, bts.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                OutputLogToString(string.Format("Write log file ({0}) failed: {1}", logfile, ex.ToString()));
            }
        }

        public static void ClearLogFiles(DateTime today, int logFileDays)
        {
            if (!Directory.Exists(LogPath)) return;
            var files = Directory.GetFiles(LogPath, string.Format("*.{0}", LogFileExt));
            foreach (var f in files)
            {
                string name = Path.GetFileNameWithoutExtension(f);
                IFormatProvider ifp = new CultureInfo("zh-CN", true);
                try
                {

                    DateTime dt = DateTime.ParseExact(name, LogFileNameDateFormat, ifp);
                    if (dt.Date.AddDays(logFileDays) < today.Date)
                    {
                        AddLog(MTLogTypeEnum.Warning, string.Format("Clear log file: {0}", f));
                        File.Delete(f);
                    }
                }
                catch { }
            }
        }
        #endregion
    }
}
