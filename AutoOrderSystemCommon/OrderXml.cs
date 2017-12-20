using AutoOrderSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AutoOrderSystem.Common
{
    public class OrderXml
    {
        public string FilePath { get; set; }
        public OrderXml()
        {

        }
        public OrderXml(string filePath)
        {
            FilePath = filePath;
        }
        public bool Convert2Model(out List<ExOrder> orderList)
        {
            orderList = new List<ExOrder>();

            XmlDocument doc = new XmlDocument();
            doc.Load(FilePath);

            foreach (XmlNode nodeOrder in doc.SelectNodes("Orders/Order"))
            {
                ExOrder objOrder = new ExOrder()
                {
                    OrderNo = nodeOrder.Attributes["orderNo"].Value,
                    CustomerName = nodeOrder.Attributes["customerName"].Value,
                    CustomerPhone = nodeOrder.Attributes["customerPhone"].Value,
                    CustomerAddress = nodeOrder.Attributes["customerAddress"].Value,
                    OrderDate = Convert.ToDateTime(nodeOrder.Attributes["orderDate"].Value),
                    DeliveryDate = Convert.ToDateTime(nodeOrder.Attributes["deliveryDate"].Value),
                    ItemList = new List<ExOrderItem>()
                };

                foreach (XmlNode nodeProduct in nodeOrder.SelectNodes("Product"))
                {
                    string str = nodeProduct.Attributes["size"].Value;

                    double length = Convert.ToDouble(str.Split('*')[0]);
                    double width = Convert.ToDouble(str.Split('*')[1]);
                    double height = Convert.ToDouble(str.Split('*')[2]);
                    string remark = $"{nodeProduct.Attributes["grain"].Value}/{nodeProduct.Attributes["grain"].Value}/{nodeProduct.Attributes["color"].Value}/{nodeProduct.Attributes["grain"].Value}/{nodeProduct.Attributes["grain"].Value}/{nodeProduct.Attributes["grain"].Value}";


                    ExOrderItem objItem = new ExOrderItem()
                    {
                        ProductName = nodeProduct.Attributes["name"].Value,
                        ProductType = nodeProduct.Attributes["type"].Value,
                        Model = nodeProduct.Attributes["model"].Value,
                        ModelSource = "File",
                        Length = length,
                        Width = width,
                        Height = height,
                        Count = Convert.ToInt32(nodeProduct.Attributes["count"].Value),
                        ProductDec = new StringBuilder(nodeProduct.OuterXml),
                        Remarks = remark
                    };

                    objOrder.ItemList.Add(objItem);
                }

                orderList.Add(objOrder);
            }

            return true;


        }
    }
}
