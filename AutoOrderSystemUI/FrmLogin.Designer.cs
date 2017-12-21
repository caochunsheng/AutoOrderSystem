namespace AutoOrderSystem.UI
{
    partial class FrmLogin
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.checkBox_Remember = new System.Windows.Forms.CheckBox();
            this.comboBox_ServerIP = new System.Windows.Forms.ComboBox();
            this.label_Port = new System.Windows.Forms.Label();
            this.label_Pass = new System.Windows.Forms.Label();
            this.label_ServerIP = new System.Windows.Forms.Label();
            this.label_User = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.textBox_Pass = new System.Windows.Forms.TextBox();
            this.textBox_User = new System.Windows.Forms.TextBox();
            this.checkBox_SSL = new System.Windows.Forms.CheckBox();
            this.picLogin = new System.Windows.Forms.PictureBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblCapation = new System.Windows.Forms.Label();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnMax = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pnlMain);
            this.panel1.Controls.Add(this.pnlTop);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 351);
            this.panel1.TabIndex = 0;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.checkBox_Remember);
            this.pnlMain.Controls.Add(this.comboBox_ServerIP);
            this.pnlMain.Controls.Add(this.label_Port);
            this.pnlMain.Controls.Add(this.label_Pass);
            this.pnlMain.Controls.Add(this.label_ServerIP);
            this.pnlMain.Controls.Add(this.label_User);
            this.pnlMain.Controls.Add(this.textBox_Port);
            this.pnlMain.Controls.Add(this.textBox_Pass);
            this.pnlMain.Controls.Add(this.textBox_User);
            this.pnlMain.Controls.Add(this.checkBox_SSL);
            this.pnlMain.Controls.Add(this.picLogin);
            this.pnlMain.Controls.Add(this.btnLogin);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 40);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(418, 311);
            this.pnlMain.TabIndex = 2;
            // 
            // checkBox_Remember
            // 
            this.checkBox_Remember.AutoSize = true;
            this.checkBox_Remember.Location = new System.Drawing.Point(333, 206);
            this.checkBox_Remember.Name = "checkBox_Remember";
            this.checkBox_Remember.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Remember.TabIndex = 16;
            this.checkBox_Remember.Text = "记住";
            this.checkBox_Remember.UseVisualStyleBackColor = true;
            // 
            // comboBox_ServerIP
            // 
            this.comboBox_ServerIP.FormattingEnabled = true;
            this.comboBox_ServerIP.Location = new System.Drawing.Point(102, 177);
            this.comboBox_ServerIP.Name = "comboBox_ServerIP";
            this.comboBox_ServerIP.Size = new System.Drawing.Size(100, 20);
            this.comboBox_ServerIP.TabIndex = 13;
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(208, 180);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(41, 12);
            this.label_Port.TabIndex = 19;
            this.label_Port.Text = "端口：";
            // 
            // label_Pass
            // 
            this.label_Pass.AutoSize = true;
            this.label_Pass.Location = new System.Drawing.Point(208, 207);
            this.label_Pass.Name = "label_Pass";
            this.label_Pass.Size = new System.Drawing.Size(41, 12);
            this.label_Pass.TabIndex = 20;
            this.label_Pass.Text = "口令：";
            // 
            // label_ServerIP
            // 
            this.label_ServerIP.AutoSize = true;
            this.label_ServerIP.Location = new System.Drawing.Point(36, 183);
            this.label_ServerIP.Name = "label_ServerIP";
            this.label_ServerIP.Size = new System.Drawing.Size(53, 12);
            this.label_ServerIP.TabIndex = 17;
            this.label_ServerIP.Text = "服务器：";
            // 
            // label_User
            // 
            this.label_User.AutoSize = true;
            this.label_User.Location = new System.Drawing.Point(36, 207);
            this.label_User.Name = "label_User";
            this.label_User.Size = new System.Drawing.Size(53, 12);
            this.label_User.TabIndex = 18;
            this.label_User.Text = "用户名：";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(255, 177);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(72, 21);
            this.textBox_Port.TabIndex = 14;
            this.textBox_Port.Text = "443";
            // 
            // textBox_Pass
            // 
            this.textBox_Pass.Location = new System.Drawing.Point(253, 204);
            this.textBox_Pass.Name = "textBox_Pass";
            this.textBox_Pass.Size = new System.Drawing.Size(74, 21);
            this.textBox_Pass.TabIndex = 12;
            this.textBox_Pass.Text = "admin";
            this.textBox_Pass.UseSystemPasswordChar = true;
            // 
            // textBox_User
            // 
            this.textBox_User.Location = new System.Drawing.Point(102, 204);
            this.textBox_User.Name = "textBox_User";
            this.textBox_User.Size = new System.Drawing.Size(100, 21);
            this.textBox_User.TabIndex = 11;
            this.textBox_User.Text = "admin";
            // 
            // checkBox_SSL
            // 
            this.checkBox_SSL.AutoSize = true;
            this.checkBox_SSL.Checked = true;
            this.checkBox_SSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SSL.Location = new System.Drawing.Point(333, 179);
            this.checkBox_SSL.Name = "checkBox_SSL";
            this.checkBox_SSL.Size = new System.Drawing.Size(42, 16);
            this.checkBox_SSL.TabIndex = 15;
            this.checkBox_SSL.Text = "SSL";
            this.checkBox_SSL.UseVisualStyleBackColor = true;
            // 
            // picLogin
            // 
            this.picLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.picLogin.Image = global::AutoOrderSystem.UI.Properties.Resources.Loginlogo;
            this.picLogin.Location = new System.Drawing.Point(0, 0);
            this.picLogin.Name = "picLogin";
            this.picLogin.Size = new System.Drawing.Size(418, 168);
            this.picLogin.TabIndex = 8;
            this.picLogin.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(169)))), ((int)(((byte)(156)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Location = new System.Drawing.Point(96, 248);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(227, 34);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "登  录";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.pnlTop.Controls.Add(this.lblCapation);
            this.pnlTop.Controls.Add(this.pnlLogo);
            this.pnlTop.Controls.Add(this.btnMin);
            this.pnlTop.Controls.Add(this.btnMax);
            this.pnlTop.Controls.Add(this.btnClose);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(418, 40);
            this.pnlTop.TabIndex = 0;
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlCaption_MouseDown);
            this.pnlTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlCaption_MouseMove);
            this.pnlTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCaption_MouseUp);
            // 
            // lblCapation
            // 
            this.lblCapation.AutoSize = true;
            this.lblCapation.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCapation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCapation.Location = new System.Drawing.Point(42, 8);
            this.lblCapation.Name = "lblCapation";
            this.lblCapation.Size = new System.Drawing.Size(88, 25);
            this.lblCapation.TabIndex = 3;
            this.lblCapation.Text = "系统登录";
            this.lblCapation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackgroundImage = global::AutoOrderSystem.UI.Properties.Resources.logo;
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlLogo.Enabled = false;
            this.pnlLogo.Location = new System.Drawing.Point(6, 4);
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(33, 33);
            this.pnlLogo.TabIndex = 2;
            // 
            // btnMin
            // 
            this.btnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Image = global::AutoOrderSystem.UI.Properties.Resources.Min;
            this.btnMin.Location = new System.Drawing.Point(304, 0);
            this.btnMin.Margin = new System.Windows.Forms.Padding(0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(38, 33);
            this.btnMin.TabIndex = 1;
            this.btnMin.UseVisualStyleBackColor = true;
            this.btnMin.Visible = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnMax
            // 
            this.btnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMax.FlatAppearance.BorderSize = 0;
            this.btnMax.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnMax.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMax.Image = global::AutoOrderSystem.UI.Properties.Resources.Max;
            this.btnMax.Location = new System.Drawing.Point(342, 0);
            this.btnMax.Margin = new System.Windows.Forms.Padding(0);
            this.btnMax.Name = "btnMax";
            this.btnMax.Size = new System.Drawing.Size(38, 33);
            this.btnMax.TabIndex = 1;
            this.btnMax.UseVisualStyleBackColor = true;
            this.btnMax.Visible = false;
            this.btnMax.Click += new System.EventHandler(this.btnMax_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::AutoOrderSystem.UI.Properties.Resources.close;
            this.btnClose.Location = new System.Drawing.Point(380, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(38, 33);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(165)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(422, 355);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(422, 355);
            this.MinimumSize = new System.Drawing.Size(422, 355);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统登录";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(165)))), ((int)(((byte)(231)))));
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.panel1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogin)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnMax;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Panel pnlMain;
        public System.Windows.Forms.Label lblCapation;
        private System.Windows.Forms.CheckBox checkBox_Remember;
        private System.Windows.Forms.ComboBox comboBox_ServerIP;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.Label label_Pass;
        private System.Windows.Forms.Label label_ServerIP;
        private System.Windows.Forms.Label label_User;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.TextBox textBox_Pass;
        private System.Windows.Forms.TextBox textBox_User;
        private System.Windows.Forms.CheckBox checkBox_SSL;
        private System.Windows.Forms.PictureBox picLogin;
        private System.Windows.Forms.Button btnLogin;
    }
}