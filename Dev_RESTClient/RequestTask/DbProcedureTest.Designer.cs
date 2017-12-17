namespace RequestTask
{
    partial class DbProcedureTest
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
            this.textBoxDbServer = new System.Windows.Forms.TextBox();
            this.textBoxDbUser = new System.Windows.Forms.TextBox();
            this.textBoxDbPass = new System.Windows.Forms.TextBox();
            this.buttonGetWorkOrder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxGCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxDbServer
            // 
            this.textBoxDbServer.Location = new System.Drawing.Point(139, 22);
            this.textBoxDbServer.Name = "textBoxDbServer";
            this.textBoxDbServer.Size = new System.Drawing.Size(174, 21);
            this.textBoxDbServer.TabIndex = 0;
            // 
            // textBoxDbUser
            // 
            this.textBoxDbUser.Location = new System.Drawing.Point(139, 50);
            this.textBoxDbUser.Name = "textBoxDbUser";
            this.textBoxDbUser.Size = new System.Drawing.Size(174, 21);
            this.textBoxDbUser.TabIndex = 1;
            // 
            // textBoxDbPass
            // 
            this.textBoxDbPass.Location = new System.Drawing.Point(139, 78);
            this.textBoxDbPass.Name = "textBoxDbPass";
            this.textBoxDbPass.Size = new System.Drawing.Size(174, 21);
            this.textBoxDbPass.TabIndex = 2;
            // 
            // buttonGetWorkOrder
            // 
            this.buttonGetWorkOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetWorkOrder.Location = new System.Drawing.Point(541, 39);
            this.buttonGetWorkOrder.Name = "buttonGetWorkOrder";
            this.buttonGetWorkOrder.Size = new System.Drawing.Size(129, 41);
            this.buttonGetWorkOrder.TabIndex = 3;
            this.buttonGetWorkOrder.Text = "GetWorkOrder";
            this.buttonGetWorkOrder.UseVisualStyleBackColor = true;
            this.buttonGetWorkOrder.Click += new System.EventHandler(this.buttonGetWorkOrder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "MES 数据库服务器";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "用户名";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "口令";
            // 
            // textBoxGCode
            // 
            this.textBoxGCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGCode.Location = new System.Drawing.Point(12, 118);
            this.textBoxGCode.Multiline = true;
            this.textBoxGCode.Name = "textBoxGCode";
            this.textBoxGCode.ReadOnly = true;
            this.textBoxGCode.Size = new System.Drawing.Size(678, 292);
            this.textBoxGCode.TabIndex = 7;
            // 
            // DbProcedureTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 422);
            this.Controls.Add(this.textBoxGCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGetWorkOrder);
            this.Controls.Add(this.textBoxDbPass);
            this.Controls.Add(this.textBoxDbUser);
            this.Controls.Add(this.textBoxDbServer);
            this.Name = "DbProcedureTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DbProcedureTest";
            this.Load += new System.EventHandler(this.DbProcedureTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDbServer;
        private System.Windows.Forms.TextBox textBoxDbUser;
        private System.Windows.Forms.TextBox textBoxDbPass;
        private System.Windows.Forms.Button buttonGetWorkOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxGCode;
    }
}