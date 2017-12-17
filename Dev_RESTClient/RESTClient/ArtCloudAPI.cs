using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using CuttingStock;
using Upload;
using Util;
using System.IO;

namespace RESTClient
{
    [Guid("16607626-9E50-42A4-A0D1-ABC07C342ADC")]
    [ComVisible(true)]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IArtCloud
    {
        [DispId(1)]
        String CuttingStock( [In, MarshalAs(UnmanagedType.BStr)] string type,
                              [In, MarshalAs(UnmanagedType.BStr)] string xmlFile);

        [DispId(2)]
        String Upload([In, MarshalAs(UnmanagedType.BStr)] string srvRelLibFolder,
                              [In, MarshalAs(UnmanagedType.BStr)] string clnFullPath,
                              [In, MarshalAs(UnmanagedType.BStr)] string srvProcessor);

        [DispId(3)]
        String Download([In, MarshalAs(UnmanagedType.BStr)] string srvRelLibFolder,
                              [In, MarshalAs(UnmanagedType.BStr)] string clnFullPath,
                              [In, MarshalAs(UnmanagedType.BStr)] string srvProcessor);
    }

    [Guid("DA5176F0-02AC-4AD8-85B9-DD765BED99D5")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("RESTClient.ArtCloudClass")]
    public class ArtCloudClass : IArtCloud
    {
        public String CuttingStock([In, MarshalAs(UnmanagedType.BStr)] string type, 
                                    [In, MarshalAs(UnmanagedType.BStr)] string xmlFile)
        {
            String[] args = new String[2];
            args[0] = type;
            args[1] = xmlFile;

            String errMsg = "";
            StringBuilder error = new StringBuilder();
            int nResult = CuttingAPI.SawCuttingStock(args, error);
            if ((nResult < 0) || (error.Length > 0))
            {
                Debug.WriteLine("Usage: CuttingStock saw <NestingData.xml>");
                errMsg = error.ToString();
                return errMsg;
            }
            return "";
        }

        public String Upload([In, MarshalAs(UnmanagedType.BStr)] string srvRelLibFolder, 
                                [In, MarshalAs(UnmanagedType.BStr)] string clnFullPath,
                                [In, MarshalAs(UnmanagedType.BStr)] string srvProcessor)
        { //upload.exe /ServerRelPath:"drawLibs"  /LocalFullPath:“C:\SmartHomeDesign_x64\2.0\drawLibs\Config.ini” /ServerProcessor:“Raw Copy”
            String errMsg = "";

            String[] args = new String[3];
            args[0] = srvRelLibFolder;
            args[1] = clnFullPath;
            args[2] = srvProcessor;

            Dictionary<String, String> arguments = WebUtil.getArguments(args);
            WebRequestSession _reqSession = new WebRequestSession();

            XmlConfig.getInstance().LoadConfig("ServerInfo.xml");

            Boolean ssl = (Convert.ToInt32(XmlConfig.getInstance().GetParam("server", "ssl", "1")) > 0);
            String textBoxServerIP = XmlConfig.getInstance().GetParam("server", "host", "localhost");// "localhost";
            String textBoxPort = XmlConfig.getInstance().GetParam("server", "port", "9443"); //"9443";
            String textBoxUser = XmlConfig.getInstance().GetParam("server", "user", "admin"); //"admin";
            String textBoxPass = XmlConfig.getInstance().GetParam("server", "pass", ""); //"";

            if (!_reqSession.Login(ssl, textBoxServerIP, Convert.ToInt32(textBoxPort), textBoxUser, textBoxPass, String.Empty, "此处放机器ID"))
            {
                Login login = new Login();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    ssl = (Convert.ToInt32(XmlConfig.getInstance().GetParam("server", "ssl", "1")) > 0);
                    textBoxServerIP = XmlConfig.getInstance().GetParam("server", "host", "localhost");// "localhost";
                    textBoxPort = XmlConfig.getInstance().GetParam("server", "port", "9443"); //"9443";
                    textBoxUser = XmlConfig.getInstance().GetParam("server", "user", "admin"); //"admin";
                    textBoxPass = XmlConfig.getInstance().GetParam("server", "pass", ""); //"";

                    if (!_reqSession.Login(ssl, textBoxServerIP, Convert.ToInt32(textBoxPort), textBoxUser, textBoxPass, String.Empty, "此处放机器ID"))
                    {
                        errMsg = "客户端登录失败";
                        //MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return errMsg;
                    }
                }
                else
                {
                    errMsg = "客户端登录失败";
                    //MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return errMsg;
                }
            }

            String IsFolder = "1";
            String ServerRelPath = "";
            String LocalFullPath = "";
            String ServerProcessor = "";

