using Art.Scheduling.OrderForm;
using AutoOrderSystem.Common;
using AutoOrderSystem.Model;
using CCWin;
using CCWin.SkinControl;
using RESTClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace AutoOrderSystem
{
    public partial class FrmMain : CCSkinMain
    {
        private DataTable dt_source;
        private WebRequestSession _reqSession;
        private string _excelPath;
        public FrmMain()
        {
            InitializeComponent();
        }
        public FrmMain(WebRequestSession session) : this()
        {
            _reqSession = session;
        }
        private void TSButton_Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();          

            ofd.Filter = "Excel2003|*.xls|Excel2007|*.xlsx";
            
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _excelPath = ofd.FileName;

                //this.DGV_ERPData.DataSource = null;

                this.TSSLabel_Log.Text = "正在载入数据，请稍等……";

                LogHelper.WriteLog($"载入文档：{_excelPath}……", LogHelper.LogType.Status);

                this.Refresh();

                List<string> sheetNames = ExcelHelper.GetExcelSheetNames(_excelPath);

                if (sheetNames != null)
                {
                    //DataTable dt_source = new DataTable();

                    if (ExcelHelper.ReadExcel(_excelPath, sheetNames[0], out dt_source))
                    {
                        DataTable dt = dt_source.Copy();                                               
                        foreach (string productType in ProductRules.GetProductList())
                        {
                            TabPage tp = new TabPage(productType);

                            Dictionary<string, List<string>> rules = ProductRules.GetRulesList(productType);
                            List<string> list2 = new List<string>();
                            foreach (string item in rules.Keys)
                            {
                                List<string> list = rules[item];
                                foreach (string item2 in list)
                                {
                                    list2.Add($"{item}='{item2}'");
                                }
                            }

                            string filterStr = string.Join(" or ", list2);

                            DataTable dt_filter = Common.Common.FilterData(ref dt, filterStr, true);
                            dt_filter.TableName = productType;

                            SkinDataGridView dgv = new SkinDataGridView();
                            dgv.Dock = DockStyle.Fill;
                            dgv.Name = "dgv";
                            dgv.DataSource = dt_filter;
                            tp.Controls.Add(dgv);

                            tctl_Main.TabPages.Add(tp);
                        }

                        TabPage tp2 = new TabPage("未识别");

                        SkinDataGridView dgv2 = new SkinDataGridView();
                        dgv2.Dock = DockStyle.Fill;
                        dgv2.Name = "dgv";
                        dgv2.DataSource = dt;
                        tp2.Controls.Add(dgv2);

                        tctl_Main.TabPages.Add(tp2);

                        this.TSSLabel_Log.Text = "正在载入数据，请稍等……完成";

                        LogHelper.WriteLog($"载入文档：{_excelPath}……成功！", LogHelper.LogType.Status);

                        this.TSButton_CreateOrder.Enabled = true;
                        this.btn_Export.Enabled = true;

                        this.Refresh();

                    }
                    else
                    {
                        this.TSSLabel_Log.Text = "正在载入数据，请稍等……失败";
                        LogHelper.WriteLog($"载入文档：{_excelPath}……失败！", LogHelper.LogType.Error);
                        this.Refresh();
                    }
                }
            }

        }

        private void TSButton_Close_Click(object sender, EventArgs e)
        {

            this.tctl_Main.TabPages.Clear();

            this.TSSLabel_Log.Text = "您关闭了当前文档……";

            LogHelper.WriteLog($"关闭文档：{_excelPath}……", LogHelper.LogType.Status);

            this.TSButton_CreateOrder.Enabled = false;
            this.btn_Export.Enabled = false;

            this.Refresh();
        }

        private void TSButton_CreateOrder_Click(object sender, EventArgs e)
        {

            DataTable dt1 = (DataTable)((SkinDataGridView)tctl_Main.TabPages[0].Controls["dgv"]).DataSource;
            DataTable dt2 = (DataTable)((SkinDataGridView)tctl_Main.TabPages[1].Controls["dgv"]).DataSource;

            DataTable dt_two = Common.Common.MergeDataTables(dt1, dt2);

            List<Order> orders = new List<Order>();

            bool b = Common.Common.ConvertToModel(dt_two, out orders);

            if (b)
            {
                if (orders.Count != 0)
                {
                    //加载预览窗体
                    FrmOrderPreview opw = new FrmOrderPreview(_reqSession, orders);
                    if (opw.ShowDialog() == DialogResult.OK)
                    {
                        return;
                    }
                }
            }
            else
            {
                LogHelper.WriteLog("excel文档数据转模型时发生错误！", LogHelper.LogType.Error);
                MessageBox.Show("数据转模型出错");
            }

        }

        private void tSButton_NewOrder_Click(object sender, EventArgs e)
        {

        }

        private void tSButton_OrderList_Click(object sender, EventArgs e)
        {
            FrmSchedulingOrders frm = new FrmSchedulingOrders(_reqSession);

            frm.Show();
            //FrmOrderList frm = new FrmOrderList(_reqSession);
            //frm.Show();

            //Process.Start("notepad.exe");
        }

        private void toolStripButton_OrderEdit_Click(object sender, EventArgs e)
        {
            FrmOrderEdit frm = new FrmOrderEdit(_reqSession);
            frm.ShowDialog();
        }

        private void toolStripButton_Option_Click(object sender, EventArgs e)
        {
            FrmOption frm = new FrmOption();

            frm.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //string temp = "";
            //string strURL =$@"https://{_reqSession.host}:{_reqSession.port}/caxa/get_allfile?{WebParams.filedirectory}={temp}&{WebParams.suffix}=.svg|.nc";

            string path = "/caxa/get_allfile?filedirectory=DataSet\\LF111706160005\\CAMData\\M01&suffix=.xls";

            path = "/caxa/get_allfile?filedirectory=Models\\";

            StringBuilder Msg = new StringBuilder(1024);

            string str = RemoteCall.RESTGetAsFile(_reqSession.ssl, _reqSession.host, _reqSession.port, path, Msg);




            //Process.Start("osk.exe");
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            TabPage currentTp = tctl_Main.SelectedTab;

            SkinDataGridView dgv = (SkinDataGridView)currentTp.Controls["dgv"];

            DataTable dt = (DataTable)dgv.DataSource;

            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel文件|*.xls",
                Title = $"导出【{currentTp.Text}】文档",
                OverwritePrompt = true,
                FileName = currentTp.Text,
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (ExcelHelper.WriteExcel(dt, sfd.FileName))
                {
                    MessageBox.Show("导出成功！", "提示");
                    LogHelper.WriteLog($"导出文件成功->{sfd.FileName}", LogHelper.LogType.Status);
                }
                else
                {
                    MessageBox.Show("导出错误！", "提示");
                    LogHelper.WriteLog($"导出文件发生错误！", LogHelper.LogType.Error);

                }
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _reqSession.Logout(true);
        }

        private void btnUpdateLog_Click(object sender, EventArgs e)
        {
            FrmUpdateLog frm = new FrmUpdateLog();
            frm.Show();
        }
    }
}
