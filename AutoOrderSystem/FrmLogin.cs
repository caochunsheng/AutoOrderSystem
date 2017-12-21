using AutoOrderSystem.Common;
using AutoOrderSystemCommon;
using CCWin;
using RESTClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace AutoOrderSystem
{
    public partial class FrmLogin : CCSkinMain
    {
        public WebRequestSession Session { get; set; }
        public string User
        {
            get { return this.textBox_User.Text; }
        }
        public string Pass
        {
            get { return this.textBox_Pass.Text; }
        }
        public int Port
        {
            get { return Convert.ToInt32(this.textBox_Port.Text); }
        }
        public bool SSL
        {
            get { return this.checkBox_SSL.Checked; }
        }
        public string ServerIP
        {
            get { return this.comboBox_ServerIP.Text; }
        }
        public FrmLogin()
        {
            InitializeComponent();
            Session = new WebRequestSession();
        }

        private void button_Login_Click(object sender, EventArgs e)
        {
            if (this.checkBox_Remember.Checked)
            {

                AppConfig.SetValue("IsRemember", "True");
                AppConfig.SetValue("UserName", this.textBox_User.Text);
                AppConfig.SetValue("UserPass", this.textBox_Pass.Text);
                AppConfig.SetValue("ServerIP", this.comboBox_ServerIP.Text);
                AppConfig.SetValue("ServerPort", this.textBox_Port.Text);
                AppConfig.SetValue("SSL", this.checkBox_SSL.Checked.ToString());

            }
            else
            {
                AppConfig.SetValue("IsRemember", "False");
            }

            if (Session.IsSigned())
            {
                Session.Logout();

                MessageBox.Show("客户端已注销，请求数据时，需要重新登录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (Session.Login(this.SSL, this.ServerIP, this.Port, this.User, this.Pass, String.Empty, "此处放机器ID"))
                {
                    //this.timer_IsLogined.Enabled = true;
                    LogHelper.WriteLog($"登录成功！用户：{User} 服务器地址：{ServerIP}", LogType.Status);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {

                    LogHelper.WriteLog($"登录失败！用户：{User} 服务器地址：{ServerIP}", LogType.Status);
                    MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    return;
                }

            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (AppConfig.GetValue("IsRemember") == "")
            {
                AppConfig.SetValue("IsRemember", this.checkBox_Remember.Checked.ToString());
            }
            else if (AppConfig.GetValue("IsRemember") == "True")
            {
                this.checkBox_Remember.Checked = true;
                this.textBox_User.Text = AppConfig.GetValue("UserName");
                this.textBox_Pass.Text = AppConfig.GetValue("UserPass");
                this.textBox_Port.Text = AppConfig.GetValue("ServerPort");
                this.comboBox_ServerIP.Items.Add(AppConfig.GetValue("ServerIP"));
                this.checkBox_SSL.Checked = Convert.ToBoolean(AppConfig.GetValue("SSL"));

            }
            else if (AppConfig.GetValue("IsRemember") == "False")
            {
                this.checkBox_Remember.Checked = false;
            }


            string Local_IP = GetAddressIP();
            string[] IP = Local_IP.Split('.');
            string Ip = string.Format("{0}.{1}.{2}.", IP[0], IP[1], IP[2]);

            try
            {
                for (int i = 1; i <= 255; i++)
                {
                    Ping myPing;
                    myPing = new Ping();
                    myPing.PingCompleted += new PingCompletedEventHandler(_myPing_PingCompleted);

                    string pingIP = Ip + i.ToString();
                    myPing.SendAsync(pingIP, 1000, null);
                }
            }
            catch
            {

            }
        }

        public static string GetAddressIP()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
        private  void _myPing_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                this.comboBox_ServerIP.Items.Add(e.Reply.Address.ToString());
            }
            this.comboBox_ServerIP.SelectedIndex = 0;
        }
    }
}
