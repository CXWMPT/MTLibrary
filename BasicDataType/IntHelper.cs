using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTLibrary
{
    public class IntHelper
    {


        #region 

        /// <summary>
        /// object转int
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int Conversion(object o)
        {
            try
            {
                if (o==null) return 0;
                return Convert.ToInt32(o);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// byte 转 int 通过高低位转换
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="byteDataFormat"></param>
        /// <returns></returns>
        public static int Conversion(byte[] byteArray, MTByteDataFormatEnum byteDataFormat)
        {
            try
            {
                int i;
                switch (byteDataFormat)
                {
                    case (MTByteDataFormatEnum.AB):
                        i = (int)(((byteArray[0] & 0xFF) << 8)
                         | (byteArray[1] & 0xFF));
                        break;
                    case (MTByteDataFormatEnum.BA):
                        i = (int)(((byteArray[1] & 0xFF) << 8)
                          | (byteArray[0] & 0xFF));
                        break;
                    case (MTByteDataFormatEnum.ABCD):
                        i = (int)(((byteArray[0] & 0xFF) << 24)
                         | ((byteArray[1] & 0xFF) << 16)
                         | ((byteArray[2] & 0xFF) << 8)
                         | (byteArray[3] & 0xFF));
                        break;
                    case (MTByteDataFormatEnum.DCBA):
                        i = (int)(((byteArray[3] & 0xFF) << 24)
                         | ((byteArray[2] & 0xFF) << 16)
                         | ((byteArray[1] & 0xFF) << 8)
                         | (byteArray[0] & 0xFF));
                        break;
                    case (MTByteDataFormatEnum.BADC):
                        i = (int)(((byteArray[1] & 0xFF) << 24)
                          | ((byteArray[0] & 0xFF) << 16)
                          | ((byteArray[3] & 0xFF) << 8)
                          | (byteArray[2] & 0xFF));
                        break;
                    case (MTByteDataFormatEnum.CDAB):
                        i = (int)(((byteArray[2] & 0xFF) << 24)
                         | ((byteArray[3] & 0xFF) << 16)
                         | ((byteArray[0] & 0xFF) << 8)
                         | (byteArray[1] & 0xFF));
                        break;
                    default:
                        i = 0;
                        break;
                }

                return i;
            }
            catch (Exception)
            {
                return 0;
            }
        }



        /// <summary>
        /// 获取小数点后的长度
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int GetDecimalPlaces(object o)
        {
            try
            {
                if (o == null) return 0;
                var strArray = o.ToString().Split('.');
                if (strArray.Length == 2)
                {

                    return strArray[1].Length;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

    }
}
