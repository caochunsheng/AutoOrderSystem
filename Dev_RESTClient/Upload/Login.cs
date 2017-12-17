using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RESTClient;
using Util;

namespace Upload
{
    public partial class Login : Form
    {
        private static WebRequestSession _reqSession;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            XmlConfig.getInstance().LoadConfig("ServerInfo.xml");

            this.checkBoxSSL.Checked = (Convert.ToInt32(XmlConfig.getInstance().GetParam("server", "ssl", "1")) > 0);
            this.textBoxServerIP.Text = XmlConfig.getInstance().GetParam("server", "host", "localhost");// "localhost";
            this.textBoxPort.Text = XmlConfig.getInstance().GetParam("server", "port", "9443"); //"9443";
            this.textBoxUser.Text = XmlConfig.getInstance().GetParam("server", "user", "admin"); //"admin";
            this.textBoxPass.Text = XmlConfig.getInstance().GetParam("server", "pass", ""); //"";

            _reqSession = new WebRequestSession();
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            if (_reqSession.Login(this.checkBoxSSL.Checked, this.textBoxServerIP.Text, Convert.ToInt32(this.textBoxPort.Text),
                this.textBoxUser.Text.Trim(), this.textBoxPass.Text.Trim(), String.Empty, "此处放机器ID"))
            {
                _reqSession.Logout();

                if (this.checkBoxSSL.Checked)
                    XmlConfig.getInstance().SetParam("server", "ssl", "1");
                else
                    XmlConfig.getInstance().SetParam("server", "ssl", "0");

                XmlConfig.getInstance().SetParam("server", "host", this.textBoxServerIP.Text);// "localhost";
                XmlConfig.getInstance().SetParam("server", "port", this.textBoxPort.Text); //"9443";
                XmlConfig.getInstance().SetParam("server", "user", this.textBoxUser.Text); //"admin";

                String userName = this.textBoxPass.Text.Trim();
                if (userName.Length < 32)
                    userName = WebUtil.GetMd5Hash(userName);
                XmlConfig.getInstance().SetParam("server", "pass", userName); //"";
                XmlConfig.getInstance().SaveConfig();

                this.Close();
                DialogResult = DialogResult.OK;
                return;
            }
            MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
    }
}
