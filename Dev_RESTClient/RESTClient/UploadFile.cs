//Created by HEJS, 2016-05-21

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Net;
using System.Globalization;
using System.Diagnostics;
using RESTClient;
using System.Net.Http;

namespace Upload
{
    public class UploadFile
    {
        public UploadFile()
        {
            ContentType = "application/octet-stream";
        }

        public string FullPath { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public Stream Stream { get; set; }

        public static Boolean PostFiles(string address, string sessionid, List<UploadFile> files, NameValueCollection values)
        {
            try
            {
                //Uri uri = new Uri((address));
                var cookieContainer = new CookieContainer();
                if (!String.IsNullOrEmpty(sessionid))
                {
                    Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(address));

                    cookieContainer.Add(uri, new Cookie("SESSIONID", sessionid));
                }

                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
                {
                    using (var client = new HttpClient(handler))
                    {
                        var parts = new MultipartFormDataContent();
                        for (int i = 0; i < files.Count; i++)
                        {
                            parts.Add(new StreamContent(File.Open(files[i].FullPath, FileMode.Open)), files[i].Filename, files[i].Filename);
                        }

                        var result = client.PostAsync(address, parts);
                        if (result != null)
                        {
                            HttpResponseMessage response = result.Result;

                            Console.WriteLine(result.Result.ToString());
                            return (response.StatusCode == HttpStatusCode.OK);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return false;
        }

        public static Boolean UploadFiles(string address, string sessionid, List<UploadFile> files, NameValueCollection values, StringBuilder resultMessage)
        {
            try
            {
                //Uri uri = new Uri((address));
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(address);

                request.CookieContainer = new CookieContainer();
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                if (!String.IsNullOrEmpty(sessionid))
                {
                    Uri uri = new Uri(UrlUtility.GetUrlWithoutQuery(address));
                    request.CookieContainer.Add(uri, new Cookie("SESSIONID", sessionid));
                }

                request.Method = "POST";
                var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                boundary = "--" + boundary;

                using (var bodyStream = request.GetRequestStream())
                {
                    // Write the values
                    if (values != null)
                    {
                        foreach (string name in values.Keys)
                        {
                            var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                            bodyStream.Write(buffer, 0, buffer.Length);
                            buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                            bodyStream.Write(buffer, 0, buffer.Length);
                            buffer = Encoding.ASCII.GetBytes(values[name] + Environment.NewLine);
                            bodyStream.Write(buffer, 0, buffer.Length);
                        }
                    }

                    // Write the files
                    if (files != null)
                    {
                        foreach (UploadFile file in files)
                        {
                            var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                            bodyStream.Write(buffer, 0, buffer.Length);

                            buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", 
                                    UrlUtility.UrlEncode(file.Name), 
                                    UrlUtility.UrlEncode(file.Filename), Environment.NewLine));
                            bodyStream.Write(buffer, 0, buffer.Length);

                            buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
                            bodyStream.Write(buffer, 0, buffer.Length);

                            buffer = File.ReadAllBytes(file.FullPath);
                            bodyStream.Write(buffer, 0, buffer.Length);

                            //using (FileStream fileStream = File.Open(file.FullPath, FileMode.Open))
                            //{
                                //fileStream.CopyTo(requestStream);
                            //}

                            buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                            bodyStream.Write(buffer, 0, buffer.Length);
                        }
                    }

                    var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                    bodyStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response != null)
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (resultMessage != null)
                        {
                            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                            StreamReader readStream = new StreamReader(responseStream, encode);
                            char[] read = new char[2048];
                            int count = readStream.Read(read, 0, read.Length);
                            while (count > 0)
                            {
                                String str = new String(read, 0, count);
                                resultMessage.Append(str);
                                count = readStream.Read(read, 0, read.Length);
                            }
                        }
                    }
                }

                if ((resultMessage != null) && (resultMessage.Length <= 0))
                {
                    resultMessage.Append("无任务编号");
                    return false;
                }

                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                if (resultMessage != null)
                    resultMessage.Append(e.Message);

                Debug.WriteLine(e.Message);
            }

            if ((resultMessage != null) && (resultMessage.Length <= 0))
                resultMessage.Append("上载失败");

            return false;
        }

        private void Test()
        {
            using (var stream1 = File.Open("test.txt", FileMode.Open))
            using (var stream2 = File.Open("test.xml", FileMode.Open))
            using (var stream3 = File.Open("test.pdf", FileMode.Open))
            {
                List<UploadFile> files = new List<UploadFile> 
                {
                    new UploadFile
                    {
                        Name = "file",
                        Filename = "test.txt",
                        ContentType = "text/plain",
                        Stream = stream1
                    },
                    new UploadFile
                    {
                        Name = "file",
                        Filename = "test.xml",
                        ContentType = "text/xml",
                        Stream = stream2
                    },
                    new UploadFile
                    {
                        Name = "file",
                        Filename = "test.pdf",
                        ContentType = "application/pdf",
                        Stream = stream3
                    }
                };

                var values = new NameValueCollection
                {
                    { "key1", "value1" },
                    { "key2", "value2" },
                    { "key3", "value3" },
                };

                Boolean result = UploadFiles("http://localhost:1234/upload", "sid", files, values, null);
            }
        }
    }
}
