using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoOrderSystem.UI
{
    public partial class FrmBase : Form
    {
        public FrmBase()
        {
            InitializeComponent();
        }

        private void btnLogo_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.artisman.cn/"); // 使用本机默认浏览器打开网址
        }
    }
}
