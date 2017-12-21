using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AutoOrderSystem.Model
{
    public class ExOrderItem
    {
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string ProductCode { get; set; }
        public string Model { get; set; }
        public string ModelSource { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int Count { get; set; }
        public string Remarks { get; set; }
        public StringBuilder ProductDec { get; set; }
        public XmlNode ProductNode { get; set; }
    }
}
