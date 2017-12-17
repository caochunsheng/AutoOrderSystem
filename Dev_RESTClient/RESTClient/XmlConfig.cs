using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Management;

namespace Util
{
    public class XmlConfig
    {
        public const string WORKING_FOLDER = "WebServer";

        private string _xmlFile;
        private object _lockXml;
        private XmlDocument _doc;

        private string _workingFolder = "";

        private static XmlConfig _Config = null;
        public static XmlConfig getInstance()
        {
            if (_Config == null)
                _Config = new XmlConfig();
            return _Config;
        }

        public XmlConfig()
        {
            //_workingFolder = Path.GetPathRoot(Environment.SystemDirectory);
            //_workingFolder += String.Format("ProgramData\\{0}\\", WORKING_FOLDER);
            _workingFolder = AppDomain.CurrentDomain.BaseDirectory;

            _xmlFile = "";
            _lockXml = new object();
            _doc = new XmlDocument();
        }

        public string GetWorkingFolder()
        {
            return _workingFolder;
        }

        public void LoadConfig(String strXml)
        {
            lock (_lockXml)
            {
                try
                {
                    _xmlFile = _workingFolder + strXml;
                    if (File.Exists(_xmlFile))
                    {
                        _doc.Load(_xmlFile);
                    }
                    else
                    {
                        string defaultXml = "";
                        defaultXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n";
                        defaultXml += "<Configuration>\r\n";
                        //defaultXml += "    <Velocity>\r\n";
                        //defaultXml += "        <Travel>200</Travel>\r\n";
                        //defaultXml += "        <Plunge>10</Plunge>\r\n";
                        //defaultXml += "        <Feed>1800</Feed>\r\n";
                        //defaultXml += "        <Spindle>15000</Spindle>\r\n";
                        //defaultXml += "    </Velocity>\r\n";
                        defaultXml += "</Configuration>\r\n";
                        _doc.LoadXml(defaultXml);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }
        }

        public string GetParam(string catalog, string key, string defaultValue) //"Velocity", "Travel"
        {
            lock (_lockXml)
            {
                string value = defaultValue;
                try
                {
                    string xpath = "Configuration/" + catalog + "/" + key;
                    XmlNode nodes = _doc.SelectSingleNode(xpath);
                    if (nodes != null)
                        value = nodes.InnerText;
                    else
                        SetParam(catalog, key, value);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
                return value;
            }
        }

        public void SetParam(string catalog, string key, string value)
        {
            lock (_lockXml)
            {
                try
                {
                    string xRootPath = "Configuration";
                    XmlNode nodeRoot = _doc.SelectSingleNode(xRootPath);
                    if (nodeRoot == null)
                        return;

                    string xCatalogPath = "Configuration/" + catalog;
                    XmlNode nodeCatalog = _doc.SelectSingleNode(xCatalogPath);
                    if (nodeCatalog == null)
                    {
                        XmlNode newCatalog = _doc.CreateNode(XmlNodeType.Element, catalog, null);
                        nodeRoot.AppendChild(newCatalog);

                        nodeCatalog = _doc.SelectSingleNode(xCatalogPath);
                    }
                    if (nodeCatalog == null)
                        return;

                    string xKeyPath = "Configuration/" + catalog + "/" + key;
                    XmlNode nodeKey = _doc.SelectSingleNode(xKeyPath);
                    if (nodeKey != null)
                        nodeKey.InnerText = value;
                    else
                    {
                        XmlNode newKey = _doc.CreateNode(XmlNodeType.Element, key, null);
                        newKey.InnerText = value;
                        nodeCatalog.AppendChild(newKey);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }
        }

        public void SaveConfig()
        {
            lock (_lockXml)
            {
                try
                {
                    _doc.Save(_xmlFile);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }
        }
    }

    public class Util
    {
        public static Dictionary<string, string> GetOsInfo()
        {
            Dictionary<String, String> osInfo = new Dictionary<string, string>();
            ManagementClass osClass = new ManagementClass("Win32_OperatingSystem");
            foreach (ManagementObject queryObj in osClass.GetInstances())
            {
                foreach (PropertyData prop in queryObj.Properties)
                {
                    if ((prop.Name == null) || (prop.Value == null))
                        continue;
                    osInfo[prop.Name] = prop.Value.ToString();
                }
            }
            return osInfo;
        }

        public static string GetTempFileName(string extension)
        {
            int attempt = 0;
            while (true)
            {
                string fileName = Path.GetRandomFileName();
                fileName = Path.ChangeExtension(fileName, extension);
                String tempDir = AppDomain.CurrentDomain.BaseDirectory + "Temp\\"; //Path.GetTempPath()
                if (!Directory.Exists(tempDir))
                    Directory.CreateDirectory(tempDir);
                fileName = Path.Combine(tempDir, fileName);
                if (fileName.EndsWith("."))
                    fileName = fileName.Substring(0, fileName.Length-1);

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

        public static void RecursiveDelete(string FolderName)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(FolderName);

                foreach (FileInfo fi in dir.GetFiles())
                {
                    fi.Delete();
                }

                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    RecursiveDelete(di.FullName);

                    di.Delete();
                }

                dir.Delete();
            }
            catch (Exception e)
            { }
        }

        public static List<DateTime> SortAscending(List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        public static List<DateTime> SortDescending(List<DateTime> list)
        {
            list.Sort((a, b) => b.CompareTo(a));
            return list;
        }

        public static List<DateTime> SortMonthAscending(List<DateTime> list)
        {
            list.Sort((a, b) => a.Month.CompareTo(b.Month));
            return list;
        }

        public static List<DateTime> SortMonthDescending(List<DateTime> list)
        {
            list.Sort((a, b) => b.Month.CompareTo(a.Month));
            return list;
        }
    }
}
