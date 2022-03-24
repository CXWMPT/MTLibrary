using System;

namespace MTLibrary
{
    public class StringHelper
    {
        #region 

        /// <summary>
        /// object强转string
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string Conversion(object o)
        {
            try
            {
                if (o == null) return null;
                return Convert.ToString(o);
            }
            catch
            {
                return string.Empty;
            }
        }



        /// <summary>
        /// 保留小数点指定位数(默认两位小数,默认四舍五入)
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string KeepSpecifyDecimalPlaces(object o)
        {
            try
            {
                  return  KeepSpecifyDecimalPlaces(o,2,true);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 保留小数点指定位数(默认两位小数)
        /// </summary>
        /// <param name="o">小数值</param>
        /// <param name="keepDecimalLength">保留小数位</param>
        /// <param name="rounding">是否四舍五入</param>
        /// <returns></returns>
        public static string KeepSpecifyDecimalPlaces(object o,int keepDecimalLength,bool rounding)
        {
            string tmp = string.Empty;
            try
            {
                if (o == null) return tmp;
                string valueStr = o.ToString();
                if (!string.IsNullOrEmpty(valueStr) && keepDecimalLength >= 0)
                {
                    //判断是否四舍五入
                    if (rounding)
                    {
                        return DecimalHelper.Conversion(valueStr).ToString("f" + keepDecimalLength);
                    }
                    else
                    {
                        //获取小数位的下标
                        int index = valueStr.IndexOf(".");
                        int length = valueStr.Length;
                        if (index != -1)
                        {
                            tmp = string.Format("{0}.{1}", valueStr.Substring(0, index), valueStr.Substring(index + 1, Math.Min(length - index - 1, keepDecimalLength)));
                        }
                        else
                        {
                            tmp = valueStr;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return tmp;
            }
            return tmp;
        }


        /// <summary>
        /// 十六进制转换到十进制
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HexToTen(string s)
        {
            int ten = 0;
            for (int i = 0, j = s.Length - 1; i < s.Length; i++)
            {
                ten += HexChar2Value(s.Substring(i, 1)) * ((int)Math.Pow(16, j));
                j--;
            }
            return ten.ToString();
        }

        /// <summary>
        /// 从十进制转换到十六进制
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TenToHex(string s)
        {
            ulong tenValue = Convert.ToUInt64(s);
            ulong divValue, resValue;
            string hex = "";
            do
            {
                //divValue = (ulong)Math.Floor(tenValue / 16);

                divValue = (ulong)Math.Floor((double)(tenValue / 16));

                resValue = tenValue % 16;
                hex = tenValue2Char(resValue) + hex;
                tenValue = divValue;
            }
            while (tenValue >= 16);
            if (tenValue != 0)
                hex = tenValue2Char(tenValue) + hex;
            return hex;
        }

        #endregion


        #region 内部调用方法
        private static int HexChar2Value(string hexChar)
        {
            switch (hexChar)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return Convert.ToInt32(hexChar);
                case "a":
                case "A":
                    return 10;
                case "b":
                case "B":
                    return 11;
                case "c":
                case "C":
                    return 12;
                case "d":
                case "D":
                    return 13;
                case "e":
                case "E":
                    return 14;
                case "f":
                case "F":
                    return 15;
                default:
                    return 0;
            }
        }
        private static string tenValue2Char(ulong ten)
        {
            switch (ten)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return ten.ToString();
                case 10:
                    return "A";
                case 11:
                    return "B";
                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
                default:
                    return "";
            }
        }
        #endregion

    }
}
