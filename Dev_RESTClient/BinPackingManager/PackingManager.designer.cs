namespace BinPacking
{
    partial class PackingManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackingManager));
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonRebuild = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewTasks = new System.Windows.Forms.TreeView();
            this.tabControlTask = new System.Windows.Forms.TabControl();
            this.tabPagePath = new System.Windows.Forms.TabPage();
            this.tabPageLayout = new System.Windows.Forms.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imageListStatus = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlTask.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(11, 12);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(116, 36);
            this.buttonNew.TabIndex = 0;
            this.buttonNew.Text = "新建拼料";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonRebuild
            // 
            this.buttonRebuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRebuild.Location = new System.Drawing.Point(511, 12);
            this.buttonRebuild.Name = "buttonRebuild";
            this.buttonRebuild.Size = new System.Drawing.Size(116, 36);
            this.buttonRebuild.TabIndex = 2;
            this.buttonRebuild.Text = "重建任务";
            this.buttonRebuild.UseVisualStyleBackColor = true;
            this.buttonRebuild.Click += new System.EventHandler(this.buttonRebuild_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(641, 12);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(116, 36);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "删除任务";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Location = new System.Drawing.Point(11, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 431);
            this.panel1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewTasks);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlTask);
            this.splitContainer1.Size = new System.Drawing.Size(745, 431);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeViewTasks
            // 
            this.treeViewTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTasks.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeViewTasks.Location = new System.Drawing.Point(0, 0);
            this.treeViewTasks.Name = "treeViewTasks";
            this.treeViewTasks.Size = new System.Drawing.Size(192, 431);
            this.treeViewTasks.TabIndex = 0;
            this.treeViewTasks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTasks_AfterSelect);
            this.treeViewTasks.Click += new System.EventHandler(this.treeViewTasks_Click);
            this.treeViewTasks.DoubleClick += new System.EventHandler(this.treeViewTasks_DoubleClick);
            // 
            // tabControlTask
            // 
            this.tabControlTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlTask.Controls.Add(this.tabPagePath);
            this.tabControlTask.Controls.Add(this.tabPageLayout);
            this.tabControlTask.Location = new System.Drawing.Point(0, 0);
            this.tabControlTask.Name = "tabControlTask";
            this.tabControlTask.SelectedIndex = 0;
            this.tabControlTask.ShowToolTips = true;
            this.tabControlTask.Size = new System.Drawing.Size(549, 431);
            this.tabControlTask.TabIndex = 0;
            this.tabControlTask.SelectedIndexChanged += new System.EventHandler(this.tabControlTask_SelectedIndexChanged);
            // 
            // tabPagePath
            // 
            this.tabPagePath.Location = new System.Drawing.Point(4, 22);
            this.tabPagePath.Name = "tabPagePath";
            this.tabPagePath.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePath.Size = new System.Drawing.Size(541, 405);
            this.tabPagePath.TabIndex = 0;
            this.tabPagePath.Text = "路径预览图";
            this.tabPagePath.UseVisualStyleBackColor = true;
            // 
            // tabPageLayout
            // 
            this.tabPageLayout.Location = new System.Drawing.Point(4, 22);
            this.tabPageLayout.Name = "tabPageLayout";
            this.tabPageLayout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLayout.Size = new System.Drawing.Size(541, 405);
            this.tabPageLayout.TabIndex = 1;
            this.tabPageLayout.Text = "工件布局图";
            this.tabPageLayout.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 431);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // imageListStatus
            // 
            this.imageListStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListStatus.ImageStream")));
            this.imageListStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListStatus.Images.SetKeyName(0, "StatusAnnotations_Complete_and_ok_16xLG.png");
            this.imageListStatus.Images.SetKeyName(1, "StatusAnnotations_Complete_and_ok_16xLG_color.png");
            this.imageListStatus.Images.SetKeyName(2, "StatusAnnotations_Complete_and_ok_16xMD.png");
            this.imageListStatus.Images.SetKeyName(3, "StatusAnnotations_Complete_and_ok_16xMD_color.png");
            this.imageListStatus.Images.SetKeyName(4, "StatusAnnotations_Complete_and_ok_16xSM.png");
            this.imageListStatus.Images.SetKeyName(5, "StatusAnnotations_Complete_and_ok_16xSM_color.png");
            this.imageListStatus.Images.SetKeyName(6, "StatusAnnotations_Complete_and_ok_32xLG.png");
            this.imageListStatus.Images.SetKeyName(7, "StatusAnnotations_Complete_and_ok_32xLG_color.png");
            this.imageListStatus.Images.SetKeyName(8, "StatusAnnotations_Complete_and_ok_32xMD.png");
            this.imageListStatus.Images.SetKeyName(9, "StatusAnnotations_Complete_and_ok_32xMD_color.png");
            this.imageListStatus.Images.SetKeyName(10, "StatusAnnotations_Complete_and_ok_32xSM.png");
            this.imageListStatus.Images.SetKeyName(11, "StatusAnnotations_Complete_and_ok_32xSM_color.png");
            this.imageListStatus.Images.SetKeyName(12, "StatusAnnotations_Critical_16xLG.png");
            this.imageListStatus.Images.SetKeyName(13, "StatusAnnotations_Critical_16xLG_color.png");
            this.imageListStatus.Images.SetKeyName(14, "StatusAnnotations_Critical_16xMD.png");
            this.imageListStatus.Images.SetKeyName(15, "StatusAnnotations_Critical_16xMD_color.png");
            this.imageListStatus.Images.SetKeyName(16, "StatusAnnotations_Critical_16xSM.png");
            this.imageListStatus.Images.SetKeyName(17, "StatusAnnotations_Critical_16xSM_color.png");
            this.imageListStatus.Images.SetKeyName(18, "StatusAnnotations_Critical_32xLG.png");
            this.imageListStatus.Images.SetKeyName(19, "StatusAnnotations_Critical_32xLG_color.png");
            this.imageListStatus.Images.SetKeyName(20, "StatusAnnotations_Critical_32xMD.png");
            this.imageListStatus.Images.SetKeyName(21, "StatusAnnotations_Critical_32xMD_color.png");
            this.imageListStatus.Images.SetKeyName(22, "StatusAnnotations_Critical_32xSM.png");
            this.imageListStatus.Images.SetKeyName(23, "StatusAnnotations_Critical_32xSM_color.png");
            // 
            // PackingManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 504);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonRebuild);
            this.Controls.Add(this.buttonNew);
            this.Name = "PackingManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拼料任务管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PackingTest_FormClosing);
            this.Load += new System.EventHandler(this.PackingTest_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlTask.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonRebuild;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewTasks;
        private System.Windows.Forms.TabControl tabControlTask;
        private System.Windows.Forms.TabPage tabPagePath;
        private System.Windows.Forms.TabPage tabPageLayout;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imageListStatus;
    }
}