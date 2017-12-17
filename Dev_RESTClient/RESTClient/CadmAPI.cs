using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;

namespace RESTClient
{
    public class CadmAPI
    {
        public const String TASK_PATHIMPORT = "PathImport";
        public const String TASK_BINPACKING = "BinPacking";

        public static int TaskSubmit(WebRequestSession session, String taskType, String taskName, Dictionary<String, String> files2Post)
        {
            string url = "/caxa/task_submit";
            try
            {
                NameValueCollection valuePairs = new NameValueCollection();
                valuePairs.Add("task_type", taskType);
                valuePairs.Add("task_name", taskName);

                NameValueCollection files = new NameValueCollection();
                foreach (KeyValuePair<String, String> filePath in files2Post)
                    files.Add(filePath.Key, filePath.Value); //path, file_id

                string response = RemoteCall.PostMultipartRequest(session, url, valuePairs, files);
                JObject jResult = JObject.Parse(response);
                if ((int)jResult["result_code"] > 0)
                {
                    if (!String.IsNullOrEmpty("" + jResult["task_id"]))
                    {
                        int taskId = Convert.ToInt32(jResult["task_id"]);
                        return taskId;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return -1;
        }

        public static int TaskProgress(WebRequestSession session, String taskID, StringBuilder taskError)
        {
            string url = "/caxa/task_progress";

            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;

                String urlReq = String.Format("{0}?taskid={1}", url, taskID);

                int nProgress = -1;
                String taskStatus = "";

                StringBuilder errMessage = new StringBuilder();
                String txtResult = RemoteCall.RESTGetAsString(bSSL, host, port, urlReq, errMessage);
                if (txtResult != null)
                {
                    JObject jResult = JObject.Parse(txtResult);

                    if ((int)jResult["result_code"] > 0)
                    {
                        if (!String.IsNullOrEmpty("" + jResult["task_id"]))
                        {
                            if (!String.IsNullOrEmpty("" + jResult["task_progress"]))
                                nProgress = Convert.ToInt32(jResult["task_progress"]);
                            if (!String.IsNullOrEmpty("" + jResult["task_status"]))
                            {
                                taskStatus = Convert.ToString(jResult["task_status"]);
                                if (taskError != null)
                                    taskError.Append(taskStatus);
                            }
                        }
                    }
                    else
                    {
                        if (errMessage.Length > 0)
                        {
                            if (taskError != null)
                                taskError.Append(errMessage);
                        }
                    }
                }
                return nProgress;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return -1;
        }

        public static int TaskCancel(WebRequestSession session, String taskID, StringBuilder taskError)
        {
            string url = "/caxa/task_cancel";

            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;

                JObject obj2Post = new JObject(); 
                obj2Post["taskid"] = taskID;

                StringBuilder errMessage = new StringBuilder();
                string txtResult = RemoteCall.PostJObject(session, url, obj2Post);
                if (txtResult != null)
                {
                    JObject jResult = JObject.Parse(txtResult);

                    if ((int)jResult["result_code"] > 0)
                    {
                        if (jResult["message"] != null)
                        {
                            if (taskError != null)
                                taskError.Append(txtResult);
                        }
                        return 1;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                if (taskError != null)
                    taskError.Append(e.Message);
            }
            return -1;
        }

        public static int TaskResult(WebRequestSession session, String taskID, StringBuilder taskError)
        {
            string relUrl = "/caxa/task_result?taskid=" + taskID;

            String msg = "";
            try
            {
                Boolean bSSL = session.ssl;
                String host = session.host;
                int port = session.port;

                String url = relUrl.Replace("\\", "/");

                StringBuilder errMessage = new StringBuilder();
                String file = RemoteCall.RESTGetAsFile(bSSL, host, port, url, errMessage);
                if (!String.IsNullOrEmpty(file) && File.Exists(file))
                {
                    //if (file.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        string LocalFullPath = Util.Util.GetTempFileName("");

                        String extractTo = LocalFullPath.Replace("/", "\\");
                        if (!extractTo.EndsWith("\\"))
                            extractTo += "\\";


                        if (!Directory.Exists(extractTo))
                            Directory.CreateDirectory(extractTo);
                        GZip.UnZip(file, extractTo);

                        if (taskError != null)
                            taskError.Append(extractTo);

                        try
                        {
                            File.Delete(file);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                        }

                        return +1;
                    }
                }
                else
                {
                    msg = errMessage.ToString();
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            if (taskError != null)
                taskError.Append(msg);

            return -1;
        }
    }
}
