
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace MTLibrary
{
    public class AjaxHelper
    {  
        
        //解析字符集00
        public static Encoding encoding = Encoding.UTF8;
        #region 
        /// <summary>
        /// 添加头部参数
        /// </summary>
        /// <param name="req"></param>
        /// <param name="headersDic"></param>
        public static void AddHttpHeaders(ref HttpWebRequest req, Dictionary<string, string> headersDic)
        {
            if (headersDic != null && headersDic.Count > 0)
            {
                foreach (var item in headersDic)
                {
                    req.Headers.Add(item.Key, item.Value);
                }
            }
        }

        /// <summary>
        /// 获取内容
        /// </summary>
        /// <param name="streamSend"></param>
        /// <returns></returns>
        public static string ReadToStream(Stream streamSend)
        {
            // 创建一个 StreamReader 的实例来读取文件 
            // using 语句也能关闭 StreamReader
            Encoding encoding = Encoding.UTF8;
            StreamReader reader = null;
            string result = string.Empty;
            try
            {
            load:
                reader = new StreamReader(streamSend, encoding);
                result = reader.ReadToEnd();
                if (result.Contains("�") || result.Contains("★") || result.Contains("╀") || result.Contains("??"))
                {
                    switch (encoding.WebName)
                    {
                        case "utf-8":
                            encoding = Encoding.Default;
                            break;
                        case "GB2312":
                            encoding = Encoding.ASCII;
                            break;
                        case "us-ascii":
                            encoding = Encoding.UTF7;
                            break;
                        case "utf-7":
                            encoding = Encoding.Unicode;
                            break;
                        default:
                            throw new Exception("内容乱码");
                    }
                    goto load;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            return result;
        }



        /// <summary>
        /// 指定Url地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">服务器访问地址[带有https://或http://]</param>
        /// <param name="result">从服务器返回的字符串</param>
        /// <param name="IsSaveLogData">是否写入log日志</param>
        /// <param name="timeout">连接服务器超时时间</param>
        /// <param name="type">传参格式[对象还是Json]</param>
        /// <returns></returns>
        public static bool Get(string url, out string result, Dictionary<string, string> headersDic, bool IsSaveLogData=false, int timeout= 60000, string type= "application/json")
        {
            result = string.Empty;
            if (!url.Contains("http"))
            {
                url = "http://" + url;
            }
            string HttpServerAddress = url;
            Stream streamSend = null;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            try
            {
                //.net4.5及以上https 证书问题
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                req = (HttpWebRequest)WebRequest.Create(HttpServerAddress);
                req.Method = "GET";
                req.Timeout = timeout;
                req.ContentType = type;

                //添加请求头
                AddHttpHeaders(ref req, headersDic);
                //添加参数
                resp = (HttpWebResponse)req.GetResponse();
                streamSend = resp.GetResponseStream();
                //获取http返回数据
                result = ReadToStream(streamSend);

                return true;
            }
            catch (WebException wex)
            {
                result = wex.Message;
                return false;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            finally
            {
                if (streamSend != null) streamSend.Close();
                if (req != null) req.Abort();
                if (resp != null) resp.Close();
                if (IsSaveLogData) LogFileHelper.AddLog("url:" + url + ";result:" + result);
            }
        }


        /// <summary>
        /// 指定Url地址使用Get方式获取全部字符串
        /// </summary>
        /// <param name="url">服务器访问地址[带有https://或http://]</param>
        /// <param name="dic">键值对应</param>
        /// <param name="result">>http返回的值</param>
        /// <param name="IsSaveLogData">是否写入log日志</param>
        /// <param name="timeout">连接服务器超时时间</param>
        /// <param name="type">传参格式[对象还是Json]</param>
        /// <returns></returns>
        public static bool Get(string url, Dictionary<string, string> dic, out string result, Dictionary<string, string> headersDic, bool IsSaveLogData=false, int timeout=60000 , string type = "application/json")
        {
            result = string.Empty;
            if (!url.Contains("http"))
            {
                url = "http://" + url;
            }
            string HttpServerAddress = url;
            Stream streamSend = null;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            try
            {
                //.net4.5及以上https 证书问题
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                StringBuilder builder = new StringBuilder();
                builder.Append(HttpServerAddress);
                if (dic.Count > 0)
                {
                    builder.Append("?");
                    int i = 0;
                    foreach (var item in dic)
                    {
                        if (i > 0)
                            builder.Append("&");
                        builder.AppendFormat("{0}={1}", item.Key, item.Value);
                        i++;
                    }
                }
                req = (HttpWebRequest)WebRequest.Create(builder.ToString());
                req.Method = "GET";
                req.Timeout = timeout;
                req.ContentType = type;

                //添加请求头
                AddHttpHeaders(ref req, headersDic);
                //添加参数
                resp = (HttpWebResponse)req.GetResponse();
                streamSend = resp.GetResponseStream();
                //获取http返回数据
                result = ReadToStream(streamSend);
                return true;
            }
            catch (WebException wex)
            {
                result = wex.Message;
                return false;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            finally
            {
                if (streamSend != null) streamSend.Close();
                if (req != null) req.Abort();
                if (resp != null) resp.Close();
                if (IsSaveLogData) LogFileHelper.AddLog("url:" + url + ";result:" + result);
            }


        }

        /// <summary>
        /// 指定Url地址使用Post方式提交
        /// </summary>
        /// <param name="url">服务器访问地址[带有https://或http://]</param>
        /// <param name="result">>http返回的值</param>
        /// <param name="IsSaveLogData">是否写入log日志</param>
        /// <param name="timeout">连接服务器超时时间</param>
        /// <param name="type">传参格式</param>
        /// <returns></returns>
        /// <returns></returns>
        public static bool Post(string url, out string result, Dictionary<string, string> headersDic, bool IsSaveLogData = false, int timeout = 60000, string type = "application/json")
        {

            result = string.Empty;
            if (!url.Contains("http"))
            {
                url = "http://" + url;
            }
            string HttpServerAddress = url;
            Stream streamSend = null;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            try
            {
                //.net4.5及以上https 证书问题
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                req = (HttpWebRequest)WebRequest.Create(HttpServerAddress);
                req.Method = "POST";
                req.Timeout = timeout;
                req.ContentType = type;

                //添加请求头
                AddHttpHeaders(ref req, headersDic);
                //添加参数
                resp = (HttpWebResponse)req.GetResponse();
                streamSend = resp.GetResponseStream();
                //获取http返回数据
                result = ReadToStream(streamSend);

                return true;
            }
            catch (WebException wex)
            {
                result = wex.Message;
                return false;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            finally
            {
                if (streamSend != null) streamSend.Close();
                if (req != null) req.Abort();
                if (resp != null) resp.Close();
                if (IsSaveLogData) LogFileHelper.AddLog("url:" + url + ";result:" + result);
            }
        }


        /// <summary>
        /// 指定Url地址使用Post方式提交
        /// </summary>
        /// <param name="url">服务器访问地址[带有https://或http://]</param>
        /// <param name="content">json内容</param>
        /// <param name="result">>http返回的值</param>
        /// <param name="IsSaveLogData">是否写入log日志</param>
        /// <param name="timeout">连接服务器超时时间</param>
        /// <param name="type">传参格式</param>
        /// <returns></returns>
        public static bool Post(string url, string content, out string result, Dictionary<string, string> headersDic, bool IsSaveLogData = false, int timeout = 60000, string type = "application/json")
        {

            result = string.Empty;
            if (!url.Contains("http"))
            {
                url = "http://" + url;
            }
            string HttpServerAddress = url;
            Stream streamSend = null;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            try
            {
                //.net4.5及以上https 证书问题
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                req = (HttpWebRequest)WebRequest.Create(HttpServerAddress);
                req.Method = "POST";
                req.Timeout = timeout;
                req.ContentType = type;

                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                //添加请求头
                AddHttpHeaders(ref req, headersDic);
                //添加参数
                resp = (HttpWebResponse)req.GetResponse();
                streamSend = resp.GetResponseStream();
                //获取http返回数据
                result = ReadToStream(streamSend);
                return true;
            }
            catch (WebException wex)
            {

                result = wex.Message;
                return false;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            finally
            {
                if (streamSend != null) streamSend.Close();
                if (req != null) req.Abort();
                if (resp != null) resp.Close();
                if (IsSaveLogData) LogFileHelper.AddLog("url:" + url + ";result:" + result);
            }

        }



        /// <summary>
        /// 指定Url地址使用Post方式提交
        /// </summary>
        /// <param name="url">服务器访问地址[带有https://或http://]</param>
        /// <param name="dic">键值对应</param>
        /// <param name="result">>http返回的值</param>
        /// <param name="IsSaveLogData">是否写入log日志</param>
        /// <param name="timeout">连接服务器超时时间</param>
        /// <param name="type">传参格式</param>
        /// <returns></returns>
        public static bool Post(string url, Dictionary<string, string> dic, out string result, Dictionary<string, string> headersDic, bool IsSaveLogData = false, int timeout = 60000, string type = "application/x-www-form-urlencoded")
        {

            result = string.Empty;
            if (!url.Contains("http"))
            {
                url = "http://" + url;
            }
            string HttpServerAddress = url;
            Stream streamSend = null;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            try
            {
                //.net4.5及以上https 证书问题
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                req = (HttpWebRequest)WebRequest.Create(HttpServerAddress);
                req.Method = "POST";
                req.Timeout = timeout;
                req.ContentType = type;//传json对象
                #region 添加Post 参数
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                //添加请求头
                AddHttpHeaders(ref req, headersDic);
                //添加参数
                resp = (HttpWebResponse)req.GetResponse();
                streamSend = resp.GetResponseStream();
                //获取http返回数据
                result = ReadToStream(streamSend);
                return true;
            }
            catch (WebException wex)
            {
                result = wex.Message;
                return false;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            finally
            {
                if (streamSend != null) streamSend.Close();
                if (req != null) req.Abort();
                if (resp != null) resp.Close();
                if (IsSaveLogData) LogFileHelper.AddLog("url:" + url + ";result:" + result);
            }
        }


        /// <summary>
        /// 指定Url地址使用Post方式提交
        /// </summary>
        /// <param name="url">服务器访问地址[带有https://或http://]</param>
        /// <param name="result">>http返回的值</param>
        /// <param name="IsSaveLogData">是否写入log日志</param>
        /// <param name="timeout">连接服务器超时时间</param>
        /// <param name="type">传参格式</param>
        /// <returns></returns>
        /// <returns></returns>
        public static bool Put(string url, out string result, Dictionary<string, string> headersDic, bool IsSaveLogData = false, int timeout = 60000, string type = "application/json")
        {

            result = string.Empty;
            if (!url.Contains("http"))
            {
                url = "http://" + url;
            }
            string HttpServerAddress = url;
            Stream streamSend = null;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            try
            {
                //.net4.5及以上https 证书问题
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                req = (HttpWebRequest)WebRequest.Create(HttpServerAddress);
                req.Method = "PUT";
                req.Timeout = timeout;
                req.ContentType = type;

                //添加请求头
                AddHttpHeaders(ref req, headersDic);
                //添加参数
                resp = (HttpWebResponse)req.GetResponse();
                streamSend = resp.GetResponseStream();
                //获取http返回数据
                result = ReadToStream(streamSend);
                return true;
            }
            catch (WebException wex)
            {
                result = wex.Message;
                return false;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            finally
            {
                if (streamSend != null) streamSend.Close();
                if (req != null) req.Abort();
                if (resp != null) resp.Close();
                if (IsSaveLogData) LogFileHelper.AddLog("url:" + url + ";result:" + result);
            }
        }


        /// <summary>
        /// 指定Url地址使用Put方式提交
        /// </summary>
        /// <param name="url">服务器访问地址[带有https://或http://]</param>
        /// <param name="content">json内容</param>
        /// <param name="result">>http返回的值</param>
        /// <param name="IsSaveLogData">是否写入log日志</param>
        /// <param name="timeout">连接服务器超时时间</param>
        /// <param name="type">传参格式</param>
        /// <returns></returns>
        public static bool Put(string url, string content, out string result, Dictionary<string, string> headersDic, bool IsSaveLogData = false, int timeout = 60000, string type = "application/json")
        {

            result = string.Empty;
            if (!url.Contains("http"))
            {
                url = "http://" + url;
            }
            string HttpServerAddress = url;
            Stream streamSend = null;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            try
            {
                //.net4.5及以上https 证书问题
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                req = (HttpWebRequest)WebRequest.Create(HttpServerAddress);
                req.Method = "PUT";
                req.Timeout = timeout;
                req.ContentType = type;

                #region 添加Post 参数
                byte[] data = Encoding.UTF8.GetBytes(content);
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                //添加请求头
                AddHttpHeaders(ref req, headersDic);
                //添加参数
                resp = (HttpWebResponse)req.GetResponse();
                streamSend = resp.GetResponseStream();
                //获取http返回数据
                result = ReadToStream(streamSend);
                return true;
            }
            catch (WebException wex)
            {
                result = wex.Message;
                return false;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            finally
            {
                if (streamSend != null) streamSend.Close();
                if (req != null) req.Abort();
                if (resp != null) resp.Close();
                if (IsSaveLogData) LogFileHelper.AddLog("url:" + url + ";result:" + result);
            }

        }




        /// <summary>
        /// 指定Url地址使用Put方式提交
        /// </summary>
        /// <param name="url">服务器访问地址[带有https://或http://]</param>
        /// <param name="dic">键值对应</param>
        /// <param name="result">>http返回的值</param>
        /// <param name="IsSaveLogData">是否写入log日志</param>
        /// <param name="timeout">连接服务器超时时间</param>
        /// <param name="type">传参格式</param>
        /// <returns></returns>
        public static bool Put(string url, Dictionary<string, string> dic, out string result, Dictionary<string, string> headersDic, bool IsSaveLogData = false, int timeout = 60000, string type = "application/x-www-form-urlencoded")
        {

            result = string.Empty;
            if (!url.Contains("http"))
            {
                url = "http://" + url;
            }
            string HttpServerAddress = url;
            Stream streamSend = null;
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            try
            {
                //.net4.5及以上https 证书问题
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                req = (HttpWebRequest)WebRequest.Create(HttpServerAddress);
                req.Method = "PUT";
                req.Timeout = timeout;
                req.ContentType = type;//传json对象
                #region 添加Post 参数
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion

                //添加请求头
                AddHttpHeaders(ref req, headersDic);
                //添加参数
                resp = (HttpWebResponse)req.GetResponse();
                streamSend = resp.GetResponseStream();
                //获取http返回数据
                result = ReadToStream(streamSend);
                return true;
            }
            catch (WebException wex)
            {
                result = wex.Message;
                return false;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
            finally
            {
                if (streamSend != null) streamSend.Close();
                if (req != null) req.Abort();
                if (resp != null) resp.Close();
                if (IsSaveLogData) LogFileHelper.AddLog("url:" + url + ";result:" + result);
            }
        }




        #endregion

    }
}