            if (arguments.ContainsKey("ServerRelPath".ToLower()))
                ServerRelPath = arguments["ServerRelPath".ToLower()].Trim();
            if (arguments.ContainsKey("LocalFullPath".ToLower()))
                LocalFullPath = arguments["LocalFullPath".ToLower()].Trim();
            if (arguments.ContainsKey("ServerProcessor".ToLower()))
                ServerProcessor = arguments["ServerProcessor".ToLower()].Trim();
            if (arguments.ContainsKey("IsFolder".ToLower()))
                IsFolder = arguments["IsFolder".ToLower()].Trim();

            if (!String.IsNullOrEmpty(LocalFullPath) && File.Exists(LocalFullPath))
            { //file
                IsFolder = "0";
            }
            else if (!Directory.Exists(LocalFullPath))
            { //directory
                _reqSession.Logout();

                errMsg = "本地路径无效";
                //MessageBox.Show("本地路径无效", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return errMsg;
            }

            StringBuilder filePath = new StringBuilder();
            Boolean result = RemoteCall.UploadFiles(_reqSession, IsFolder, ServerRelPath, LocalFullPath, ServerProcessor, null);
            _reqSession.Logout();

            if (result)
            {
                //MessageBox.Show("上载成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return "";
            }

            errMsg = "上载失败";
            //MessageBox.Show("上载失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            return errMsg;
        }

        public String Download([In, MarshalAs(UnmanagedType.BStr)] string srvRelLibFolder, 
                                [In, MarshalAs(UnmanagedType.BStr)] string clnFullPath,
                                [In, MarshalAs(UnmanagedType.BStr)] string srvProcessor)
        { //upload.exe /ServerRelPath:"drawLibs"  /LocalFullPath:“C:\SmartHomeDesign_x64\2.0\drawLibs\Config.ini” /ServerProcessor:“Raw Copy”
            String errMsg = "";

            String[] args = new String[3];
            args[0] = srvRelLibFolder;
            args[1] = clnFullPath;
            args[2] = srvProcessor;

            Dictionary<String, String> arguments = WebUtil.getArguments(args);
            WebRequestSession _reqSession = new WebRequestSession();

            XmlConfig.getInstance().LoadConfig("ServerInfo.xml");

            Boolean ssl = (Convert.ToInt32(XmlConfig.getInstance().GetParam("server", "ssl", "1")) > 0);
            String textBoxServerIP = XmlConfig.getInstance().GetParam("server", "host", "localhost");// "localhost";
            String textBoxPort = XmlConfig.getInstance().GetParam("server", "port", "9443"); //"9443";
            String textBoxUser = XmlConfig.getInstance().GetParam("server", "user", "admin"); //"admin";
            String textBoxPass = XmlConfig.getInstance().GetParam("server", "pass", ""); //"";

            if (!_reqSession.Login(ssl, textBoxServerIP, Convert.ToInt32(textBoxPort), textBoxUser, textBoxPass, String.Empty, "此处放机器ID"))
            {
                Login login = new Login();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    ssl = (Convert.ToInt32(XmlConfig.getInstance().GetParam("server", "ssl", "1")) > 0);
                    textBoxServerIP = XmlConfig.getInstance().GetParam("server", "host", "localhost");// "localhost";
                    textBoxPort = XmlConfig.getInstance().GetParam("server", "port", "9443"); //"9443";
                    textBoxUser = XmlConfig.getInstance().GetParam("server", "user", "admin"); //"admin";
                    textBoxPass = XmlConfig.getInstance().GetParam("server", "pass", ""); //"";

                    if (!_reqSession.Login(ssl, textBoxServerIP, Convert.ToInt32(textBoxPort), textBoxUser, textBoxPass, String.Empty, "此处放机器ID"))
                    {
                        errMsg = "客户端登录失败";
                        //MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return errMsg;
                    }
                }
                else
                {
                    errMsg = "客户端登录失败";
                    //MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return errMsg;
                }
            }

            String IsFolder = "1";
            String ServerRelPath = "";
            String LocalFullPath = "";
            String ServerProcessor = "";

            if (arguments.ContainsKey("ServerRelPath".ToLower()))
                ServerRelPath = arguments["ServerRelPath".ToLower()].Trim();
            if (arguments.ContainsKey("LocalFullPath".ToLower()))
                LocalFullPath = arguments["LocalFullPath".ToLower()].Trim();
            if (arguments.ContainsKey("ServerProcessor".ToLower()))
                ServerProcessor = arguments["ServerProcessor".ToLower()].Trim();
            if (arguments.ContainsKey("IsFolder".ToLower()))
                IsFolder = arguments["IsFolder".ToLower()].Trim();

            StringBuilder filePath = new StringBuilder();
            Boolean result = RemoteCall.DownloadFiles(_reqSession, IsFolder, ServerRelPath, LocalFullPath, ServerProcessor);
            //String template = RemoteCall.GetTemplate(_reqSession);

            _reqSession.Logout(false);
            if (result)//!String.IsNullOrEmpty(template))
            {
                //MessageBox.Show("下载成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return "";
            }

            errMsg = "下载失败";
            //MessageBox.Show("下载失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            return errMsg;
        }
    }
}
