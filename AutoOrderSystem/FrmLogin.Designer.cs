namespace AutoOrderSystem
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.comboBox_ServerIP = new System.Windows.Forms.ComboBox();
            this.label_Port = new System.Windows.Forms.Label();
            this.label_Pass = new System.Windows.Forms.Label();
            this.label_ServerIP = new System.Windows.Forms.Label();
            this.label_User = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.textBox_Pass = new System.Windows.Forms.TextBox();
            this.textBox_User = new System.Windows.Forms.TextBox();
            this.checkBox_SSL = new System.Windows.Forms.CheckBox();
            this.button_Login = new System.Windows.Forms.Button();
            this.timer_IsLogined = new System.Windows.Forms.Timer(this.components);
            this.checkBox_Remember = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_ServerIP
            // 
            this.comboBox_ServerIP.FormattingEnabled = true;
            this.comboBox_ServerIP.Location = new System.Drawing.Point(94, 232);
            this.comboBox_ServerIP.Name = "comboBox_ServerIP";
            this.comboBox_ServerIP.Size = new System.Drawing.Size(120, 20);
            this.comboBox_ServerIP.TabIndex = 2;
            // 
            // label_Port
            // 
            this.label_Port.AutoSize = true;
            this.label_Port.Location = new System.Drawing.Point(220, 235);
            this.label_Port.Name = "label_Port";
            this.label_Port.Size = new System.Drawing.Size(41, 12);
            this.label_Port.TabIndex = 9;
            this.label_Port.Text = "端口：";
            // 
            // label_Pass
            // 
            this.label_Pass.AutoSize = true;
            this.label_Pass.Location = new System.Drawing.Point(220, 262);
            this.label_Pass.Name = "label_Pass";
            this.label_Pass.Size = new System.Drawing.Size(41, 12);
            this.label_Pass.TabIndex = 10;
            this.label_Pass.Text = "口令：";
            // 
            // label_ServerIP
            // 
            this.label_ServerIP.AutoSize = true;
            this.label_ServerIP.Location = new System.Drawing.Point(29, 235);
            this.label_ServerIP.Name = "label_ServerIP";
            this.label_ServerIP.Size = new System.Drawing.Size(65, 12);
            this.label_ServerIP.TabIndex = 7;
            this.label_ServerIP.Text = "服务器IP：";
            // 
            // label_User
            // 
            this.label_User.AutoSize = true;
            this.label_User.Location = new System.Drawing.Point(35, 262);
            this.label_User.Name = "label_User";
            this.label_User.Size = new System.Drawing.Size(53, 12);
            this.label_User.TabIndex = 8;
            this.label_User.Text = "用户名：";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(267, 232);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(72, 21);
            this.textBox_Port.TabIndex = 3;
            this.textBox_Port.Text = "9443";
            // 
            // textBox_Pass
            // 
            this.textBox_Pass.Location = new System.Drawing.Point(265, 259);
            this.textBox_Pass.Name = "textBox_Pass";
            this.textBox_Pass.Size = new System.Drawing.Size(74, 21);
            this.textBox_Pass.TabIndex = 1;
            this.textBox_Pass.Text = "admin";
            this.textBox_Pass.UseSystemPasswordChar = true;
            // 
            // textBox_User
            // 
            this.textBox_User.Location = new System.Drawing.Point(94, 259);
            this.textBox_User.Name = "textBox_User";
            this.textBox_User.Size = new System.Drawing.Size(120, 21);
            this.textBox_User.TabIndex = 0;
            this.textBox_User.Text = "admin";
            // 
            // checkBox_SSL
            // 
            this.checkBox_SSL.AutoSize = true;
            this.checkBox_SSL.Checked = true;
            this.checkBox_SSL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_SSL.Location = new System.Drawing.Point(345, 234);
            this.checkBox_SSL.Name = "checkBox_SSL";
            this.checkBox_SSL.Size = new System.Drawing.Size(42, 16);
            this.checkBox_SSL.TabIndex = 4;
            this.checkBox_SSL.Text = "SSL";
            this.checkBox_SSL.UseVisualStyleBackColor = true;
            // 
            // button_Login
            // 
            this.button_Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_Login.Location = new System.Drawing.Point(82, 299);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(257, 24);
            this.button_Login.TabIndex = 6;
            this.button_Login.Text = "登录";
            this.button_Login.UseVisualStyleBackColor = true;
            this.button_Login.Click += new System.EventHandler(this.button_Login_Click);
            // 
            // timer_IsLogined
            // 
            this.timer_IsLogined.Interval = 5000;
            // 
            // checkBox_Remember
            // 
            this.checkBox_Remember.AutoSize = true;
            this.checkBox_Remember.Location = new System.Drawing.Point(345, 261);
            this.checkBox_Remember.Name = "checkBox_Remember";
            this.checkBox_Remember.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Remember.TabIndex = 5;
            this.checkBox_Remember.Text = "记住";
            this.checkBox_Remember.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AutoOrderSystem.Properties.Resources.Loginlogo;
            this.pictureBox1.Location = new System.Drawing.Point(7, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(418, 170);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.button_Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CanResize = false;
            this.CaptionBackColorBottom = System.Drawing.SystemColors.ButtonHighlight;
            this.CaptionBackColorTop = System.Drawing.SystemColors.ActiveCaption;
            this.CaptionFont = new System.Drawing.Font("华文仿宋", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CaptionHeight = 30;
            this.ClientSize = new System.Drawing.Size(432, 358);
            this.CloseBoxSize = new System.Drawing.Size(32, 30);
            this.CloseDownBack = global::AutoOrderSystem.Properties.Resources.Close_3;
            this.CloseMouseBack = global::AutoOrderSystem.Properties.Resources.Close_2;
            this.CloseNormlBack = global::AutoOrderSystem.Properties.Resources.Close_1;
            this.Controls.Add(this.checkBox_Remember);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBox_ServerIP);
            this.Controls.Add(this.label_Port);
            this.Controls.Add(this.label_Pass);
            this.Controls.Add(this.label_ServerIP);
            this.Controls.Add(this.label_User);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.textBox_Pass);
            this.Controls.Add(this.textBox_User);
            this.Controls.Add(this.checkBox_SSL);
            this.Controls.Add(this.button_Login);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ICoOffset = new System.Drawing.Point(10, 0);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(432, 358);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(432, 358);
            this.Name = "FrmLogin";
            this.Radius = 0;
            this.RoundStyle = CCWin.SkinClass.RoundStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.TitleCenter = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox_ServerIP;
        private System.Windows.Forms.Label label_Port;
        private System.Windows.Forms.Label label_Pass;
        private System.Windows.Forms.Label label_ServerIP;
        private System.Windows.Forms.Label label_User;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.TextBox textBox_Pass;
        private System.Windows.Forms.TextBox textBox_User;
        private System.Windows.Forms.CheckBox checkBox_SSL;
        private System.Windows.Forms.Button button_Login;
        private System.Windows.Forms.Timer timer_IsLogined;
        private System.Windows.Forms.CheckBox checkBox_Remember;

    }
}