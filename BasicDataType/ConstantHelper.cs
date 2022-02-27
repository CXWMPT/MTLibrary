using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTLibrary
{
    
    //常量
    public  class ConstantHelper
    {

        /// <summary>
        /// 两位转一位
        /// </summary>
        public static object[] MT_HEX = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        public static object GetNTDHEX(int i)
        {

            if (i > MT_HEX.Length || i <= 0)
            {
                return "";
            }
            else
            {
                return MT_HEX[i - 1];
            }

        }

        /// <summary>
        /// 串口所需波特率
        /// </summary>
        public static object[] BAUD_RATE_ARRAY = { 1200, 2400,4800,9600,19200,38400,43000,56000,57600,115200 };
        public static ArrayList BAUD_RATE_ARRAY_List = new ArrayList() { new DictionaryEntry(1200, 1200), new DictionaryEntry(2400, 2400), new DictionaryEntry(4800, 4800), new DictionaryEntry(9600, 9600), new DictionaryEntry(19200, 19200), new DictionaryEntry(38400, 38400), new DictionaryEntry(43000, 43000), new DictionaryEntry(56000, 56000), new DictionaryEntry(57600, 57600), new DictionaryEntry(115200, 115200) };


        /// <summary>
        ///  分度值
        /// </summary>
        public static object[] SCALE_DIVISION_KG_ARRAY = { "0.00001", "0.00002", "0.00005", "0.0001", "0.0002", "0.0005", "0.001", "0.002", "0.005", "0.01","0.02","0.05","0.1","0.2","0.5","1","2","5" };//kg 分度值
        public static object[] SCALE_DIVISION_G_ARRAY = { "0.01", "0.02", "0.05", "0.1", "0.2", "0.5", "1", "2", "5", "10", "20", "50", "100", "200", "500", "1000", "2000", "5000" };//g 分度值
       
        /// <summary>
        /// 性别
        /// </summary>
        public static object[] SEX_ARRAY = { "男", "女"};
        public static ArrayList SEX_ARRAY_LIST = new ArrayList() { new DictionaryEntry("0", "男"), new DictionaryEntry("1", "女"), };


        /// <summary>
        /// 秤台通讯方式
        /// </summary>
        public static object[] BALANCE_COMMUNICATION_MODE_ARRAY = { "串口", "TCP/IP" };
        public static ArrayList BALANCE_COMMUNICATION_MODE_ARRAY_LIST = new ArrayList() { new DictionaryEntry("SerialPort", "串口"), new DictionaryEntry("TCP/IP", "TCP/IP"), };

        /// <summary>
        /// 秤台的输出模式
        /// </summary>
        public static object[] BALANCE_OUTPUT_TYPE = { "continue", "sics", "print" };
        public static ArrayList BALANCE_OUTPUT_TYPE_ARRAY_LIST = new ArrayList() { new DictionaryEntry("continue", "连续输出"), new DictionaryEntry("sics", "问答模式"), new DictionaryEntry("print", "打印模式") };


        /// <summary>
        /// 质量单位
        /// </summary>
        public static object[] MASS_UNIT_ARRAY = { "kg", "g" };
        public static ArrayList MASS_UNIT_ARRAY_LIST = new ArrayList() { new DictionaryEntry("0", "kg"), new DictionaryEntry("1", "g")};


    }
}
