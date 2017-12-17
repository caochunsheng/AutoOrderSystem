using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BinPacking;
using CNConnector;
using System.IO;
using System.Diagnostics;

namespace BinPacking
{
    public partial class PackingManager : Form
    {
        CNController _cncConnector = new CNController();
        static private string _CodeBase = AppDomain.CurrentDomain.BaseDirectory;

        private static int _goodIconIndex = -1;
        private static int _badIconIndex = -1;

        public PackingManager()
        {
            InitializeComponent();
        }

        private void PackingTest_Load(object sender, EventArgs e)
        {
            CNController.CNC_Init();

            _CodeBase = _CodeBase.Replace("/", "\\");
            if (!_CodeBase.EndsWith("\\"))
                _CodeBase += "\\";

            treeViewTasks.LabelEdit = false;
            treeViewTasks.ImageList = imageListStatus;

            for (int i = 0; i < treeViewTasks.ImageList.Images.Count; i++)
            {
                if (treeViewTasks.ImageList.Images.Keys[i].Equals("StatusAnnotations_Complete_and_ok_16xLG_color.png", StringComparison.InvariantCultureIgnoreCase))
                {
                    _goodIconIndex = i;
                }
                else if (treeViewTasks.ImageList.Images.Keys[i].Equals("StatusAnnotations_Critical_16xLG_color.png", StringComparison.InvariantCultureIgnoreCase))
                {
                    _badIconIndex = i;
                }
            }

            tabControlTask.TabPages.Clear();

            RefreshTaskTree();

            WindowState = FormWindowState.Maximized;
        }

        private void PackingTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cncConnector.Disconnect();
            CNController.CNC_Exit();
        }

        private string GetTaskName(string xmlFileName)
        {
            string taskName = xmlFileName.Replace("/", "\\");
            int index = taskName.LastIndexOf("(");
            if (index >= 0)
                taskName = taskName.Substring(0, index);
            else
            {
                index = taskName.LastIndexOf(".");
                if (index >= 0)
                    taskName = taskName.Substring(0, index);
            }

            index = taskName.LastIndexOf("\\");
            if (index >= 0)
                taskName = taskName.Substring(index+1);

            return taskName;
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            List<MDF> allMDF = null;
            string xmlResultFile = CNController.CADM_BinPacking(_cncConnector.GetModelName());
            if (!String.IsNullOrEmpty(xmlResultFile))
            {
                //System.Diagnostics.Process.Start(xmlResultFile);
                allMDF = BinPackingInfo.LoadFromXml(xmlResultFile);
                if (allMDF.Count > 0)
                {
                    try
                    {
                        string taskName = GetTaskName(xmlResultFile);
                        string sTaskFolder = String.Format("{0}PackingTasks\\", _CodeBase);
                        string sTaskFile = String.Format("{0}PackingTasks\\{1}.xml", _CodeBase, taskName);

                        if (!Directory.Exists(sTaskFolder))
                            Directory.CreateDirectory(sTaskFolder);

                        int seq = 1;
                        while (File.Exists(sTaskFile))
                            sTaskFile = String.Format("{0}PackingTasks\\{1}-{2:000}.xml", _CodeBase, taskName, seq++);

                        File.Copy(xmlResultFile, sTaskFile);

                        RefreshTaskTree();
                    }
                    catch (Exception er)
                    { }
                }
            }
        }

        private void RefreshTaskTree()
        {
            List<string> listTasks = new List<string>();

            string sTaskFolder = String.Format("{0}PackingTasks\\", _CodeBase);
            DirectoryInfo taskFiles = new DirectoryInfo(sTaskFolder);
            foreach (var xmlTaskFile in taskFiles.EnumerateFiles())
            {
                string taskName = GetTaskName(xmlTaskFile.FullName);
                listTasks.Add(taskName);
            }
            listTasks.Sort();

            treeViewTasks.Nodes.Clear();

            for (int i = 0; i < listTasks.Count; i++)
            {
                string taskName = listTasks[i];
                TreeNode rootTask = new TreeNode();
                rootTask.Text = taskName;
                rootTask.ImageIndex = _goodIconIndex;
                rootTask.SelectedImageIndex = _goodIconIndex;

                string sTaskFile = String.Format("{0}PackingTasks\\{1}.xml", _CodeBase, listTasks[i]);
                List<MDF> allMDF = BinPackingInfo.LoadFromXml(sTaskFile);
                for (int p = 0; p < allMDF.Count; p++)
                {
                    TreeNode child = new TreeNode();
                    child.Text = allMDF[p]._JobName;
                    child.ImageIndex = _badIconIndex;
                    child.SelectedImageIndex = _badIconIndex;
                    string jobFolder = String.Format("{0}Jobs\\{1}\\", _CodeBase, allMDF[p]._JobName);
                    if (Directory.Exists(jobFolder))
                    {
                        string jobFile = String.Format("{0}Jobs\\{1}\\{1}.cf", _CodeBase, allMDF[p]._JobName);
                        if (File.Exists(jobFile))
                        {
                            child.ImageIndex = _goodIconIndex;
                            child.SelectedImageIndex = _goodIconIndex;
                        }
                    }
                    if (child.ImageIndex == _badIconIndex)
                    {
                        rootTask.ImageIndex = _badIconIndex;
                        rootTask.SelectedImageIndex = _badIconIndex;
                    }
                    rootTask.Nodes.Add(child);
                }
                treeViewTasks.Nodes.Add(rootTask);
            }
        }

