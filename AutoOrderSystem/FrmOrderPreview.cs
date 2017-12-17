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
    public partial class FrmOrderPreview :FrmBase
    {
        private List<Order> _orderList;

        private WebRequestSession _reqSession;
        public FrmOrderPreview()
        {
            InitializeComponent();
            //Extensions.SetDoubleBuffered(dGV_DataShow, true);
        }
        public FrmOrderPreview(WebRequestSession session,List<Order> orders):this()
        {
            _orderList = orders;
            _reqSession = session;
        }

        private void OrderPreview_Load(object sender, EventArgs e)
        {
            //订单数据加载到treeView
            foreach (Order order in _orderList)
            {
                TreeNode orderNode = new TreeNode();
                orderNode.Name = order.OrderNo;
                orderNode.Text = order.OrderNo;

                
                foreach (Product product in order.Products)
                {
                    if (!orderNode.Nodes.ContainsKey(product.Position))
                    {
                        TreeNode positionNode = new TreeNode();
                        positionNode.Name = product.Position;
                        positionNode.Text = product.Position;

                        TreeNode productNode = new TreeNode();
                        productNode.Name = product.ProductName;
                        productNode.Text = product.ProductName;
                        positionNode.Nodes.Add(productNode);

                        orderNode.Nodes.Add(positionNode);
                    }
                    else
                    {
                        TreeNode productNode = new TreeNode();
                        productNode.Name = product.ProductName;
                        productNode.Text = product.ProductName;
                        orderNode.Nodes[product.Position].Nodes.Add(productNode);
                    }

                }
                this.treeView_Order.Nodes[0].Nodes.Add(orderNode);
 
            }

            this.treeView_Order.ExpandAll();

            Orders_Num.Text = _orderList.Count.ToString();
        }

        private void treeView_Order_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Level==0)//订单根节点，显示所有的订单
            {
                //清除列
                dGV_DataShow.Columns.Clear();

                #region 添加列
                dGV_DataShow.Columns.Add("OrderNo", "单据编号");
                dGV_DataShow.Columns.Add("ProductionNo", "生产编号");
                dGV_DataShow.Columns.Add("Area", "大区");
                dGV_DataShow.Columns.Add("City", "城市");
                dGV_DataShow.Columns.Add("Customer", "客户姓名");
                //dGV_DataShow.Columns.Add("MaterialType", "材质类型");
                dGV_DataShow.Columns.Add("MainMaterial", "主单材质");
                dGV_DataShow.Columns.Add("Date", "日期");
                dGV_DataShow.Columns.Add("OrderType", "单据类型");
                #endregion

                #region 添加行
                foreach (Order order in _orderList)
                {
                    int index = dGV_DataShow.Rows.Add();
                    dGV_DataShow.Rows[index].Cells["OrderNo"].Value = order.OrderNo;       
                    dGV_DataShow.Rows[index].Cells["City"].Value = order.City;
                    dGV_DataShow.Rows[index].Cells["Customer"].Value = order.Customer;
                    //dGV_DataShow.Rows[index].Cells["MaterialType"].Value = order.MaterialType;
                    dGV_DataShow.Rows[index].Cells["MainMaterial"].Value = order.MainMaterial;
                    dGV_DataShow.Rows[index].Cells["Date"].Value = order.ProductionDate;       
                }
                #endregion

            }
            else if(node.Level==1)//订单号节点,显示此订单的所有产品
            {
                string productionNo = node.Name;
                dGV_DataShow.Columns.Clear();
                #region 添加列
                dGV_DataShow.Columns.Add("Serial", "单序号");
                dGV_DataShow.Columns.Add("ProductName", "产品名称");
                dGV_DataShow.Columns.Add("Position", "位置");
                dGV_DataShow.Columns.Add("OpenDirection", "开向");
                dGV_DataShow.Columns.Add("Lockset", "锁具");
                dGV_DataShow.Columns.Add("Hinge", "合页");
                dGV_DataShow.Columns.Add("InstallationMode", "安装方式");
                dGV_DataShow.Columns.Add("HoleHeight", "洞高");
                dGV_DataShow.Columns.Add("HoleWidth", "洞宽");
                dGV_DataShow.Columns.Add("HoleThick", "洞厚");         
                dGV_DataShow.Columns.Add("ProductMaterial", "产品材质");
                dGV_DataShow.Columns.Add("ProductSize", "尺寸");
                dGV_DataShow.Columns.Add("ProductQuantity", "数量");
                dGV_DataShow.Columns.Add("ProductRemarks", "备注");
                #endregion

                #region 添加行
                foreach (Order order in _orderList)
                {
                    if (order.OrderNo== productionNo)
                    {
                        List<Product> list = order.Products;
                        foreach (Product product in list)
                        {
                            int index = dGV_DataShow.Rows.Add();
                            dGV_DataShow.Rows[index].Cells["Serial"].Value = product.ProductSerial;
                            dGV_DataShow.Rows[index].Cells["Position"].Value = product.Position;
                            dGV_DataShow.Rows[index].Cells["OpenDirection"].Value = product.OpenDirection;
                            dGV_DataShow.Rows[index].Cells["Lockset"].Value = product.Lockset;
                            dGV_DataShow.Rows[index].Cells["Hinge"].Value = product.Hinge;
                            dGV_DataShow.Rows[index].Cells["InstallationMode"].Value = product.InstallationMode;
                            dGV_DataShow.Rows[index].Cells["ProductName"].Value = product.ProductName;
                            dGV_DataShow.Rows[index].Cells["ProductMaterial"].Value = product.ProductMaterial;
                            dGV_DataShow.Rows[index].Cells["ProductSize"].Value = "";
                            dGV_DataShow.Rows[index].Cells["ProductQuantity"].Value = product.ProductQuantity;
                            dGV_DataShow.Rows[index].Cells["ProductRemarks"].Value = product.ProductRemarks;

                        }
                    }
                }
                #endregion

            }
            else if(node.Level==2)//位置的节点
            {
                string position = node.Name;
            }
            else if(node.Level==3)//产品的节点 显示所有部件
            {
                string productname = node.Name;
                string position = node.Parent.Name;
                string productionNo = node.Parent.Parent.Name;
                dGV_DataShow.Columns.Clear();

                #region 添加列
                dGV_DataShow.Columns.Add("PartName", "部件名称");
                dGV_DataShow.Columns.Add("PartMaterial", "部件材质");
                dGV_DataShow.Columns.Add("PartLength", "部件长度");
                dGV_DataShow.Columns.Add("PartWidth", "部件宽度");
                dGV_DataShow.Columns.Add("PartThick", "部件厚度");
                dGV_DataShow.Columns.Add("PartNum", "部件数量");
                #endregion
                #region 添加行
                foreach (Order order in _orderList)
                {
                    if (order.OrderNo==productionNo)
                    {
                        foreach (Product product in order.Products)
                        {
                            if (product.Position== position)
                            {
                                if (product.PartInfo!=null)
                                {
                                    foreach (Part part in product.PartInfo)
                                    {
                                        int index = dGV_DataShow.Rows.Add();
                                        dGV_DataShow.Rows[index].Cells["PartName"].Value = part.PartName;
                                        dGV_DataShow.Rows[index].Cells["PartMaterial"].Value = part.PartMaterialColor;
                                        dGV_DataShow.Rows[index].Cells["PartLength"].Value = part.PartLength;
                                        dGV_DataShow.Rows[index].Cells["PartWidth"].Value = part.PartWidth;
                                        dGV_DataShow.Rows[index].Cells["PartThick"].Value = part.PartThick;
                                        dGV_DataShow.Rows[index].Cells["PartNum"].Value = part.PartNum;

                                    }
                                }
        
                            }
                        }
              
                    }
                }
                #endregion

            }
        }

        private void TSMItem_UploadOrder_Click(object sender, EventArgs e)
        {
            
            if(Common.Common.UploadOrder(_reqSession, _orderList))
            {
                this.DialogResult = DialogResult.OK;
            }

            
            //this.Close();
        }

        private void treeView_Order_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                //textBox1.Text = e.Node.Text;
                if (e.Node.Checked == true)
                {
                    //选中节点之后，选中该节点所有的子节点
                    setChildNodeCheckedState(e.Node, true);
                }
                else if (e.Node.Checked == false)
                {
                    //取消节点选中状态之后，取消该节点所有子节点选中状态
                    setChildNodeCheckedState(e.Node, false);
                    //如果节点存在父节点，取消父节点的选中状态
                    if (e.Node.Parent != null)
                    {
                        setParentNodeCheckedState(e.Node, false);
                    }
                }
            }
        }

        //取消节点选中状态之后，取消所有父节点的选中状态
        private void setParentNodeCheckedState(TreeNode currNode, bool state)
        {
            TreeNode parentNode = currNode.Parent;
            parentNode.Checked = state;
            if (currNode.Parent.Parent != null)
            {
                setParentNodeCheckedState(currNode.Parent, state);
            }
        }
        //选中节点之后，选中节点的所有子节点
        private void setChildNodeCheckedState(TreeNode currNode, bool state)
        {
            TreeNodeCollection nodes = currNode.Nodes;
            if (nodes.Count > 0)
            {
                foreach (TreeNode tn in nodes)
                {
                    tn.Checked = state;
                    setChildNodeCheckedState(tn, state);
                }
            }
        }

        private void tSButton_Upload_Click(object sender, EventArgs e)
        {
            if (_reqSession.IsSigned()) 
            {
                if (Common.Common.UploadOrder(_reqSession, _orderList))
                {
                    MessageBox.Show("ERP订单上载完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("登录超时，请重新登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
 
        }
    }
}
