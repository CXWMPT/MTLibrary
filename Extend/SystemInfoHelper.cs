using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    public class SystemInfoHelper
    {
        /// <summary>
        /// 获取系统信息的公共方法
        /// </summary>
        /// <param name="objquery">查询语句</param>
        /// <param name="infoname">系统信息字段</param>
        /// <returns></returns>
        public static string GetSystemInfoObject(string classname, string infoname)
        {
            try
            {
                ObjectQuery objquery = new ObjectQuery($"Select * From {classname}");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(objquery);
                var info = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    info = mo[infoname].ToString();   // get first item
                    break;
                }
                return info;
            }
            catch
            {
                return null;
            }
        }

        public static List<string> GetSystemInfoObjects(string classname, string infoname)
        {
            try
            {
                List<string> infos = new List<string>();
                ObjectQuery objquery = new ObjectQuery($"Select * From {classname}");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(objquery);
                var info = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    info = mo[infoname].ToString();
                    infos.Add(info);
                }
                return infos;
            }
            catch
            {
                return null;
            }
        }

        // BISO编号
        public static string GetBIOSNumber()
        {
            return GetSystemInfoObject("Win32_BIOS", "SerialNumber");
        }

        // 计算机名  
        public static string GetPCName()
        {
            return Environment.MachineName;
        }

        // 磁盘序列号
        public static string GetDiskNumber()
        {
            return GetSystemInfoObject("Win32_PhysicalMedia", "SerialNumber");
            //return GetSystemInfoObject("Win32_DiskDrive", "SerialNumber");
        }

        // 磁盘驱动器
        public static string[] GetDisks()
        {
            var infos = GetSystemInfoObjects("Win32_DiskDrive", "Caption");
            return infos.ToArray();
        }

        // 固定磁盘
        public static string[] GetFixedDisks()
        {
            List<string> infos = new List<string>();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Fixed)
                {
                    infos.Add(drive.Name);
                }
            }
            return infos.ToArray();
        }

        // 移动磁盘
        public static string[] GetRemovableDisks()
        {
            List<string> infos = new List<string>();
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    infos.Add(drive.Name);
                }
            }
            return infos.ToArray();
        }

        // 键盘信息
        public static string[] GetKeyboards()
        {
            List<string> keyboards = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Keyboard");
            foreach (ManagementObject mo in searcher.Get())
            {
                keyboards.Add($"{mo["Name"].ToString()}:{mo["Description"].ToString()} ({mo["DeviceID"].ToString()})");
            }
            return keyboards.ToArray();
        }

        public static List<string> GetNetCardMACAddresses()
        {
            List<string> macs = new List<string>();
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))");
                foreach (ManagementObject mo in searcher.Get())
                {
                    var mac = mo["MACAddress"].ToString().Trim();
                    macs.Add(mac);
                }
            }
            catch { }
            return macs;
        }

        public static string GetFirstNetCardMACAddress()
        {
            return GetNetCardMACAddresses().FirstOrDefault();
        }

        // 操作系统  
        public static string GetOSName()
        {
            return GetSystemInfoObject("Win32_OperatingSystem", "Name");
        }

        // 操作系统位数
        public static string GetOSBit()
        {
            return GetSystemInfoObject("Win32_OperatingSystem", "OSArchitecture");
        }

        // 操作系统语言
        public static string GetLanguage()
        {
            return CultureInfo.InstalledUICulture.NativeName;
        }

        //// PC模块
        //public static string GetPCModel()
        //{
        //    return GetSystemInfoObject("Win32_ComputerSystem", "Model");
        //}

        // CPU名称
        public static string GetCPUName()
        {
            return GetSystemInfoObject("Win32_Processor", "Name");
        }

        // CPU ID
        public static string GetCPUId()
        {
            return GetSystemInfoObject("Win32_Processor", "ProcessorId");
        }

        // 主板ID
        public static string GetMainboardId()
        {
            return GetSystemInfoObject("Win32_BaseBoard", "SerialNumber");
        }

        // 内存
        public static string GetTotalMemory()
        {
            return GetSystemInfoObject("Win32_ComputerSystem", "TotalPhysicalMemory");
        }

        // 硬盘
        public static string GetDiskSpace()
        {
            // ?????????
            long lsum = 0;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                //判断是否是固定磁盘  
                if (drive.DriveType == DriveType.Fixed)
                {
                    lsum += drive.TotalSize;
                }
            }
            var disk = lsum / (1024 * 1024 * 1024);
            if (disk > 300)
            {
                return "SSD " + disk.ToString();
            }
            else
            {
                return "HDD " + disk.ToString();
            }
        }
        //获取第一个mac地址
        public static string GetMacAddressByNetworkInformation()
        {
            string macAddress = "";
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (!adapter.GetPhysicalAddress().ToString().Equals(""))
                    {
                        macAddress = adapter.GetPhysicalAddress().ToString();
                        for (int i = 1; i < 6; i++)
                        {
                            macAddress = macAddress.Insert(3 * i - 1, "-");
                        }

                        //break;
                    }
                }
            }
            catch
            {

            }
            return macAddress;
        }
        /// <summary>
        /// 获取客户端网卡物理地址
        /// added by ll 20120924
        /// </summary>
        /// <returns>mac</returns>
        public static List<string> GetAllMacAddress()
        {
            List<string> macAddressList = new List<string>(); ;
            string macAddress = "";
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (!adapter.GetPhysicalAddress().ToString().Equals(""))
                    {
                        macAddress = adapter.GetPhysicalAddress().ToString();
                        for (int i = 1; i < 6; i++)
                        {
                            macAddress = macAddress.Insert(3 * i - 1, "-");
                        }
                        macAddressList.Add(macAddress);
                    }
                }
            }
            catch
            {
            }
            return macAddressList;
        }
        // 调用ManagementObjectSearcher实例中的Get()方法，在ManagementObject中的对象中获取我们想要的数据
        // 在https://msdn.microsoft.com/en-us搜索（如Win32_Processor）即可查看到对应的属性名
        //// 硬件  
        //Win32_Processor, // CPU 处理器  
        //Win32_PhysicalMemory, // 物理内存条  
        //Win32_Keyboard, // 键盘  
        //Win32_PointingDevice, // 点输入设备，包括鼠标。  
        //Win32_FloppyDrive, // 软盘驱动器  
        //Win32_DiskDrive, // 硬盘驱动器  
        //Win32_CDROMDrive, // 光盘驱动器  
        //Win32_BaseBoard, // 主板  
        //Win32_BIOS, // BIOS 芯片  
        //Win32_ParallelPort, // 并口  
        //Win32_SerialPort, // 串口  
        //Win32_SerialPortConfiguration, // 串口配置  
        //Win32_SoundDevice, // 多媒体设置，一般指声卡。  
        //Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)  
        //Win32_USBController, // USB 控制器  
        //Win32_NetworkAdapter, // 网络适配器  
        //Win32_NetworkAdapterConfiguration, // 网络适配器设置  
        //Win32_Printer, // 打印机  
        //Win32_PrinterConfiguration, // 打印机设置  
        //Win32_PrintJob, // 打印机任务  
        //Win32_TCPIPPrinterPort, // 打印机端口  
        //Win32_POTSModem, // MODEM  
        //Win32_POTSModemToSerialPort, // MODEM 端口  
        //Win32_DesktopMonitor, // 显示器  
        //Win32_DisplayConfiguration, // 显卡  
        //Win32_DisplayControllerConfiguration, // 显卡设置  
        //Win32_VideoController, // 显卡细节。  
        //Win32_VideoSettings, // 显卡支持的显示模式。  

        //// 操作系统  
        //Win32_TimeZone, // 时区  
        //Win32_SystemDriver, // 驱动程序  
        //Win32_DiskPartition, // 磁盘分区  
        //Win32_LogicalDisk, // 逻辑磁盘  
        //Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。  
        //Win32_LogicalMemoryConfiguration, // 逻辑内存配置  
        //Win32_PageFile, // 系统页文件信息  
        //Win32_PageFileSetting, // 页文件设置  
        //Win32_BootConfiguration, // 系统启动配置  
        //Win32_ComputerSystem, // 计算机信息简要  
        //Win32_OperatingSystem, // 操作系统信息  
        //Win32_StartupCommand, // 系统自动启动程序  
        //Win32_Service, // 系统安装的服务  
        //Win32_Group, // 系统管理组  
        //Win32_GroupUser, // 系统组帐号  
        //Win32_UserAccount, // 用户帐号  
        //Win32_Process, // 系统进程  
        //Win32_Thread, // 系统线程  
        //Win32_Share, // 共享  
        //Win32_NetworkClient, // 已安装的网络客户端  
        //Win32_NetworkProtocol, // 已安装的网络协议 
    }
}