        private void buttonRebuild_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void treeViewTasks_DoubleClick(object sender, EventArgs e)
        {
            return;

            for (int i = 0; i < treeViewTasks.Nodes.Count; i++)
            {
                if (treeViewTasks.Nodes[i].IsExpanded)
                    treeViewTasks.Nodes[i].Collapse();
                else
                    treeViewTasks.Nodes[i].Expand();
            }
        }

        private void RefreshPropertyPage(string jobName)
        {
            //tabControlTask.TabPages.Clear();

            if (tabControlTask.TabPages.Count == 0)
                tabControlTask.TabPages.Add(tabPagePath);//"路径预览图");
            if (tabControlTask.TabPages.Count < 2)
                tabControlTask.TabPages.Add(tabPageLayout);//"工件布局图");

            string sPathImageFile = String.Format("{0}Jobs\\{1}\\{2}.snapshot", _CodeBase, jobName, jobName);
            tabPagePath.Controls.Clear();
            var path = new PictureBox();
            if (File.Exists(sPathImageFile))
            {
                path.Image = Bitmap.FromFile(sPathImageFile);
            }
            else
            {
                int nWidth = 1024;
                int nHeight = 768;

                Bitmap bmpCross = new Bitmap(nWidth, nHeight); ;
                Graphics g = Graphics.FromImage(bmpCross);
                Pen errPen = new Pen(Color.Red, 10);
                g.DrawLine(errPen, nWidth / 4, nHeight / 4, 3 * nWidth / 4, 3 * nHeight / 4);
                g.DrawLine(errPen, nWidth / 4, 3 * nHeight / 4, 3 * nWidth / 4, nHeight / 4);
                g.Dispose();

                path.Image = bmpCross;
            }
            path.SizeMode = PictureBoxSizeMode.Zoom;//.AutoSize;//.StretchImage;
            path.Dock = DockStyle.Fill;
            this.tabPagePath.Controls.Add(path);

            sPathImageFile = String.Format("{0}Jobs\\{1}\\{2}.layout", _CodeBase, jobName, jobName);
            tabPageLayout.Controls.Clear();
            var layout = new PictureBox();
            if (File.Exists(sPathImageFile))
            {
                layout.Image = Bitmap.FromFile(sPathImageFile);
            }
            else
            {
                int nWidth = 1024;
                int nHeight = 768;

                Bitmap bmpCross = new Bitmap(nWidth, nHeight); ;
                Graphics g = Graphics.FromImage(bmpCross);
                Pen errPen = new Pen(Color.Red, 10);
                g.DrawLine(errPen, nWidth / 4, nHeight / 4, 3 * nWidth / 4, 3 * nHeight / 4);
                g.DrawLine(errPen, nWidth / 4, 3 * nHeight / 4, 3 * nWidth / 4, nHeight / 4);
                g.Dispose();

                layout.Image = bmpCross;
            }
            layout.SizeMode = PictureBoxSizeMode.Zoom;//.AutoSize;//.StretchImage;
            layout.Dock = DockStyle.Fill;
            this.tabPageLayout.Controls.Add(layout);
        }

        private void treeViewTasks_Click(object sender, EventArgs e)
        {
        }

        private void treeViewTasks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = treeViewTasks.SelectedNode;
            if (selected == null)
                return;

            if (selected.Parent != null)
            {
                string jobName = selected.Text;
                Debug.WriteLine("Selected:" + jobName);

                RefreshPropertyPage(jobName);
            }
        }

        private void tabControlTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode selected = treeViewTasks.SelectedNode;
            if (selected == null)
                return;
        }

        private void tabPageTask_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen arrow = new Pen(Brushes.Black, 4);
            arrow.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            e.Graphics.DrawLine(arrow, 10, 10, 100, 100);
            arrow.Dispose();
        }
    }
}
