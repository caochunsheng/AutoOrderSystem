using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DbControl;
using RESTClient;

namespace RequestTask
{
    public partial class DbProcedureTest : Form
    {

        public DbProcedureTest()
        {
            InitializeComponent();
        }

        private void DbProcedureTest_Load(object sender, EventArgs e)
        {
            this.textBoxDbServer.Text = "sql12.htcxms.com,21433";
            this.textBoxDbUser.Text = "cutting_user";
            this.textBoxDbPass.Text = "cuttingSa1234";
        }

        private void buttonGetWorkOrder_Click(object sender, EventArgs e)
        {
            double sawDiameter = 500;
            double sawDepth = -400;
            double lift = 100;

            WorkOrder order = new WorkOrder();
            string path = order.GetPath(sawDiameter, sawDepth, lift);

            textBoxGCode.Text = path;

            DataTable workOrder = DbComm.GetWorkOrder(this.textBoxDbServer.Text, this.textBoxDbUser.Text, this.textBoxDbPass.Text);
            MESaw saw = new MESaw();
            path = saw.GetPath(workOrder, sawDiameter, sawDepth, lift);
            textBoxGCode.Text += path;
        }
    }
}
