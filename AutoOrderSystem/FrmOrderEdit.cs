using RESTClient;
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
    public partial class FrmOrderEdit : FrmBase
    {
        private DateTimePicker dTPicker_OrderDate;

        private WebRequestSession _reqSession;

        public FrmOrderEdit()
        {
            InitializeComponent();

            dTPicker_OrderDate = new DateTimePicker();
            dTPicker_OrderDate.Width = 130;
            this.toolStrip1.Items.Insert(1, new ToolStripControlHost(dTPicker_OrderDate));
        }

        public FrmOrderEdit(WebRequestSession session) :this()
        {
            _reqSession = session;
        }

        private void toolStripButton_Query_Click(object sender, EventArgs e)
        {
            DateTime time_Order = dTPicker_OrderDate.Value;

            string sql = $"select * from orders where order_date='{time_Order.ToShortDateString()}'";

            DataTable dt=RemoteCall.RESTQuery(_reqSession, sql);

            skinDataGridView_Order.DataSource = dt;
        }

        private void skinDataGridView_Order_SelectionChanged(object sender, EventArgs e)
        {

            try
            {

                int order_id = Convert.ToInt32(skinDataGridView_Order.SelectedRows[0].Cells["order_id"].Value);

                string sql = $"select * from order_items where order_id={order_id}";

                DataTable dt = RemoteCall.RESTQuery(_reqSession, sql);

                skinDataGridView_OrderItem.DataSource = dt;
            }
            catch (Exception)
            {

       
            }


        }

        private void toolStripButton_Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in skinDataGridView_Order.SelectedRows)
            {
                int order_id = Convert.ToInt32(dr.Cells["order_id"].Value);

                string sql = $"delete  from order_items where order_id={order_id}";

                RemoteCall.RESTQuery(_reqSession, sql);

                sql = $"delete from orders where order_id={order_id}";

                RemoteCall.RESTQuery(_reqSession, sql);

            }

            toolStripButton_Query_Click(null, null);
        }
    }
}
