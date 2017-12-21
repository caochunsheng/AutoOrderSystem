using AutoOrderSystem.Model;
using Newtonsoft.Json.Linq;
using RESTClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace AutoOrderSystem.Common
{
    public static class Common
    {
        public static bool UploadOrder(WebRequestSession session, List<Order> orders)
        {
            foreach (Order order in orders)
            {
                //判断订单是否已经存在
                string sql = $"SELECT EXISTS(SELECT 1 FROM orders WHERE order_no = '{order.OrderNo}')";
                DataTable dt = RemoteCall.RESTQuery(session, sql);

                bool b = Convert.ToBoolean(dt.Rows[0][0]);
                if (!Convert.ToBoolean(dt.Rows[0][0]))
                {
                    if (order.Products.Count == 0)
                    {
                        continue;
                    }
                    int order_id = RemoteCall.GetNextID(session, "Order_Id");

                    string url = "/caxa/add_order_item";
                    JObject newOrder = new JObject(
                        new JProperty("order_id", order_id),
                        new JProperty("order_no", order.OrderNo),
                        new JProperty("customer", order.Customer),
                        new JProperty("phone", ""),
                        new JProperty("person", order.Customer),
                        new JProperty("address", order.City),
                        new JProperty("order_date", order.ProductionDate),
                        new JProperty("delivery_date", order.DeliveryDate),
                        new JProperty("order_memo", order.MainMaterial),
                        new JProperty("order_status", "scheduling"),
                        new JProperty("projectid", 0));

                    string response = RemoteCall.PostJObject(session, url, newOrder);
                    JObject jResult = JObject.Parse(response);

                    if ((int)jResult["result_code"] > 0)
                    {
                        LogHelper.WriteLog($"增加订单{order_id}成功", LogType.Status);

                        for (int i = 0; i < order.Products.Count; i++)
                        {
                            Product product = order.Products[i];

                            int item_id = RemoteCall.GetNextID(session, "Item_Id");
                            url = "/caxa/multipart_order_item";
                            NameValueCollection valuePairs = new NameValueCollection();
                            valuePairs.Add("item_id", item_id.ToString());
                            valuePairs.Add("order_id", order_id.ToString());
                            valuePairs.Add("model_name", product.ProductName);
                            valuePairs.Add("amount", product.ProductQuantity.ToString());
                            //valuePairs.Add("length", product.HoleWidth);
                            //valuePairs.Add("width", product.HoleThick);
                            //valuePairs.Add("height", product.HoleHeight);
                            string item_memo = $"{product.Position}/{product.OpenDirection}/{product.Lockset}/{product.Hinge}/{product.InstallationMode}/{product.ProductRemarks}";
                            valuePairs.Add("item_memo", item_memo);
                            valuePairs.Add("attachment", "File:1");
                            valuePairs.Add("productname", GetModelSeriesName(GetModelName(product.ProductName)));

                            NameValueCollection files = new NameValueCollection();

                            if (product.ProductType == "正常门" || product.ProductType == "磁吸门" || product.ProductType == "JO门" || product.ProductType == "其他门")//门扇类型
                            {
                                //2085*750*40
                                valuePairs.Add("length", product.ProductWidth);
                                valuePairs.Add("width", product.ProductThick);
                                valuePairs.Add("height", product.ProductLength);

                                valuePairs.Add("ordertype", "1007");//门扇类型
                            }
                            else //门套类型
                            {
                                if (product.PartInfo != null)
                                {

                                    valuePairs.Add("length", product.ProductWidth);
                                    valuePairs.Add("width", product.ProductThick);
                                    valuePairs.Add("height", product.ProductLength);

                                    valuePairs.Add("ordertype", "1008");//门套类型
                                    //string tempFile = CreateXmlFromPartInfo(order.ProductionNo, product);         
                                }

                            }
                            string tempFile = CreateProductDescription(order.OrderNo, product);
                            files.Add("部件信息", tempFile);


                            response = RemoteCall.PostMultipartRequest(session, url, valuePairs, files);
                            jResult = JObject.Parse(response);
                            if ((int)jResult["result_code"] > 0)
                            {
                                LogHelper.WriteLog($"增加订单项{item_id}成功", LogType.Status);
                                //this.listBoxLog.Items.Add(String.Format("增加订单项成功"));
                            }
                            else
                            {
                                LogHelper.WriteLog($"增加订单项{item_id}失败", LogType.Status);
                                //this.listBoxLog.Items.Add(String.Format("增加订单项失败"));
                            }

                        }

                    }
                    else
                    {
                        LogHelper.WriteLog($"增加订单{order_id}失败",LogType.Status);
                        //this.Log.Text = String.Format("增加订单{0}失败", order.OrderNo);

                        //return false;
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show($"订单{order.OrderNo}已经存在！", "提示", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Information);
                    if (result == DialogResult.Ignore)
                    {

                        LogHelper.WriteLog($"订单{order.OrderNo}已经存在！",LogType.Status);
                        continue;
                    }
                    else if (result == DialogResult.Abort)
                    {
                        break;
                    }

                }

            }

            return true;
        }
        public static bool SubmitOrders(WebRequestSession session,List<Order> orders)
        {
            //两张表，orders和order_items
            DataTable dt_orders = new DataTable("orders");
            #region dt_orders
            DataColumn newCol = new DataColumn("order_id", typeof(int));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = 0;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("order_no", typeof(string));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = "";
            newCol.MaxLength = 128;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("customer", typeof(string));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = "";
            newCol.MaxLength = 256;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("phone", typeof(string));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = "";
            newCol.MaxLength = 128;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("person", typeof(string));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = "";
            newCol.MaxLength = 128;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("address", typeof(string));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = "";
            newCol.MaxLength = 128;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("order_date", typeof(DateTime));
            newCol.AllowDBNull = true;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("delivery_date", typeof(DateTime));
            newCol.AllowDBNull = true;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("order_memo", typeof(string));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = "";
            newCol.MaxLength = 256;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("order_status", typeof(string));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = "";
            newCol.MaxLength = 128;
            dt_orders.Columns.Add(newCol);

            newCol = new DataColumn("projectid", typeof(int));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = 0;
            dt_orders.Columns.Add(newCol);
            #endregion
            DataTable dt_order_items = new DataTable("order_items");
            #region dt_order_items
            newCol = new DataColumn("item_id", typeof(int));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = 0;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("order_id", typeof(int));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = 0;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("model_name", typeof(string));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = "";
            newCol.MaxLength = 256;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("amount", typeof(int));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = 0;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("length", typeof(double));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = 0;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("width", typeof(double));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = 0;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("height", typeof(double));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = 0;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("item_memo", typeof(string));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = "";
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("ordertype", typeof(int));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = 0;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("attachment", typeof(string));
            newCol.AllowDBNull = true;
            newCol.DefaultValue = "";
            newCol.MaxLength = 512;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("productname", typeof(string));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = "";
            newCol.MaxLength = 128;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("projectid", typeof(int));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = 0;
            dt_order_items.Columns.Add(newCol);

            newCol = new DataColumn("productname", typeof(string));
            newCol.AllowDBNull = false;
            newCol.DefaultValue = "";
            dt_order_items.Columns.Add(newCol);

            #endregion

            return true;
        }
        public static string CreateProductDescription(string order_no, Product product)
        {
            XmlDocument doc = new XmlDocument();

            //<? xml version = "1.0" encoding = "UTF-8" standalone = "yes" ?>
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            doc.AppendChild(dec);

            XmlElement eOrder = doc.CreateElement("Order");
            eOrder.SetAttribute("type", "0");
            eOrder.SetAttribute("codeid", order_no);
            doc.AppendChild(eOrder);

            XmlElement eProduct = doc.CreateElement("Product");
            eProduct.SetAttribute("type", product.ProductType);
            eProduct.SetAttribute("codeid", product.ProductSerial);
            eProduct.SetAttribute("name", product.ProductName);
            int result = 0;
            if (Int32.TryParse(product.ProductType, out result))//对于门扇类的则转换失败，对于门套类的则成功
            {
                eProduct.SetAttribute("length", product.ProductLength);
                eProduct.SetAttribute("width", product.ProductWidth);
                eProduct.SetAttribute("thick", product.ProductThick);
                //eProduct.SetAttribute("size", $"{product.ProductLength}*{product.ProductWidth}*{product.ProductThick}");//门套
            }
            else
            {
                eProduct.SetAttribute("length", product.ProductLength);
                eProduct.SetAttribute("width", product.ProductWidth);
                eProduct.SetAttribute("thick", product.ProductThick);
                //eProduct.SetAttribute("size", $"{product.ProductLength}*{product.ProductWidth}*{product.ProductThick}");//门扇
            }
            //eProduct.SetAttribute("size", product.ProductSize);
            eProduct.SetAttribute("material", product.ProductMaterial);
            eProduct.SetAttribute("position", product.Position);//位置
            eProduct.SetAttribute("openDirection", product.OpenDirection);//开向
            eProduct.SetAttribute("lockset", product.Lockset);//锁型
            eProduct.SetAttribute("hinge", product.Hinge);//合页
            eProduct.SetAttribute("glass", product.Glass);//玻璃
            eProduct.SetAttribute("installationMode", product.InstallationMode);//安装方式
            eProduct.SetAttribute("productRemarks", product.ProductRemarks);//备注

            eOrder.AppendChild(eProduct);
            if (product.PartInfo != null)
            {
                int index = 1;

                foreach (Part part in product.PartInfo)
                {
                    for (int i = 0; i < part.PartNum; i++)
                    {
                        XmlElement epart = doc.CreateElement("part");
                        epart.SetAttribute("type", product.ProductType);
                        epart.SetAttribute("codeid", product.ProductSerial + index.ToString("000"));
                        epart.SetAttribute("name", part.PartName);
                        if (part.PartName == "立挺(竖)" && part.PartNum == 2)
                        {
                            if (i == 0)
                            {
                                epart.SetAttribute("name", part.PartName + "A");
                            }
                            else if (i == 1)
                            {
                                epart.SetAttribute("name", part.PartName + "B");
                            }

                        }
                        else
                        {
                            epart.SetAttribute("name", part.PartName);
                        }
                        eProduct.AppendChild(epart);

                        XmlElement Material = doc.CreateElement("Material");
                        Material.SetAttribute("name", product.ProductName.Last().ToString());
                        Material.SetAttribute("type", part.PartMaterialType);
                        Material.SetAttribute("color", part.PartMaterialColor);
                        epart.AppendChild(Material);

                        XmlElement size = doc.CreateElement("size");
                        size.SetAttribute("length", part.PartLength);
                        size.SetAttribute("width", part.PartWidth);
                        size.SetAttribute("thick", part.PartThick);
                        epart.AppendChild(size);

                        index++;
                    }
                }
            }

            string path = Path.GetTempFileName();
            doc.Save(path);

            return path;

        }
        public static bool Convert2Model(DataTable dt, out List<Order> orders)
        {
            if (dt == null)
            {
                orders = null;
                return false;
            }
            else
            {
                try
                {
                    //统计订单号
                    List<string> order_nos = new List<string>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!order_nos.Contains(dr["生产总编号"].ToString()))
                        {
                            order_nos.Add(dr["生产总编号"].ToString());
                        }
                    }

                    orders = new List<Order>();
                    //遍历订单号
                    foreach (string order_no in order_nos)
                    {
                        //筛选同一个订单的所有行
                        DataTable dt_order = FilterData(dt, $"生产总编号='{order_no}'");

                        //筛选出来的首行订单数据填入对象
                        Order order = new Order();
                        order.OrderNo = dt_order.Rows[0]["生产总编号"].ToString();
                        order.City = dt_order.Rows[0]["城市名称"].ToString();
                        order.Customer = dt_order.Rows[0]["客户姓名"].ToString();
                        //order.MaterialType = dt_order.Rows[0]["材质"].ToString();
                        order.MainMaterial = dt_order.Rows[0]["主单材质"].ToString();
                        order.ProductionDate = Convert.ToDateTime(dt_order.Rows[0]["分厂日期"].ToString()); // drs_order[0]["日期"].ToString();
                        order.DeliveryDate = Convert.ToDateTime(dt_order.Rows[0]["计划出货日期"].ToString());
                        order.Products = new List<Product>();

                        //统计单序号，一个单序号是一个产品号
                        List<string> orderserials = new List<string>();
                        foreach (DataRow dr_OrderSerial in dt_order.Rows)
                        {
                            if (!orderserials.Contains(dr_OrderSerial["序号"].ToString()))
                            {
                                orderserials.Add(dr_OrderSerial["序号"].ToString());
                            }
                        }

                        foreach (string orderserial in orderserials)
                        {
                            DataTable dt_orderserial = FilterData(dt_order, $"序号='{orderserial}'");


                            bool isDoor = false;

                            //遍历确定有没有门
                            foreach (DataRow dr in dt_orderserial.Rows)
                            {
                                if (dr["结构类型"].ToString().Contains("门扇"))
                                {
                                    isDoor = true;
                                    break;
                                }
                            }

                            if (!isDoor)//一个单序号下没有门的情况,即窗套，垭口，踢脚线……
                            {
                                Product product = new Product();

                                if (dt_orderserial.Rows[0]["产品名称"].ToString().Contains("窗套"))
                                {
                                    product.ProductType = "1";
                                }
                                else if (dt_orderserial.Rows[0]["产品名称"].ToString().Contains("垭口"))
                                {
                                    product.ProductType = "2";
                                }
                                else
                                {
                                    product.ProductType = "0";
                                }

                                product.ProductSerial = order.OrderNo + dt_orderserial.Rows[0]["序号"].ToString().PadLeft(3, '0');//产品编号=订单号+单序号
                                product.ProductName = dt_orderserial.Rows[0]["产品名称"].ToString();
                                product.ProductMaterial = dt_orderserial.Rows[0]["材质"].ToString();
                                product.Position = dt_orderserial.Rows[0]["位置"].ToString();
                                product.OpenDirection = dt_orderserial.Rows[0]["开向"].ToString();
                                product.Lockset = dt_orderserial.Rows[0]["锁具"].ToString();
                                product.Hinge = dt_orderserial.Rows[0]["合页"].ToString();
                                product.InstallationMode = dt_orderserial.Rows[0]["安装方式"].ToString();
                                product.ProductLength = "0";
                                product.ProductWidth = "0";
                                product.ProductThick = "0";
                                product.ProductQuantity = 1;// Convert.ToInt32(dt_orderserial.Rows[0]["数量"].ToString());
                                product.ProductRemarks = dt_orderserial.Rows[0]["备注"].ToString();
                                product.PartInfo = new List<Part>();

                                foreach (DataRow dr in dt_orderserial.Rows)
                                {
                                    Part part = new Part();
                                    part.PartName = dr["结构类型"].ToString();
                                    part.PartMaterialType = "";
                                    part.PartMaterialColor = dr["材质"].ToString();
                                    part.PartLength = dr["高"].ToString();
                                    part.PartWidth = dr["宽"].ToString();
                                    part.PartThick = dr["厚"].ToString();
                                    part.PartNum = Convert.ToInt32(dr["数量"].ToString());
                                    product.PartInfo.Add(part);
                                }
                                order.Products.Add(product);
                                LogHelper.WriteLog($"订单{order.OrderNo}中添加一个产品{product.ProductName}", LogType.Status);
                            }
                            else
                            {
                                string doorPockct_Type = "";//门型类别

                                //先筛选出来门，除了子母门,子母门是两扇门
                                DataTable dt_Door = FilterData(dt_orderserial, $"结构类型 like '%门扇%'");
                                if (dt_Door.Rows.Count == 1) //单开门
                                {
                                    Product door = new Product();
                                    //通过门型识别门套的类别
                                    if (dt_Door.Rows[0]["厚"].ToString() == "40")
                                    {
                                        doorPockct_Type = "正常门套";
                                        door.ProductType = "正常门";
                                    }
                                    else if (dt_Door.Rows[0]["产品名称"].ToString().Contains("磁吸") && dt_Door.Rows[0]["厚"].ToString() == "45")
                                    {
                                        doorPockct_Type = "磁吸门套";
                                        door.ProductType = "磁吸门";
                                    }
                                    else if (dt_Door.Rows[0]["产品名称"].ToString().Contains("JO") && dt_Door.Rows[0]["厚"].ToString() == "45")
                                    {
                                        doorPockct_Type = "JO门套";
                                        door.ProductType = "JO门";
                                    }
                                    else
                                    {
                                        doorPockct_Type = "其他门套";
                                        door.ProductType = "其他门";
                                    }

                                    door.ProductSerial = order.OrderNo + dt_Door.Rows[0]["序号"].ToString().PadLeft(3, '0');//产品编号=订单号+单序号
                                    door.Position = dt_Door.Rows[0]["位置"].ToString();
                                    door.OpenDirection = dt_Door.Rows[0]["开向"].ToString();
                                    door.Lockset = dt_Door.Rows[0]["锁具"].ToString();
                                    door.Hinge = dt_Door.Rows[0]["合页"].ToString();
                                    door.InstallationMode = dt_Door.Rows[0]["安装方式"].ToString();
                                    door.ProductName = dt_Door.Rows[0]["产品名称"].ToString();
                                    door.ProductMaterial = dt_Door.Rows[0]["材质"].ToString();
                                    door.ProductLength = dt_Door.Rows[0]["高"].ToString(); ;
                                    door.ProductWidth = dt_Door.Rows[0]["宽"].ToString();
                                    door.ProductThick = dt_Door.Rows[0]["厚"].ToString();
                                    door.ProductQuantity = 1;// Convert.ToInt32(dt_orderserial.Rows[0]["数量"].ToString());
                                    door.ProductRemarks = dt_Door.Rows[0]["备注"].ToString();
                                    order.Products.Add(door);
                                    LogHelper.WriteLog($"订单{order.OrderNo}中添加一个产品{ door.ProductName}", LogType.Status);
                                }
                                else if (dt_Door.Rows.Count == 2)//子母门
                                {

                                    for (int i = 0; i < dt_Door.Rows.Count; i++)
                                    {
                                        if (dt_Door.Rows[i]["结构类型"].ToString() == "母门扇")
                                        {
                                            Product mother_door = new Product();
                                            //通过门型识别门套的类别
                                            if (dt_Door.Rows[i]["厚"].ToString() == "40")
                                            {
                                                doorPockct_Type = "正常门套";
                                                mother_door.ProductType = "正常门";
                                            }
                                            else if (dt_Door.Rows[i]["产品名称"].ToString().Contains("磁吸") && dt_Door.Rows[0]["厚"].ToString() == "45")
                                            {
                                                doorPockct_Type = "磁吸门套";
                                                mother_door.ProductType = "磁吸门";
                                            }
                                            else if (dt_Door.Rows[i]["产品名称"].ToString().Contains("JO") && dt_Door.Rows[0]["厚"].ToString() == "45")
                                            {
                                                doorPockct_Type = "JO门套";
                                                mother_door.ProductType = "JO门";
                                            }
                                            else
                                            {
                                                doorPockct_Type = "其他门套";
                                                mother_door.ProductType = "其他门";
                                            }
                                            mother_door.ProductSerial = order.OrderNo + dt_Door.Rows[i]["序号"].ToString().PadLeft(3, '0');//产品编号=订单号+单序号
                                            mother_door.Position = dt_Door.Rows[i]["位置"].ToString();
                                            mother_door.OpenDirection = dt_Door.Rows[i]["开向"].ToString();
                                            mother_door.Lockset = dt_Door.Rows[i]["锁具"].ToString();
                                            mother_door.Hinge = dt_Door.Rows[i]["合页"].ToString();
                                            mother_door.InstallationMode = dt_Door.Rows[i]["安装方式"].ToString();

                                            mother_door.ProductName = dt_Door.Rows[i]["产品名称"].ToString();
                                            mother_door.ProductMaterial = dt_Door.Rows[i]["材质"].ToString();
                                            mother_door.ProductLength = dt_Door.Rows[i]["高"].ToString(); ;
                                            mother_door.ProductWidth = dt_Door.Rows[i]["宽"].ToString(); ;
                                            mother_door.ProductThick = dt_Door.Rows[i]["厚"].ToString(); ;
                                            mother_door.ProductQuantity = 1;// Convert.ToInt32(dt_orderserial.Rows[0]["数量"].ToString());
                                            mother_door.ProductRemarks = dt_Door.Rows[i]["备注"].ToString();
                                            order.Products.Add(mother_door);
                                            LogHelper.WriteLog($"订单{order.OrderNo}中添加一个产品{ mother_door.ProductName}", LogType.Status);


                                        }
                                        else if (dt_Door.Rows[i]["结构类型"].ToString() == "子门扇")
                                        {
                                            Product child_door = new Product();
                                            //通过门型识别门套的类别
                                            if (dt_Door.Rows[i]["厚"].ToString() == "40")
                                            {
                                                child_door.ProductType = "正常门";
                                            }
                                            else if (dt_Door.Rows[i]["产品名称"].ToString().Contains("磁吸") && dt_Door.Rows[0]["厚"].ToString() == "45")
                                            {
                                                child_door.ProductType = "磁吸门";
                                            }
                                            else if (dt_Door.Rows[i]["产品名称"].ToString().Contains("JO") && dt_Door.Rows[0]["厚"].ToString() == "45")
                                            {
                                                child_door.ProductType = "JO门";
                                            }
                                            else
                                            {
                                                child_door.ProductType = "其他门";
                                            }

                                            child_door.ProductSerial = order.OrderNo + dt_Door.Rows[i]["序号"].ToString().PadLeft(3, '0');//产品编号=订单号+单序号
                                            child_door.Position = dt_Door.Rows[i]["位置"].ToString();
                                            child_door.OpenDirection = dt_Door.Rows[i]["开向"].ToString();
                                            child_door.Lockset = dt_Door.Rows[i]["锁具"].ToString();
                                            child_door.Hinge = dt_Door.Rows[i]["合页"].ToString();
                                            child_door.InstallationMode = dt_Door.Rows[i]["安装方式"].ToString();
                                            child_door.ProductName = dt_Door.Rows[i]["产品名称"].ToString();
                                            child_door.ProductMaterial = dt_Door.Rows[i]["材质"].ToString();
                                            child_door.ProductLength = dt_Door.Rows[i]["高"].ToString();
                                            child_door.ProductWidth = dt_Door.Rows[i]["宽"].ToString();
                                            child_door.ProductThick = dt_Door.Rows[i]["厚"].ToString();
                                            child_door.ProductQuantity = 1;// Convert.ToInt32(dt_orderserial.Rows[0]["数量"].ToString());
                                            child_door.ProductRemarks = dt_Door.Rows[i]["备注"].ToString();
                                            order.Products.Add(child_door);
                                            LogHelper.WriteLog($"订单{order.OrderNo}中添加一个产品{ child_door.ProductName}", LogType.Status);

                                        }

                                    }
                                }

                                //在筛选出来玻璃
                                DataRow[] dr_Glass = dt_orderserial.Select($"结构类型='玻璃'");

                                //再筛选出来非门非玻璃,即门套
                                DataTable dt_DoorPocket = FilterData(dt_orderserial, $"结构类型 <>'玻璃' and 结构类型 not like '%门扇%'");

                                if (dt_DoorPocket.Rows.Count > 0)
                                {
                                    Product door_pocket = new Product();//门套
                                    if (doorPockct_Type == "正常门套")
                                    {
                                        door_pocket.ProductType = "4";
                                    }
                                    else if (doorPockct_Type == "磁吸门套")
                                    {
                                        door_pocket.ProductType = "5";
                                    }
                                    else if (doorPockct_Type == "JO门套")
                                    {
                                        door_pocket.ProductType = "3";
                                    }
                                    else
                                    {
                                        door_pocket.ProductType = "0";//未识别的产品
                                    }

                                    door_pocket.ProductSerial = order.OrderNo + dt_DoorPocket.Rows[0]["序号"].ToString().PadLeft(3, '0');//产品编号=订单号+单序号
                                    door_pocket.Position = dt_DoorPocket.Rows[0]["位置"].ToString();
                                    door_pocket.OpenDirection = dt_DoorPocket.Rows[0]["开向"].ToString();
                                    door_pocket.Lockset = dt_DoorPocket.Rows[0]["锁具"].ToString();
                                    door_pocket.Hinge = dt_DoorPocket.Rows[0]["合页"].ToString();
                                    door_pocket.InstallationMode = dt_DoorPocket.Rows[0]["安装方式"].ToString();
                                    door_pocket.ProductName = dt_DoorPocket.Rows[0]["产品名称"].ToString();
                                    door_pocket.ProductMaterial = dt_DoorPocket.Rows[0]["材质"].ToString();
                                    door_pocket.ProductLength = "0";// dt_orderserial.Rows[0]["高"].ToString();
                                    door_pocket.ProductWidth = "0";// dt_orderserial.Rows[0]["宽"].ToString();
                                    door_pocket.ProductThick = "0";// dt_orderserial.Rows[0]["厚"].ToString();//门套产品没有尺寸，尺寸在部件里
                                    door_pocket.ProductQuantity = 1;// Convert.ToInt32(dt_orderserial.Rows[0]["数量"].ToString());
                                    door_pocket.ProductRemarks = dt_DoorPocket.Rows[0]["备注"].ToString();
                                    door_pocket.PartInfo = new List<Part>();

                                    for (int i = 0; i < dt_DoorPocket.Rows.Count; i++)
                                    {
                                        Part part = new Part();
                                        part.PartName = dt_DoorPocket.Rows[i]["结构类型"].ToString();
                                        part.PartMaterialType = dt_DoorPocket.Rows[i]["材质"].ToString();
                                        part.PartMaterialColor = dt_DoorPocket.Rows[i]["材质"].ToString();
                                        part.PartLength = dt_DoorPocket.Rows[i]["高"].ToString();
                                        part.PartWidth = dt_DoorPocket.Rows[i]["宽"].ToString();
                                        part.PartThick = dt_DoorPocket.Rows[i]["厚"].ToString();
                                        part.PartNum = Convert.ToInt32(dt_DoorPocket.Rows[i]["数量"].ToString());
                                        door_pocket.PartInfo.Add(part);
                                        LogHelper.WriteLog($"产品{ door_pocket.ProductName}中添加一个部件{part.PartName}", LogType.Status);
                                    }
                                    order.Products.Add(door_pocket);
                                    LogHelper.WriteLog($"订单{order.OrderNo}中添加一个产品{ door_pocket.ProductName}", LogType.Status);
                                }
                            }

                        }
                        orders.Add(order);
                    }

                    return true;
                }
                catch (Exception)
                {
                    orders = null;
                    return false;
                }
            }
        }
        public static bool ConvertToModel(DataTable dt, out List<Order> orders)
        {
            orders = new List<Order>();
            //统计订单号
            List<string> orderCodeList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (!orderCodeList.Contains(dr["生产总编号"].ToString()))
                {
                    orderCodeList.Add(dr["生产总编号"].ToString());
                }
            }
            //遍历订单号
            foreach (string orderCode in orderCodeList)
            {
                //通过订单号筛选订单
                DataTable dt_order = FilterData(dt, $"生产总编号='{orderCode}'");
                //筛选出来的首行订单数据填入对象
                Order order = new Order()
                {
                    OrderNo = dt_order.Rows[0]["生产总编号"].ToString(),
                    City = dt_order.Rows[0]["城市名称"].ToString(),
                    Customer = dt_order.Rows[0]["客户姓名"].ToString(),
                    MainMaterial = dt_order.Rows[0]["主单材质"].ToString(),
                    ProductionDate = Convert.ToDateTime(dt_order.Rows[0]["分厂日期"].ToString()),// drs_order[0]["日期"].ToString();
                    DeliveryDate = Convert.ToDateTime(dt_order.Rows[0]["计划出货日期"].ToString()),
                    Products = new List<Product>()
                };


                //统计单序号，一个单序号是一个产品号
                List<string> orderSerialList = new List<string>();
                foreach (DataRow dr_OrderSerial in dt_order.Rows)
                {
                    if (!orderSerialList.Contains(dr_OrderSerial["序号"].ToString()))
                    {
                        orderSerialList.Add(dr_OrderSerial["序号"].ToString());
                    }
                }
                //循环遍历单序号
                foreach (string orderSerial in orderSerialList)
                {
                    //通过单序号筛选产品
                    DataTable dt_serial = FilterData(dt_order, $"序号='{orderSerial}'");

                    //筛选产品中的门类型
                    DataTable dt_Door = FilterData(dt_serial, $"结构类型='门扇' or 结构类型='门(活动扇)' or 结构类型='门(固定扇)' or 结构类型='母门扇' or 结构类型='子门扇'");



                    if (dt_Door.Rows.Count != 0)
                    {
                        foreach (DataRow door in dt_Door.Rows)
                        {
                            Product product = new Product();

                            //门类型：单开门，双开门，磁吸门，普通推拉门，阻尼推拉门，折叠门



                            if (door["厚"].ToString() == "40")
                            {
                                product.ProductType = "正常门";
                            }
                            else if (door["产品名称"].ToString().Contains("磁吸") && door["厚"].ToString() == "45")
                            {
                                product.ProductType = "磁吸门";
                            }
                            else if (door["产品名称"].ToString().Contains("JO") && door["厚"].ToString() == "45")
                            {
                                product.ProductType = "JO门";
                            }
                            else
                            {
                                product.ProductType = "其他门";
                            }

                            product.ProductSerial = order.OrderNo + door["序号"].ToString().PadLeft(3, '0');//产品编号=订单号+单序号
                            product.ProductName = door["产品名称"].ToString();
                            product.ModelName = GetModelName(door["产品名称"].ToString());
                            product.ProductMaterial = door["材质"].ToString();
                            product.Position = door["位置"].ToString();
                            product.Glass = door["玻璃"].ToString();
                            product.OpenDirection = door["开向"].ToString();
                            product.Lockset = door["锁具"].ToString();
                            product.Hinge = door["合页"].ToString();
                            product.InstallationMode = door["安装方式"].ToString();
                            product.ProductLength = door["高"].ToString();
                            product.ProductWidth = door["宽"].ToString();
                            product.ProductThick = door["厚"].ToString();
                            product.ProductQuantity = Convert.ToInt32(door["数量"].ToString());
                            product.ProductRemarks = door["备注"].ToString();
                            product.PartInfo = new List<Part>();

                            order.Products.Add(product);
                        }
                    }

                    //筛选产品中的门套，窗套，垭口类型
                    DataTable dt_DoorPocket = FilterData(dt_serial, $"结构类型<>'门扇' and 结构类型<>'门(活动扇)' and 结构类型<>'门(固定扇)' and 结构类型<>'母门扇' and 结构类型<>'子门扇'");

                    if (dt_DoorPocket.Rows.Count != 0)
                    {
                        Product product = new Product();

                        if (dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("普通门套"))
                        {
                            if (dt_DoorPocket.Rows[0]["门厚"].ToString() == "40")//普通门套
                            {
                                product.ProductType = "4";
                            }
                            else if (dt_DoorPocket.Rows[0]["门厚"].ToString() == "45")//JO门套
                            {
                                product.ProductType = "3";
                            }
                            else//未知
                            {
                                product.ProductType = "0";
                            }
                        }
                        else if(dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("入户门套"))//入户门套
                        {
                            if (dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("磁吸入户门套"))
                            {
                                product.ProductType = "5";
                            }
                            else if(dt_DoorPocket.Rows[0]["门厚"].ToString() == "40")
                            {
                                product.ProductType = "4";
                            }
                            else if(dt_DoorPocket.Rows[0]["门厚"].ToString() == "45")
                            {
                                product.ProductType = "3";
                            }
                            else
                            {
                                product.ProductType = "0";
                            }
                        }
                        else if (dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("磁吸门套"))//磁吸门套
                        {
                            product.ProductType = "5";
                        }
                        else if (dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("推拉门套"))//推拉门套，处理为垭口
                        {
                            product.ProductType = "2";
                        }
                        else if (dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("隐形门套"))//隐形门套，处理为未知
                        {
                            product.ProductType = "0";
                        }
                        else if (dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("窗套"))//窗套
                        {
                            product.ProductType = "1";
                        }
                        else if (dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("垭口"))//垭口
                        {
                            product.ProductType = "2";
                        }
                        else
                        {
                            product.ProductType = "0";
                        }



                        //if (dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("窗套"))
                        //{
                        //    product.ProductType = "1";
                        //}
                        //else if (dt_DoorPocket.Rows[0]["产品名称"].ToString().Contains("垭口"))
                        //{
                        //    product.ProductType = "2";
                        //}
                        //else if (dt_DoorPocket.Rows[0]["门厚"].ToString() == "40")
                        //{
                        //    product.ProductType = "4";
                        //}
                        //else if (dt_DoorPocket.Rows[0]["门扇名称"].ToString().Contains("磁吸") && dt_DoorPocket.Rows[0]["门厚"].ToString() == "45")
                        //{
                        //    product.ProductType = "5";
                        //}
                        //else if (dt_DoorPocket.Rows[0]["门扇名称"].ToString().Contains("JO") && dt_DoorPocket.Rows[0]["门厚"].ToString() == "45")
                        //{
                        //    product.ProductType = "3";
                        //}
                        //else
                        //{
                        //    product.ProductType = "0";
                        //}

                        product.ProductSerial = order.OrderNo + dt_DoorPocket.Rows[0]["序号"].ToString().PadLeft(3, '0');//产品编号=订单号+单序号
                        product.ProductName = dt_DoorPocket.Rows[0]["产品名称"].ToString();
                        product.ModelName = GetModelName(dt_DoorPocket.Rows[0]["产品名称"].ToString());
                        product.ProductMaterial = dt_DoorPocket.Rows[0]["材质"].ToString();
                        product.Position = dt_DoorPocket.Rows[0]["位置"].ToString();
                        product.OpenDirection = dt_DoorPocket.Rows[0]["开向"].ToString();
                        product.Lockset = dt_DoorPocket.Rows[0]["锁具"].ToString();
                        product.Hinge = dt_DoorPocket.Rows[0]["合页"].ToString();
                        product.InstallationMode = dt_DoorPocket.Rows[0]["安装方式"].ToString();
                        product.ProductLength = "0";// dt_DoorPocket.Rows[0]["高"].ToString();
                        product.ProductWidth = "0";// dt_DoorPocket.Rows[0]["宽"].ToString();
                        product.ProductThick = "0";// dt_DoorPocket.Rows[0]["厚"].ToString();
                        product.ProductQuantity = 1;// Convert.ToInt32(dt_DoorPocket.Rows[0]["数量"].ToString());
                        product.ProductRemarks = dt_DoorPocket.Rows[0]["备注"].ToString();
                        product.PartInfo = new List<Part>();



                        for (int i = 0; i < dt_DoorPocket.Rows.Count; i++)
                        {
                            Part part = new Part();
                            part.PartName = dt_DoorPocket.Rows[i]["结构类型"].ToString();
                            part.PartMaterialType = dt_DoorPocket.Rows[i]["材质"].ToString();
                            part.PartMaterialColor = dt_DoorPocket.Rows[i]["材质"].ToString();
                            part.PartLength = dt_DoorPocket.Rows[i]["高"].ToString();
                            part.PartWidth = dt_DoorPocket.Rows[i]["宽"].ToString();
                            part.PartThick = dt_DoorPocket.Rows[i]["厚"].ToString();
                            part.PartNum = Convert.ToInt32(dt_DoorPocket.Rows[i]["数量"].ToString());
                            product.PartInfo.Add(part);
                        }

                        order.Products.Add(product);
                    }

                    //筛选产品中的挂板类型

                }

                orders.Add(order);
            }

            return true;
        }
        public static bool ConvertModel(DataTable dt, out List<Order> orders)
        {
            orders = new List<Order>();
            //统计订单号
            List<string> orderCodeList = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                if (!orderCodeList.Contains(dr["生产总编号"].ToString()))
                {
                    orderCodeList.Add(dr["生产总编号"].ToString());
                }
            }
            //遍历订单号
            foreach (string orderCode in orderCodeList)
            {
                //通过订单号筛选订单
                DataTable dt_order = FilterData(dt, $"生产总编号='{orderCode}'");
                //筛选出来的首行订单数据填入对象
                Order order = new Order()
                {
                    OrderNo = dt_order.Rows[0]["生产总编号"].ToString(),
                    City = dt_order.Rows[0]["城市名称"].ToString(),
                    Customer = dt_order.Rows[0]["客户姓名"].ToString(),
                    MainMaterial = dt_order.Rows[0]["主单材质"].ToString(),
                    ProductionDate = Convert.ToDateTime(dt_order.Rows[0]["分厂日期"].ToString()),// drs_order[0]["日期"].ToString();
                    DeliveryDate = Convert.ToDateTime(dt_order.Rows[0]["计划出货日期"].ToString()),
                    Products = new List<Product>()
                };


                //统计单序号，一个单序号是一个产品号
                List<string> orderSerialList = new List<string>();
                foreach (DataRow dr_OrderSerial in dt_order.Rows)
                {
                    if (!orderSerialList.Contains(dr_OrderSerial["序号"].ToString()))
                    {
                        orderSerialList.Add(dr_OrderSerial["序号"].ToString());
                    }
                }
                //循环遍历单序号
                foreach (string orderSerial in orderSerialList)
                {
                    //通过单序号筛选产品
                    DataTable dt_serial = FilterData(dt_order, $"序号='{orderSerial}'");

                    foreach (string productType in ProductRules.GetProductList())
                    {
                        Dictionary<string, List<string>> rules = ProductRules.GetRulesList(productType);
                        List<string> list2 = new List<string>();
                        foreach (string item in rules.Keys)
                        {
                            List<string> list = rules[item];
                            foreach (string item2 in list)
                            {
                                list2.Add($"{item}='{item2}'");
                            }
                        }
                        string filterStr = string.Join(" or ", list2);

                        DataTable dt_product = FilterData(dt_serial, filterStr);

                        if (dt_product.Rows.Count>0)
                        {
                            Product product = new Product()
                            {
                                ProductType = productType,
                                ProductSerial = order.OrderNo + dt_product.Rows[0]["序号"].ToString().PadLeft(3, '0'),//产品编号=订单号+单序号
                                ProductName = dt_product.Rows[0]["产品名称"].ToString(),
                                ModelName = dt_product.Rows[0]["门扇名称"].ToString(),
                                ProductMaterial = dt_product.Rows[0]["材质"].ToString(),
                                Position = dt_product.Rows[0]["位置"].ToString(),
                                Glass = dt_product.Rows[0]["玻璃"].ToString(),
                                OpenDirection = dt_product.Rows[0]["开向"].ToString(),
                                Lockset = dt_product.Rows[0]["锁具"].ToString(),
                                Hinge = dt_product.Rows[0]["合页"].ToString(),
                                InstallationMode = dt_product.Rows[0]["安装方式"].ToString(),
                                ProductLength = dt_product.Rows[0]["门高"].ToString(),
                                ProductWidth = dt_product.Rows[0]["门宽"].ToString(),
                                ProductThick = dt_product.Rows[0]["门厚"].ToString(),
                                ProductQuantity = 1,// Convert.ToInt32(dt_serial.Rows[0]["数量"].ToString()),
                                ProductRemarks = dt_product.Rows[0]["备注"].ToString(),
                                PartInfo = new List<Part>()
                            };

                            foreach (DataRow dr in dt_product.Rows)
                            {
                                Part part = new Part()
                                {
                                    PartName = dr["结构类型"].ToString(),
                                    PartMaterialType = dr["材质"].ToString(),
                                    PartMaterialColor = dr["材质"].ToString(),
                                    PartLength = dr["高"].ToString(),
                                    PartWidth = dr["宽"].ToString(),
                                    PartThick = dr["厚"].ToString(),
                                    PartNum = Convert.ToInt32(dr["数量"].ToString())
                                };
                                product.PartInfo.Add(part);
                            }

                            order.Products.Add(product);
                        }


                        #region
                        //if (productType=="门扇")
                        //{
                        //    foreach (DataRow dr in dt_filter.Rows)
                        //    {
                        //        Product product = new Product()
                        //        {
                        //            ProductType = "Door",//门扇
                        //            ProductSerial = order.OrderNo + dr["序号"].ToString().PadLeft(3, '0'),//产品编号=订单号+单序号
                        //            ProductName = dr["产品名称"].ToString(),
                        //            ModelName = GetModelName(dr["产品名称"].ToString()),
                        //            ProductMaterial = dr["材质"].ToString(),
                        //            Position = dr["位置"].ToString(),
                        //            Glass = dr["玻璃"].ToString(),
                        //            OpenDirection = dr["开向"].ToString(),
                        //            Lockset = dr["锁具"].ToString(),
                        //            Hinge = dr["合页"].ToString(),
                        //            InstallationMode = dr["安装方式"].ToString(),
                        //            ProductLength = dr["高"].ToString(),
                        //            ProductWidth = dr["宽"].ToString(),
                        //            ProductThick = dr["厚"].ToString(),
                        //            ProductQuantity = Convert.ToInt32(dr["数量"].ToString()),
                        //            ProductRemarks = dr["备注"].ToString(),
                        //            PartInfo = new List<Part>()

                        //        };
                        //        order.Products.Add(product);
                        //    }
                        //}
                        //else if (productType == "门套")
                        //{
                        //    Product product = new Product()
                        //    {
                        //        ProductType = "DoorPocket",//门套
                        //        ProductSerial = order.OrderNo + dt_serial.Rows[0]["序号"].ToString().PadLeft(3, '0'),//产品编号=订单号+单序号
                        //        ProductName = dt_serial.Rows[0]["产品名称"].ToString(),
                        //        ModelName = dt_serial.Rows[0]["门扇名称"].ToString(),
                        //        ProductMaterial = dt_serial.Rows[0]["材质"].ToString(),
                        //        Position = dt_serial.Rows[0]["位置"].ToString(),
                        //        Glass = dt_serial.Rows[0]["玻璃"].ToString(),
                        //        OpenDirection = dt_serial.Rows[0]["开向"].ToString(),
                        //        Lockset = dt_serial.Rows[0]["锁具"].ToString(),
                        //        Hinge = dt_serial.Rows[0]["合页"].ToString(),
                        //        InstallationMode = dt_serial.Rows[0]["安装方式"].ToString(),
                        //        ProductLength = dt_serial.Rows[0]["门高"].ToString(),
                        //        ProductWidth = dt_serial.Rows[0]["门宽"].ToString(),
                        //        ProductThick = dt_serial.Rows[0]["门厚"].ToString(),
                        //        ProductQuantity = 1,// Convert.ToInt32(dt_serial.Rows[0]["数量"].ToString()),
                        //        ProductRemarks = dt_serial.Rows[0]["备注"].ToString(),
                        //        PartInfo = new List<Part>()
                        //    };
                        //    foreach (DataRow dr in dt_filter.Rows)
                        //    {
                        //        Part part = new Part()
                        //        {
                        //            PartName = dr["结构类型"].ToString(),
                        //            PartMaterialType = dr["材质"].ToString(),
                        //            PartMaterialColor = dr["材质"].ToString(),
                        //            PartLength = dr["高"].ToString(),
                        //            PartWidth = dr["宽"].ToString(),
                        //            PartThick = dr["厚"].ToString(),
                        //            PartNum = Convert.ToInt32(dr["数量"].ToString())
                        //        };
                        //        product.PartInfo.Add(part);
                        //    }
                        //    order.Products.Add(product);
                        //}
                        //else if (productType == "窗套")
                        //{
                        //    Product product = new Product()
                        //    {
                        //        ProductType = "WindowPocket",//窗套
                        //        ProductSerial = order.OrderNo + dt_serial.Rows[0]["序号"].ToString().PadLeft(3, '0'),//产品编号=订单号+单序号
                        //        ProductName = dt_serial.Rows[0]["产品名称"].ToString(),
                        //        ModelName = dt_serial.Rows[0]["门扇名称"].ToString(),
                        //        ProductMaterial = dt_serial.Rows[0]["材质"].ToString(),
                        //        Position = dt_serial.Rows[0]["位置"].ToString(),
                        //        Glass = dt_serial.Rows[0]["玻璃"].ToString(),
                        //        OpenDirection = dt_serial.Rows[0]["开向"].ToString(),
                        //        Lockset = dt_serial.Rows[0]["锁具"].ToString(),
                        //        Hinge = dt_serial.Rows[0]["合页"].ToString(),
                        //        InstallationMode = dt_serial.Rows[0]["安装方式"].ToString(),
                        //        ProductLength = dt_serial.Rows[0]["门高"].ToString(),
                        //        ProductWidth = dt_serial.Rows[0]["门宽"].ToString(),
                        //        ProductThick = dt_serial.Rows[0]["门厚"].ToString(),
                        //        ProductQuantity = 1,// Convert.ToInt32(dt_serial.Rows[0]["数量"].ToString()),
                        //        ProductRemarks = dt_serial.Rows[0]["备注"].ToString(),
                        //        PartInfo = new List<Part>()
                        //    };

                        //    foreach (DataRow dr in dt_filter.Rows)
                        //    {
                        //        Part part = new Part()
                        //        {
                        //            PartName = dr["结构类型"].ToString(),
                        //            PartMaterialType = dr["材质"].ToString(),
                        //            PartMaterialColor = dr["材质"].ToString(),
                        //            PartLength = dr["高"].ToString(),
                        //            PartWidth = dr["宽"].ToString(),
                        //            PartThick = dr["厚"].ToString(),
                        //            PartNum = Convert.ToInt32(dr["数量"].ToString())
                        //        };
                        //        product.PartInfo.Add(part);
                        //    }
                        //    order.Products.Add(product);
                        //}
                        //else if (productType == "垭口")
                        //{
                        //    Product product = new Product()
                        //    {
                        //        ProductType = "Pass",//垭口
                        //        ProductSerial = order.OrderNo + dt_serial.Rows[0]["序号"].ToString().PadLeft(3, '0'),//产品编号=订单号+单序号
                        //        ProductName = dt_serial.Rows[0]["产品名称"].ToString(),
                        //        ModelName = dt_serial.Rows[0]["门扇名称"].ToString(),
                        //        ProductMaterial = dt_serial.Rows[0]["材质"].ToString(),
                        //        Position = dt_serial.Rows[0]["位置"].ToString(),
                        //        Glass = dt_serial.Rows[0]["玻璃"].ToString(),
                        //        OpenDirection = dt_serial.Rows[0]["开向"].ToString(),
                        //        Lockset = dt_serial.Rows[0]["锁具"].ToString(),
                        //        Hinge = dt_serial.Rows[0]["合页"].ToString(),
                        //        InstallationMode = dt_serial.Rows[0]["安装方式"].ToString(),
                        //        ProductLength = dt_serial.Rows[0]["门高"].ToString(),
                        //        ProductWidth = dt_serial.Rows[0]["门宽"].ToString(),
                        //        ProductThick = dt_serial.Rows[0]["门厚"].ToString(),
                        //        ProductQuantity = 1,// Convert.ToInt32(dt_serial.Rows[0]["数量"].ToString()),
                        //        ProductRemarks = dt_serial.Rows[0]["备注"].ToString(),
                        //        PartInfo = new List<Part>()
                        //    };

                        //    foreach (DataRow dr in dt_filter.Rows)
                        //    {
                        //        Part part = new Part()
                        //        {
                        //            PartName = dr["结构类型"].ToString(),
                        //            PartMaterialType = dr["材质"].ToString(),
                        //            PartMaterialColor = dr["材质"].ToString(),
                        //            PartLength = dr["高"].ToString(),
                        //            PartWidth = dr["宽"].ToString(),
                        //            PartThick = dr["厚"].ToString(),
                        //            PartNum = Convert.ToInt32(dr["数量"].ToString())
                        //        };
                        //        product.PartInfo.Add(part);
                        //    }
                        //    order.Products.Add(product);
                        //}
                        //else
                        //{

                        //}
                        #endregion
                    }

                }

                orders.Add(order);
            }

            return true;
        }
        //public static bool CombineOrder(List<DataTable> )
        public static DataTable FilterData(DataTable sourceData, string filterExpression)
        {
            DataRow[] drs_result = sourceData.Select(filterExpression);
            DataTable dt_result = sourceData.Clone();
            for (int i = 0; i < drs_result.Length; i++)
            {
                dt_result.ImportRow(drs_result[i]);
            }
            return dt_result;
        }
        public static DataTable FilterData(ref DataTable sourceData, string filterExpression,bool isDelete=false )
        {
            DataRow[] drs_result = sourceData.Select(filterExpression);

            DataTable dt_result = sourceData.Clone();
            for (int i = 0; i < drs_result.Length; i++)
            {
                dt_result.ImportRow(drs_result[i]);
            }

            if (isDelete)
            {
                foreach (DataRow dr in drs_result)
                {
                    sourceData.Rows.Remove(dr);
                }
            }
            return dt_result;
        }
        public static string GetModelName(string productName)
        {
            Regex oRegex = new Regex(@"[0-9a-zA-Z]");
            var mach = oRegex.Matches(productName);

            productName = "";
            foreach (Match item in mach)
            {
                productName += item.ToString();
            }
            return productName;
        }
        public static string GetModelSeriesName(string modelName)
        {
            Regex r = new Regex(@"\d");
            return modelName.Remove(r.Match(modelName).Index);
        }
        public static DataTable MergeDataTables(DataTable dt1, DataTable dt2)
        {
            DataTable newDataTable = dt1.Clone();

            object[] obj = new object[newDataTable.Columns.Count];
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i].ItemArray.CopyTo(obj, 0);
                newDataTable.Rows.Add(obj);
            }

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dt2.Rows[i].ItemArray.CopyTo(obj, 0);
                newDataTable.Rows.Add(obj);
            }

            return newDataTable;
        }

       

    }
}
