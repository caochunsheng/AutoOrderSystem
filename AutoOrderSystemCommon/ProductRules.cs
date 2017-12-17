using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace AutoOrderSystem.Common
{
    public class ProductRules
    {
        public static string ProductRulesXmlPath { get; set; }= Application.StartupPath + "\\ProductRules.xml";

        public static List<string> GetProductList()
        {
            List<string> productList = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(ProductRulesXmlPath);

            foreach (XmlNode node in doc.SelectNodes("RecognitionRules/ProductType"))
            {
                productList.Add(node.Attributes["name"].Value);
            }

            return productList;
        }

        public static Dictionary<string,List<string>> GetRulesList(string productType)
        {
            Dictionary<string, List<string>> rules = new Dictionary<string, List<string>>();

            XmlDocument doc = new XmlDocument();
            doc.Load(ProductRulesXmlPath);

            XmlNodeList ruleNodeList = doc.SelectNodes($"RecognitionRules/ProductType[@name='{productType}']/Rule");

            foreach (XmlNode ruleNode in ruleNodeList)
            {
                if (rules.ContainsKey(ruleNode.Attributes["key"].Value))
                {
                    rules[ruleNode.Attributes["key"].Value].Add(ruleNode.Attributes["value"].Value);
                }
                else
                {
                    rules.Add(ruleNode.Attributes["key"].Value, new List<string>(new string[] { ruleNode.Attributes["value"].Value }));
                }
                
                
            }

            return rules;
        }
    }
}
