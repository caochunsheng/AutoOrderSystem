using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace RESTClient
{
    public class ConfigNode
    {
        public string Name;
        public Dictionary<string, String> Attributes;
        public List<ConfigNode> Childs;

        public ConfigNode()
        {
            Name = "";
            Attributes = new Dictionary<string, string>();
            Childs = new List<ConfigNode>();
        }
    }

    public class AssemblyLine
    {
        private static AssemblyLine _AssemblyLine = null;
        public static AssemblyLine getInstance(String xmlFile)
        {
            if (_AssemblyLine == null)
                _AssemblyLine = new AssemblyLine();
            _AssemblyLine.Reload(xmlFile);
            return _AssemblyLine;
        }

        private ConfigNode _rootNode;
        public AssemblyLine()
        {
            _rootNode = new ConfigNode();
        }

        public Boolean Reload(String xmlFile)
        {
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(xmlFile))
                return false;
            try
            {
                doc.Load(xmlFile);

                XmlNode xmlContainer = doc.SelectSingleNode("/");
                if (xmlContainer == null)
                    return false;
                _rootNode = ReadNode(xmlContainer);
            }
            catch (Exception e)
            { }

            return false;
        }

        public ConfigNode ReadNode(XmlNode childNode)
        {
            ConfigNode curNode = new ConfigNode();
            curNode.Name = childNode.Name;

            if (childNode.Attributes != null)
            {
                foreach (XmlAttribute attribute in childNode.Attributes)
                    curNode.Attributes[attribute.Name] = attribute.Value;
            }

            for (int i = 0; i < childNode.ChildNodes.Count; i++)
            {
                ConfigNode newChild = ReadNode(childNode.ChildNodes[i]);
                if (!String.IsNullOrEmpty(newChild.Name))
                    curNode.Childs.Add(newChild);
            }
            return curNode;
        }
    }
}
