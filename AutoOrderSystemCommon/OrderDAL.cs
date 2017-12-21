using AutoOrderSystem.Model;
using Newtonsoft.Json.Linq;
using RESTClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace AutoOrderSystem.Common
{
    public class OrderDAL
    {
        private WebRequestSession _reqSession;
        public OrderDAL(WebRequestSession session)
        {
            _reqSession = session;
        }
        public int AddOrder(ExOrder objOrder)
        {
            int order_id = RemoteCall.GetNextID(_reqSession, "Order_Id");

            string url = "/caxa/add_order_item";

            JObject newOrder = new JObject
                {
                    new JProperty("order_id", order_id),
                    new JProperty("order_no", objOrder.OrderNo),
                    new JProperty("customer", objOrder.CustomerName),
                    new JProperty("phone", objOrder.CustomerPhone),
                    new JProperty("person", objOrder.CustomerName),
                    new JProperty("address", objOrder.CustomerAddress),
                    new JProperty("order_date", objOrder.OrderDate),
                    new JProperty("delivery_date", objOrder.DeliveryDate),
                    new JProperty("order_memo", objOrder.Remarks),
                    new JProperty("order_status", "scheduling"),
                    new JProperty("projectid", 0)
                };

            string response = RemoteCall.PostJObject(_reqSession, url, newOrder);

            JObject jResult = JObject.Parse(response);

            if ((int)jResult["result_code"] > 0)
            {
                return order_id;
            }
            else
            {
                return 0;
            }
        }
        public bool DelOrder(ExOrder objOrder)
        {
            try
            {
                string sql = $"delete from orders where order_no='{objOrder.OrderNo}'";
                RemoteCall.RESTQuery(_reqSession, sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }  
        }
        public int AddOrderItem(ExOrderItem objItem,int order_id,int orderType,int productType)
        {
            int item_id = RemoteCall.GetNextID(_reqSession, "Item_Id");
            string url = "/caxa/multipart_order_item";

            NameValueCollection valuePairs = new NameValueCollection();
            valuePairs.Add("item_id", item_id.ToString());
            valuePairs.Add("order_id", order_id.ToString());
            valuePairs.Add("model_name", objItem.Model);
            valuePairs.Add("amount", objItem.Count.ToString());
            valuePairs.Add("length", objItem.Length.ToString());
            valuePairs.Add("width", objItem.Width.ToString());
            valuePairs.Add("height", objItem.Height.ToString());           
            valuePairs.Add("item_memo", objItem.Remarks);
            valuePairs.Add("productname", objItem.ProductName);
            valuePairs.Add("ordertype", orderType.ToString());//订单类型：料单
            valuePairs.Add("product_type_id", productType.ToString());//产品类型ID
            string response = "";
            if (orderType == 1002)//料单需要上传产品描述
            {
                valuePairs.Add("attachment", "File:1");
                NameValueCollection files = new NameValueCollection();

                string tempFile = Path.GetTempFileName();

                XmlDocument doc = new XmlDocument();
                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                doc.AppendChild(dec);
                XmlNode nodeProduct = doc.ImportNode(objItem.ProductNode, true);
                doc.AppendChild(nodeProduct);
                doc.Save(tempFile);

                files.Add("部件信息", tempFile);
                response = RemoteCall.PostMultipartRequest(_reqSession, url, valuePairs, files);
            }
            else
            {
                valuePairs.Add("attachment", "File:0");
                response = RemoteCall.PostMultipartRequest(_reqSession, url, valuePairs);
            }
            JObject jResult = JObject.Parse(response);
            if ((int)jResult["result_code"] > 0)
            {
                return item_id;
            }
            else
            {
                return 0;
            }
        }
        public bool DelOrderItem()
        {
            return true;
        }
        public bool Exists(ExOrder objOrder)
        {
            string sql = $"select count(*) from orders where order_no='{objOrder.OrderNo}'";

            if(Convert.ToInt32(RemoteCall.RESTQuery(_reqSession, sql).Rows[0][0])>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
