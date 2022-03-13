using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace MTLibrary
{
    public class HttpServiceHelper
    {
        public List<HttpListenEntry> ListenEntries { get; set; } = new List<HttpListenEntry>();

        public HttpListener Listener { get; set; }
        public bool IsListening { get; set; } = false;

        public void AddListenUrl(string url, HttpListenEntry.OnRequestDG onRequest)
        {
            ListenEntries.Add(new HttpListenEntry()
            {
                Url = url,
                OnRequest = onRequest,
            });
        }
        public void Start()
        {
            if (IsListening) return;

            try
            {
                Listener = new HttpListener();
                Listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
                foreach (var l in ListenEntries)
                {
                   
                    Listener.Prefixes.Add(l.Url);
                }
             
                Listener.Start();
                IsListening = true;
                Thread listenThread = new Thread(() =>
                {
                    try
                    {
                        while (IsListening)
                        {
                            var context = Listener.GetContext();
                        
                            var a = ListenEntries.FirstOrDefault(r => r.IsMatched(context.Request.Url.ToString()));

                            if (a == null)
                            {
                               
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                context.Response.Close();
                            }
                            else
                            {
                              
                                a.ProcessRequest(context);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    
                    }
                });
                listenThread.Start();
            }
            catch (Exception ex)
            {
              
            }
        }
        public void Start(OnLogDG onLog)
        {
            if (IsListening) return;

            try
            {
                Listener = new HttpListener();
                Listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
                foreach (var l in ListenEntries)
                {
                    onLog?.Invoke($"Adding {l.Url}");
                    Listener.Prefixes.Add(l.Url);
                }
                onLog?.Invoke($"Start service");
                Listener.Start();
                IsListening = true;
                Thread listenThread = new Thread(() =>
                {
                    try
                    {
                        while (IsListening)
                        {
                            var context = Listener.GetContext();
                            onLog?.Invoke($"[{context?.Request.RemoteEndPoint.Address}] New request reached: url={context.Request.Url}");
                            var a = ListenEntries.FirstOrDefault(r => r.IsMatched(context.Request.Url.ToString()));

                            if (a == null)
                            {
                                onLog?.Invoke($"[{context?.Request.RemoteEndPoint.Address}] Can not found matched listener");
                                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                                context.Response.Close();
                            }
                            else
                            {
                                onLog?.Invoke($"[{context?.Request.RemoteEndPoint.Address}] Process request");
                                a.ProcessRequest(context);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        onLog?.Invoke(ex.Message);
                    }
                });
                listenThread.Start();
            }
            catch (Exception ex)
            {
                onLog?.Invoke(ex.ToString());
            }
        }

        public void Stop()
        {
            if (!IsListening)
                return;
            IsListening = false;
            Listener?.Close();
        }

        public delegate void OnLogDG(string text);
    }

    public class HttpListenEntry
    {
        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (_url != value)
                {
                    _url = value;
                    UrlMatcher = new Regex($"^{value?.ToLower().TrimEnd('/').Replace("*", ".*")}");
                }
            }
        }
        public OnRequestDG OnRequest { get; set; }
        public Regex UrlMatcher { get; set; }

        public bool IsMatched(Uri url)
        {
            return IsMatched(url?.ToString());
        }

        public bool IsMatched(string url)
        {
            return UrlMatcher?.IsMatch(url.ToLower()) ?? false;
        }

        private object _lock = new object();
        public List<HttpRequestTask> Requests { get; set; } = new List<HttpRequestTask>();

        public void ProcessRequest(HttpListenerContext context)
        {
            if (Requests.Exists(r => r.Context == context))
                return;
            var req = new HttpRequestTask()
            {
                Parent = this,
                Context = context,
                WorkingThread = new Thread((a) =>
                {
                    if (a is HttpRequestTask task)
                    {
                        try
                        {
                            OnRequest?.Invoke(task);
                        }
                        catch { }
                        finally
                        {
                            try
                            {
                                task.Context.Response.Close();
                            }
                            catch { }
                            task.Parent.RemoveRequest(task);
                        }
                    }
                })
            };
            lock (_lock)
            {
                Requests.Add(req);
            }
            req.WorkingThread.Start(req);
        }

        public void RemoveRequest(HttpRequestTask task)
        {
            lock (_lock)
            {
                Requests.Remove(task);
            }
        }

        public delegate void OnRequestDG(HttpRequestTask task);
    }

    public class HttpRequestTask
    {
        public HttpListenEntry Parent { get; set; }
        public HttpListenerContext Context { get; set; }
        public Thread WorkingThread { get; set; }

        public int ReadInterruptTimeout { get; set; } = 5000;

        public Stream InputStream => Context?.Request.InputStream;
        public Stream OutputStream => Context?.Response.OutputStream;

        public EndPoint LocalEndPoint => Context.Request.LocalEndPoint;
        public EndPoint RemoteEndPoint => Context.Request.RemoteEndPoint;

        public string RequestMethod => Context?.Request.HttpMethod;
        public NameValueCollection RequstHeaders => Context?.Request.Headers;
        public byte[] RequestContent
        {
            get
            {
                if (Context?.Request.ContentLength64 > 0)
                {
                    var len = (int)Context?.Request.ContentLength64;
                    var buf = new byte[len];
                    int read = 0;
                    var start = DateTime.Now;
                    while (read < len)
                    {
                        int n = InputStream.Read(buf, read, buf.Length - read);
                        if (n > 0)
                        {
                            start = DateTime.Now;
                            read += n;
                        }
                        else
                        {
                            if ((DateTime.Now - start).TotalMilliseconds > ReadInterruptTimeout)
                                throw new TimeoutException();
                        }
                    }
                    return buf;
                }
                return null;
            }
        }
        public string RequestContentString => Context?.Request.ContentEncoding?.GetString(RequestContent ?? new byte[0]) ?? "";

        public string RequestQueryName
        {
            get
            {
                var query = Context?.Request.Url.ToString().Remove(0, Parent.Url.Length) ?? "";
                return query.Split('?').FirstOrDefault();
            }
        }

        public Dictionary<string, string> RequestQueryParams
        {
            get
            {
                return GetDecodedRequestActionParams(Encoding.UTF8);
            }
        }

        public string RequestActionParamString
        {
            get
            {
                var query = Context?.Request.Url.Query ?? "";
                query = query.TrimStart('?');
                return query;
            }
        }

        public Dictionary<string, string> GetDecodedRequestActionParams(Encoding encoding)
        {
            var items = new Dictionary<string, string>();
            var query = RequestActionParamString;
            var parts = query.Split('&');
            foreach (var p in parts)
            {
                string key = null, val = null;
                var ss = p.Split('=');
                if (ss.Length > 0)
                    key = ss[0] ?? "";
                if (ss.Length > 1)
                    val = ss[1] ?? "";
                if (!string.IsNullOrEmpty(key))
                {
                    key = HttpUtility.UrlDecode(key, encoding);
                    val = HttpUtility.UrlDecode(val, encoding);
                    items.Add(key, val);
                }
            }

            return items;
        }

        [Obsolete("use GetDecodedRequestActionParams instead")]
        public Dictionary<string, string> GetRequestActionParams(Encoding encoding)
        {
            return GetDecodedRequestActionParams(encoding);
        }



        public void Response(HttpStatusCode status, string desc)
        {
            var response = Context?.Response;
            if (response == null)
                return;

            response.StatusCode = (int)status;
            response.StatusDescription = desc;
        }

        public void ResponseJson(HttpStatusCode status, string jsonStr)
        {
            ResponseJson(status, jsonStr, Encoding.UTF8);
        }

        public void ResponseJson(HttpStatusCode status, string jsonStr, Encoding encoding)
        {
            var response = Context?.Response;
            if (response == null)
                return;

            var buf = encoding.GetBytes(jsonStr);

            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            response.ContentEncoding = encoding;
            response.OutputStream.Write(buf, 0, buf.Length);
        }

    }

}
