using System;
using System.Xml;
using System.Text.RegularExpressions;

namespace Base
{
    public static class QueryReplace
    {
        /// <summary>
        /// Do multiple replaces using a valid xml query.
        /// </summary>
        /// <param name="Xml">Input string.</param>
        /// <param name="queryFileName">Valid Xml file containing [multiple ]replace instructions.</param>
        /// <returns>Output string after replaces.</returns>
        public static string Replace(string Xml, string queryFileName)
        {
            string xml = Xml;
            FileProcessor fp = new FileProcessor(queryFileName);
            fp.GetContent();

            XmlDocument xmlfp = new XmlDocument();

            try
            {
                xmlfp.InnerXml = fp.Xml;
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, "QueryReplace", 1);
            }

            try
            {
                XmlNodeList nodeList = xmlfp.SelectNodes("//replace");
                foreach (XmlNode node in nodeList)
                {
                    string a = Regex.Replace(Regex.Replace(node["A"].InnerXml, @">\s+<", "><"), @"</tn-part><tn-part", "</tn-part> <tn-part");
                    string b = Regex.Replace(Regex.Replace(node["B"].InnerXml, @">\s+<", "><"), @"</tn-part><tn-part", "</tn-part> <tn-part");

                    Alert.Message(a);
                    Alert.Message(b);

                    if (node.Attributes.Count > 0)
                    {
                        xml = Regex.Replace(xml, a, b);
                    }
                    else
                    {
                        xml = Regex.Replace(xml, Regex.Escape(a), b);
                    }
                }
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, "QueryReplace", 2);
            }

            return xml;
        }
    }
}
