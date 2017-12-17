//云服务请求接口
//HEJS 创建，2016-04-24

/*
https://localhost:9443/caxa/get_task_list
https://localhost:9443/caxa/get_task?task_id=0Hck3KI5kEmJWh
https://localhost:9443/caxa/get_object_data?proc_id=E09E68C9-EEDA-4966-AA4D-DCCCE465074B&object_id=0Hck3KI5kEmJWh&flag=
https://localhost:9443/caxa/get_settings
https://localhost:9443/caxa/get_picture?task_id=0Hck3KI5kEmJWh
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using Upload;
using DbControl;
using System.Data;

namespace RESTClient
{
    public class WebRequestSession
    {
        public Boolean ssl;
        public String host;
        public int port;
        public String url;

        public String user;
        public String pass;
        public String language;
        public String sessionid;

        public String cookie;
        public String content_type;
        public byte[] content;

        public int delay;
        public CancellationTokenSource cts;

        public Timer _KeepaliveTimer;

        public WebRequestSession()
        {
            ssl = true;
            host = "";
            port = -1;
            url = "";

            user = "";
            pass = "";
            language = "";
            sessionid = "";

            cookie = "";
            content_type = "";
            content = null;

            delay = 100;
            cts = null;
            _KeepaliveTimer = null;
        }

        public void cbKeepalive(Object stateInfo)
        {
            String kpAlive = "/admin/keepalive.do";
            StringBuilder errMessage = new StringBuilder();
            String result = RemoteCall.RESTGetAsString(ssl, host, port, kpAlive, errMessage, cts);
            if (errMessage.Length > 0)
            {
                if (_KeepaliveTimer != null)
                {
                    _KeepaliveTimer.Dispose();
                    _KeepaliveTimer = null;

                    sessionid = "";

                    RemoteCall.SetSession("");
                    RemoteCall.Cleanup();
                }
            }
        }

        public WebRequestSession Clone()
        {
            WebRequestSession session = new WebRequestSession();
            session.ssl = ssl;
            session.host = host;
            session.port = port;
            session.url = url;

            session.user = user;
            session.pass = pass;
            session.language = language;
            session.sessionid = sessionid;

            session.cookie = cookie;
            session.content_type = content_type;
            session.content = null;
            if (content != null)
                session.content = (byte[])content.Clone();

            session.delay = delay;
            session.cts = cts;
            return session;
        }

        public Boolean Login(Boolean Ssl, String Host, int Port, String User, String Pass, String Language, String SiteID)
        {
            if (_KeepaliveTimer != null)
            {
                _KeepaliveTimer.Dispose();
                _KeepaliveTimer = null;
            }

            cts = new CancellationTokenSource();

            ssl = Ssl;
            host = Host;
            port = Port;
            user = User;
            pass = Pass;
            language = Language;

            url = "/auth/login.do";

            String sid = RemoteCall.RESTLogin(this, url, cts);
            if (!String.IsNullOrEmpty(sid))
            {
                sessionid = sid;

                RemoteCall.Init(ssl, host, port);
                RemoteCall.SetSession(sessionid);

                TimerCallback tcb = cbKeepalive;
                _KeepaliveTimer = new Timer(tcb, this, 0, 5000);
                return true;
            }
            return false;
        }

        public Boolean IsSigned()
        {
            return !String.IsNullOrEmpty(sessionid);
        }

        public void Logout(Boolean bCleanup=false)
        {
            if (_KeepaliveTimer != null)
            {
                _KeepaliveTimer.Dispose();
                _KeepaliveTimer = null;
            }

            String path = "/auth/logout.do";
            StringBuilder errMessage = new StringBuilder();
            String result = RemoteCall.RESTGetAsString(ssl, host, port, path, errMessage, cts);

            sessionid = "";

            RemoteCall.SetSession("");

            if (bCleanup)
                RemoteCall.Cleanup();
        }
    }

    public class RemoteCall
    {
        private static Boolean _SSL = true;
        private static String _HOST = "localhost";
        private static int _PORT = 9443;

        private static String _SessionID = "";

        public const int LOG_INFO = 1;
        public const int LOG_WARNING = 2;
        public const int LOG_FATAL = 3;

        public const String API_PREFIX = "caxa";

        public static object _lockCall = new object();

        public static ConcurrentQueue<String> _DownloadedFiles = new ConcurrentQueue<string>();
        private static ConcurrentQueue<string> _logList = new ConcurrentQueue<string>();

        //本地记个日志
        public static void DebugLog(int level, string format, params object[] paramList)
        {
            string log = String.Format(format, paramList);
            _logList.Enqueue(log);

            while (_logList.Count > 200)
            {
                string rmlog = "";
                if (_logList.TryDequeue(out rmlog))
                {
                }
            }
        }

        public static void SetSession(String sid)
        {
            _SessionID = sid;
        }

        public static List<String> GetLog()
        {
            List<String> logList = new List<string>();
            while (_logList.Count > 0)
            {
                string log = "";
                if (_logList.TryDequeue(out log))
                {
                    logList.Add(log);
                }
            }
            return logList;
        }

        //主程序启动后调用一次，设置Server的ip和端口
        public static void Init(Boolean ssl, String host, int port)
        {
            _SSL = ssl;
            _HOST = host;
            _PORT = port;
        }

        //主程序退出之前调用，以便清理用过的临时文件
        public static void Cleanup(Boolean all=true)
        {
            while (_DownloadedFiles.Count > 0)
            {
                try
                {
                    String tmpFile;
                    if (_DownloadedFiles.TryPeek(out tmpFile))
                    {
                        DateTime tLastWrite = File.GetLastWriteTime(tmpFile);
                        TimeSpan tSpan = DateTime.Now - tLastWrite;
                        if (all || (tSpan.TotalHours > 1))
                        { //全部删除，或一小时之前的文件
                            if (_DownloadedFiles.TryDequeue(out tmpFile))
                            {
                                if (File.Exists(tmpFile))
                                    File.Delete(tmpFile);
                            }
                        }
                        else if (!all)
                        { //排在最前面的，一定是最旧的，所以不满足，则结束
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
        }

        //向服务器请求任务列表，返回结果存在本地临时文件 pTaskFilePath 为文件路径
        public static Boolean GetTaskList(StringBuilder pTaskFilePath)
        {   //"/caxa/get_task_list"
            pTaskFilePath.Clear();
            try
            {
                StringBuilder errMessage = new StringBuilder();
                String file = RESTGetAsFile(_SSL, _HOST, _PORT, "/caxa/get_task_list", errMessage);
                if (!String.IsNullOrEmpty(file) && File.Exists(file))
                {
                    pTaskFilePath.Append(file);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return false;
        }

        //请求任务相关文件
        public static Boolean StartTask(String taskID, StringBuilder taskFile)
        { ////"/caxa/get_task?task_id=xxxx"
            taskFile.Clear();
            try
            {
                StringBuilder errMessage = new StringBuilder();
                String file = RESTGetAsFile(_SSL, _HOST, _PORT, String.Format("/caxa/get_task?task_id={0}", taskID.Trim()), errMessage);
                if (!String.IsNullOrEmpty(file) && File.Exists(file))
                {
                    taskFile.Append(file);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return false;
        }

        //请求部件相关工艺文件
        public static Boolean GetObjectData(String ProcID, String ObjectID, StringBuilder filepath, String flag)
        { //"/caxa/get_object_data?proc_id=xxxx&object_id=xxxx"
            filepath.Clear();
            try
            {
                StringBuilder errMessage = new StringBuilder();
                String file = RESTGetAsFile(_SSL, _HOST, _PORT, String.Format("/caxa/get_object_data?proc_id={0}&object_id={1}&flag={2}", ProcID.Trim(), ObjectID.Trim(), flag.Trim()), errMessage);
                if (!String.IsNullOrEmpty(file) && File.Exists(file))
                {
                    filepath.Append(file);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return false;
        }

        //获取设置文件
        public static Boolean GetSettingFile(StringBuilder settingFile)
        { //"/caxa/get_settings"
            settingFile.Clear();
            try
            {
                StringBuilder errMessage = new StringBuilder();
                String file = RESTGetAsFile(_SSL, _HOST, _PORT, "/caxa/get_settings", errMessage);
                if (!String.IsNullOrEmpty(file) && File.Exists(file))
                {
                    settingFile.Append(file);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return false;
        }

        //获取图片文件
        public static Boolean GetTaskPic(string i_taskID, StringBuilder o_filepath)
        { //"/caxa/get_picture"
            o_filepath.Clear();
            try
            {
                StringBuilder errMessage = new StringBuilder();
                String file = RESTGetAsFile(_SSL, _HOST, _PORT, String.Format("/caxa/get_picture?task_id={0}", i_taskID.Trim()), errMessage);
                if (!String.IsNullOrEmpty(file) && File.Exists(file))
                {
                    o_filepath.Append(file);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return false;
        }

        //上载料单文件
        public static Boolean UploadNestingList(String[] files)
        {
            String result = RESTUpload(_SSL, _HOST, _PORT, "/caxa/parts_nesting", files, null);
            return !String.IsNullOrEmpty(result);
        }

        //上载多个文件
        public static String RESTUpload(Boolean bSSL, String host, int port, String path, String[] files, CancellationTokenSource cts = null)
        {
            try
            {
                String url = String.Format("http{0}://{1}:{2}{3}", bSSL ? "s" : "", host, port, path);
                DebugLog(LOG_INFO, "Uploading URL: {0}", url);

                List<UploadFile> upFiles = new List<UploadFile>();
                for (int i = 0; i < files.Length; i++)
                {
                    String filePath = files[i];
                    using (FileStream stream = File.Open(filePath, FileMode.Open))
                    {
                        String fileName = Path.GetFileName(filePath);
                        String fileExt = Path.GetExtension(filePath);
                        UploadFile file = new UploadFile
                        {
                            FullPath = filePath,
                            Name = fileName,
                            Filename = fileName,
                            ContentType = MimeTypes.GetMimeTypeByExt(fileExt),
                            Stream = stream
                        };
                        upFiles.Add(file);
                    }
                }

                if (UploadFile.UploadFiles(url, _SessionID, upFiles, null, null))
                    return "Success";
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "RESTUpload failed: {0}", e.Message);
            }
            return null;
        }

        public static String RESTGetAsFile(Boolean bSSL, String host, int port, String path, StringBuilder errMessage, CancellationTokenSource cts = null)
        {
            lock (_lockCall)
            {
                errMessage.Append("Failed");

                String localFile = "";
                String url = String.Format("http{0}://{1}:{2}{3}", bSSL ? "s" : "", host, port, path);
                if (!url.EndsWith("keepalive.do"))
                    DebugLog(LOG_INFO, "Requesting URL: {0}", url);

                HttpClient client = null;
                HttpResponseMessage response = null;
                try
                {
                    HttpClientHandler handler = new HttpClientHandler
                    {
                        AllowAutoRedirect = true,
                        UseCookies = true,
                        CookieContainer = new CookieContainer()
                    };
                    //var httpBaseFilter = new HttpBaseProtocolFilter
                    //{
                    //  AllowUI = false
                    //};

                    client = new HttpClient(handler);
                    //client.DefaultRequestHeaders.Add("Connection", "close");
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    if (!String.IsNullOrEmpty(_SessionID))
                    {
                        Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(url));
                        handler.CookieContainer.Add(uri, new Cookie("SESSIONID", _SessionID));
                    }

                    if (cts != null)
                        response = client.GetAsync(url, cts.Token).Result;
                    else
                        response = client.GetAsync(url).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        errMessage.Clear();

                        HttpContent content = response.Content;
                        if (content != null)
                        {
                            byte[] result = content.ReadAsByteArrayAsync().Result;
                            if (result != null && result.Length > 0)
                            {
                                localFile = WebUtil.GetTempFileName(".tmp");//Path.GetTempFileName();
                                Boolean zip = false;
                                if (content.Headers.ContentType.ToString().EndsWith("zip", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    zip = true;
                                    localFile += ".zip";
                                }

                                File.WriteAllBytes(localFile, result);
                                _DownloadedFiles.Enqueue(localFile);

                                DebugLog(LOG_INFO, "Downloaded file: {0}, size={1}", localFile, result.Length.ToString());
                                return localFile;
                            }
                        }
                        return String.Empty;
                    }
                    else
                    {
                        HttpContent content = response.Content;
                        if (content != null)
                        {
                            byte[] result = content.ReadAsByteArrayAsync().Result;
                            if (result != null && result.Length > 0)
                                errMessage.Append(Encoding.UTF8.GetString(result));
                        }
                        DebugLog(LOG_FATAL, "Http status code: {0}, {1}", response.StatusCode.ToString(), errMessage.ToString());
                    }
                }
                catch (Exception e)
                {
                    DebugLog(LOG_FATAL, "RESTGet failed: {0}", e.StackTrace);
                    Debug.WriteLine(e.StackTrace);
                }

                finally
                {
                    if (response != null)
                        response.Dispose();
                    if (client != null)
                        client.Dispose();
                }

                DebugLog(LOG_FATAL, "Content error: {0}", url);
                return null;
            }
        }

        public static byte[] RESTGetAsBytes(Boolean bSSL, String host, int port, String path, StringBuilder errMessage, CancellationTokenSource cts = null)
        {
            lock (_lockCall)
            {
                errMessage.Append("Failed");

                String url = String.Format("http{0}://{1}:{2}{3}", bSSL ? "s" : "", host, port, path);
                if (!url.EndsWith("keepalive.do"))
                    DebugLog(LOG_INFO, "Requesting URL: {0}", url);

                HttpClient client = null;
                HttpResponseMessage response = null;
                try
                {
                    HttpClientHandler handler = new HttpClientHandler
                    {
                        AllowAutoRedirect = true,
                        UseCookies = true,
                        CookieContainer = new CookieContainer()
                    };

                    client = new HttpClient(handler);
                    //client.DefaultRequestHeaders.Add("Connection", "close");
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    if (!String.IsNullOrEmpty(_SessionID))
                    {
                        Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(url));
                        handler.CookieContainer.Add(uri, new Cookie("SESSIONID", _SessionID));
                    }

                    if (cts != null)
                        response = client.GetAsync(url, cts.Token).Result;
                    else
                        response = client.GetAsync(url).Result;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        errMessage.Clear();

                        HttpContent content = response.Content;
                        byte[] result = new byte[0];
                        if (content != null)
                        {
                            result = content.ReadAsByteArrayAsync().Result;
                            return result;
                        }
                        return result;
                    }
                }
                catch (Exception e)
                {
                    DebugLog(LOG_FATAL, "RESTGet failed: {0}", e.StackTrace);
                    Debug.WriteLine(e.StackTrace);
                }

                finally
                {
                    if (response != null)
                        response.Dispose();
                    if (client != null)
                        client.Dispose();
                }

                DebugLog(LOG_FATAL, "Content error: {0}", url);
                return null;
            }
        }

        public static String RESTGetAsString(Boolean bSSL, String host, int port, String path, StringBuilder errMessage, CancellationTokenSource cts = null)
        {
            lock (_lockCall)
            {
                errMessage.Append("Failed");

                String url = String.Format("http{0}://{1}:{2}{3}", bSSL ? "s" : "", host, port, path);
                if (!url.EndsWith("keepalive.do"))
                    DebugLog(LOG_INFO, "Requesting URL: {0}", url);

                HttpClient client = null;
                HttpResponseMessage response = null;
                try
                {
                    HttpClientHandler handler = new HttpClientHandler
                    {
                        AllowAutoRedirect = true,
                        UseCookies = true,
                        CookieContainer = new CookieContainer()
                    };

                    client = new HttpClient(handler);
                    //client.DefaultRequestHeaders.Add("Connection", "close");
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    if (!String.IsNullOrEmpty(_SessionID))
                    {
                        Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(url));
                        handler.CookieContainer.Add(uri, new Cookie("SESSIONID", _SessionID));
                    }

                    if (cts != null)
                        response = client.GetAsync(url, cts.Token).Result;
                    else
                        response = client.GetAsync(url).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        errMessage.Clear();

                        HttpContent content = response.Content;
                        if (content != null)
                        {
                            byte[] result = content.ReadAsByteArrayAsync().Result;
                            if (result != null && result.Length > 0)
                                return Encoding.UTF8.GetString(result);
                        }
                        return String.Empty;
                    }
                }
                catch (Exception e)
                {
                    DebugLog(LOG_FATAL, "RESTGet failed: {0}", e.StackTrace);
                    Debug.WriteLine(e.StackTrace);
                }

                finally
                {
                    if (response != null)
                        response.Dispose();
                    if (client != null)
                        client.Dispose();
                }

                DebugLog(LOG_FATAL, "Content error: {0}", url);
                return null;
            }
        }

        public static String RESTLogin(WebRequestSession session, String path, CancellationTokenSource cts = null)
        {
            Boolean bSSL = session.ssl;
            String host = session.host;
            int port = session.port;
            String user = session.user;
            String pass =  session.pass;
            String language = session.language; 

            String sessionid = "";
            session.sessionid = "";

            String url = String.Format("http{0}://{1}:{2}{3}", bSSL ? "s" : "", host, port, path);
            DebugLog(LOG_INFO, "Post login URL: {0}", url);

            HttpClient client = null;
            HttpResponseMessage response = null;

            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    UseCookies = true,
                    CookieContainer = new CookieContainer()
                };

                client = new HttpClient(handler);
                //client.DefaultRequestHeaders.Add("Connection", "close");
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                FormUrlEncodedContent toPost = new FormUrlEncodedContent(new[] 
                { 
                    new KeyValuePair<string, string>("username", user), 
                    new KeyValuePair<string, string>("password", pass),
                    new KeyValuePair<string, string>("language", language)
                });

                if (cts != null)
                    response = client.PostAsync(url, toPost, cts.Token).Result;
                else
                    response = client.PostAsync(url, toPost).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(url));
                    IEnumerable<Cookie> responseCookies = handler.CookieContainer.GetCookies(uri).Cast<Cookie>();
                    foreach (Cookie cookie in responseCookies)
                    {
                        Debug.WriteLine(cookie.Name + ": " + cookie.Value);
                        if (cookie.Name.Equals("sessionid", StringComparison.InvariantCultureIgnoreCase))
                        {
                            sessionid = cookie.Value.Trim();
                        }
                    }

                    HttpContent content = response.Content;
                    if (content != null)
                    {
                        String result = content.ReadAsStringAsync().Result;
                        if (result != null && result.Length > 0)
                        {
                            DebugLog(LOG_INFO, "Login successfully, user: {0}, sid={1}", user, sessionid);
                            session.sessionid = sessionid;
                            return sessionid;
                        }
                    }
                }
                else
                {
                    DebugLog(LOG_FATAL, "Http status code: {0}", response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "RESTLogin failed: {0}", e.Message);
            }

            finally
            {
                if (response != null)
                    response.Dispose();
                if (client != null)
                    client.Dispose();
            }

            DebugLog(LOG_FATAL, "Failed to login, user: {0}", user);
            return null;
        }

        public static DataTable RESTQuery(WebRequestSession session, String sql)
        {
            DataTable dt = null;
            try
            {
                if (!session.IsSigned())
                {
                    throw new Exception("Session is not signed");
                    return null;
                }

                if (String.IsNullOrEmpty(sql))
                {
                    throw new Exception("SQL script is not defined");
                    return null;
                }

                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language; 

                byte[] sqlBytes = Encoding.UTF8.GetBytes(sql);

                String path = String.Format("/{0}/get_sql_result.zip?sql={1}", API_PREFIX, DbComm.byte2hex(sqlBytes));
                StringBuilder errMessage = new StringBuilder();
                String resultFile = RemoteCall.RESTGetAsFile(bSSL, host, port, path, errMessage);
                if (File.Exists(resultFile))
                {
                    byte[] queryResult = File.ReadAllBytes(resultFile);
                    dt = DbComm.DecompressHex2DataTable(queryResult);

					try { File.Delete(resultFile); }  catch(Exception e) {}
                }
                else
                {
                    if (errMessage.Length > 0)
                    {
                        throw new Exception(errMessage.ToString());
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            return dt;
        }

        public static DataTable GetDataTable(WebRequestSession session, String dataType, String paramPair)
        { //get_table_result.json?type={1}
            DataTable dt = null;
            try
            {
                if (!session.IsSigned())
                {
                    throw new Exception("Session is not signed");
                }

                if (String.IsNullOrEmpty(dataType))
                {
                    throw new Exception("Data type is not defined");
                }

                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String sQuery = String.Format("{0},{1}", dataType, paramPair);
                byte[] sqlBytes = Encoding.UTF8.GetBytes(sQuery);

                String path = String.Format("/{0}/get_table_result.json?q={1}", API_PREFIX, DbComm.byte2hex(sqlBytes));
                StringBuilder errMessage = new StringBuilder();
                String resultFile = RemoteCall.RESTGetAsFile(bSSL, host, port, path, errMessage);
                if (File.Exists(resultFile))
                {
                    byte[] queryResult = File.ReadAllBytes(resultFile);
                    dt = DbComm.DecompressHex2DataTable(queryResult);

					try { File.Delete(resultFile); }  catch(Exception e) {}
                }
                else
                {
                    if (errMessage.Length > 0)
                    {
                        throw new Exception(errMessage.ToString());
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            return dt;
        }

        public static Boolean RESTNonQuery(WebRequestSession session, String sql)
        {
            try
            {
                if (!session.IsSigned())
                {
                    throw new Exception("Session is not signed");
                    return false;
                }

                if (String.IsNullOrEmpty(sql))
                {
                    throw new Exception("SQL script is not defined");
                    return false;
                }

                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                byte[] sqlBytes = Encoding.UTF8.GetBytes(sql);

                String path = String.Format("/{0}/get_sql_result.json?non_query=1&sql={1}", API_PREFIX, DbComm.byte2hex(sqlBytes));
                StringBuilder errMessage = new StringBuilder();
                String result = RemoteCall.RESTGetAsString(bSSL, host, port, path, errMessage);
                if (errMessage.Length <= 0)
                    return true;

                throw new Exception(errMessage.ToString());
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            return false;
        }

        public static Boolean UploadFiles(WebRequestSession session, String IsFolder, String ServerRelPath, String LocalFullPath, String ServerProcessor, Dictionary<String,String> other, StringBuilder resultMessage=null)
        {
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String zipFile = WebUtil.GetTempFileName(".zip");//Path.GetTempFileName() + ".zip";// "C:\\temp\\bsjj.zip";
                if (Convert.ToInt32(IsFolder) > 0)
                {
                    String srcFolder = LocalFullPath.Replace("/", "\\").Trim();
                    if (!srcFolder.EndsWith("\\"))
                        srcFolder += "\\";

                    if (!RESTClient.GZip.ZipFolder(srcFolder, zipFile))
                    {
                        if (resultMessage != null)
                            resultMessage.Append("文件压缩失败");
                        return false;
                    }
                }
                else
                {
                    zipFile = LocalFullPath;
                }

                List<UploadFile> upFiles = new List<UploadFile>();
                using (FileStream stream = File.Open(zipFile, FileMode.Open))
                {
                    String fileName = Path.GetFileName(zipFile);
                    String fileExt = Path.GetExtension(zipFile);
                    UploadFile file = new UploadFile
                    {
                        FullPath = zipFile,
                        Name = fileName,
                        Filename = fileName,
                        ContentType = MimeTypes.GetMimeTypeByExt(fileExt),
                        Stream = stream
                    };
                    upFiles.Add(file);
                }

                String otherPairs = "";
                if ((other != null) && (other.Count > 0))
                {
                    foreach (String key in other.Keys)
                        otherPairs += String.Format("&{0}={1}", key, other[key]);
                }

                String url = String.Format("http{0}://{1}:{2}/caxa/upload_files?isfolder={3}&serverrelpath={4}&localfullpath={5}&serverprocessor={6}{7}",
                                            bSSL ? "s" : "", host, port,
                                            UrlUtility.UrlEncode(IsFolder),
                                            UrlUtility.UrlEncode(ServerRelPath),
                                            UrlUtility.UrlEncode(LocalFullPath),
                                            UrlUtility.UrlEncode(ServerProcessor),
                                            otherPairs);
                DebugLog(LOG_INFO, "UploadFiles URL: {0}", url);

                if (UploadFile.UploadFiles(url, _SessionID, upFiles, null, resultMessage))
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                if (resultMessage != null)
                    resultMessage.Append(e.Message);
                DebugLog(LOG_FATAL, "UploadFiles failed: {0}", e.Message);
            }

            if ((resultMessage != null) && (resultMessage.Length <= 0))
                resultMessage.Append("文件压缩失败");
            return false;
        }

        public static Boolean DownloadFiles(WebRequestSession session, String IsFolder, String ServerRelPath, String LocalFullPath, String ServerProcessor)
        {
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String url = String.Format("/caxa/download_files?isfolder={0}&serverrelpath={1}&localfullpath={2}&serverprocessor={3}",
                                            UrlUtility.UrlEncode(IsFolder),
                                            UrlUtility.UrlEncode(ServerRelPath),
                                            UrlUtility.UrlEncode(LocalFullPath),
                                            UrlUtility.UrlEncode(ServerProcessor));
                DebugLog(LOG_INFO, "DownloadFiles URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String file = RESTGetAsFile(bSSL, host, port, url, errMessage);
                    if (!String.IsNullOrEmpty(file) && File.Exists(file))
                    {
                        if (file.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (Convert.ToInt32(IsFolder) > 0)
                            {
                                String extractTo = LocalFullPath.Replace("/", "\\");
                                if (!extractTo.EndsWith("\\"))
                                    extractTo += "\\";

                                if (!Directory.Exists(extractTo))
                                    Directory.CreateDirectory(extractTo);
                                GZip.UnZip(file, extractTo);
                            }
                        }
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "DownloadFiles failed: {0}", e.Message);
            }

            return false;
        }

        public static String GetTemplate(WebRequestSession session)
        {
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String url = String.Format("/caxa/get_files.xml?type=template");
                DebugLog(LOG_INFO, "GetTemplate URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String file = RESTGetAsFile(_SSL, _HOST, _PORT, url, errMessage);
                    if (!String.IsNullOrEmpty(file) && File.Exists(file))
                    {
                        return file;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "GetTemplate failed: {0}", e.Message);
            }

            return String.Empty;
        }

        public static Boolean OrderPartition(WebRequestSession session, String xmlOrders, String processor, Dictionary<String, String> other, StringBuilder taskIDorError)
        {
            return UploadFiles(session, "0", "", xmlOrders, processor, other, taskIDorError); //processor:"ProcessByOrderList" "ProcessByPartList"
        }

        public static Boolean OrderProgress(WebRequestSession session, String taskID, StringBuilder taskIDorError)
        {
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String url = String.Format("/caxa/order_progress?taskid={0}", taskID);
                DebugLog(LOG_INFO, "OrderProgress URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String txtResult = RESTGetAsString(bSSL, host, port, url, errMessage);
                    if (errMessage.Length <= 0)
                    {
						if ((txtResult != null) && (taskIDorError != null))
                        	taskIDorError.Append(txtResult);
                       	return true;
                    }
                    else
                    {
                        DebugLog(LOG_INFO, "OrderProgress No File");
                    }
                }
                catch (Exception e)
                {
                    if (taskIDorError != null)
                        taskIDorError.Append(e.Message);

                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                if (taskIDorError != null)
                    taskIDorError.Append(e.Message);

                DebugLog(LOG_FATAL, "OrderProgress failed: {0}", e.Message);
            }

            if ((taskIDorError != null) && (taskIDorError.Length <= 0))
                taskIDorError.Append("进度错误");

            return false;
        }

        public static Boolean OrderRemove(WebRequestSession session, String taskID, StringBuilder taskIDorError)
        {
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String url = String.Format("/caxa/order_remove?taskid={0}", taskID);
                DebugLog(LOG_INFO, "OrderRemove URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String txtResult = RESTGetAsString(bSSL, host, port, url, errMessage);
                    if (errMessage.Length <= 0)//!String.IsNullOrEmpty(file) && File.Exists(file))
                    {
						if ((txtResult != null) && (taskIDorError != null))
                            taskIDorError.Append(txtResult);//File.ReadAllText(file));
                        return true;
                    }
                }
                catch (Exception e)
                {
                    if (taskIDorError != null)
                        taskIDorError.Append(e.Message);

                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                if (taskIDorError != null)
                    taskIDorError.Append(e.Message);

                DebugLog(LOG_FATAL, "OrderRemove failed: {0}", e.Message);
            }

            if ((taskIDorError != null) && (taskIDorError.Length <= 0))
                taskIDorError.Append("删除错误");

            return false;
        }

        public static Boolean OrderResult(WebRequestSession session, String taskID, StringBuilder resultFile)
        {
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String url = String.Format("/caxa/order_result?taskid={0}", taskID);
                DebugLog(LOG_INFO, "OrderResult URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String file = RESTGetAsFile(bSSL, host, port, url, errMessage);
                    if (!String.IsNullOrEmpty(file) && File.Exists(file))
                    {
                        if (resultFile != null)
                            resultFile.Append(file);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "OrderResult failed: {0}", e.Message);
            }

            return false;
        }

        public static DataTable GetAssemblyLines(WebRequestSession session)
        {
            DataTable dt = null;
            try
            {
                if (!session.IsSigned())
                {
                    throw new Exception("Session is not signed");
                    return null;
                }

                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String path = String.Format("/{0}/get_assembly_list.json", API_PREFIX);
                StringBuilder errMessage = new StringBuilder();
                byte[] queryResult = RemoteCall.RESTGetAsBytes(bSSL, host, port, path, errMessage);
                if ((queryResult != null) && (errMessage.Length <= 0))//File.Exists(resultFile))
                {
                    //byte[] queryResult = File.ReadAllBytes(resultFile);
                    dt = DbComm.DecompressHex2DataTable(queryResult);
                }
                else
                {
                    if (errMessage.Length > 0)
                    {
                        throw new Exception(errMessage.ToString());
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            return dt;
        }

        public static Boolean PostDataSet(WebRequestSession session, DataSet ds, Dictionary<String, String> queryParam)
        {
            try
            {
                if (!session.IsSigned())
                {
                    throw new Exception("Session is not signed");
                }

                if (ds == null)
                    return false;

                MemoryStream stream = new MemoryStream();
                if (!DbComm.Serialize(ds, stream))
                    return false;

                byte[] dsBytes = stream.ToArray();
                String dsFile = DbComm.GetTempFileName(".dat");
                if (String.IsNullOrEmpty(dsFile))
                    return false;

                File.WriteAllBytes(dsFile, dsBytes);

                String qString = "";
                if (queryParam != null)
                {
                    foreach (String key in queryParam.Keys)
                    {
                        if (!String.IsNullOrEmpty(qString))
                            qString += "&";
                        qString += String.Format("{0}={1}", key, UrlUtility.UrlEncode(queryParam[key]));
                    }
                }

                String path = String.Format("/{0}/post_dataset?{1}", API_PREFIX, qString);

                String[] files = new string[1];
                files[0] = dsFile;
                String result = RESTUpload(session.ssl, session.host, session.port, path, files, null);
                return !String.IsNullOrEmpty(result);
            }
            catch (Exception e)
            { }

            return false;
        }

        public static int GetNextID(WebRequestSession session, String idName)
        {
            try
            {
                DataTable dt = RemoteCall.GetDataTable(session, "nextid", idName);
                if ((dt != null) && (dt.Rows.Count >= 0))
                { //SQL 执行成功
                    return Convert.ToInt32(dt.Rows[0][1]);
                }
            }
            catch (Exception e)
            {}
            return -1;
        }

        public static int GetNextIDByPrefix(WebRequestSession session, String idPrefixName)
        {
            try
            {
                DataTable dt = RemoteCall.GetDataTable(session, "nextid_byprefix", idPrefixName);
                if ((dt != null) && (dt.Rows.Count >= 0))
                { //SQL 执行成功
                    return Convert.ToInt32(dt.Rows[0][1]);
                }
            }
            catch (Exception e)
            { }
            return -1;
        }

        public static String RESTPost(WebRequestSession session, String path, Dictionary<String, String> postData, CancellationTokenSource cts = null)
        {
            Boolean bSSL = session.ssl;
            String host = session.host;
            int port = session.port;
            String user = session.user;
            String pass = session.pass;
            String language = session.language;

            String url = String.Format("http{0}://{1}:{2}{3}", bSSL ? "s" : "", host, port, path);
            DebugLog(LOG_INFO, "Post URL: {0}", url);

            HttpClient client = null;
            HttpResponseMessage response = null;

            try
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    UseCookies = true,
                    CookieContainer = new CookieContainer()
                };

                client = new HttpClient(handler);
                //client.DefaultRequestHeaders.Add("Connection", "close");
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                if (!String.IsNullOrEmpty(_SessionID))
                {
                    Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(url));
                    handler.CookieContainer.Add(uri, new Cookie("SESSIONID", _SessionID));
                }

                var keyValues = new List<KeyValuePair<string, string>>();
                foreach (String key in postData.Keys)
                    keyValues.Add(new KeyValuePair<string, string>(key, postData[key]));
                FormUrlEncodedContent toPost = new FormUrlEncodedContent(keyValues);

                if (cts != null)
                    response = client.PostAsync(url, toPost, cts.Token).Result;
                else
                    response = client.PostAsync(url, toPost).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    if (content != null)
                    {
                        String result = content.ReadAsStringAsync().Result;
                        if (result != null && result.Length > 0)
                        {
                            DebugLog(LOG_INFO, "Post successfully, {0}", result);
                            return result;
                        }
                    }
                }
                else
                {
                    DebugLog(LOG_FATAL, "Http status code: {0}", response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "RESTPost failed: {0}", e.Message);
            }

            finally
            {
                if (response != null)
                    response.Dispose();
                if (client != null)
                    client.Dispose();
            }

            DebugLog(LOG_FATAL, "Failed to login, user: {0}", user);
            return null;
        }

        public static Boolean GetSheetList(WebRequestSession session, String package_codeid, StringBuilder resultFile)
        {
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String url = String.Format("/caxa/get_sheet_list.xml?package_codeid={0}", package_codeid);
                DebugLog(LOG_INFO, "GetSheetList URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String file = RESTGetAsFile(bSSL, host, port, url, errMessage);
                    if (!String.IsNullOrEmpty(file) && File.Exists(file))
                    {
                        if (resultFile != null)
                            resultFile.Append(file);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "GetSheetList failed: {0}", e.Message);
            }

            return false;
        }

        public static Boolean UpdateSheetStatus(WebRequestSession session, String sheet_codeid, String status)
        {
            try
            {
                String url = String.Format("/caxa/update_sheet_status.json");

                Dictionary<String, String> postData = new Dictionary<string, string>();
                postData["sheet_codeid"] = sheet_codeid;
                postData["status"] = status;

                DebugLog(LOG_INFO, "UpdateSheetStatus URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String result = RESTPost(session, url, postData);
                    if (!String.IsNullOrEmpty(result))
                    {
                        //DebugLog(LOG_INFO, "UpdateSheetStatus Success");
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "UpdateSheetStatus failed: {0}", e.Message);
            }
            return false;
        }

        public static Boolean UpdatePartStatus(WebRequestSession session, String part_codeid, String part_status)
        {
            try
            {
                String url = String.Format("/caxa/update_part_status.json");

                Dictionary<String, String> postData = new Dictionary<string, string>();
                postData["part_codeid"] = part_codeid;
                postData["part_status"] = part_status;

                DebugLog(LOG_INFO, "UpdatePartStatus URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String result = RESTPost(session, url, postData);
                    if (!String.IsNullOrEmpty(result))
                    {
                        //DebugLog(LOG_INFO, "UpdatePartStatus Success");
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "UpdatePartStatus failed: {0}", e.Message);
            }
            return false;
        }

        public static Boolean GetOrderFile(WebRequestSession session, int order_id, int item_id, StringBuilder orderFile)
        {//"/caxa/get_order_file"
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String url = String.Format("/caxa/get_order_file?order_id={0}&item_id={1}", order_id, item_id);
                DebugLog(LOG_INFO, "GetOrderFile URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String file = RESTGetAsFile(bSSL, host, port, url, errMessage);
                    if (!String.IsNullOrEmpty(file) && File.Exists(file))
                    {
                        if (orderFile != null)
                            orderFile.Append(file);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "GetOrderFile failed: {0}", e.Message);
            }

            return false;
        }

        public static Boolean OrderDataset(WebRequestSession session, String taskID, StringBuilder resultFile)
        {
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;

                String url = String.Format("/caxa/order_dataset?taskid={0}", taskID);
                DebugLog(LOG_INFO, "OrderDataset URL: {0}", url);

                try
                {
                    StringBuilder errMessage = new StringBuilder();
                    String file = RESTGetAsFile(bSSL, host, port, url, errMessage);
                    if (!String.IsNullOrEmpty(file) && File.Exists(file))
                    {
                        if (resultFile != null)
                            resultFile.Append(file);
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                DebugLog(LOG_FATAL, "OrderDataset failed: {0}", e.Message);
            }

            return false;
        }

        public static string PostMultipartRequest(WebRequestSession session, string relUrl, NameValueCollection values, NameValueCollection files = null)
        {
            string msg = "";
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;
                String url = relUrl.Replace("\\", "/");

                if (!url.StartsWith("http"))
                {
                    if (!url.StartsWith("/"))
                        url = "/" + url;
                    url = String.Format("http{0}://{1}:{2}{3}", bSSL ? "s" : "", host, port, url);
                }

                string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
                // The first boundary
                byte[] boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                // The last boundary
                byte[] trailer = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                // The first time it itereates, we need to make sure it doesn't put too many new paragraphs down or it completely messes up poor webbrick
                byte[] boundaryBytesF = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

                // Create the request and set parameters
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (request.CookieContainer == null)
                    request.CookieContainer = new CookieContainer();

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                if (!String.IsNullOrEmpty(_SessionID))
                {
                    Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(url));
                    request.CookieContainer.Add(uri, new Cookie("SESSIONID", _SessionID));
                }

                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = System.Net.CredentialCache.DefaultCredentials;

                // Get request stream
                Stream requestStream = request.GetRequestStream();

                foreach (string key in values.Keys)
                {
                    // Write item to stream
                    byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}", key, values[key]));
                    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    requestStream.Write(formItemBytes, 0, formItemBytes.Length);
                }

                if (files != null)
                {
                    foreach (string key in files.Keys)
                    {
                        if (File.Exists(files[key]))
                        {
                            int bytesRead = 0;
                            byte[] buffer = new byte[2048];
                            byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", key, files[key]));
                            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                            requestStream.Write(formItemBytes, 0, formItemBytes.Length);

                            using (FileStream fileStream = new FileStream(files[key], FileMode.Open, FileAccess.Read))
                            {
                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    // Write file content to stream, byte by byte
                                    requestStream.Write(buffer, 0, bytesRead);
                                }

                                fileStream.Close();
                            }
                        }
                    }
                }

                // Write trailer and close stream
                requestStream.Write(trailer, 0, trailer.Length);
                requestStream.Close();

                using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    return reader.ReadToEnd();
                };
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            JObject error = new JObject(
                new JProperty("result_code", -1),
                new JProperty("message", msg)
            );

            return error.ToString();
        }

        public static string GetJsonData(WebRequestSession session, string relUrl, string filter)
        {
            string msg = "";
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;
                String url = relUrl.Replace("\\", "/");

                if (!url.StartsWith("http"))
                {
                    if (!url.StartsWith("/"))
                        url = "/" + url;
                    url = String.Format("http{0}://{1}:{2}{3}", bSSL ? "s" : "", host, port, url);
                }

                if (String.IsNullOrEmpty(filter))
                    url += "?" + filter;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (request.CookieContainer == null)
                    request.CookieContainer = new CookieContainer();

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                if (!String.IsNullOrEmpty(_SessionID))
                {
                    Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(url));
                    request.CookieContainer.Add(uri, new Cookie("SESSIONID", _SessionID));
                }

                request.Method = "GET";
                request.KeepAlive = true;
                request.Timeout = 10000;
                request.Credentials = System.Net.CredentialCache.DefaultCredentials;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                };
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            JObject error = new JObject(
                new JProperty("result_code", -1),
                new JProperty("message", msg)
            );

            return error.ToString();
        }

        public static string PostJObject(WebRequestSession session, string relUrl, JObject jObject)
        {
            string msg = "";
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;
                String url = relUrl.Replace("\\", "/");

                if (!url.StartsWith("http"))
                {
                    if (!url.StartsWith("/"))
                        url = "/" + url;
                    url = String.Format("http{0}://{1}:{2}{3}", bSSL ? "s" : "", host, port, url);
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                if (request.CookieContainer == null)
                    request.CookieContainer = new CookieContainer();

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                if (!String.IsNullOrEmpty(_SessionID))
                {
                    Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(url));
                    request.CookieContainer.Add(uri, new Cookie("SESSIONID", _SessionID));
                }

                request.ContentType = "application/json; charset=utf-8";
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = System.Net.CredentialCache.DefaultCredentials;

                Stream requestStream = request.GetRequestStream();
                byte[] jBytes = System.Text.Encoding.UTF8.GetBytes(jObject.ToString());
                requestStream.Write(jBytes, 0, jBytes.Length);
                requestStream.Close();

                using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    return reader.ReadToEnd();
                };
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            JObject error = new JObject(
                new JProperty("result_code", -1),
                new JProperty("message", msg)
            );

            return error.ToString();
        }

        public static string GetCadmModelData(WebRequestSession session, string relUrl, String modelPath)
        {
            String msg = "";
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;
                String user = session.user;
                String pass = session.pass;
                String language = session.language;
                String url = relUrl.Replace("\\", "/");

                if (!String.IsNullOrEmpty(modelPath))
                {
                    url += "?model=" + modelPath;
                }

                StringBuilder errMessage = new StringBuilder();
                String file = RESTGetAsFile(bSSL, host, port, url, errMessage);
                if (!String.IsNullOrEmpty(file) && File.Exists(file))
                {
                    if (file.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        string LocalFullPath = Util.Util.GetTempFileName(".zip");

                        String extractTo = LocalFullPath.Replace("/", "\\");
                        if (!extractTo.EndsWith("\\"))
                            extractTo += "\\";

                        if (!Directory.Exists(extractTo))
                            Directory.CreateDirectory(extractTo);
                        GZip.UnZip(file, extractTo);

                        JObject success = new JObject(
                            new JProperty("result_code", +1),
                            new JProperty("folder", extractTo),
                            new JProperty("message", "success"));

                        return success.ToString();
                    }
                }
                else
                {
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            JObject error = new JObject(
                new JProperty("result_code", -1),
                new JProperty("message", msg)
            );

            return error.ToString();
        }
    }
}
