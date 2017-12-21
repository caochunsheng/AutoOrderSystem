using AutoOrderSystemCommon;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoOrderSystem.UI
{
    public partial class FrmLogin : Form
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
            Session = new WebRequestSession();
            InitializeComponent();    
        }
        #region 最大最小关闭实现
        private void btnMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.btnMax.Image = global::AutoOrderSystem.UI.Properties.Resources.Max;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                this.btnMax.Image = global::AutoOrderSystem.UI.Properties.Resources.Normal;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region 拖动窗体的实现
        private Point mouseOff;//鼠标移动位置变量
        private bool leftFlag;//标签是否为左键
        private void pnlCaption_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void pnlCaption_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        private void pnlCaption_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }
        #endregion

        #region 拖动改变窗体大小
        const int HTLEFT = 10;

        const int HTRIGHT = 11;

        const int HTTOP = 12;

        const int HTTOPLEFT = 13;

        const int HTTOPRIGHT = 14;

        const int HTBOTTOM = 15;

        const int HTBOTTOMLEFT = 0x10;

        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)

        {

            switch (m.Msg)

            {

                case 0x0084:

                    base.WndProc(ref m);

                    Point vPoint = new Point((int)m.LParam & 0xFFFF,

                                   (int)m.LParam >> 16 & 0xFFFF);

                    vPoint = PointToClient(vPoint);

                    if (vPoint.X <= 5)

                        if (vPoint.Y <= 5)

                            m.Result = (IntPtr)HTTOPLEFT;

                    else if(vPoint.Y >= ClientSize.Height - 5)
        
                   m.Result = (IntPtr)HTBOTTOMLEFT;

              else m.Result = (IntPtr)HTLEFT;

                    else if(vPoint.X >= ClientSize.Width - 5)
        
               if (vPoint.Y <= 5)

                        m.Result = (IntPtr)HTTOPRIGHT;

                    else if(vPoint.Y >= ClientSize.Height - 5)
        
                     m.Result = (IntPtr)HTBOTTOMRIGHT;

                else m.Result = (IntPtr)HTRIGHT;

                    else if(vPoint.Y <= 5)
        
                  m.Result = (IntPtr)HTTOP;

                    else if(vPoint.Y >= ClientSize.Height - 5)
        
                   m.Result = (IntPtr)HTBOTTOM;

                    break;

                case 0x0201://鼠标左键按下的消息 用于实现拖动窗口功能

                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标

                    m.LParam = IntPtr.Zero;//默认值

                    m.WParam = new IntPtr(2);//鼠标放在标题栏内

                    base.WndProc(ref m);

                    break;

                default:

                    base.WndProc(ref m);

                    break;

            }

        }
        #endregion

        public string GetAddressIP()
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

        private void _myPing_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                comboBox_ServerIP.Items.Add(e.Reply.Address.ToString());
            }
            comboBox_ServerIP.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
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
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
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
    }
}
