using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    //打印机工具类
    public class PrinterHelper
    {
        #region 
        /// <summary>
        /// 获取默认打印机名称
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultPrinter()
        {
            //取得默认打印机名
            PrintDocument pdoc = new PrintDocument();
            string defaultPrinterName = pdoc.PrinterSettings.PrinterName;
            return defaultPrinterName;
        }

        /// <summary>
        /// 获取所有的打印机名称
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllPrinter()
        {
            string[] arrPrinterName = new string[PrinterSettings.InstalledPrinters.Count];
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)       //获取当前打印机
            {
                arrPrinterName[i] = PrinterSettings.InstalledPrinters[i];
            }
            return arrPrinterName;
        }

        /// <summary>
        /// 获取所有的打印机名称
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetAllPrinterToMap()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)       //获取当前打印机
            {
                dic.Add(i, PrinterSettings.InstalledPrinters[i]);
            }
            return dic;
        }


        /// <summary>
        /// 判断打印机名称是否于打印机列表
        /// </summary>
        /// <returns></returns>
        public static bool PrinterIsExists(string printerName)
        {
            List<string> AllList = new List<string>();
            string[] arrPrinterName = new string[PrinterSettings.InstalledPrinters.Count];
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)       //获取当前打印机
            {
                AllList.Add(PrinterSettings.InstalledPrinters[i]);
            }
            return AllList.Exists(s => s == printerName);
        }

        /// <summary>
        /// 默认打印机是否在线
        /// </summary>
        /// <returns></returns>
        public static bool CheckDefaultPrinterStatus()
        {
            try
            {
                PrintDocument pdoc = new PrintDocument();
                string defaultPrinterName = pdoc.PrinterSettings.PrinterName;
                ManagementScope scope = new ManagementScope(@"\root\cimv2");
                scope.Connect();
                // Select Printers from WMI Object Collections
                ManagementObjectSearcher searcher = new
                 ManagementObjectSearcher("SELECT * FROM Win32_Printer");
                string printerName = "";
                foreach (ManagementObject printer in searcher.Get())
                {
                    printerName = printer["Name"].ToString().ToLower();
                    if (printerName.IndexOf(defaultPrinterName.ToLower()) > -1)
                    {
                        if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                        {
                            return false;
                            // printer is offline by user
                        }
                        else
                        {
                            // printer is not offline
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        /// <summary>
        /// 打印机的状态
        /// </summary>打印机名称
        /// <returns></returns>
        public static bool CheckPrinterStatus(string printerName1)
        {
            try
            {
                ManagementScope scope = new ManagementScope(@"\root\cimv2");
                scope.Connect();
                // Select Printers from WMI Object Collections
                ManagementObjectSearcher searcher = new
                 ManagementObjectSearcher("SELECT * FROM Win32_Printer");
                string printerName = "";
                foreach (ManagementObject printer in searcher.Get())
                {
                    printerName = printer["Name"].ToString().ToLower();
                    if (printerName.IndexOf(printerName1.ToLower()) > -1)
                    {
                        if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                        {
                            return false;
                            // printer is offline by user
                        }
                        else
                        {
                            // printer is not offline
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

     
    }
}
