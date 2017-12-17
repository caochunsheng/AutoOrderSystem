using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoOrderSystem
{
    public partial class FrmProductType : FrmBase
    {
        private List<string> _productTypeList = new List<string>();

        public List<string> _checkedProductTypeList = new List<string>();


        public FrmProductType()
        {
            InitializeComponent();
        }
        public FrmProductType(List<string> productTypeList) : this()
        {
            _productTypeList = productTypeList;
        }

        private void FrmProductType_Load(object sender, EventArgs e)
        {
            string[] strArry = _productTypeList.ToArray();

            foreach (string item in _productTypeList)
            {
                skinTreeView1.Nodes.Add(item);
            }
        }

        private void button_Confirm_Click(object sender, EventArgs e)
        {

            foreach (TreeNode node in skinTreeView1.Nodes)
            {
                if (node.Checked)
                {
                    _checkedProductTypeList.Add(node.Text);
                }
            }


            this.DialogResult = DialogResult.OK;
        }

        private void skinTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
           
            if (e.Node.Checked == true)
            {
                e.Node.Checked = false;
            }
            else if (e.Node.Checked == false)
            {
                e.Node.Checked = true;
            }
        }
    }
}
