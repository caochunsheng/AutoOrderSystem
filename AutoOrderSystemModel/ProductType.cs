using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoOrderSystem.Model
{
    public class ProductType
    {
        public string Code { get; set; }
        public string TypeName { get; set; }
        public List<StructureType> StructureTypeList { get; set; }
    }
}
