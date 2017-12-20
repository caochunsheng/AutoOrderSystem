using AutoOrderSystem.Common;
using AutoOrderSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoOrderSystem.UI
{
    public partial class FrmERPOrder : Form
    {
        private const string xmlpath = @"D:\桌面\orderDateChanage.xml";
        public List<ExOrder> _orderList;
        public FrmERPOrder()
        {
            InitializeComponent();
            _orderList = new List<ExOrder>();   
        }
        #region 最大最小关闭事件
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

        private void btnGet_Click(object sender, EventArgs e)
        {
            OrderXml xm = new OrderXml(xmlpath);
   
            if(xm.Convert2Model(out _orderList))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }


        }
    }
}
