namespace AutoOrderSystem
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TSSLabel_Log = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tSButton_NewOrder = new System.Windows.Forms.ToolStripButton();
            this.TSButton_Open = new System.Windows.Forms.ToolStripButton();
            this.TSButton_Close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TSButton_CreateOrder = new System.Windows.Forms.ToolStripButton();
            this.btn_Export = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tSButton_OrderList = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_OrderEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_Option = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdateLog = new System.Windows.Forms.ToolStripButton();
            this.tctl_Main = new System.Windows.Forms.TabControl();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSSLabel_Log});
            this.statusStrip1.Location = new System.Drawing.Point(4, 549);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1428, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TSSLabel_Log
            // 
            this.TSSLabel_Log.Name = "TSSLabel_Log";
            this.TSSLabel_Log.Size = new System.Drawing.Size(74, 21);
            this.TSSLabel_Log.Text = "日志显示";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSButton_NewOrder,
            this.TSButton_Open,
            this.TSButton_Close,
            this.toolStripSeparator2,
            this.TSButton_CreateOrder,
            this.btn_Export,
            this.toolStripSeparator1,
            this.tSButton_OrderList,
            this.toolStripButton_OrderEdit,
            this.toolStripSeparator3,
            this.toolStripButton_Option,
            this.toolStripSeparator4,
            this.btnUpdateLog});
            this.toolStrip1.Location = new System.Drawing.Point(4, 36);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1428, 38);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tSButton_NewOrder
            // 
            this.tSButton_NewOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSButton_NewOrder.Image = global::AutoOrderSystem.Properties.Resources.new_file_32px__2_;
            this.tSButton_NewOrder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tSButton_NewOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSButton_NewOrder.Name = "tSButton_NewOrder";
            this.tSButton_NewOrder.Size = new System.Drawing.Size(36, 35);
            this.tSButton_NewOrder.Text = "新建订单";
            this.tSButton_NewOrder.ToolTipText = "新建订单";
            this.tSButton_NewOrder.Click += new System.EventHandler(this.tSButton_NewOrder_Click);
            // 
            // TSButton_Open
            // 
            this.TSButton_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSButton_Open.Image = global::AutoOrderSystem.Properties.Resources.open_file_32px;
            this.TSButton_Open.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSButton_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSButton_Open.Name = "TSButton_Open";
            this.TSButton_Open.Size = new System.Drawing.Size(36, 35);
            this.TSButton_Open.Text = "打开(&O)";
            this.TSButton_Open.Click += new System.EventHandler(this.TSButton_Open_Click);
            // 
            // TSButton_Close
            // 
            this.TSButton_Close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSButton_Close.Image = global::AutoOrderSystem.Properties.Resources.delete_file_32px;
            this.TSButton_Close.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSButton_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSButton_Close.Name = "TSButton_Close";
            this.TSButton_Close.Size = new System.Drawing.Size(36, 35);
            this.TSButton_Close.Text = "关闭(&C)";
            this.TSButton_Close.ToolTipText = "关闭(C)";
            this.TSButton_Close.Click += new System.EventHandler(this.TSButton_Close_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // TSButton_CreateOrder
            // 
            this.TSButton_CreateOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TSButton_CreateOrder.Enabled = false;
            this.TSButton_CreateOrder.Image = global::AutoOrderSystem.Properties.Resources.Import_export_32px;
            this.TSButton_CreateOrder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.TSButton_CreateOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSButton_CreateOrder.Name = "TSButton_CreateOrder";
            this.TSButton_CreateOrder.Size = new System.Drawing.Size(36, 35);
            this.TSButton_CreateOrder.Text = "生成订单";
            this.TSButton_CreateOrder.ToolTipText = "生成订单";
            this.TSButton_CreateOrder.Click += new System.EventHandler(this.TSButton_CreateOrder_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Enabled = false;
            this.btn_Export.Image = global::AutoOrderSystem.Properties.Resources.export_32px;
            this.btn_Export.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btn_Export.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(67, 35);
            this.btn_Export.Text = "导出";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // tSButton_OrderList
            // 
            this.tSButton_OrderList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSButton_OrderList.Image = global::AutoOrderSystem.Properties.Resources.history_order_32px;
            this.tSButton_OrderList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tSButton_OrderList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSButton_OrderList.Name = "tSButton_OrderList";
            this.tSButton_OrderList.Size = new System.Drawing.Size(36, 35);
            this.tSButton_OrderList.Text = "浏览订单";
            this.tSButton_OrderList.Click += new System.EventHandler(this.tSButton_OrderList_Click);
            // 
            // toolStripButton_OrderEdit
            // 
            this.toolStripButton_OrderEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_OrderEdit.Image")));
            this.toolStripButton_OrderEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_OrderEdit.Name = "toolStripButton_OrderEdit";
            this.toolStripButton_OrderEdit.Size = new System.Drawing.Size(75, 35);
            this.toolStripButton_OrderEdit.Text = "订单编辑";
            this.toolStripButton_OrderEdit.Click += new System.EventHandler(this.toolStripButton_OrderEdit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripButton_Option
            // 
            this.toolStripButton_Option.Image = global::AutoOrderSystem.Properties.Resources.options2;
            this.toolStripButton_Option.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton_Option.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Option.Name = "toolStripButton_Option";
            this.toolStripButton_Option.Size = new System.Drawing.Size(67, 35);
            this.toolStripButton_Option.Text = "选项";
            this.toolStripButton_Option.Click += new System.EventHandler(this.toolStripButton_Option_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 38);
            // 
            // btnUpdateLog
            // 
            this.btnUpdateLog.Image = global::AutoOrderSystem.Properties.Resources.logviewer1;
            this.btnUpdateLog.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnUpdateLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateLog.Name = "btnUpdateLog";
            this.btnUpdateLog.Size = new System.Drawing.Size(91, 35);
            this.btnUpdateLog.Text = "更新日志";
            this.btnUpdateLog.Click += new System.EventHandler(this.btnUpdateLog_Click);
            // 
            // tctl_Main
            // 
            this.tctl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tctl_Main.HotTrack = true;
            this.tctl_Main.ItemSize = new System.Drawing.Size(50, 25);
            this.tctl_Main.Location = new System.Drawing.Point(4, 74);
            this.tctl_Main.Multiline = true;
            this.tctl_Main.Name = "tctl_Main";
            this.tctl_Main.Padding = new System.Drawing.Point(10, 5);
            this.tctl_Main.SelectedIndex = 0;
            this.tctl_Main.Size = new System.Drawing.Size(1428, 475);
            this.tctl_Main.TabIndex = 5;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.CaptionBackColorBottom = System.Drawing.SystemColors.ButtonHighlight;
            this.CaptionBackColorTop = System.Drawing.SystemColors.ActiveCaption;
            this.CaptionFont = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CaptionHeight = 32;
            this.ClientSize = new System.Drawing.Size(1436, 579);
            this.CloseBoxSize = new System.Drawing.Size(32, 30);
            this.CloseDownBack = global::AutoOrderSystem.Properties.Resources.Close_3;
            this.CloseMouseBack = global::AutoOrderSystem.Properties.Resources.Close_2;
            this.CloseNormlBack = global::AutoOrderSystem.Properties.Resources.Close_1;
            this.Controls.Add(this.tctl_Main);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.EffectCaption = CCWin.TitleType.Title;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ICoOffset = new System.Drawing.Point(10, 0);
            this.MaxDownBack = global::AutoOrderSystem.Properties.Resources.Max_3;
            this.MaxMouseBack = global::AutoOrderSystem.Properties.Resources.Max_2;
            this.MaxNormlBack = global::AutoOrderSystem.Properties.Resources.Max_1;
            this.MaxSize = new System.Drawing.Size(32, 30);
            this.MiniDownBack = global::AutoOrderSystem.Properties.Resources.Min_3;
            this.MiniMouseBack = global::AutoOrderSystem.Properties.Resources.Min_2;
            this.MiniNormlBack = global::AutoOrderSystem.Properties.Resources.Min_1;
            this.MiniSize = new System.Drawing.Size(32, 30);
            this.Name = "FrmMain";
            this.Opacity = 0.5D;
            this.Radius = 0;
            this.RestoreDownBack = global::AutoOrderSystem.Properties.Resources.Restore_3;
            this.RestoreMouseBack = global::AutoOrderSystem.Properties.Resources.Restore_2;
            this.RestoreNormlBack = global::AutoOrderSystem.Properties.Resources.Restore_1;
            this.RoundStyle = CCWin.SkinClass.RoundStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ERP订单录入系统(2017.09.20)";
            this.TitleCenter = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel TSSLabel_Log;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TSButton_Open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton TSButton_CreateOrder;
        private System.Windows.Forms.ToolStripButton TSButton_Close;
        private System.Windows.Forms.ToolStripButton tSButton_NewOrder;
        private System.Windows.Forms.ToolStripButton tSButton_OrderList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_OrderEdit;
        private System.Windows.Forms.TabControl tctl_Main;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButton_Option;
        private System.Windows.Forms.ToolStripButton btn_Export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnUpdateLog;
    }
}