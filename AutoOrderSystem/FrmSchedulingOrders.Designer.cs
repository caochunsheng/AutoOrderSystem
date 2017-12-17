namespace Art.Scheduling.OrderForm
{
    partial class FrmSchedulingOrders
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tSLabel_ProductType = new System.Windows.Forms.ToolStripLabel();
            this.tSComboBox_ProductType = new System.Windows.Forms.ToolStripComboBox();
            this.tSButton_Update = new System.Windows.Forms.ToolStripButton();
            this.tSButton_Scheduling = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.sDGView_Orders = new CCWin.SkinControl.SkinDataGridView();
            this.Col_OrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Customer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_OrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_DeliveryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_OrderStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sDGView_Products = new CCWin.SkinControl.SkinDataGridView();
            this.Col_ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_ProductSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_Info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dTPicker_ProductionDate = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sDGView_Orders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sDGView_Products)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.tSLabel_ProductType,
            this.tSComboBox_ProductType,
            this.tSButton_Update,
            this.tSButton_Scheduling});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1188, 40);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(67, 37);
            this.toolStripLabel1.Text = "生产日期：";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(126, 37);
            this.toolStripLabel2.Text = "XXXXXXXXXXXXXXXXX";
            // 
            // tSLabel_ProductType
            // 
            this.tSLabel_ProductType.Name = "tSLabel_ProductType";
            this.tSLabel_ProductType.Size = new System.Drawing.Size(67, 37);
            this.tSLabel_ProductType.Text = "产品类型：";
            // 
            // tSComboBox_ProductType
            // 
            this.tSComboBox_ProductType.Items.AddRange(new object[] {
            "门扇",
            "门套/窗套/垭口"});
            this.tSComboBox_ProductType.Name = "tSComboBox_ProductType";
            this.tSComboBox_ProductType.Size = new System.Drawing.Size(121, 40);
            this.tSComboBox_ProductType.Text = "门扇";
            // 
            // tSButton_Update
            // 
            this.tSButton_Update.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tSButton_Update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSButton_Update.Name = "tSButton_Update";
            this.tSButton_Update.Size = new System.Drawing.Size(59, 37);
            this.tSButton_Update.Text = "更新显示";
            this.tSButton_Update.Click += new System.EventHandler(this.tSButton_Update_Click);
            // 
            // tSButton_Scheduling
            // 
            this.tSButton_Scheduling.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tSButton_Scheduling.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSButton_Scheduling.Name = "tSButton_Scheduling";
            this.tSButton_Scheduling.Size = new System.Drawing.Size(59, 37);
            this.tSButton_Scheduling.Text = "计划排产";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.sDGView_Orders);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sDGView_Products);
            this.splitContainer1.Size = new System.Drawing.Size(1188, 551);
            this.splitContainer1.SplitterDistance = 752;
            this.splitContainer1.TabIndex = 1;
            // 
            // sDGView_Orders
            // 
            this.sDGView_Orders.AllowUserToAddRows = false;
            this.sDGView_Orders.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.sDGView_Orders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.sDGView_Orders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.sDGView_Orders.BackgroundColor = System.Drawing.SystemColors.Window;
            this.sDGView_Orders.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sDGView_Orders.ColumnFont = null;
            this.sDGView_Orders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.sDGView_Orders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.sDGView_Orders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sDGView_Orders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_OrderNo,
            this.Col_Customer,
            this.Col_Phone,
            this.Col_Address,
            this.Col_OrderDate,
            this.Col_DeliveryDate,
            this.Col_Material,
            this.Col_OrderStatus});
            this.sDGView_Orders.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.sDGView_Orders.DefaultCellStyle = dataGridViewCellStyle3;
            this.sDGView_Orders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sDGView_Orders.EnableHeadersVisualStyles = false;
            this.sDGView_Orders.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.sDGView_Orders.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sDGView_Orders.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.sDGView_Orders.Location = new System.Drawing.Point(0, 0);
            this.sDGView_Orders.Name = "sDGView_Orders";
            this.sDGView_Orders.ReadOnly = true;
            this.sDGView_Orders.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sDGView_Orders.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.sDGView_Orders.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.sDGView_Orders.RowTemplate.Height = 23;
            this.sDGView_Orders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.sDGView_Orders.Size = new System.Drawing.Size(752, 551);
            this.sDGView_Orders.TabIndex = 0;
            this.sDGView_Orders.TitleBack = null;
            this.sDGView_Orders.TitleBackColorBegin = System.Drawing.Color.White;
            this.sDGView_Orders.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            this.sDGView_Orders.SelectionChanged += new System.EventHandler(this.sDGView_Orders_SelectionChanged);
            // 
            // Col_OrderNo
            // 
            this.Col_OrderNo.DataPropertyName = "order_no";
            this.Col_OrderNo.HeaderText = "订单编号";
            this.Col_OrderNo.Name = "Col_OrderNo";
            this.Col_OrderNo.ReadOnly = true;
            // 
            // Col_Customer
            // 
            this.Col_Customer.DataPropertyName = "customer";
            this.Col_Customer.HeaderText = "客户名称";
            this.Col_Customer.Name = "Col_Customer";
            this.Col_Customer.ReadOnly = true;
            // 
            // Col_Phone
            // 
            this.Col_Phone.DataPropertyName = "phone";
            this.Col_Phone.HeaderText = "联系电话";
            this.Col_Phone.Name = "Col_Phone";
            this.Col_Phone.ReadOnly = true;
            // 
            // Col_Address
            // 
            this.Col_Address.DataPropertyName = "address";
            this.Col_Address.HeaderText = "发货地址";
            this.Col_Address.Name = "Col_Address";
            this.Col_Address.ReadOnly = true;
            // 
            // Col_OrderDate
            // 
            this.Col_OrderDate.DataPropertyName = "order_date";
            this.Col_OrderDate.HeaderText = "下单日期";
            this.Col_OrderDate.Name = "Col_OrderDate";
            this.Col_OrderDate.ReadOnly = true;
            // 
            // Col_DeliveryDate
            // 
            this.Col_DeliveryDate.DataPropertyName = "delivery_date";
            this.Col_DeliveryDate.HeaderText = "交付日期";
            this.Col_DeliveryDate.Name = "Col_DeliveryDate";
            this.Col_DeliveryDate.ReadOnly = true;
            // 
            // Col_Material
            // 
            this.Col_Material.DataPropertyName = "order_memo";
            this.Col_Material.HeaderText = "主单材质";
            this.Col_Material.Name = "Col_Material";
            this.Col_Material.ReadOnly = true;
            // 
            // Col_OrderStatus
            // 
            this.Col_OrderStatus.DataPropertyName = "order_status";
            this.Col_OrderStatus.HeaderText = "订单状态";
            this.Col_OrderStatus.Name = "Col_OrderStatus";
            this.Col_OrderStatus.ReadOnly = true;
            // 
            // sDGView_Products
            // 
            this.sDGView_Products.AllowUserToAddRows = false;
            this.sDGView_Products.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(246)))), ((int)(((byte)(253)))));
            this.sDGView_Products.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.sDGView_Products.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.sDGView_Products.BackgroundColor = System.Drawing.SystemColors.Window;
            this.sDGView_Products.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sDGView_Products.ColumnFont = null;
            this.sDGView_Products.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(246)))), ((int)(((byte)(239)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.sDGView_Products.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.sDGView_Products.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sDGView_Products.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_ProductName,
            this.Col_ProductSize,
            this.Col_Num,
            this.Col_Info});
            this.sDGView_Products.ColumnSelectForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(188)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.sDGView_Products.DefaultCellStyle = dataGridViewCellStyle7;
            this.sDGView_Products.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sDGView_Products.EnableHeadersVisualStyles = false;
            this.sDGView_Products.GridColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.sDGView_Products.HeadFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sDGView_Products.HeadSelectForeColor = System.Drawing.SystemColors.HighlightText;
            this.sDGView_Products.Location = new System.Drawing.Point(0, 0);
            this.sDGView_Products.Name = "sDGView_Products";
            this.sDGView_Products.ReadOnly = true;
            this.sDGView_Products.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.sDGView_Products.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.sDGView_Products.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.sDGView_Products.RowTemplate.Height = 23;
            this.sDGView_Products.Size = new System.Drawing.Size(432, 551);
            this.sDGView_Products.TabIndex = 0;
            this.sDGView_Products.TitleBack = null;
            this.sDGView_Products.TitleBackColorBegin = System.Drawing.Color.White;
            this.sDGView_Products.TitleBackColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(196)))), ((int)(((byte)(242)))));
            // 
            // Col_ProductName
            // 
            this.Col_ProductName.HeaderText = "产品名称";
            this.Col_ProductName.Name = "Col_ProductName";
            this.Col_ProductName.ReadOnly = true;
            // 
            // Col_ProductSize
            // 
            this.Col_ProductSize.HeaderText = "产品尺寸";
            this.Col_ProductSize.Name = "Col_ProductSize";
            this.Col_ProductSize.ReadOnly = true;
            // 
            // Col_Num
            // 
            this.Col_Num.HeaderText = "产品数量";
            this.Col_Num.Name = "Col_Num";
            this.Col_Num.ReadOnly = true;
            // 
            // Col_Info
            // 
            this.Col_Info.HeaderText = "产品信息";
            this.Col_Info.Name = "Col_Info";
            this.Col_Info.ReadOnly = true;
            // 
            // dTPicker_ProductionDate
            // 
            this.dTPicker_ProductionDate.Location = new System.Drawing.Point(73, 10);
            this.dTPicker_ProductionDate.Name = "dTPicker_ProductionDate";
            this.dTPicker_ProductionDate.Size = new System.Drawing.Size(126, 21);
            this.dTPicker_ProductionDate.TabIndex = 1;
            // 
            // FrmSchedulingOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 591);
            this.Controls.Add(this.dTPicker_ProductionDate);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSchedulingOrders";
            this.Text = "预览订单";
            this.Load += new System.EventHandler(this.FrmSchedulingOrders_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sDGView_Orders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sDGView_Products)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private CCWin.SkinControl.SkinDataGridView sDGView_Orders;
        private CCWin.SkinControl.SkinDataGridView sDGView_Products;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_OrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Customer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_OrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_DeliveryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Material;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_OrderStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_ProductSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_Info;
        private System.Windows.Forms.ToolStripButton tSButton_Scheduling;
        private System.Windows.Forms.ToolStripButton tSButton_Update;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.DateTimePicker dTPicker_ProductionDate;
        private System.Windows.Forms.ToolStripLabel tSLabel_ProductType;
        private System.Windows.Forms.ToolStripComboBox tSComboBox_ProductType;
    }
}