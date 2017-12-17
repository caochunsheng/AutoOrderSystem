namespace AutoOrderSystem
{
    partial class FrmOrderPreview
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("订单");
            this.treeView_Order = new System.Windows.Forms.TreeView();
            this.dGV_DataShow = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tSSL_OrdersNum = new System.Windows.Forms.ToolStripStatusLabel();
            this.Orders_Num = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tSButton_Upload = new System.Windows.Forms.ToolStripButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_DataShow)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView_Order
            // 
            this.treeView_Order.CheckBoxes = true;
            this.treeView_Order.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_Order.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView_Order.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.treeView_Order.Location = new System.Drawing.Point(0, 0);
            this.treeView_Order.Name = "treeView_Order";
            treeNode1.Checked = true;
            treeNode1.Name = "OrdersNode";
            treeNode1.NodeFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode1.Text = "订单";
            this.treeView_Order.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView_Order.Size = new System.Drawing.Size(259, 429);
            this.treeView_Order.TabIndex = 0;
            this.treeView_Order.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView_Order_AfterCheck);
            this.treeView_Order.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_Order_NodeMouseClick);
            // 
            // dGV_DataShow
            // 
            this.dGV_DataShow.AllowUserToAddRows = false;
            this.dGV_DataShow.AllowUserToDeleteRows = false;
            this.dGV_DataShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_DataShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGV_DataShow.Location = new System.Drawing.Point(0, 0);
            this.dGV_DataShow.Name = "dGV_DataShow";
            this.dGV_DataShow.ReadOnly = true;
            this.dGV_DataShow.RowTemplate.Height = 23;
            this.dGV_DataShow.Size = new System.Drawing.Size(910, 429);
            this.dGV_DataShow.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSSL_OrdersNum,
            this.Orders_Num});
            this.statusStrip1.Location = new System.Drawing.Point(4, 504);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1173, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tSSL_OrdersNum
            // 
            this.tSSL_OrdersNum.Name = "tSSL_OrdersNum";
            this.tSSL_OrdersNum.Size = new System.Drawing.Size(67, 17);
            this.tSSL_OrdersNum.Text = "订单总数：";
            // 
            // Orders_Num
            // 
            this.Orders_Num.Name = "Orders_Num";
            this.Orders_Num.Size = new System.Drawing.Size(13, 17);
            this.Orders_Num.Text = "0";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSButton_Upload});
            this.toolStrip1.Location = new System.Drawing.Point(4, 36);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1173, 39);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tSButton_Upload
            // 
            this.tSButton_Upload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSButton_Upload.Image = global::AutoOrderSystem.Properties.Resources.upload_32px;
            this.tSButton_Upload.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tSButton_Upload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSButton_Upload.Name = "tSButton_Upload";
            this.tSButton_Upload.Size = new System.Drawing.Size(36, 36);
            this.tSButton_Upload.Text = "上载订单";
            this.tSButton_Upload.ToolTipText = "上载订单";
            this.tSButton_Upload.Click += new System.EventHandler(this.tSButton_Upload_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(4, 75);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView_Order);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dGV_DataShow);
            this.splitContainer2.Size = new System.Drawing.Size(1173, 429);
            this.splitContainer2.SplitterDistance = 259;
            this.splitContainer2.TabIndex = 6;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // OrderPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 530);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OrderPreview";
            this.Opacity = 1D;
            this.Text = "订单预览";
            this.Load += new System.EventHandler(this.OrderPreview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_DataShow)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView_Order;
        private System.Windows.Forms.DataGridView dGV_DataShow;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tSSL_OrdersNum;
        private System.Windows.Forms.ToolStripStatusLabel Orders_Num;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tSButton_Upload;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ImageList imageList1;
    }
}