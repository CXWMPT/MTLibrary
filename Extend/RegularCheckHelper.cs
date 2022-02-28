using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTLibrary
{
    //正则校验
    public class RegularCheckHelper
    {

        #region 

        /// <summary>
        /// 校验手机号码[1 3-9 9位]
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool PhoneNumber(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            Regex regex = new Regex(@"^[1][3-9]\d{9}$");
            return regex.IsMatch(s);
        }


        /// <summary>
        /// 校验IPv4地址[第一位和最后一位数字不能是0或255；允许用0补位]  
        /// </summary>
        /// <param name="s"></param>
        /// <param name="IsCheckNullOrWhiteSpace"></param>
        /// <returns></returns>
        public static bool IPv4(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            string[] IPs = s.Split('.');
            if (IPs.Length != 4)
                return false;
            int n = -1;
            for (int i = 0; i < IPs.Length; i++)
            {
                if (i == 0 || i == 3)
                {
                    if (int.TryParse(IPs[i], out n) && n > 0 && n < 255)
                        continue;
                    else
                        return false;
                }
                else
                {
                    if (int.TryParse(IPs[i], out n) && n >= 0 && n <= 255)
                        continue;
                    else
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 校验IPv6地址[可用于匹配任何一个合法的IPv6地址]
        /// </summary>
        /// <param name="s"></param>
        /// <param name="IsCheckNullOrWhiteSpace"></param>
        /// <returns></returns>
        public static bool IPv6(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            Regex rx = new Regex(@"^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$");
            return rx.IsMatch(s.Trim());

        }

        /// <summary>
        /// 校验端口
        /// </summary>
        /// <param name="s"></param>
        /// <param name="IsCheckNullOrWhiteSpace"></param>
        /// <returns></returns>
        public static bool Port(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            Regex rx = new Regex(@"^[0-9]+$");
            return rx.IsMatch(s.Trim());
        }

        /// <summary>
        /// 校验是否为图片路径
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ImgPathFormat(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            bool isJPG = false;
            System.Drawing.Image img = System.Drawing.Image.FromFile(s);
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                isJPG = true;

            }
            return isJPG;
        }

        /// <summary>
        /// 校验车牌号[普通车牌(7)和新能源车牌(8)]
        /// </summary>
        /// <param name="s"></param>
        /// <param name="IsCheckNullOrWhiteSpace"></param>
        /// <returns></returns>
        public static bool LicensePlateNumber(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            List<string> strList = new List<string>() { "京", "津", "冀", "晋", "蒙", "辽", "吉", "黑", "沪", "苏", "浙", "皖", "闽", "赣", "鲁", "豫", "鄂", "湘", "粤", "桂", "琼", "渝", "川", "贵", "云", "藏", "陕", "甘", "青", "宁", "新", "学" };
           
            if (s.Length > 8 || s.Length < 7)
            {
                return false;
            }
            bool b = strList.Exists(it => it == s.Substring(0, 1));
            if (!b)
            {
                return false;
            }
            //第二位必须是大写字母
            if (s.Length == 2 && !(Convert.ToInt32(s[1]) > Convert.ToInt32('A') - 1 && Convert.ToInt32(s[1]) < Convert.ToInt32('Z') + 1))
            {
                return false;
            }
            for (int i = 2; i < s.Length; i++)
            {
                if (!((Convert.ToInt32(s[i]) > Convert.ToInt32('A') - 1 && Convert.ToInt32(s[i]) < Convert.ToInt32('Z') + 1) || (Convert.ToInt32(s[i]) > Convert.ToInt32('0') - 1 && Convert.ToInt32(s[i]) < Convert.ToInt32('9') + 1)))
                {
                    return false;
                }
            }
            return true;

        }
        #endregion

    }
}
