using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTLibrary
{
    public class ByteHelper
    {
        #region 

        /// <summary>
        /// 将图片绝对路径转成byte
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] ImageFilePathConversion(string filePath)
        {
            try
            {
                /*fs = new FileStream(fileName, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
                Bitmap bt = new Bitmap(fs);
                int streamLength = (int)fs.Length;
                byte[] image = new byte[streamLength];
                fs.Read(image, 0, streamLength);*/
                Bitmap bmp = new Bitmap(filePath);
                //字面是对当前图片进行了二进制转换
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return arr;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 比较两个数组是否相同
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static bool Equals(byte[] b1, byte[] b2)
        {
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
                if (b1[i] != b2[i])
                    return false;
            return true;
        }



        /// <summary>
        /// int 转byte 通过高低位转换
        /// </summary>
        /// <param name="i"></param>
        /// <param name="byteDataFormat"></param>
        /// <returns></returns>
        public static byte[] Conversion(int i, MTByteDataFormatEnum byteDataFormat)
        {
            try
            {
                byte[] b4 = new byte[4];
                byte[] b2 = new byte[2];
                switch (byteDataFormat)
                {
                    case (MTByteDataFormatEnum.AB):
                        b2[1] = (byte)(i & 0xff);
                        b2[0] = (byte)(i >> 8 & 0xff);
                        return b2;
                    case (MTByteDataFormatEnum.BA):
                        b2[0] = (byte)(i & 0xff);
                        b2[1] = (byte)(i >> 8 & 0xff);
                        return b2;
                    case (MTByteDataFormatEnum.ABCD):
                        b4[3] = (byte)(i & 0xff);
                        b4[2] = (byte)(i >> 8 & 0xff);
                        b4[1] = (byte)(i >> 16 & 0xff);
                        b4[0] = (byte)(i >> 24 & 0xff);
                        return b4;
                    case (MTByteDataFormatEnum.DCBA):
                        b4[0] = (byte)(i & 0xff);
                        b4[1] = (byte)(i >> 8 & 0xff);
                        b4[2] = (byte)(i >> 16 & 0xff);
                        b4[3] = (byte)(i >> 24 & 0xff);
                        return b4;
                    case (MTByteDataFormatEnum.BADC):
                        b4[2] = (byte)(i & 0xff);
                        b4[3] = (byte)(i >> 8 & 0xff);
                        b4[0] = (byte)(i >> 16 & 0xff);
                        b4[4] = (byte)(i >> 24 & 0xff);
                        return b4;
                    case (MTByteDataFormatEnum.CDAB):
                        b4[1] = (byte)(i & 0xff);
                        b4[0] = (byte)(i >> 8 & 0xff);
                        b4[3] = (byte)(i >> 16 & 0xff);
                        b4[2] = (byte)(i >> 24 & 0xff);
                        return b4;
                    default:
                        return new byte[0];
                }
            }
            catch (Exception)
            {
                return new byte[0];
            }

        }



        /// <summary>
        /// 将两个数组合并成一个数组
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        public static byte[] Merge(byte[] b1, byte[] b2)
        {
            byte[] c = new byte[b1.Length + b2.Length];
            Array.Copy(b1, 0, c, 0, b1.Length);
            Array.Copy(b2, 0, c, b1.Length, b2.Length);
            return c;
        }

        #endregion

    }
}
