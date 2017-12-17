using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoOrderSystem.Model
{
    //单序号,位置,开向,锁具,合页,安装方式,洞高,洞宽,洞厚,产品名称,产品材质,结构类型,尺寸,数量,备注  
    public class Product
    {
        public string ProductType { get; set; }//产品类型//门扇、门套、垭口
        public string ProductSerial { get; set; }//产品编号//单序号
        public string ProductName { get; set; }//产品名称
        public string ModelName { get; set; }//产品模型名称
        public string ProductMaterial { get; set; }//产品材质
        public string ProductLength { get; set; }//产品长度
        public string ProductWidth { get; set; }//产品宽度
        public string ProductThick { get; set; }//产品厚度
        public int ProductQuantity { get; set; }//数量
        public string Glass { get; set; }//玻璃
        public string Position { get; set; }//位置
        public string OpenDirection { get; set; }//开向
        public string Lockset { get; set; }//锁具
        public string Hinge { get; set; }//合页
        public string InstallationMode { get; set; }//安装方式
        public string ProductRemarks { get; set; }//备注
        public List<Part> PartInfo { get; set; }//部件信息
    }
}
