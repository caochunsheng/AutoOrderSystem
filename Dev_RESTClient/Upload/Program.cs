using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RESTClient;
using System.Windows.Forms;
using Util;
using System.IO;

namespace Upload
{
    class Program
    {
        private static WebRequestSession _reqSession;

        static void Main(string[] args)
        { //upload.exe /ServerRelPath:"drawLibs"  /LocalFullPath:“C:\SmartHomeDesign_x64\2.0\drawLibs\Config.ini” /ServerProcessor:“Raw Copy”
            Dictionary<String, String> arguments = WebUtil.getArguments(args);

            _reqSession = new WebRequestSession();
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
                        MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
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
                MessageBox.Show("本地路径无效", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            StringBuilder filePath = new StringBuilder();
            Boolean result = RemoteCall.UploadFiles(_reqSession, IsFolder, ServerRelPath, LocalFullPath, ServerProcessor, null);
            _reqSession.Logout();

            if (result)
            {
                MessageBox.Show("上载成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("上载失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

    }
}
