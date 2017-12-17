using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoOrderSystem.Model
{
    public class Part
    {
        public string PartName { get; set; }
        public string PartMaterialType { get; set; }//材质类型
        public string PartMaterialColor { get; set; }//材质颜色
        public string PartLength { get; set; }
        public string PartWidth { get; set; }
        public string PartThick { get; set; }
        public int PartNum { get; set; }

    }
}
