using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;


namespace MTLibrary
{
    public class SocketHelper
    {
        #region 
  
        /// <summary>
        /// 通过Socket发送数据
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="timeout"></param>
        public static void Send(Socket socket, byte[] buffer, int offset, int size, int timeout)
        {
            if (socket == null) throw new Exception("Socket为null");
            //判断服务端是否关闭
            if (socket.Poll(3000, SelectMode.SelectRead)) throw new Exception("Socket连接已断开");
            int startTickCount = Environment.TickCount;
            int sent = 0;  // how many bytes is already sent
            do
            {
                if (Environment.TickCount > startTickCount + timeout)
                    throw new Exception("Timeout.");
                try
                {
                    sent += socket.Send(buffer, offset + sent, size - sent, SocketFlags.None);
                }
                catch (SocketException ex)
                {

                    Thread.Sleep(30);

                }
            } while (sent < size);
        }
     

        /// <summary>
        /// 根据接收缓存出现异常次数判断是否超时(推荐使用数据接收数据特别多的情况)(Wince必用)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="terminator"></param>
        /// <returns></returns>
        public static byte[] Receive(Socket socket, string terminator)
        {
            if (socket == null) throw new Exception("Socket为初始化.");
            int reconnectionsCount = 5;//阈值
            int reconnections = 0;//超时次数
            byte[] buffer = new byte[1];   //按一个字节接收
            byte[] bufferReceiveData = new byte[0]; //接收的所有字节
            List<byte> receiveByteList = new List<byte>();//接收的缓存数据
            byte[] terminatorByte = Encoding.UTF8.GetBytes(terminator);//byte结束符

            //判断接收的字节是否一直为空的处理
            int bufferReconnectionsCount = 1000;//butter比较的阈值
            int bufferReconnections = 0;//butter比较的阈值
            do
            {
            login:
                try
                {
                    //一个一个字节接收
                    buffer = new byte[1];
                    socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

                    //判断是否接受数据一直为空并超出阈值
                    if (buffer[0] == 0) bufferReconnections++;
                    else bufferReconnections = 0;
                    if (bufferReconnections >= bufferReconnectionsCount) throw new Exception("Timeout.");

                    receiveByteList.AddRange(buffer);
                    bufferReceiveData = new byte[receiveByteList.Count];
                    Array.Copy(receiveByteList.ToArray(), bufferReceiveData, receiveByteList.Count);
                    //判断接收的数据是否大于结束符的长度

                    if (bufferReceiveData.Length >= terminatorByte.Length)
                    {
                        for (int i = 1; i <= terminatorByte.Length; i++)
                        {
                            if (terminatorByte[terminatorByte.Length - i] != bufferReceiveData[bufferReceiveData.Length - i]) goto login;
                        }
                        return bufferReceiveData;
                    }
                }
                catch (SocketException ex)
                {
                    reconnections++;
                    if (reconnections >= reconnectionsCount) throw new Exception("Timeout.");
                    Thread.Sleep(30);
                }
            } while (true);
        }

        /// <summary>
        /// 获取一行数据(推荐使用数据接收数据特别多的情况)(Wince必用但是接收时间特别长)
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="reconnectionsCount"></param>
        /// <returns></returns>
        public static byte[] ReadLine(Socket socket)
        {
            if (socket == null) throw new Exception("Socket为初始化.");
            int reconnectionsCount = 5;//阈值
            int reconnections = 0;//超时次数
            byte[] buffer = new byte[1];   //按一个字节接收
            byte[] bufferReceiveData = new byte[0]; //接收的所有字节
            List<byte> receiveByteList = new List<byte>();//接收的缓存数据
            byte[] terminatorByte = Encoding.UTF8.GetBytes("\r\n");//byte结束符

            //判断接收的字节是否一直为空的处理
            int bufferReconnectionsCount = 1000;//butter比较的阈值
            int bufferReconnections = 0;//butter比较的阈值

            do
            {
            login:
                try
                {
                    //一个一个字节接收
                    buffer = new byte[1];
                    socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

                    //判断是否接受数据一直为空并超出阈值
                    if (buffer[0] == 0) bufferReconnections++;
                    else bufferReconnections = 0;
                    if (bufferReconnections >= bufferReconnectionsCount) throw new Exception("Timeout.");

                    receiveByteList.AddRange(buffer);
                    bufferReceiveData = new byte[receiveByteList.Count];
                    Array.Copy(receiveByteList.ToArray(), bufferReceiveData, receiveByteList.Count);
                    //判断接收的数据是否大于结束符的长度

                    if (bufferReceiveData.Length >= terminatorByte.Length)
                    {
                        for (int i = 1; i <= terminatorByte.Length; i++)
                        {
                            if (terminatorByte[terminatorByte.Length - i] != bufferReceiveData[bufferReceiveData.Length - i]) goto login;
                        }
                        return bufferReceiveData;
                    }
                }
                catch (SocketException ex)
                {
                    throw new Exception("Timeout.");
                    //reconnections++;
                    //if (reconnections >= reconnectionsCount) throw new Exception("Timeout.");
                    //Thread.Sleep(30);
                }
                catch (ObjectDisposedException closedException)
                {

                    throw new Exception("Timeout.");
                }
                catch (Exception ex)
                {
                    throw new Exception("Timeout.");
                }
            } while (true);
        }

        #endregion

     
    }
}
