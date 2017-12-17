using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Collections.Specialized;
using RESTClient;
using Threads;
using DbControl;
using Newtonsoft.Json.Linq;
using CaxaAPI;

namespace RequestTask
{
    public partial class RequestTask : Form
    {
        public const int LOG_INFO = 1;
        public const int LOG_WARNING = 2;
        public const int LOG_FATAL = 3;

        private static string _CodeBase = AppDomain.CurrentDomain.BaseDirectory;
        private static ConcurrentQueue<string> _logList = new ConcurrentQueue<string>();
        private static ThreadScheduler<WebRequestSession> _StressTester = null;

        private Boolean _Running;
        private int _SubmitCountdown;

        private WebRequestSession _reqSession;
        private String _TaskType = "";
        private String _LastTaskID = "";
        private String _TaskID = "";
        private int _CadmTaskID = -1;

        private static int FinishedFiles = 0;

        public RequestTask()
        {
            InitializeComponent();

            _reqSession = new WebRequestSession();
        }

        private void RequestTask_Load(object sender, EventArgs e)
        {
            _Running = false;
            _SubmitCountdown = 0;

            this.timerUI_Update.Enabled = true;

            this.textBoxServerIP.Text = "localhost";
            this.textBoxPort.Text = "9443";
            this.textBoxThreads.Text = "4";
            this.checkBoxSSL.Checked = true;
            this.textBoxThreadDelay.Text = "100";

            this.textBoxUser.Text = "admin";
            this.textBoxPass.Text = "admin";

            this.textBoxSQL.Text = "select parts.*,sortingpart.* from parts left outer join sortingpart on parts.part_codeid=sortingpart.part_codeid where parts.product_id=(select product_id from products where product_codeid='170107010001')";

            this.textBoxLength.Text = "2000";
            this.textBoxWidth.Text = "1250";
            this.textBoxHeight.Text = "40";

            this.progressBar.Maximum = 100;
            this.progressBar.Value = 0;

            this.radioButtonBinPacking.Checked = true;
            this.radioButtonPathImport.Checked = false;

            this.comboBoxTx.Items.Add("T1");
            this.comboBoxTx.Items.Add("T2");
            this.comboBoxTx.Items.Add("T3");
            this.comboBoxTx.Items.Add("T4");
            this.comboBoxTx.SelectedIndex = 2;

            this.comboBoxDx.Items.Add("D-");
            this.comboBoxDx.Items.Add("D+");
            this.comboBoxDx.SelectedIndex = 0;

            this.comboBoxTy.Items.Add("T1");
            this.comboBoxTy.Items.Add("T2");
            this.comboBoxTy.Items.Add("T3");
            this.comboBoxTy.Items.Add("T4");
            this.comboBoxTy.SelectedIndex = 3;

            this.comboBoxDy.Items.Add("D-");
            this.comboBoxDy.Items.Add("D+");
            this.comboBoxDy.SelectedIndex = 1;

            this.textBoxSawThickness.Text = "5";
            this.textBoxTrim.Text = "10";

            if (File.Exists("D:\\gitArtisman\\ArtCloud\\Nesting.xml"))
            {
                this.textBoxFile.Text = "D:\\gitArtisman\\ArtCloud\\Nesting.xml";
            }

            UpdateButtonState();

            TaskController.CadmInit();
        }

        private void RequestTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Running)
            {
                MessageBox.Show("Stop before exit", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }
            TaskController.CadmExit();
        }

        public static void AppendLog(string log)
        {
            _logList.Enqueue(log);
        }

