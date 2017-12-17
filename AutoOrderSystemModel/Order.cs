using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoOrderSystem.Model
{
    //订单编号,城市名称,客户姓名,材质类型,主单材质,生产日期,出货日期,
    public class Order
    {
        public string OrderNo { get; set; }
        public string City { get; set; }
        public string Customer { get; set; }
        public string MaterialType { get; set; }
        public string MainMaterial { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<Product> Products { get; set; }
    }
}
