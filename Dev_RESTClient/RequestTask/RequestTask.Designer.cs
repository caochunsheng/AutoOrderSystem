namespace RequestTask
{
    partial class RequestTask
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
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonGetTaskList = new System.Windows.Forms.Button();
            this.buttonGetTask = new System.Windows.Forms.Button();
            this.buttonGetObject = new System.Windows.Forms.Button();
            this.buttonGetSettings = new System.Windows.Forms.Button();
            this.labelIP = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonGetPicture = new System.Windows.Forms.Button();
            this.timerUI_Update = new System.Windows.Forms.Timer(this.components);
            this.buttonStressTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxThreads = new System.Windows.Forms.TextBox();
            this.textBoxFinished = new System.Windows.Forms.TextBox();
            this.textBoxElapsed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxThreadDelay = new System.Windows.Forms.TextBox();
            this.checkBoxSSL = new System.Windows.Forms.CheckBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSign = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCADM = new System.Windows.Forms.TabPage();
            this.textBoxTrim = new System.Windows.Forms.TextBox();
            this.textBoxSawThickness = new System.Windows.Forms.TextBox();
            this.comboBoxDy = new System.Windows.Forms.ComboBox();
            this.comboBoxDx = new System.Windows.Forms.ComboBox();
            this.comboBoxTy = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBoxTx = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxLoops = new System.Windows.Forms.TextBox();
            this.checkBoxAPI = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxLength = new System.Windows.Forms.TextBox();
            this.labelHeight = new System.Windows.Forms.Label();
            this.labelWidth = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.radioButtonPathImport = new System.Windows.Forms.RadioButton();
            this.radioButtonBinPacking = new System.Windows.Forms.RadioButton();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.tabPageCaxa = new System.Windows.Forms.TabPage();
            this.buttonProjectData = new System.Windows.Forms.Button();
            this.buttonSplit = new System.Windows.Forms.Button();
            this.buttonOrderFile = new System.Windows.Forms.Button();
            this.buttonNextID = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.tabPageSQL = new System.Windows.Forms.TabPage();
            this.buttonLines = new System.Windows.Forms.Button();
            this.dataGridViewSql = new System.Windows.Forms.DataGridView();
            this.listBoxSqlLog = new System.Windows.Forms.ListBox();
            this.buttonNonQuery = new System.Windows.Forms.Button();
            this.buttonQuery = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSQL = new System.Windows.Forms.TextBox();
            this.tabPageDpJia = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.listBoxDpLog = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPageCADM.SuspendLayout();
            this.tabPageCaxa.SuspendLayout();
            this.tabPageSQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSql)).BeginInit();
            this.tabPageDpJia.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxLog
            // 
            this.listBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 12;
            this.listBoxLog.Location = new System.Drawing.Point(6, 78);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(897, 376);
            this.listBoxLog.TabIndex = 0;
            // 
            // buttonGetTaskList
            // 
            this.buttonGetTaskList.Location = new System.Drawing.Point(6, 6);
            this.buttonGetTaskList.Name = "buttonGetTaskList";
            this.buttonGetTaskList.Size = new System.Drawing.Size(117, 30);
            this.buttonGetTaskList.TabIndex = 1;
            this.buttonGetTaskList.Text = "Get Task List";
            this.buttonGetTaskList.UseVisualStyleBackColor = true;
            this.buttonGetTaskList.Click += new System.EventHandler(this.buttonTaskList_Click);
            // 
            // buttonGetTask
            // 
            this.buttonGetTask.Location = new System.Drawing.Point(127, 6);
            this.buttonGetTask.Name = "buttonGetTask";
            this.buttonGetTask.Size = new System.Drawing.Size(117, 30);
            this.buttonGetTask.TabIndex = 1;
            this.buttonGetTask.Text = "StartTask";
            this.buttonGetTask.UseVisualStyleBackColor = true;
            this.buttonGetTask.Click += new System.EventHandler(this.buttonGetTask_Click);
            // 
            // buttonGetObject
            // 
            this.buttonGetObject.Location = new System.Drawing.Point(248, 6);
            this.buttonGetObject.Name = "buttonGetObject";
            this.buttonGetObject.Size = new System.Drawing.Size(117, 30);
            this.buttonGetObject.TabIndex = 1;
            this.buttonGetObject.Text = "GetObjectData";
            this.buttonGetObject.UseVisualStyleBackColor = true;
            this.buttonGetObject.Click += new System.EventHandler(this.buttonGetObject_Click);
            // 
            // buttonGetSettings
            // 
            this.buttonGetSettings.Location = new System.Drawing.Point(369, 6);
            this.buttonGetSettings.Name = "buttonGetSettings";
            this.buttonGetSettings.Size = new System.Drawing.Size(117, 30);
            this.buttonGetSettings.TabIndex = 1;
            this.buttonGetSettings.Text = "GetSettingFile";
            this.buttonGetSettings.UseVisualStyleBackColor = true;
            this.buttonGetSettings.Click += new System.EventHandler(this.buttonGetSettings_Click);
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(12, 15);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(59, 12);
            this.labelIP.TabIndex = 2;
            this.labelIP.Text = "服务器 IP";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(174, 16);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 12);
            this.labelPort.TabIndex = 3;
            this.labelPort.Text = "端口";
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(77, 11);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(91, 21);
            this.textBoxServerIP.TabIndex = 4;
            this.textBoxServerIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(209, 11);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(40, 21);
            this.textBoxPort.TabIndex = 5;
            this.textBoxPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonGetPicture
            // 
            this.buttonGetPicture.Location = new System.Drawing.Point(490, 6);
            this.buttonGetPicture.Name = "buttonGetPicture";
            this.buttonGetPicture.Size = new System.Drawing.Size(117, 30);
            this.buttonGetPicture.TabIndex = 6;
            this.buttonGetPicture.Text = "GetTaskPic";
            this.buttonGetPicture.UseVisualStyleBackColor = true;
            this.buttonGetPicture.Click += new System.EventHandler(this.buttonGetPicture_Click);
            // 
            // timerUI_Update
            // 
            this.timerUI_Update.Interval = 200;
            this.timerUI_Update.Tick += new System.EventHandler(this.timerUI_Update_Tick);
            // 
            // buttonStressTest
            // 
            this.buttonStressTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStressTest.Location = new System.Drawing.Point(813, 9);
            this.buttonStressTest.Name = "buttonStressTest";
            this.buttonStressTest.Size = new System.Drawing.Size(116, 27);
            this.buttonStressTest.TabIndex = 7;
            this.buttonStressTest.Text = "多线程并发测试";
            this.buttonStressTest.UseVisualStyleBackColor = true;
            this.buttonStressTest.Click += new System.EventHandler(this.buttonStressTest_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(577, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "线程数";
            // 
            // textBoxThreads
            // 
            this.textBoxThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxThreads.Location = new System.Drawing.Point(623, 11);
            this.textBoxThreads.Name = "textBoxThreads";
            this.textBoxThreads.Size = new System.Drawing.Size(30, 21);
            this.textBoxThreads.TabIndex = 9;
            this.textBoxThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxFinished
            // 
            this.textBoxFinished.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFinished.Location = new System.Drawing.Point(8, 468);
            this.textBoxFinished.Name = "textBoxFinished";
            this.textBoxFinished.ReadOnly = true;
            this.textBoxFinished.Size = new System.Drawing.Size(114, 21);
            this.textBoxFinished.TabIndex = 10;
            this.textBoxFinished.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxElapsed
            // 
            this.textBoxElapsed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxElapsed.Location = new System.Drawing.Point(129, 468);
            this.textBoxElapsed.Name = "textBoxElapsed";
            this.textBoxElapsed.ReadOnly = true;
            this.textBoxElapsed.Size = new System.Drawing.Size(114, 21);
            this.textBoxElapsed.TabIndex = 10;
            this.textBoxElapsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(666, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "线程附加延时(ms)";
            // 
            // textBoxThreadDelay
            // 
            this.textBoxThreadDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxThreadDelay.Location = new System.Drawing.Point(773, 11);
            this.textBoxThreadDelay.Name = "textBoxThreadDelay";
            this.textBoxThreadDelay.Size = new System.Drawing.Size(34, 21);
            this.textBoxThreadDelay.TabIndex = 9;
            this.textBoxThreadDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxSSL
            // 
            this.checkBoxSSL.AutoSize = true;
            this.checkBoxSSL.Location = new System.Drawing.Point(261, 14);
            this.checkBoxSSL.Name = "checkBoxSSL";
            this.checkBoxSSL.Size = new System.Drawing.Size(42, 16);
            this.checkBoxSSL.TabIndex = 11;
            this.checkBoxSSL.Text = "SSL";
            this.checkBoxSSL.UseVisualStyleBackColor = true;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(77, 43);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(91, 21);
            this.textBoxUser.TabIndex = 13;
            this.textBoxUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "用户名";
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(210, 43);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.Size = new System.Drawing.Size(91, 21);
            this.textBoxPass.TabIndex = 15;
            this.textBoxPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "口令";
            // 
            // buttonSign
            // 
            this.buttonSign.Location = new System.Drawing.Point(310, 43);
            this.buttonSign.Name = "buttonSign";
            this.buttonSign.Size = new System.Drawing.Size(91, 23);
            this.buttonSign.TabIndex = 16;
            this.buttonSign.Text = "登 录";
            this.buttonSign.UseVisualStyleBackColor = true;
            this.buttonSign.Click += new System.EventHandler(this.buttonSign_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(766, 471);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(137, 12);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://localhost:9443";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(6, 42);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(117, 30);
            this.buttonUpload.TabIndex = 18;
            this.buttonUpload.Text = "上载料单";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageCADM);
            this.tabControl1.Controls.Add(this.tabPageCaxa);
            this.tabControl1.Controls.Add(this.tabPageSQL);
            this.tabControl1.Controls.Add(this.tabPageDpJia);
            this.tabControl1.Location = new System.Drawing.Point(12, 75);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(917, 521);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPageCADM
            // 
            this.tabPageCADM.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageCADM.Controls.Add(this.textBoxTrim);
            this.tabPageCADM.Controls.Add(this.textBoxSawThickness);
            this.tabPageCADM.Controls.Add(this.comboBoxDy);
            this.tabPageCADM.Controls.Add(this.comboBoxDx);
            this.tabPageCADM.Controls.Add(this.comboBoxTy);
            this.tabPageCADM.Controls.Add(this.label12);
            this.tabPageCADM.Controls.Add(this.comboBoxTx);
            this.tabPageCADM.Controls.Add(this.label11);
            this.tabPageCADM.Controls.Add(this.label10);
            this.tabPageCADM.Controls.Add(this.label9);
            this.tabPageCADM.Controls.Add(this.label8);
            this.tabPageCADM.Controls.Add(this.textBoxLoops);
            this.tabPageCADM.Controls.Add(this.checkBoxAPI);
            this.tabPageCADM.Controls.Add(this.label7);
            this.tabPageCADM.Controls.Add(this.label6);
            this.tabPageCADM.Controls.Add(this.buttonBrowse);
            this.tabPageCADM.Controls.Add(this.textBoxHeight);
            this.tabPageCADM.Controls.Add(this.textBoxWidth);
            this.tabPageCADM.Controls.Add(this.textBoxLength);
            this.tabPageCADM.Controls.Add(this.labelHeight);
            this.tabPageCADM.Controls.Add(this.labelWidth);
            this.tabPageCADM.Controls.Add(this.labelLength);
            this.tabPageCADM.Controls.Add(this.radioButtonPathImport);
            this.tabPageCADM.Controls.Add(this.radioButtonBinPacking);
            this.tabPageCADM.Controls.Add(this.textBoxStatus);
            this.tabPageCADM.Controls.Add(this.progressBar);
            this.tabPageCADM.Controls.Add(this.textBoxFile);
            this.tabPageCADM.Controls.Add(this.buttonSubmit);
            this.tabPageCADM.Location = new System.Drawing.Point(4, 22);
            this.tabPageCADM.Name = "tabPageCADM";
            this.tabPageCADM.Size = new System.Drawing.Size(909, 495);
            this.tabPageCADM.TabIndex = 2;
            this.tabPageCADM.Text = "CADM接口";
            // 
            // textBoxTrim
            // 
            this.textBoxTrim.Location = new System.Drawing.Point(300, 126);
            this.textBoxTrim.Name = "textBoxTrim";
            this.textBoxTrim.Size = new System.Drawing.Size(59, 21);
            this.textBoxTrim.TabIndex = 15;
            // 
            // textBoxSawThickness
            // 
            this.textBoxSawThickness.Location = new System.Drawing.Point(118, 126);
            this.textBoxSawThickness.Name = "textBoxSawThickness";
            this.textBoxSawThickness.Size = new System.Drawing.Size(59, 21);
            this.textBoxSawThickness.TabIndex = 15;
            // 
            // comboBoxDy
            // 
            this.comboBoxDy.FormattingEnabled = true;
            this.comboBoxDy.Location = new System.Drawing.Point(189, 95);
            this.comboBoxDy.Name = "comboBoxDy";
            this.comboBoxDy.Size = new System.Drawing.Size(50, 20);
            this.comboBoxDy.TabIndex = 14;
            // 
            // comboBoxDx
            // 
            this.comboBoxDx.FormattingEnabled = true;
            this.comboBoxDx.Location = new System.Drawing.Point(189, 63);
            this.comboBoxDx.Name = "comboBoxDx";
            this.comboBoxDx.Size = new System.Drawing.Size(50, 20);
            this.comboBoxDx.TabIndex = 14;
            // 
            // comboBoxTy
            // 
            this.comboBoxTy.FormattingEnabled = true;
            this.comboBoxTy.Location = new System.Drawing.Point(118, 95);
            this.comboBoxTy.Name = "comboBoxTy";
            this.comboBoxTy.Size = new System.Drawing.Size(59, 20);
            this.comboBoxTy.TabIndex = 14;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(197, 131);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "板材齐边宽度(mm)";
            // 
            // comboBoxTx
            // 
            this.comboBoxTx.FormattingEnabled = true;
            this.comboBoxTx.Location = new System.Drawing.Point(118, 63);
            this.comboBoxTx.Name = "comboBoxTx";
            this.comboBoxTx.Size = new System.Drawing.Size(59, 20);
            this.comboBoxTx.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(37, 130);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 13;
            this.label11.Text = "锯片厚度(mm)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 99);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "Y方向垂直切割线";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "X方向水平切割线";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(443, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "循环次数";
            // 
            // textBoxLoops
            // 
            this.textBoxLoops.Location = new System.Drawing.Point(502, 27);
            this.textBoxLoops.Name = "textBoxLoops";
            this.textBoxLoops.Size = new System.Drawing.Size(72, 21);
            this.textBoxLoops.TabIndex = 11;
            this.textBoxLoops.Text = "1";
            this.textBoxLoops.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBoxAPI
            // 
            this.checkBoxAPI.AutoSize = true;
            this.checkBoxAPI.Location = new System.Drawing.Point(263, 27);
            this.checkBoxAPI.Name = "checkBoxAPI";
            this.checkBoxAPI.Size = new System.Drawing.Size(72, 16);
            this.checkBoxAPI.TabIndex = 10;
            this.checkBoxAPI.Text = "Test API";
            this.checkBoxAPI.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(52, 226);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "任务状态";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "任务进度";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(30, 155);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 32);
            this.buttonBrowse.TabIndex = 7;
            this.buttonBrowse.Text = "选择文件";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(626, 61);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(72, 21);
            this.textBoxHeight.TabIndex = 6;
            this.textBoxHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(502, 61);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(72, 21);
            this.textBoxWidth.TabIndex = 6;
            this.textBoxWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxLength
            // 
            this.textBoxLength.Location = new System.Drawing.Point(380, 61);
            this.textBoxLength.Name = "textBoxLength";
            this.textBoxLength.Size = new System.Drawing.Size(72, 21);
            this.textBoxLength.TabIndex = 6;
            this.textBoxLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(590, 64);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(29, 12);
            this.labelHeight.TabIndex = 5;
            this.labelHeight.Text = "厚度";
            // 
            // labelWidth
            // 
            this.labelWidth.AutoSize = true;
            this.labelWidth.Location = new System.Drawing.Point(467, 64);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Size = new System.Drawing.Size(29, 12);
            this.labelWidth.TabIndex = 5;
            this.labelWidth.Text = "宽度";
            // 
            // labelLength
            // 
            this.labelLength.AutoSize = true;
            this.labelLength.Location = new System.Drawing.Point(344, 64);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(29, 12);
            this.labelLength.TabIndex = 5;
            this.labelLength.Text = "长度";
            // 
            // radioButtonPathImport
            // 
            this.radioButtonPathImport.AutoSize = true;
            this.radioButtonPathImport.Location = new System.Drawing.Point(151, 27);
            this.radioButtonPathImport.Name = "radioButtonPathImport";
            this.radioButtonPathImport.Size = new System.Drawing.Size(71, 16);
            this.radioButtonPathImport.TabIndex = 4;
            this.radioButtonPathImport.TabStop = true;
            this.radioButtonPathImport.Text = "路径导入";
            this.radioButtonPathImport.UseVisualStyleBackColor = true;
            this.radioButtonPathImport.CheckedChanged += new System.EventHandler(this.radioButtonPathImport_CheckedChanged);
            // 
            // radioButtonBinPacking
            // 
            this.radioButtonBinPacking.AutoSize = true;
            this.radioButtonBinPacking.Location = new System.Drawing.Point(31, 27);
            this.radioButtonBinPacking.Name = "radioButtonBinPacking";
            this.radioButtonBinPacking.Size = new System.Drawing.Size(71, 16);
            this.radioButtonBinPacking.TabIndex = 4;
            this.radioButtonBinPacking.TabStop = true;
            this.radioButtonBinPacking.Text = "拼料料单";
            this.radioButtonBinPacking.UseVisualStyleBackColor = true;
            this.radioButtonBinPacking.CheckedChanged += new System.EventHandler(this.radioButtonBinPacking_CheckedChanged);
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStatus.Location = new System.Drawing.Point(112, 223);
            this.textBoxStatus.Multiline = true;
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStatus.Size = new System.Drawing.Size(702, 258);
            this.textBoxStatus.TabIndex = 3;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(112, 190);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(702, 23);
            this.progressBar.TabIndex = 2;
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(112, 160);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.ReadOnly = true;
            this.textBoxFile.Size = new System.Drawing.Size(702, 21);
            this.textBoxFile.TabIndex = 1;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(732, 27);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(82, 55);
            this.buttonSubmit.TabIndex = 0;
            this.buttonSubmit.Text = "提交任务";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // tabPageCaxa
            // 
            this.tabPageCaxa.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageCaxa.Controls.Add(this.buttonProjectData);
            this.tabPageCaxa.Controls.Add(this.buttonSplit);
            this.tabPageCaxa.Controls.Add(this.buttonOrderFile);
            this.tabPageCaxa.Controls.Add(this.buttonNextID);
            this.tabPageCaxa.Controls.Add(this.buttonOrder);
            this.tabPageCaxa.Controls.Add(this.listBoxLog);
            this.tabPageCaxa.Controls.Add(this.buttonUpload);
            this.tabPageCaxa.Controls.Add(this.textBoxElapsed);
            this.tabPageCaxa.Controls.Add(this.linkLabel1);
            this.tabPageCaxa.Controls.Add(this.textBoxFinished);
            this.tabPageCaxa.Controls.Add(this.buttonGetTaskList);
            this.tabPageCaxa.Controls.Add(this.buttonGetTask);
            this.tabPageCaxa.Controls.Add(this.buttonGetObject);
            this.tabPageCaxa.Controls.Add(this.buttonGetSettings);
            this.tabPageCaxa.Controls.Add(this.buttonGetPicture);
            this.tabPageCaxa.Location = new System.Drawing.Point(4, 22);
            this.tabPageCaxa.Name = "tabPageCaxa";
            this.tabPageCaxa.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCaxa.Size = new System.Drawing.Size(909, 495);
            this.tabPageCaxa.TabIndex = 0;
            this.tabPageCaxa.Text = "CAXA 接口";
            // 
            // buttonProjectData
            // 
            this.buttonProjectData.Location = new System.Drawing.Point(613, 42);
            this.buttonProjectData.Name = "buttonProjectData";
            this.buttonProjectData.Size = new System.Drawing.Size(115, 30);
            this.buttonProjectData.TabIndex = 23;
            this.buttonProjectData.Text = "CreateProjectData";
            this.buttonProjectData.UseVisualStyleBackColor = true;
            this.buttonProjectData.Click += new System.EventHandler(this.buttonProjectData_Click);
            // 
            // buttonSplit
            // 
            this.buttonSplit.Location = new System.Drawing.Point(492, 42);
            this.buttonSplit.Name = "buttonSplit";
            this.buttonSplit.Size = new System.Drawing.Size(115, 30);
            this.buttonSplit.TabIndex = 22;
            this.buttonSplit.Text = "模型转部件";
            this.buttonSplit.UseVisualStyleBackColor = true;
            this.buttonSplit.Click += new System.EventHandler(this.buttonSplit_Click);
            // 
            // buttonOrderFile
            // 
            this.buttonOrderFile.Location = new System.Drawing.Point(369, 42);
            this.buttonOrderFile.Name = "buttonOrderFile";
            this.buttonOrderFile.Size = new System.Drawing.Size(117, 30);
            this.buttonOrderFile.TabIndex = 21;
            this.buttonOrderFile.Text = "获取订单文件";
            this.buttonOrderFile.UseVisualStyleBackColor = true;
            this.buttonOrderFile.Click += new System.EventHandler(this.buttonOrderFile_Click);
            // 
            // buttonNextID
            // 
            this.buttonNextID.Location = new System.Drawing.Point(248, 42);
            this.buttonNextID.Name = "buttonNextID";
            this.buttonNextID.Size = new System.Drawing.Size(117, 30);
            this.buttonNextID.TabIndex = 20;
            this.buttonNextID.Text = "获取 ID";
            this.buttonNextID.UseVisualStyleBackColor = true;
            this.buttonNextID.Click += new System.EventHandler(this.buttonNextID_Click);
            // 
            // buttonOrder
            // 
            this.buttonOrder.Location = new System.Drawing.Point(127, 42);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(117, 30);
            this.buttonOrder.TabIndex = 19;
            this.buttonOrder.Text = "上载订单";
            this.buttonOrder.UseVisualStyleBackColor = true;
            this.buttonOrder.Click += new System.EventHandler(this.buttonOrder_Click);
            // 
            // tabPageSQL
            // 
            this.tabPageSQL.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageSQL.Controls.Add(this.buttonLines);
            this.tabPageSQL.Controls.Add(this.dataGridViewSql);
            this.tabPageSQL.Controls.Add(this.listBoxSqlLog);
            this.tabPageSQL.Controls.Add(this.buttonNonQuery);
            this.tabPageSQL.Controls.Add(this.buttonQuery);
            this.tabPageSQL.Controls.Add(this.label5);
            this.tabPageSQL.Controls.Add(this.textBoxSQL);
            this.tabPageSQL.Location = new System.Drawing.Point(4, 22);
            this.tabPageSQL.Name = "tabPageSQL";
            this.tabPageSQL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSQL.Size = new System.Drawing.Size(909, 495);
            this.tabPageSQL.TabIndex = 1;
            this.tabPageSQL.Text = "SQL";
            // 
            // buttonLines
            // 
            this.buttonLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLines.Location = new System.Drawing.Point(786, 17);
            this.buttonLines.Name = "buttonLines";
            this.buttonLines.Size = new System.Drawing.Size(104, 24);
            this.buttonLines.TabIndex = 20;
            this.buttonLines.Text = "产线清单";
            this.buttonLines.UseVisualStyleBackColor = true;
            this.buttonLines.Click += new System.EventHandler(this.buttonLines_Click);
            // 
            // dataGridViewSql
            // 
            this.dataGridViewSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSql.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewSql.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSql.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridViewSql.Location = new System.Drawing.Point(16, 83);
            this.dataGridViewSql.MultiSelect = false;
            this.dataGridViewSql.Name = "dataGridViewSql";
            this.dataGridViewSql.ReadOnly = true;
            this.dataGridViewSql.RowTemplate.Height = 23;
            this.dataGridViewSql.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSql.Size = new System.Drawing.Size(751, 217);
            this.dataGridViewSql.TabIndex = 5;
            // 
            // listBoxSqlLog
            // 
            this.listBoxSqlLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxSqlLog.FormattingEnabled = true;
            this.listBoxSqlLog.ItemHeight = 12;
            this.listBoxSqlLog.Location = new System.Drawing.Point(16, 306);
            this.listBoxSqlLog.Name = "listBoxSqlLog";
            this.listBoxSqlLog.Size = new System.Drawing.Size(751, 88);
            this.listBoxSqlLog.TabIndex = 4;
            // 
            // buttonNonQuery
            // 
            this.buttonNonQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNonQuery.Location = new System.Drawing.Point(650, 17);
            this.buttonNonQuery.Name = "buttonNonQuery";
            this.buttonNonQuery.Size = new System.Drawing.Size(117, 27);
            this.buttonNonQuery.TabIndex = 2;
            this.buttonNonQuery.Text = "Non Query";
            this.buttonNonQuery.UseVisualStyleBackColor = true;
            this.buttonNonQuery.Click += new System.EventHandler(this.buttonNonQuery_Click);
            // 
            // buttonQuery
            // 
            this.buttonQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuery.Location = new System.Drawing.Point(650, 50);
            this.buttonQuery.Name = "buttonQuery";
            this.buttonQuery.Size = new System.Drawing.Size(117, 27);
            this.buttonQuery.TabIndex = 2;
            this.buttonQuery.Text = "Query";
            this.buttonQuery.UseVisualStyleBackColor = true;
            this.buttonQuery.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "SQL";
            // 
            // textBoxSQL
            // 
            this.textBoxSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSQL.Location = new System.Drawing.Point(43, 17);
            this.textBoxSQL.Multiline = true;
            this.textBoxSQL.Name = "textBoxSQL";
            this.textBoxSQL.Size = new System.Drawing.Size(589, 60);
            this.textBoxSQL.TabIndex = 0;
            // 
            // tabPageDpJia
            // 
            this.tabPageDpJia.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPageDpJia.Controls.Add(this.listBoxDpLog);
            this.tabPageDpJia.Controls.Add(this.label13);
            this.tabPageDpJia.Controls.Add(this.buttonSend);
            this.tabPageDpJia.Location = new System.Drawing.Point(4, 22);
            this.tabPageDpJia.Name = "tabPageDpJia";
            this.tabPageDpJia.Size = new System.Drawing.Size(909, 495);
            this.tabPageDpJia.TabIndex = 3;
            this.tabPageDpJia.Text = "搭配家";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(34, 83);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "日志";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(25, 24);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(93, 39);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(436, 43);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(91, 23);
            this.buttonTest.TabIndex = 20;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // listBoxDpLog
            // 
            this.listBoxDpLog.FormattingEnabled = true;
            this.listBoxDpLog.ItemHeight = 12;
            this.listBoxDpLog.Location = new System.Drawing.Point(70, 83);
            this.listBoxDpLog.Name = "listBoxDpLog";
            this.listBoxDpLog.Size = new System.Drawing.Size(759, 316);
            this.listBoxDpLog.TabIndex = 3;
            // 
            // RequestTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 608);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonSign);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxSSL);
            this.Controls.Add(this.textBoxThreadDelay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxThreads);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonStressTest);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxServerIP);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelIP);
            this.Name = "RequestTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "云服务接口测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RequestTask_FormClosing);
            this.Load += new System.EventHandler(this.RequestTask_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageCADM.ResumeLayout(false);
            this.tabPageCADM.PerformLayout();
            this.tabPageCaxa.ResumeLayout(false);
            this.tabPageCaxa.PerformLayout();
            this.tabPageSQL.ResumeLayout(false);
            this.tabPageSQL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSql)).EndInit();
            this.tabPageDpJia.ResumeLayout(false);
            this.tabPageDpJia.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonGetTaskList;
        private System.Windows.Forms.Button buttonGetTask;
        private System.Windows.Forms.Button buttonGetObject;
        private System.Windows.Forms.Button buttonGetSettings;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonGetPicture;
        private System.Windows.Forms.Timer timerUI_Update;
        private System.Windows.Forms.Button buttonStressTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxThreads;
        private System.Windows.Forms.TextBox textBoxFinished;
        private System.Windows.Forms.TextBox textBoxElapsed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxThreadDelay;
        private System.Windows.Forms.CheckBox checkBoxSSL;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSign;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageCaxa;
        private System.Windows.Forms.TabPage tabPageSQL;
        private System.Windows.Forms.ListBox listBoxSqlLog;
        private System.Windows.Forms.Button buttonQuery;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSQL;
        private System.Windows.Forms.Button buttonNonQuery;
        private System.Windows.Forms.DataGridView dataGridViewSql;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonOrder;
        private System.Windows.Forms.Button buttonLines;
        private System.Windows.Forms.Button buttonNextID;
        private System.Windows.Forms.Button buttonOrderFile;
        private System.Windows.Forms.Button buttonSplit;
        private System.Windows.Forms.Button buttonProjectData;
        private System.Windows.Forms.TabPage tabPageCADM;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxLength;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Label labelWidth;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.RadioButton radioButtonPathImport;
        private System.Windows.Forms.RadioButton radioButtonBinPacking;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.CheckBox checkBoxAPI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxLoops;
        private System.Windows.Forms.ComboBox comboBoxDy;
        private System.Windows.Forms.ComboBox comboBoxDx;
        private System.Windows.Forms.ComboBox comboBoxTy;
        private System.Windows.Forms.ComboBox comboBoxTx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxTrim;
        private System.Windows.Forms.TextBox textBoxSawThickness;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage tabPageDpJia;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ListBox listBoxDpLog;
    }
}