        public static void DebugLog(int level, string format, params object[] paramList)
        {
            try
            {
                DateTime t = DateTime.Now;
                string log = String.Format(format, paramList);

                log = t.ToString("yyyy-MM-dd HH:mm:ss ") + String.Format(format, paramList);

                string logFile = AppDomain.CurrentDomain.BaseDirectory + String.Format("log\\request{0}.log", t.ToString("yyyyMMdd"));
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "log\\"))
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "log\\");

                using (StreamWriter sw = File.AppendText(logFile))
                {
                    sw.WriteLine(log);
                }
            }
            catch (Exception e)
            { }
        }

        private void buttonTaskList_Click(object sender, EventArgs e)
        {
            StringBuilder filePath = new StringBuilder();
            Boolean result = RemoteCall.GetTaskList(filePath);
            if (result)
            {
                AppendLog(String.Format("Success: taskList file: {0}", filePath.ToString()));
                Process.Start(filePath.ToString());
            }
            else
            {
                this.listBoxLog.Items.Add(String.Format("Failed to get taskList"));
            }
        }

        private void buttonGetTask_Click(object sender, EventArgs e)
        {
            StringBuilder filePath = new StringBuilder();
            Boolean result = RemoteCall.StartTask("0Hck3KI5kEmJWh", filePath);
            if (result)
            {
                AppendLog(String.Format("Success: task file: {0}", filePath.ToString()));
                Process.Start(filePath.ToString());
            }
            else
            {
                this.listBoxLog.Items.Add(String.Format("Failed to get task"));
            }
        }

        private void buttonGetObject_Click(object sender, EventArgs e)
        {
            StringBuilder filePath = new StringBuilder();
            //Boolean result = RemoteCall.GetObjectData("4CE089D6-9FB9-40AF-A27A-33948FC5FC3C", "02ck3KICwBQyvF", filePath, "front"); //02ck3KICw6x5Tb
            //Boolean result = RemoteCall.GetObjectData("E09E68C9-EEDA-4966-AA4D-DCCCE465074B", "BAT00161212000818000003-BAT001612120008", filePath, "");
            //Boolean result = RemoteCall.GetObjectData("E09E68C9-EEDA-4966-AA4D-DCCCE465074B", "BAT00161225000118000000-BAT001612250001", filePath, "");
            Boolean result = RemoteCall.GetObjectData("SheetPicture", "BAT00170309000125000000-BAT001703090001", filePath, "");
            //Boolean result = RemoteCall.GetObjectData("SheetPicture", "BAT00170309000118010001-BAT001703090001", filePath, "");

            //req.url = "/caxa/get_object_data?proc_id=4CE089D6-9FB9-40AF-A27A-33948FC5FC3C&object_id=02ck3KICw6x5Tb&flag=front";//E09E68C9-EEDA-4966-AA4D-DCCCE465074B&object_id=0Hck3KI5kEmJWh&flag=";
            if (result)
            {
                AppendLog(String.Format("Success: object file: {0}", filePath.ToString()));
                Process.Start(filePath.ToString());
            }
            else
            {
                this.listBoxLog.Items.Add(String.Format("Failed to get object"));
            }
        }

        private void buttonGetSettings_Click(object sender, EventArgs e)
        {
            StringBuilder filePath = new StringBuilder();
            Boolean result = RemoteCall.GetSettingFile(filePath);
            if (result)
            {
                AppendLog(String.Format("Success: taskList file: {0}", filePath.ToString()));
                Process.Start(filePath.ToString());
            }
            else
            {
                this.listBoxLog.Items.Add(String.Format("Failed to get taskList"));
            }
        }

        private void buttonGetPicture_Click(object sender, EventArgs e)
        {
            StringBuilder filePath = new StringBuilder();
            Boolean result = RemoteCall.GetTaskPic("BAT00170309000125000000", filePath);
            if (result)
            {
                AppendLog(String.Format("Success: taskList file: {0}", filePath.ToString()));
                Process.Start(filePath.ToString());
            }
            else
            {
                this.listBoxLog.Items.Add(String.Format("Failed to get taskList"));
            }
        }

        private DateTime _dtLastUpdated = DateTime.Now;
        private void timerUI_Update_Tick(object sender, EventArgs e)
        {
            DateTime tNow = DateTime.Now;
            String timeLog = tNow.ToString("MM-dd HH:mm:ss");

            Boolean bUpdated = false;
            while (_logList.Count > 0)
            {
                string log = "";
                if (_logList.TryDequeue(out log))
                {
                    bUpdated = true;
                    DebugLog(LOG_INFO, log);
                    this.listBoxLog.Items.Add(timeLog + " " + log);
                }
            }

            List<String> logList = RemoteCall.GetLog();
            for (int i = 0; i < logList.Count; i++)
            {
                bUpdated = true;
                DebugLog(LOG_INFO, logList[i]);
                this.listBoxLog.Items.Add(timeLog + " " + logList[i]);
            }

            if (_StressTester != null)
            {
                logList = _StressTester.GetLog();
                for (int i = 0; i < logList.Count; i++)
                {
                    bUpdated = true;
                    DebugLog(LOG_INFO, logList[i]);
                    this.listBoxLog.Items.Add(timeLog + " " + logList[i]);
                }
            }

            TimeSpan tSpan = tNow - _dtLastUpdated;
            if (tSpan.TotalMilliseconds > 200)
            {
                if (_CadmTaskID > 0)
                {
                    OnCadmTimer();
                }
                if (_reqSession.IsSigned())
                {
                    if (!String.IsNullOrEmpty(_TaskID))
                    {
                        StringBuilder progress = new StringBuilder();
                        if (RESTClient.RemoteCall.OrderProgress(_reqSession, _TaskID, progress))
                        {
                            int nProgress = Convert.ToInt32(progress.ToString());
                            Debug.WriteLine("Order Progress: {0}", nProgress);

                            if ((nProgress >= 100) && (nProgress != 999))
                            {
                                StringBuilder resultFile = new StringBuilder(32768);
                                if (_TaskType.Equals("CreatePart"))
                                {
                                    if (RESTClient.RemoteCall.GetObjectData("ProcessData", _TaskID, resultFile, ""))
                                    {
                                        Debug.WriteLine(resultFile);

                                        //String resultContent = File.ReadAllText(xmlFile.ToString());
                                        //Debug.WriteLine(resultContent.Substring(0, Math.Min(resultContent.Length, 256)));
                                    }
                                }
                                else if (_TaskType.Equals("PartForm"))
                                {
                                    if (RESTClient.RemoteCall.OrderDataset(_reqSession, _TaskID, resultFile))
                                    {
                                        Debug.WriteLine(resultFile);
                                    }
                                }
                                else if (_TaskType.Equals("CreateProjectData"))
                                {
                                    if (RESTClient.RemoteCall.OrderDataset(_reqSession, _TaskID, resultFile))
                                    {
                                        Debug.WriteLine(resultFile);
                                    }
                                }
                                else if (_TaskType.Equals("OrderDirect"))
                                {
                                    if (RESTClient.RemoteCall.OrderResult(_reqSession, _TaskID, resultFile))
                                    {
                                        Debug.WriteLine(resultFile);

                                        StringBuilder error = new StringBuilder();
                                        RESTClient.RemoteCall.OrderRemove(_reqSession, _TaskID, error);
                                    }
                                }
                                _TaskID = "";
                                _TaskType = "";
                            }
                            else if (nProgress < 0)
                            {
                                _TaskID = "";
                                _TaskType = "";
                            }
                        }
                        else
                        {
                            _TaskID = "";
                            _TaskType = "";
                        }
                    }
                }
                else
                {
                    if (!this.checkBoxAPI.Checked)
                        _CadmTaskID = -1;

                    this.buttonSubmit.Text = "提交任务";
                    _TaskID = "";
                }

                _dtLastUpdated = tNow;

                this.textBoxFinished.Text = FinishedFiles.ToString();

                if (_StressTester != null)
                {
                    tSpan = tNow - tStressStarted;
                    this.textBoxElapsed.Text = tSpan.ToString(@"dd\.hh\:mm\:ss");
                }
            }

            if (bUpdated)
            {
                if (this.listBoxLog.Items.Count > 1000)
                {
                    int nCount = 100;
                    while(--nCount > 0)
                        this.listBoxLog.Items.RemoveAt(0);
                }

                this.listBoxLog.SelectedIndex = this.listBoxLog.Items.Count - 1;
            }

            if (!_reqSession.IsSigned() && !this.checkBoxAPI.Checked)
            {
                if (this.buttonSign.Text.Equals("注 销"))
                {
                    _CadmTaskID = -1;
                    _TaskID = "";

                    this.buttonSubmit.Text = "提交任务";
                    this.buttonSign.Text = ("登 录");
                    UpdateButtonState();
                }
            }
            else
            {
                if (this.buttonSign.Text.Equals("登 录"))
                {
                    this.buttonSign.Text = ("注 销");
                    UpdateButtonState();
                }
            }
        }

        public void DownloadJobData(WebRequestSession req)
        {
            StringBuilder errMessage = new StringBuilder();
            String file = RemoteCall.RESTGetAsFile(req.ssl, req.host, req.port, req.url, errMessage, req.cts);
            if (!String.IsNullOrEmpty(file) && File.Exists(file))
            {
                File.Delete(file);

                try { Thread.Sleep(req.delay); }
                catch (Exception e) { }

                Interlocked.Increment(ref FinishedFiles);
            }
        }

        private DateTime tStressStarted = DateTime.Now;
        private void buttonStressTest_Click(object sender, EventArgs e)
        {
            if (_StressTester == null)
            {
                if (!_reqSession.IsSigned())
                {
                    MessageBox.Show("客户端尚未登录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }

                _StressTester = new ThreadScheduler<WebRequestSession>();

                WebRequestSession req = null;

                req = _reqSession.Clone();
                req.delay = Convert.ToInt32(this.textBoxThreadDelay.Text);
                req.url = "/caxa/get_task_list";
                _StressTester.EnqueueTask(req);

                req = _reqSession.Clone();
                req.delay = Convert.ToInt32(this.textBoxThreadDelay.Text);
                req.url = "/caxa/get_task?task_id=0Hck3KI5kEmJWh";
                _StressTester.EnqueueTask(req);

                req = _reqSession.Clone();
                req.delay = Convert.ToInt32(this.textBoxThreadDelay.Text);
                req.url = "/caxa/get_object_data?proc_id=4CE089D6-9FB9-40AF-A27A-33948FC5FC3C&object_id=02ck3KICw6x5Tb&flag=front";//E09E68C9-EEDA-4966-AA4D-DCCCE465074B&object_id=0Hck3KI5kEmJWh&flag=";
                _StressTester.EnqueueTask(req);

                req = _reqSession.Clone();
                req.delay = Convert.ToInt32(this.textBoxThreadDelay.Text);
                req.url = "/caxa/get_settings";
                _StressTester.EnqueueTask(req);

                req = _reqSession.Clone();
                req.delay = Convert.ToInt32(this.textBoxThreadDelay.Text);
                req.url = "/caxa/get_picture?task_id=0Hck3KI5kEmJWh";
                _StressTester.EnqueueTask(req);

                _StressTester._Processor = DownloadJobData;
                _StressTester.SetMode(ThreadScheduler<WebRequestSession>.MODE_REPEAT);

                int nThreads = 2;
                try{ nThreads = Convert.ToInt32(this.textBoxThreads.Text); }
                catch (Exception em) { }

                AppendLog("Threads: " + nThreads.ToString());
                _StressTester.Start(nThreads);

                tStressStarted = DateTime.Now;
                this.buttonStressTest.Text = "停 止";

                _Running = true;
            }
            else
            {
                if (_reqSession.cts != null)
                    _reqSession.cts.Cancel();

                _StressTester.Stop();

                _StressTester = null;
                this.buttonStressTest.Text = "多线程并发测试";

                _Running = false;
            }
        }

        private void UpdateButtonState()
        {
            this.buttonGetTaskList.Enabled = _reqSession.IsSigned();
            this.buttonGetTask.Enabled = _reqSession.IsSigned();
            this.buttonGetObject.Enabled = _reqSession.IsSigned();
            this.buttonGetSettings.Enabled = _reqSession.IsSigned();
            this.buttonGetPicture.Enabled = _reqSession.IsSigned();
            this.buttonStressTest.Enabled = _reqSession.IsSigned();
            this.buttonUpload.Enabled = _reqSession.IsSigned();
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            if (_reqSession.IsSigned())
            {
                _CadmTaskID = -1;
                _TaskID = "";

                _reqSession.Logout();
                this.buttonSign.Text = "登 录";
                this.buttonSubmit.Text = "提交任务";

                MessageBox.Show("客户端已注销，请求数据时，需要重新登录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
            {
                if (!_reqSession.Login(this.checkBoxSSL.Checked, this.textBoxServerIP.Text, Convert.ToInt32(this.textBoxPort.Text),
                    this.textBoxUser.Text.Trim(), this.textBoxPass.Text.Trim(), String.Empty, "此处放机器ID"))
                {
                    MessageBox.Show("客户端登录失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                this.buttonSign.Text = "注 销";
            }

            UpdateButtonState();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string httpsLink = String.Format("https://{0}:{1}", textBoxServerIP.Text, textBoxPort.Text);
            System.Diagnostics.Process.Start(httpsLink);

        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = RemoteCall.RESTQuery(_reqSession, this.textBoxSQL.Text);
                if ((dt != null) && (dt.Rows.Count >= 0))
                { //SQL 执行成功
                    this.dataGridViewSql.DataSource = dt;
                    listBoxSqlLog.Items.Add("Rows: " + dt.Rows.Count);
                }
            }
            catch (Exception err)
            {
                listBoxSqlLog.Items.Add(err.Message); //失败时，抛出错误信息
            }
        }

        private void buttonNonQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (RemoteCall.RESTNonQuery(_reqSession, this.textBoxSQL.Text))
                { //SQL 执行成功
                    listBoxSqlLog.Items.Add("Non Query Success");
                }
            }
            catch (Exception err)
            {
                listBoxSqlLog.Items.Add(err.Message); //失败时，抛出错误信息
            }
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            OpenFileDialog openXLsFileDialog = new OpenFileDialog();
            openXLsFileDialog.Filter = "Excel Files (.xml)|*.xml|All Files (*.*)|*.*";
            openXLsFileDialog.FilterIndex = 1;
            openXLsFileDialog.Multiselect = false;

            if (DialogResult.OK == openXLsFileDialog.ShowDialog())
            {
                StringBuilder taskID = new StringBuilder(2048);
                Boolean result = RemoteCall.OrderPartition(_reqSession, openXLsFileDialog.FileName, "ProcessByOrderList", null, taskID);//processor:"ProcessByOrderList"
                if (result)
                {
                    _TaskType = "OrderDirect";
                    _TaskID = taskID.ToString();
                    _LastTaskID = "";
                    AppendLog(String.Format("Success: upload orders: {0}, task:{1}", openXLsFileDialog.FileNames.ToString(), _TaskID));
                }
                else
                {
                    _TaskID = "";
                    _TaskType = "";
                    this.listBoxLog.Items.Add(String.Format("Failed to upload orders"));
                }
            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openXLsFileDialog = new OpenFileDialog();
            openXLsFileDialog.Filter = "XML Files (.xml)|*.xml|All Files (*.*)|*.*";
            openXLsFileDialog.FilterIndex = 1;
            openXLsFileDialog.Multiselect = true;

            if (DialogResult.OK == openXLsFileDialog.ShowDialog())
            {
                //StringBuilder filePath = new StringBuilder();
                //Boolean result = RemoteCall.UploadNestingList(openXLsFileDialog.FileNames);
                //if (result)
                //{
                //    AppendLog(String.Format("Success: upload files: {0}", openXLsFileDialog.FileNames.ToString()));
                //}
                //else
                //{
                //    this.listBoxLog.Items.Add(String.Format("Failed to upload"));
                //}
                StringBuilder taskID = new StringBuilder(2048);
                Dictionary<String,String> other = new Dictionary<string,string>();
                other["taskid"] = _LastTaskID;
                Boolean result = RemoteCall.OrderPartition(_reqSession, openXLsFileDialog.FileName, "ProcessByPartList", other, taskID);//processor:"ProcessByOrderList"
                if (result)
                {
                    _TaskType = "PartForm";
                    _TaskID = taskID.ToString();
                    _LastTaskID = "";
                    AppendLog(String.Format("Success: upload part-list: {0}, task:{1}", openXLsFileDialog.FileNames.ToString(), _TaskID));
                }
                else
                {
                    _TaskID = "";
                    _TaskType = "";
                    this.listBoxLog.Items.Add(String.Format("Failed to upload part-list"));
                }
            }
        }

        //GeneratePartList

        private void buttonLines_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = RemoteCall.GetAssemblyLines(_reqSession);
                if ((dt != null) && (dt.Rows.Count >= 0))
                { //SQL 执行成功
                    this.dataGridViewSql.DataSource = dt;
                    listBoxSqlLog.Items.Add("Lines: " + dt.Rows.Count);
                }
            }
            catch (Exception err)
            {
                listBoxSqlLog.Items.Add(err.Message); //失败时，抛出错误信息
            }
        }

        private void buttonNextID_Click(object sender, EventArgs e)
        {
            try
            {
                int nextID = RemoteCall.GetNextID(_reqSession, "Product_Id");
                if (nextID > 0)
                {
                    listBoxSqlLog.Items.Add("NextID: Product_Id=" + nextID.ToString());
                }
            }
            catch (Exception err)
            {
                listBoxSqlLog.Items.Add(err.Message); //失败时，抛出错误信息
            }
        }

        private void buttonOrderFile_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder resultFile = new StringBuilder(1024);
                Boolean result = RemoteCall.GetOrderFile(_reqSession, 1000, 1012, resultFile);
                if (result)
                {
                    listBoxSqlLog.Items.Add("OrderFile: " + resultFile.ToString());
                }
            }
            catch (Exception er)
            {
                listBoxSqlLog.Items.Add(er.Message); //失败时，抛出错误信息
            }
        }

        private void buttonSplit_Click(object sender, EventArgs e)
        {
            OpenFileDialog openXLsFileDialog = new OpenFileDialog();
            openXLsFileDialog.Filter = "Excel Files (.xml)|*.xml|All Files (*.*)|*.*";
            openXLsFileDialog.FilterIndex = 1;
            openXLsFileDialog.Multiselect = false;

            if (DialogResult.OK == openXLsFileDialog.ShowDialog())
            {
                StringBuilder taskID = new StringBuilder(2048);
                Dictionary<String, String> other = new Dictionary<string, string>();
                Boolean result = RemoteCall.OrderPartition(_reqSession, openXLsFileDialog.FileName, "CreatePartForms", other, taskID);//processor:"ProcessByOrderList"
                if (result)
                {
                    _TaskType = "CreatePart";
                    _TaskID = taskID.ToString();
                    _LastTaskID = _TaskID;
                    AppendLog(String.Format("Success: upload orders: {0}, task:{1}", openXLsFileDialog.FileNames.ToString(), _TaskID));
                }
                else
                {
                    _TaskID = "";
                    _TaskType = "";
                    _LastTaskID = "";
                    this.listBoxLog.Items.Add(String.Format("Failed to upload orders"));
                }
            }
        }

        private void buttonProjectData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openXLsFileDialog = new OpenFileDialog();
            openXLsFileDialog.Filter = "Excel Files (.xml)|*.xml|All Files (*.*)|*.*";
            openXLsFileDialog.FilterIndex = 1;
            openXLsFileDialog.Multiselect = false;

            if (DialogResult.OK == openXLsFileDialog.ShowDialog())
            {
                StringBuilder taskID = new StringBuilder(2048);
                Dictionary<String, String> other = new Dictionary<string, string>();
                other["task_param"] = "ProcessByOrderList";
                Boolean result = RemoteCall.OrderPartition(_reqSession, openXLsFileDialog.FileName, "CreateProjectData", other, taskID);//processor:"ProcessByOrderList"
                if (result)
                {
                    _TaskType = "CreateProjectData";
                    _TaskID = taskID.ToString();
                    int idx = _TaskID.IndexOf("#");
                    if (idx >= 0)
                        _TaskID = _TaskID.Substring(idx+1);
                    _LastTaskID = _TaskID;
                    AppendLog(String.Format("Success: upload orders: {0}, task:{1}", openXLsFileDialog.FileNames.ToString(), _TaskID));
                }
                else
                {
                    _TaskID = "";
                    _TaskType = "";
                    _LastTaskID = "";
                    this.listBoxLog.Items.Add(String.Format("Failed to upload orders"));
                }
            }
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////////////////////////
            if (false)
            { //获取模型列表
                string url = "/caxa/get_cadm_model_list";
                String filter = "";
                string response = RemoteCall.GetJsonData(_reqSession, url, filter);
                JObject jResult = JObject.Parse(response);
                if ((int)jResult["result_code"] > 0)
                {
                    this.listBoxLog.Items.Add(String.Format("成功获取模型树"));
                }
                else
                {
                    this.listBoxLog.Items.Add(String.Format("获取模型树失败"));
                }
            }
            /////////////////////////////////////////////////////////////////////
            else if (false)
            { //获取指定模型的数据包
                string url = "/caxa/get_cadm_model_data";
                String modelPath = UrlUtility.UrlEncode("\\CADM\\SVG\\喷漆圆角矩形加负刀补(2000x750)#4");
                string response = RemoteCall.GetCadmModelData(_reqSession, url, modelPath);
                JObject jResult = JObject.Parse(response);
                if ((int)jResult["result_code"] > 0)
                {
                    this.listBoxLog.Items.Add(String.Format("成功获取模型数据"));
                }
                else
                {
                    this.listBoxLog.Items.Add(String.Format("获取模型数据失败"));
                }
            }
            /////////////////////////////////////////////////////////////////////
            else if (false)
            { //测试增加订单
                string url = "/caxa/add_order_item";

                JObject newOrder = new JObject(
                    new JProperty("order_id", -1),
                    new JProperty("order_no", ""),
                    new JProperty("customer", ""),
                    new JProperty("phone", ""),
                    new JProperty("person", ""),
                    new JProperty("address", ""),
                    new JProperty("order_date", "2017-02-19"),
                    new JProperty("delivery_date", "2017-03-19"),
                    new JProperty("order_memo", ""),
                    new JProperty("order_status", ""),
                    new JProperty("projectid", -1));

                string response = RemoteCall.PostJObject(_reqSession, url, newOrder);
                JObject jResult = JObject.Parse(response);
                if ((int)jResult["result_code"] > 0)
                {
                    this.listBoxLog.Items.Add(String.Format("增加订单成功"));
                }
                else
                {
                    this.listBoxLog.Items.Add(String.Format("增加订单失败"));
                }
            }
            /////////////////////////////////////////////////////////////////////
            else if (false)
            { //测试增加带附件的订单项
                string url = "/caxa/multipart_order_item";

                NameValueCollection valuePairs = new NameValueCollection();
                valuePairs.Add("order_name", "订单名称");
                valuePairs.Add("customer", "客户名称");

                NameValueCollection files = new NameValueCollection();
                files.Add("压缩文件", "C:\\ACC\\CuttingStockNew.rar");
                files.Add("部件名称", "C:\\ACC\\part_form.xml");

                string response = RemoteCall.PostMultipartRequest(_reqSession, url, valuePairs, files);
                JObject jResult = JObject.Parse(response);
                if ((int)jResult["result_code"] > 0)
                {
                    this.listBoxLog.Items.Add(String.Format("增加订单项成功"));
                }
                else
                {
                    this.listBoxLog.Items.Add(String.Format("增加订单项失败"));
                }
            }
            /////////////////////////////////////////////////////////////////////
            else if (false)
            {
                //String srcFolder = "C:\\SmartHomeDesign_x64\\2.0\\drawLibs\\files\\banshijiaju\\";
                //String zipFile = "C:\\temp\\bsjj.zip";
                //RESTClient.GZip.ZipFolder(srcFolder, zipFile);

                //RESTClient.GZip.UnZip(zipFile, "C:\\temp\\out\\");

                //AssemblyLine lines = AssemblyLine.getInstance("C:\\326Demo_V45\\WebServerWithDb\\WebServer\\template\\processTemplate.xml");
                DataTable dtLines = RemoteCall.GetAssemblyLines(_reqSession);
            }
            /////////////////////////////////////////////////////////////////////
            else if (false)
            {

                String package_codeid = "0PE77BF72BE832"; //PostgreSQL 9.4，PostgreSQL 9.4
                String sheet_codeid = "0Hck6vLDjhqWhu";
                String part_codeid = "02ck6vLDf8kXVJ";

                StringBuilder resultFile = new StringBuilder(1024);
                Boolean result = RemoteCall.GetSheetList(_reqSession, package_codeid, resultFile);
                if (result)
                {
                    result = RemoteCall.UpdateSheetStatus(_reqSession, sheet_codeid, "cutted");
                    result = RemoteCall.UpdatePartStatus(_reqSession, part_codeid, "cutted");
                }
            }
        }

        private void radioButtonBinPacking_CheckedChanged(object sender, EventArgs e)
        {
            //this.radioButtonPathImport.Checked = false;
        }

        private void radioButtonPathImport_CheckedChanged(object sender, EventArgs e)
        {
           // this.radioButtonBinPacking.Checked = false;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "";
            openFileDialog.Filter += "All Known Files |*.xml;*.svg;*.nc;|All Files (*.*)|*.*";

            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = true;

            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                this.textBoxFile.Text = String.Join(";", openFileDialog.FileNames);
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (!this.checkBoxAPI.Checked)
            {
                if (!_reqSession.IsSigned())
                {
                    MessageBox.Show("客户端尚未登录", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            if (sender != null)
            {
                _SubmitCountdown = Convert.ToInt32(this.textBoxLoops.Text);
            }
            else
            {
                _SubmitCountdown --;
            }

            if (_CadmTaskID < 0)
            {
                String taskType = CadmAPI.TASK_BINPACKING;
                if (this.radioButtonPathImport.Checked)
                    taskType = CadmAPI.TASK_PATHIMPORT;

                Dictionary<String, String> taskFiles = new Dictionary<string, string>();
                String[] pathFiles = this.textBoxFile.Text.Split(';');
                for(int i = 0; i < pathFiles.Length; i ++)
                    taskFiles["File" + (i + 1)] = pathFiles[i];

                //this.textBoxStatus.Text = "";

                if (this.checkBoxAPI.Checked)
                { //local
                    String sParam = "";
                    sParam += String.Format("task_type={0}", taskType);
                    sParam += String.Format("::task_name={0}", taskType);

                    sParam += String.Format("::Length={0}", this.textBoxLength.Text);
                    sParam += String.Format("::Width={0}", this.textBoxWidth.Text);
                    sParam += String.Format("::Height={0}", this.textBoxHeight.Text);

                    sParam += String.Format("::trim={0}", this.textBoxTrim.Text);
                    sParam += String.Format("::gap={0}", this.textBoxSawThickness.Text);

                    sParam += String.Format("::saw_x={0}", this.comboBoxDx.Text + "," + this.comboBoxTx.Text + ",R30");
                    sParam += String.Format("::saw_y={0}", this.comboBoxDy.Text + "," + this.comboBoxTy.Text + ",R30");

                    foreach (String path in pathFiles)
                        sParam += String.Format("::{0}=^_^", path);
                    _CadmTaskID = TaskController.SubmitTask((IntPtr)0, sParam);
                }
                else
                { //remote
                    _CadmTaskID = CadmAPI.TaskSubmit(_reqSession, taskType, "BinPacking", taskFiles);
                }
                if (_CadmTaskID < 0)
                {
                    MessageBox.Show("启动任务失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                    return;
                }
                this.buttonSubmit.Text = "终止任务";
            }
            else
            {
                if (this.checkBoxAPI.Checked)
                { //local
                    TaskController.CancelTask(_CadmTaskID);
                }
                else
                {
                    StringBuilder resultMessage = new StringBuilder(8192);
                    CadmAPI.TaskCancel(_reqSession, _CadmTaskID.ToString(), resultMessage);
                }
            }
        }

        private void OnCadmTimer()
        {
            if (_CadmTaskID <= 0)
                return;

            String result = "";

            StringBuilder taskStatus = new StringBuilder(4096);
            int progress = 0;
            Boolean finished = false;
            if (this.checkBoxAPI.Checked)
            { //local
                finished = TaskController.IsTaskFinished(_CadmTaskID);
                progress = TaskController.GetTaskProgress(_CadmTaskID, taskStatus);
            }
            else
            {
                progress = CadmAPI.TaskProgress(_reqSession, _CadmTaskID.ToString(), taskStatus);
                finished = (progress >= 1000);
            }

            this.textBoxStatus.Text += taskStatus.ToString().Replace("\t", "\r\n");
            this.textBoxStatus.SelectionStart = this.textBoxStatus.Text.Length;
            this.textBoxStatus.ScrollToCaret();

            if (progress >= 0)
            {
                this.progressBar.Value = (progress % 1000);
                Debug.WriteLine(String.Format("Progress: {0} %", (progress % 1000)));
            }

            if (finished)
            {
                try
                {
                    taskStatus.Clear();

                    int errorCode = 0;
                    if (this.checkBoxAPI.Checked)
                    { //local
                        progress = TaskController.GetTaskProgress(_CadmTaskID, taskStatus);

                        this.textBoxStatus.Text += taskStatus.ToString().Replace("\t", "\r\n");
                        this.textBoxStatus.SelectionStart = this.textBoxStatus.Text.Length;
                        this.textBoxStatus.ScrollToCaret();

                        if (progress >= 0)
                            this.progressBar.Value = (progress % 1000);
                        
                        result = TaskController.GetTaskResult(_CadmTaskID);
                        this.textBoxStatus.Text += result;
                    }
                    else
                    {
                        errorCode = CadmAPI.TaskResult(_reqSession, _CadmTaskID.ToString(), taskStatus);
                    }
                    if (errorCode > 0)
                    {
                        this.textBoxStatus.Text += taskStatus.ToString().Replace("\t", "\r\n");
                    }
                }
                catch (Exception e)
                { }

                _CadmTaskID = -1;
                this.buttonSubmit.Text = "提交任务";

                if (_SubmitCountdown > 1)
                    buttonSubmit_Click(null, null);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string url = "/dpjia/post_collocation_file";

            NameValueCollection valuePairs = new NameValueCollection();
            valuePairs.Add("projects", "001/002");

            NameValueCollection files = new NameValueCollection();
            files = null;

            string response = RemoteCall.PostMultipartRequest(_reqSession, url, valuePairs, files);
            JObject jResult = JObject.Parse(response);
            if ((int)jResult["result_code"] > 0)
            {
                this.listBoxDpLog.Items.Add(String.Format("成功"));
            }
            else
            {
                this.listBoxDpLog.Items.Add(String.Format("失败"));
            }
        }
    }
}
