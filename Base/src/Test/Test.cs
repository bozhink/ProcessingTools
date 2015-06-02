using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Base
{
    public class Test : Base
    {
        public Test() : base() { }
        public Test(string xml) : base(xml) { }

        public void ExtractSystemChecklistAuthority1()
        {
            ParseXmlStringToXmlDocument();
            XmlDocument newXml = new XmlDocument(namespaceManager.NameTable);
            XmlElement root = newXml.CreateElement("root");

            foreach (XmlNode node in xmlDocument.SelectNodes("//fields[taxon_authors_and_year[normalize-space(.)!='']]", namespaceManager))
            {
                XmlElement newNode = newXml.CreateElement("node");
                newNode.InnerXml = node["taxon_authors_and_year"].OuterXml;
                root.AppendChild(newNode);
            }

            newXml.AppendChild(root);

            xml = newXml.OuterXml;
        }

        public void ExtractSystemChecklistAuthority()
        {
            ParseXmlStringToXmlDocument();

            foreach (XmlNode node in xmlDocument.SelectNodes("//fields/taxon_authors_and_year/value[normalize-space(.)!='']", namespaceManager))
            {
                node.InnerText = Regex.Replace(node.InnerText, @"\s+and\s+", " &amp; ");
                node.InnerText = Regex.Replace(node.InnerText, @"(?<=[^,])\s+(?=\d)", ", ");
            }

            xml = xmlDocument.OuterXml;
        }
    }
}
