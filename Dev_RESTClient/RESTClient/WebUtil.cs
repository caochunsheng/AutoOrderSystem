//Created by HEJS, 2016-05-21

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Upload
{
    public class WebUtil
    {
        public static string GetTempFileName(string extension)
        {
            int attempt = 0;
            while (true)
            {
                string fileName = Path.GetRandomFileName();
                fileName = Path.ChangeExtension(fileName, extension);
                fileName = Path.Combine(Path.GetTempPath(), fileName);

                try
                {
                    //using (FileStream f = new FileStream(fileName, FileMode.CreateNew)) 
                    //{
                    //    f.Close();
                    //    File.Delete(fileName);
                    //}
                    //return fileName;

                    if (!File.Exists(fileName))
                        return fileName;
                }
                catch (Exception ex)
                {
                    if (++attempt == 20)
                        return "";
                    //throw new IOException("No unique temporary file name is available.", ex);
                }
            }
        }

        public static Dictionary<String, String> getArguments(string[] args)
        {
            Dictionary<String, String> arguments = new Dictionary<string, string>();

            String commandLine = "";
            for (int i = 0; i < args.Length; i++)
                commandLine += args[i] + " ";
            commandLine = commandLine.Trim(); //  /ServerRelPath:"drawLibs"  /LocalFullPath:“C:\SmartHomeDesign_x64\2.0\drawLibs\Config.ini” /ServerProcessor:“Raw Copy”

            String[] items = commandLine.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (String item in items)
            {
                try
                {
                    int idx = item.IndexOf(':');
                    if (idx > 0)
                    {
                        String key = item.Substring(0, idx).Trim().ToLower();
                        String value = item.Substring(idx + 1).Trim();

                        if (value.StartsWith("\"") || value.StartsWith("\'"))
                            value = value.Substring(1).Trim();

                        if (value.EndsWith("\"") || value.EndsWith("\'"))
                            value = value.Substring(0, value.Length - 1).Trim();

                        arguments[key] = value;
                    }
                }
                catch (Exception e)
                { }
            }
            return arguments;
        }

        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static Boolean IsPattern(string url, string filter)
        {
            string Path = "";
            string SessionID = "";
            string Anchor = "";
            string Query = "";
            ParseURL(url, ref Path, ref SessionID, ref Anchor, ref Query);

            string toFound = filter.Trim().ToLower();
            Boolean bStartAny = false;
            Boolean bEndAny = false;

            if (toFound.StartsWith("*"))
            {
                toFound = toFound.Substring(1);
                bStartAny = true;
            }

            if (toFound.EndsWith("*"))
            {
                toFound = toFound.Substring(0, toFound.Length-1);
                bEndAny = true;
            }

            toFound = toFound.Trim();
            Path = Path.ToLower().Trim();

            int idx = Path.IndexOf(toFound);
            if (idx < 0)
                return false;

            if (bStartAny && bEndAny)
                return true;
            if (bStartAny && !bEndAny)
                return Path.EndsWith(toFound);
            if (!bStartAny && bEndAny)
                return Path.StartsWith(toFound);

            return Path.Equals(toFound);
        }

        public static string CatPath(string root, string rel)
        {
            string local = root;
            if (local.EndsWith("/") || local.EndsWith("\\"))
                local = local.Substring(0, local.Length-1);
            if (!rel.StartsWith("/") && !rel.StartsWith("\\"))
                local += "\\";
            local += rel;

            return local.Replace("/", "\\");
        }

        public static string GetExtension(string url)
        {
            string Path = "";
            string SessionID = "";
            string Anchor = "";
            string Query = "";

            WebUtil.ParseURL(url, ref Path, ref SessionID, ref Anchor, ref Query);

            int idx = Path.LastIndexOf(".");
            if (idx >= 0)
                return Path.Substring(idx).ToLower();

            return String.Empty;
        }

        public static Dictionary<string, string> SplitPairs(string src, char[] separators, char[] assigners)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string[] items = src.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < items.Length; i++)
            {
                string[] pair = items[i].Split(assigners);
                if (pair.Length <= 0)
                    continue;

                string key = pair[0].Trim();
                if (String.IsNullOrEmpty(key))
                    continue;

                if (pair.Length > 1)
                    result[key] = pair[1].Trim();
                else
                    result[key] = String.Empty;
            }
            return result;
        }

        public static void ParseURL(string url, ref string Path, ref string SessionID, ref string Anchor, ref string Query)
        {
            int idx = 0;

            Path = url;// Uri.UnescapeDataString(url);
            SessionID = "";
            Anchor = "";
            Query = "";
            while (true)
            {
                idx = Path.LastIndexOfAny(new char[] { ';', '#', '?' });
                if (idx < 0)
                    break;
                if (Path[idx] == ';')
                    SessionID = Path.Substring(idx + 1);
                else if (Path[idx] == '#')
                    Anchor = Path.Substring(idx + 1);
                else if (Path[idx] == '?')
                    Query = Path.Substring(idx + 1);
                Path = url.Substring(0, idx);
            }

            Path = Path.Trim();
            SessionID = SessionID.Trim();
            Anchor = Anchor.Trim();
            Query = Query.Trim();
        }

        public static string GetRedirectContent(string schema, string host, string location)
        {
            string content = "";
            content += "<!DOCTYPE html>";
            content += "<html><head><meta charset=\"utf-8\"><title>Redirect</title><script>";
            content += String.Format("window.location.href=\"{0}://{1}{2}\";", schema, host, location);
            content += "</script></head><body></body></html>";
            return content;
        }
    }
}
