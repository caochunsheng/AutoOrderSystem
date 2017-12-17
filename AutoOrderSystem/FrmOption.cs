using AutoOrderSystem.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AutoOrderSystem
{
    public partial class FrmOption : FrmBase
    {
        private string productTypeSettingPath = Application.StartupPath + "\\ProductRules.xml";

        public FrmOption()
        {
            InitializeComponent();
            LoadSetting();
        }

        private void FrmOption_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否保存更改？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //保存配置
                SaveSetting();
                this.Dispose();
            }
            else
            {
                this.Dispose();
                //不保存退出
                //e.Cancel = true;
            }
        }

        private void LoadSetting()
        {
            if (File.Exists(productTypeSettingPath))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(productTypeSettingPath);

                foreach (XmlNode ruleNode in doc.SelectNodes("RecognitionRules/ProductType/Rule"))
                {
                    dgv_ProductType.Rows.Add(ruleNode.ParentNode.Attributes["name"].Value, ruleNode.Attributes["key"].Value, ruleNode.Attributes["value"].Value);
                }
            }
        }
        private void SaveSetting()
        {

            XmlDocument doc = new XmlDocument();

            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            doc.AppendChild(dec);

            XmlElement eRecongnitionRules = doc.CreateElement("RecognitionRules");
            doc.AppendChild(eRecongnitionRules);

            for (int i = 0; i < dgv_ProductType.Rows.Count - 1; i++)
            {
                DataGridViewRow dgvr = dgv_ProductType.Rows[i];

                string productType = dgvr.Cells["Col_ProductType"].Value.ToString();
                string key = dgvr.Cells["Col_Key"].Value.ToString();
                string value = dgvr.Cells["Col_Value"].Value.ToString();


                if (doc.SelectSingleNode($"RecognitionRules/ProductType[@name='{productType}']") == null)
                {
                    XmlElement eProductType = doc.CreateElement("ProductType");
                    eProductType.SetAttribute("name", productType);
                    eRecongnitionRules.AppendChild(eProductType);

                    XmlElement eRule = doc.CreateElement("Rule");
                    eRule.SetAttribute("key", key);
                    eRule.SetAttribute("value", value);
                    eProductType.AppendChild(eRule);
                }
                else
                {
                    XmlElement eProductType = (XmlElement)doc.SelectSingleNode($"RecognitionRules/ProductType[@name='{productType}']");
                    XmlElement eRule = doc.CreateElement("Rule");
                    eRule.SetAttribute("key", key);
                    eRule.SetAttribute("value", value);
                    eProductType.AppendChild(eRule);
                }
            }

            doc.Save(productTypeSettingPath);


        }
    }
}
