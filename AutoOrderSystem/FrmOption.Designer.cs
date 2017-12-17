namespace AutoOrderSystem
{
    partial class FrmOption
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pushPanelItem2 = new CCWin.SkinControl.PushPanelItem();
            this.dgv_ProductType = new CCWin.SkinControl.SkinDataGridView();
            this.Col_ProductType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skinPushPanel1 = new CCWin.SkinControl.SkinPushPanel();
            this.pushPanelItem1 = new CCWin.SkinControl.PushPanelItem();
            this.pushPanelItem2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProductType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinPushPanel1)).BeginInit();
            this.skinPushPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pushPanelItem2
            // 
            this.pushPanelItem2.Border = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(221)))), ((int)(((byte)(255)))));
            this.pushPanelItem2.CaptionBackHover = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(198)))), ((int)(((byte)(255)))));
            this.pushPanelItem2.CaptionBackNormal = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            this.pushPanelItem2.CaptionBackPressed = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.pushPanelItem2.CaptionFont = new System.Drawing.Font("微软雅黑", 9.75F);
            this.pushPanelItem2.CaptionFore = System.Drawing.Color.Black;
            this.pushPanelItem2.Controls.Add(this.dgv_ProductType);
            this.pushPanelItem2.Name = "pushPanelItem2";
            this.pushPanelItem2.Text = "产品识别设置";
            // 
            // dgv_ProductType
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.dgv_ProductType.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ProductType.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ProductType.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv_ProductType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_ProductType.ColumnFont = null;
            this.dgv_ProductType.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_ProductType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ProductType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ProductType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_ProductType,
            this.Col_Key,
            this.Col_Value});
            this.dgv_ProductType.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ProductType.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_ProductType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ProductType.EnableHeadersVisualStyles = false;
            this.dgv_ProductType.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dgv_ProductType.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgv_ProductType.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_ProductType.Location = new System.Drawing.Point(2, 24);
            this.dgv_ProductType.Name = "dgv_ProductType";
            this.dgv_ProductType.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv_ProductType.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgv_ProductType.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_ProductType.RowTemplate.Height = 23;
            this.dgv_ProductType.Size = new System.Drawing.Size(725, 302);
            this.dgv_ProductType.TabIndex = 0;
            this.dgv_ProductType.TitleBack = null;
            this.dgv_ProductType.TitleBackColorBegin = System.Drawing.Color.White;
            this.dgv_ProductType.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // Col_ProductType
            // 
            this.Col_ProductType.HeaderText = "产品类型";
            this.Col_ProductType.Name = "Col_ProductType";
            // 
            // Col_Key
            // 
            this.Col_Key.HeaderText = "识别字段";
            this.Col_Key.Name = "Col_Key";
            // 
            // Col_Value
            // 
            this.Col_Value.HeaderText = "字段值";
            this.Col_Value.Name = "Col_Value";
            // 
            // skinPushPanel1
            // 
            this.skinPushPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinPushPanel1.Items.AddRange(new CCWin.SkinControl.PushPanelItem[] {
            this.pushPanelItem2,
            this.pushPanelItem1});
            this.skinPushPanel1.Location = new System.Drawing.Point(4, 36);
            this.skinPushPanel1.Name = "skinPushPanel1";
            this.skinPushPanel1.Size = new System.Drawing.Size(733, 358);
            this.skinPushPanel1.TabIndex = 0;
            // 
            // pushPanelItem1
            // 
            this.pushPanelItem1.Border = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(221)))), ((int)(((byte)(255)))));
            this.pushPanelItem1.CaptionBackHover = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(198)))), ((int)(((byte)(255)))));
            this.pushPanelItem1.CaptionBackNormal = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(218)))), ((int)(((byte)(255)))));
            this.pushPanelItem1.CaptionBackPressed = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.pushPanelItem1.CaptionFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.pushPanelItem1.CaptionFore = System.Drawing.Color.Black;
            this.pushPanelItem1.Name = "pushPanelItem1";
            this.pushPanelItem1.Text = "其他设置";
            // 
            // FrmOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 398);
            this.Controls.Add(this.skinPushPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOption";
            this.Text = "选项";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmOption_FormClosing);
            this.pushPanelItem2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProductType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinPushPanel1)).EndInit();
            this.skinPushPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.PushPanelItem pushPanelItem2;
        private CCWin.SkinControl.SkinPushPanel skinPushPanel1;
        private CCWin.SkinControl.PushPanelItem pushPanelItem1;
        private CCWin.SkinControl.SkinDataGridView dgv_ProductType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_ProductType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Key;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Value;
    }
}