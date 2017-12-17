using RESTClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Art.Scheduling.OrderForm
{
    
    public partial class FrmSchedulingOrders : Form
    {
        private DateTime _dtTime;

        private string _productType;

        private WebRequestSession _reqSession;

        private int ordertype = 1007;//1007门扇类型，1008门套类型
        public FrmSchedulingOrders()
        {
            InitializeComponent();
        }
        public FrmSchedulingOrders(WebRequestSession session) :this()
        {
            _reqSession = session;
        }
        private void FrmSchedulingOrders_Load(object sender, EventArgs e)
        {
        }

        private void sDGView_Orders_SelectionChanged(object sender, EventArgs e)
        {
            sDGView_Products.Rows.Clear();

            DataGridView dgv = sender as DataGridView;

            foreach (DataGridViewRow row in dgv.SelectedRows)
            {
                string orderCodeid = row.Cells["Col_OrderNo"].Value.ToString();
                string sql = string.Format("select order_id from orders where order_no='{0}'", orderCodeid);
                string order_id = RemoteCall.RESTQuery(_reqSession, sql).Rows[0][0].ToString();
                sql = string.Format("select * from order_items where order_id='{0}' and ordertype={1}", order_id, ordertype);
                DataTable dt = RemoteCall.RESTQuery(_reqSession, sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int index = sDGView_Products.Rows.Add();

                    sDGView_Products.Rows[index].Cells["Col_ProductName"].Value = dt.Rows[i]["model_name"].ToString();

                    sDGView_Products.Rows[index].Cells["Col_ProductSize"].Value = dt.Rows[i]["height"].ToString() + "*" + dt.Rows[i]["length"].ToString() + "*" + dt.Rows[i]["width"].ToString();

                    sDGView_Products.Rows[index].Cells["Col_Num"].Value = dt.Rows[i]["amount"].ToString();
                    sDGView_Products.Rows[index].Cells["Col_Info"].Value = dt.Rows[i]["item_memo"].ToString();
                }

            }


            //sDGView_Products.Rows.Clear();
            //try
            //{
            //    DataGridView dgv = sender as DataGridView;

            //    string OrderCodeid = dgv.SelectedRows[0].Cells[0].Value.ToString();

            //    string sql = string.Format("select order_id from orders where order_no='{0}'", OrderCodeid);

            //    string order_id = RemoteCall.RESTQuery(_reqSession, sql).Rows[0][0].ToString();

            //    sql = string.Format("select * from order_items where order_id='{0}' and ordertype={1}", order_id,ordertype);

            //    DataTable dt = RemoteCall.RESTQuery(_reqSession, sql);

            //    //sDGView_Products.DataSource = dt;

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        int index = sDGView_Products.Rows.Add();

            //        sDGView_Products.Rows[index].Cells["Col_ProductName"].Value = dt.Rows[i]["model_name"].ToString();

            //        sDGView_Products.Rows[index].Cells["Col_ProductSize"].Value = dt.Rows[i]["length"].ToString() + "X" + dt.Rows[i]["width"].ToString() + "X" + dt.Rows[i]["height"].ToString();

            //        sDGView_Products.Rows[index].Cells["Col_Num"].Value = dt.Rows[i]["amount"].ToString();
            //        sDGView_Products.Rows[index].Cells["Col_Info"].Value = dt.Rows[i]["item_memo"].ToString();
            //    }

            //    sDGView_Products.ClearSelection();


            //}
            //catch (Exception)
            //{
            //    sDGView_Products.ClearSelection();

            //}
        }


        private void tSButton_Update_Click(object sender, EventArgs e)
        {
            _dtTime = dTPicker_ProductionDate.Value;

            _productType = tSComboBox_ProductType.Text;

            if (_productType == "门扇")
            {
                ordertype = 1007;
            }
            else if(_productType == "门套/窗套/垭口")
            {
                ordertype = 1008;
            }
            //string strTime = dtime.ToString("yyyy-MM-dd");
            string sql = $"select order_id,order_no,customer,phone,address,order_date,delivery_date,order_memo,order_status from orders where order_date='{_dtTime.ToString("yyyy-MM-dd")}'";

            DataTable dt_orders = RemoteCall.RESTQuery(_reqSession, sql);

            List<DataRow> delRows = new List<DataRow>();
            foreach (DataRow dr in dt_orders.Rows)
            {
                sql = $"select * from order_items where order_id={dr["order_id"]} and ordertype={ordertype}";
                DataTable dt_item = RemoteCall.RESTQuery(_reqSession, sql);
                if (dt_item.Rows.Count==0)
                {
                    delRows.Add(dr);
                }
            }

            foreach (DataRow item in delRows)
            {
                dt_orders.Rows.Remove(item);
            }

            if (dt_orders.Rows.Count==0)
            {
                MessageBox.Show($"{_dtTime.ToLongDateString().ToString()}没有\"{_productType}\"类型的订单。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sDGView_Orders.DataSource = dt_orders;
            }
            else
            {
                sDGView_Orders.DataSource = dt_orders;
            }
           

        }

        private void UpdateShow()
        {
            //string sql = $"select order_no,customer,phone,address,order_date,delivery_date,order_memo,order_status from orders where order_status='scheduling'";
            string sql = $"select order_no,customer,phone,address,order_date,delivery_date,order_memo,order_status from orders";

            DataTable dt_orders = RemoteCall.RESTQuery(_reqSession, sql);
            sDGView_Orders.DataSource = dt_orders;

        
            foreach (DataGridViewRow dr in sDGView_Orders.Rows)
            {
                if (dr.Cells["Col_OrderStatus"].Value.ToString().Trim() == "scheduling")
                {
                    dr.Cells["Col_OrderStatus"].Style.BackColor = Color.Red;

                }
            }
        }

    }
}
