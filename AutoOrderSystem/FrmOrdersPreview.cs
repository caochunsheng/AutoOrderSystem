using AutoOrderSystem.Model;
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
    public partial class FrmOrdersPreview : FrmBase
    {
        private List<Order> _orderList;
        private WebRequestSession _reqSession;
        public FrmOrdersPreview()
        {
            InitializeComponent();
        }
        public FrmOrdersPreview(WebRequestSession session, List<Order> orderList):this()
        {
            _orderList = orderList;
            _reqSession = session;

            ShowOrderList(orderList);
        }
        private void ShowOrderList(List<Order> orderList)
        {
            dgv_Orders.DataSource = orderList;
           
        }

        private void dgv_Orders_SelectionChanged(object sender, EventArgs e)
        {
            string orderCode = dgv_Orders.CurrentRow.Cells["OrderNo"].Value.ToString();
            foreach (Order order in _orderList)
            {
                if (order.OrderNo== orderCode)
                {
                    dgv_Products.DataSource = order.Products;
                    //dataGridView.DataSource = new BindingCollection<object>(list);
                }
            }
        }

        private void dgv_Products_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_SubmitOrders_Click(object sender, EventArgs e)
        {

        }
    }
}
