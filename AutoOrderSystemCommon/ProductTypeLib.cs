using AutoOrderSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AutoOrderSystem.Common
{
    public class ProductTypeLib
    {
        public static string ProductTypeLibPath = Application.StartupPath + "\\ProductTypeLib.xml";
        private static bool Exist(string productTypeNameOrCode, bool isTypeName = true)
        {
            if (!File.Exists(ProductTypeLibPath))
            {

                return false;
            }

            XmlDocument doc_Lib = new XmlDocument();
            doc_Lib.Load(ProductTypeLibPath);

            if(doc_Lib.SelectNodes("ProductTypeLib/ProductType")==null)
            {
                return false;
            }

            foreach (XmlNode productType in doc_Lib.SelectNodes("ProductTypeLib/ProductType"))
            {
                if (isTypeName)
                {
                    string str = productType.Attributes["name"].Value;

                    if (str == productTypeNameOrCode)
                    {
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (productType.Attributes["code"].Value == productTypeNameOrCode)
                    {
                        return true;
                    }
                    else
                    {
                        continue;
                    }
                }

            }

            return false;


        }
        private static void AddProductType(string productTypeName)
        {
            if (!File.Exists(ProductTypeLibPath))
            {
                XmlDocument doc = new XmlDocument();

                XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", "yes");
                doc.AppendChild(dec);

                XmlElement eProductTypeLib = doc.CreateElement("ProductTypeLib");
                doc.AppendChild(eProductTypeLib);

                XmlElement eProductType = doc.CreateElement("ProductType");
                eProductType.SetAttribute("code", "P001");
                eProductType.SetAttribute("name", productTypeName);
                eProductTypeLib.AppendChild(eProductType);

                doc.Save(ProductTypeLibPath);
            }
            else
            {
                XmlDocument doc_Lib = new XmlDocument();
                doc_Lib.Load(ProductTypeLibPath);

                XmlNode rootNode = doc_Lib.SelectSingleNode("ProductTypeLib");

                XmlNode lastNode = rootNode.LastChild;

                if (lastNode == null)
                {
                    XmlElement eProductType = doc_Lib.CreateElement("ProductType");
                    eProductType.SetAttribute("code", "P001");
                    eProductType.SetAttribute("name", productTypeName);
                    rootNode.AppendChild(eProductType);

                }
                else
                {
                    string lastCodeNum = lastNode.Attributes["code"].Value.Substring(1);

                    XmlElement eproductType = doc_Lib.CreateElement("ProductType");
                    eproductType.SetAttribute("code","P"+Convert.ToInt32(lastCodeNum).ToString("000"));
                    eproductType.SetAttribute("name", productTypeName);
                    rootNode.AppendChild(eproductType);

                }


                doc_Lib.Save(ProductTypeLibPath);
            }

        }
        public static bool Write2Lib(List<ProductType> productTypeList)
        {
            if (productTypeList==null||productTypeList.Count==0)
            {
                return false;
            }
            foreach (ProductType type in productTypeList)
            {
                if (!Exist(type.TypeName))
                {
                    AddProductType(type.TypeName);
                }
            }
            return true;

        }
        public static DataTable GetDistinctSelf(DataTable SourceDt, string filedName)
        {
            for (int i = SourceDt.Rows.Count - 2; i > 0; i--)
            {
                DataRow[] rows = SourceDt.Select(string.Format("{0}='{1}'", filedName, SourceDt.Rows[i][filedName]));
                if (rows.Length > 1)
                {
                    SourceDt.Rows.RemoveAt(i);
                }
            }
            return SourceDt;
        }


        public static List<ProductType> GetProductTypeModel(DataTable dt_source)
        {
            List<ProductType> list = new List<ProductType>();


            List<string> productTypeList = new List<string>();

            foreach (DataRow dr in dt_source.Rows)
            {
                if (!productTypeList.Contains(dr["产品类型"].ToString().Trim()))
                {
                    productTypeList.Add(dr["产品类型"].ToString().Trim());
                }
            }

            foreach (string productType in productTypeList)
            {
                DataTable dt_12 = Common.FilterData(dt_source, $"产品类型='{productType}'");

                ProductType ptype = new ProductType();
                ptype.TypeName = productType;
                ptype.StructureTypeList = new List<StructureType>();

                List<string> structureTypeList = new List<string>();
                foreach (DataRow dr in dt_12.Rows)
                {
                    if (!structureTypeList.Contains(dr["结构类型"].ToString().Trim()))
                    {
                        structureTypeList.Add(dr["结构类型"].ToString().Trim());
                    }
                }

                foreach (string item in structureTypeList)
                {
                    StructureType stype = new StructureType();
                    stype.TypeName = item;

                    ptype.StructureTypeList.Add(stype);
                }


                list.Add(ptype);
            }

            return list;
        }
    }
}
