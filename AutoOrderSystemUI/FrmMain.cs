using AutoOrderSystem.Common;
using AutoOrderSystem.Model;
using RESTClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoOrderSystem.UI
{
    public partial class FrmMain : Form
    {
        private List<ExOrder> _orderList;
        private WebRequestSession _reqSession;
        public FrmMain()
        {
            InitializeComponent();
            _orderList = new List<ExOrder>();
        }
        public FrmMain(WebRequestSession session)
        {
            _reqSession = session;
        }
        #region 最大最小关闭事件
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.btnMax.Image = global::AutoOrderSystem.UI.Properties.Resources.Max;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximumSize = Screen.FromHandle(this.Handle).WorkingArea.Size;
                this.WindowState = FormWindowState.Maximized;
                this.btnMax.Image = global::AutoOrderSystem.UI.Properties.Resources.Normal;
            }
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region 拖动窗体的实现
        private Point mouseOff;//鼠标移动位置变量
        private bool leftFlag;//标签是否为左键
        private void pnlCaption_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void pnlCaption_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }

        private void pnlCaption_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }
        #endregion

        #region 双击放缩窗口实现
        private void pnlCaption_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.btnMax.Image = global::AutoOrderSystem.UI.Properties.Resources.Max;
            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                this.btnMax.Image = global::AutoOrderSystem.UI.Properties.Resources.Normal;
            }
        }
        #endregion

        #region 拖动改变窗体大小
        const int HTLEFT = 10;

        const int HTRIGHT = 11;

        const int HTTOP = 12;

        const int HTTOPLEFT = 13;

        const int HTTOPRIGHT = 14;

        const int HTBOTTOM = 15;

        const int HTBOTTOMLEFT = 0x10;

        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)

        {

            switch (m.Msg)

            {

                case 0x0084:

                    base.WndProc(ref m);

                    Point vPoint = new Point((int)m.LParam & 0xFFFF,

                                   (int)m.LParam >> 16 & 0xFFFF);

                    vPoint = PointToClient(vPoint);

                    if (vPoint.X <= 5)

                        if (vPoint.Y <= 5)

                            m.Result = (IntPtr)HTTOPLEFT;

                        else if (vPoint.Y >= ClientSize.Height - 5)

                            m.Result = (IntPtr)HTBOTTOMLEFT;

                        else m.Result = (IntPtr)HTLEFT;

                    else if (vPoint.X >= ClientSize.Width - 5)

                        if (vPoint.Y <= 5)

                            m.Result = (IntPtr)HTTOPRIGHT;

                        else if (vPoint.Y >= ClientSize.Height - 5)

                            m.Result = (IntPtr)HTBOTTOMRIGHT;

                        else m.Result = (IntPtr)HTRIGHT;

                    else if (vPoint.Y <= 5)

                        m.Result = (IntPtr)HTTOP;

                    else if (vPoint.Y >= ClientSize.Height - 5)

                        m.Result = (IntPtr)HTBOTTOM;

                    break;

                case 0x0201://鼠标左键按下的消息 用于实现拖动窗口功能

                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标

                    m.LParam = IntPtr.Zero;//默认值

                    m.WParam = new IntPtr(2);//鼠标放在标题栏内

                    base.WndProc(ref m);

                    break;

                default:

                    base.WndProc(ref m);

                    break;

            }

        }
        #endregion

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            LogHelper.WriteLog("<添加订单>", LogType.Status);
            //int index = dgvOrder.Rows.Add();
            //dgvOrder.Rows[index].Cells["ColSelected"].Value = true;
            //dgvOrder.Rows[index].Cells["ColOrderNo"].Value = "1712129003";
            //dgvOrder.Rows[index].Cells["ColCustomerName"].Value = "王小二";
            //dgvOrder.Rows[index].Cells["ColCustomerPhone"].Value = "13934804590";
            //dgvOrder.Rows[index].Cells["ColCustomerAddress"].Value = "北京市顺义区李遂镇";
            //dgvOrder.Rows[index].Cells["ColOrderDate"].Value = "2017-12-18";
            //dgvOrder.Rows[index].Cells["ColDeliveryDate"].Value = "2017-12-19";
            //dgvOrder.Rows[index].Cells["ColOrderDetail"].Value = "【详情】";

        }
        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int colIndex = e.ColumnIndex;
            int rowIndex = e.RowIndex;
            if (colIndex == 8&&rowIndex>=0)
            {
                string orderNo = dgvOrder.Rows[rowIndex].Cells["ColOrderNo"].Value.ToString();
                ExOrder selectedOrder = _orderList.Find(order =>
                 {
                     if (order.OrderNo == orderNo)
                     {
                         return true;
                     }
                     else
                     {
                         return false;
                     }
                 });
                this.ShowOrderItemList(selectedOrder.ItemList);
                this.splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                this.splitContainer1.Panel2Collapsed = true;
            }
        }
        private void btnERPOrder_Click(object sender, EventArgs e)
        {
            LogHelper.WriteLog("<ERP订单>", LogType.Status);
            FrmERPOrder frm = new FrmERPOrder();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _orderList = frm._orderList;
                this.ShowOrderList(_orderList);
            }
        }
        private void ShowOrderList(List<ExOrder> orderList)
        {
            dgvOrder.Rows.Clear();
            foreach (ExOrder objOrder in orderList)
            {
                int index = dgvOrder.Rows.Add();
                dgvOrder.Rows[index].Cells["ColSelected"].Value = true;
                dgvOrder.Rows[index].Cells["ColOrderNo"].Value = objOrder.OrderNo;
                dgvOrder.Rows[index].Cells["ColCustomerName"].Value = objOrder.CustomerName;
                dgvOrder.Rows[index].Cells["ColCustomerPhone"].Value = objOrder.CustomerPhone;
                dgvOrder.Rows[index].Cells["ColCustomerAddress"].Value = objOrder.CustomerAddress;
                dgvOrder.Rows[index].Cells["ColOrderDate"].Value = objOrder.OrderDate.ToShortDateString();
                dgvOrder.Rows[index].Cells["ColDeliveryDate"].Value = objOrder.DeliveryDate.ToShortDateString();
                dgvOrder.Rows[index].Cells["ColOrderRemarks"].Value = objOrder.Remarks;
                dgvOrder.Rows[index].Cells["ColOrderDetail"].Value = "【详情】";
            }
        }
        private void ShowOrderItemList(List<ExOrderItem> itemList)
        {
            dgvOrderItem.Rows.Clear();
            int index;
            foreach (ExOrderItem objItem in itemList)
            {
                index = dgvOrderItem.Rows.Add();
                dgvOrderItem.Rows[index].Cells["ColProductName"].Value = objItem.ProductName;
                dgvOrderItem.Rows[index].Cells["ColProductType"].Value = objItem.ProductType;
                dgvOrderItem.Rows[index].Cells["ColProductCode"].Value = objItem.ProductCode;
                dgvOrderItem.Rows[index].Cells["ColModel"].Value = objItem.Model;
                dgvOrderItem.Rows[index].Cells["ColModelSource"].Value = objItem.ModelSource;
                dgvOrderItem.Rows[index].Cells["ColProductSize"].Value = $"{objItem.Length}*{objItem.Width}*{objItem.Height}";
                dgvOrderItem.Rows[index].Cells["ColProductCount"].Value = objItem.Count;
                dgvOrderItem.Rows[index].Cells["ColRemarks"].Value = objItem.Remarks;

            }
        }
        private void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            LogHelper.WriteLog("<提交订单>", LogType.Status);
            dgvOrder.EndEdit();

            List<string> unselectedorderNoList = new List<string>();
            foreach (DataGridViewRow dr in dgvOrder.Rows)
            {
                if (!Convert.ToBoolean(dr.Cells["ColSelected"].Value))//筛选出没选中的，移除，剩下的就是选中的。
                {
                    unselectedorderNoList.Add(dr.Cells["ColOrderNo"].Value.ToString());
                }
            }
            foreach (string orderNo in unselectedorderNoList)
            {
                ExOrder selectedOrder = _orderList.Find(order =>
                {
                    if (order.OrderNo == orderNo)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

                _orderList.Remove(selectedOrder);
            }

            if (_orderList.Count<=0)
            {
                MessageBox.Show("抱歉，您没有选中任何一个订单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OrderDAL orderData = new OrderDAL(_reqSession);
            ProductTypeDAL typeDate = new ProductTypeDAL(_reqSession);
            int orderid, itemid;
            
            foreach (ExOrder objOrder in _orderList)
            {
                //判断订单是否存在
                if(orderData.Exists(objOrder))
                {
                    LogHelper.WriteLog($"检测到订单{objOrder.OrderNo}已经存在！", LogType.Status);
                    DialogResult result= MessageBox.Show($"订单{objOrder.OrderNo}已经存在\n点击【是】\n点击【否】\n点击【取消】", "警告", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                    return;
                }
                else
                {
                    orderid = orderData.AddOrder(objOrder);
                    if (orderid > 0)
                    {
                        foreach (ExOrderItem item in objOrder.ItemList)
                        {
                            //检测产品类型是否存在，
                            if (typeDate.Exists(item.ProductType))
                            {
                                itemid = orderData.AddOrderItem(item, orderid, 1002, typeDate.GetIndex(item.ProductType));
                                if (itemid <= 0)//添加失败
                                {
                                    orderData.DelOrder(objOrder);
                                }
                            }
                        }
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show($"订单【{objOrder.OrderNo}】提交失败！", "错误", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);

                        return;
                    }
                }

            }
        }
        private void btnDelOrder_Click(object sender, EventArgs e)
        {
            LogHelper.WriteLog("<删除订单>", LogType.Status);
            dgvOrder.EndEdit();
            List<string> orderNoList = new List<string>();
            foreach (DataGridViewRow dr in dgvOrder.Rows)
            {
                if (Convert.ToBoolean(dr.Cells["ColSelected"].Value))
                {
                    orderNoList.Add(dr.Cells["ColOrderNo"].Value.ToString());
                }
            }
            if (orderNoList.Count==0)
            {
                MessageBox.Show("抱歉，您没有选中任何一个订单！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            foreach (string orderNo in orderNoList)
            {
                ExOrder selectedOrder = _orderList.Find(order =>
                {
                    if (order.OrderNo == orderNo)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

                _orderList.Remove(selectedOrder);
            }

            ShowOrderList(_orderList);


        }
        private void menuItemSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvOrder.Rows)
            {
                dr.Cells["ColSelected"].Value = true;
            }
        }
        private void menuItemCancelAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvOrder.Rows)
            {
                dr.Cells["ColSelected"].Value = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            WebRequestSession Session = new WebRequestSession();
            if(Session.Login(true, "192.168.7.104", 443, "admin", "admin", String.Empty, "此处放机器ID"))
            {
                _reqSession = Session;
                MessageBox.Show("登陆成功");
            }
        }
    }
}
