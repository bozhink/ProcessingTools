using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Base
{
    public abstract class Base
    {
        protected string xml;
        protected XmlDocument xmlDocument;
        private Config config;
        private XmlNamespaceManager namespaceManager;
        private UTF8Encoding encoding;

        public Base()
        {
            InitializeClass(null, string.Empty);
        }

        public Base(string xml)
        {
            InitializeClass(null, xml);
        }

        public Base(Config config)
        {
            InitializeClass(config, string.Empty);
        }

        public Base(Config config, string xml)
        {
            InitializeClass(config, xml);
        }

        public Base(Base objectToClone)
        {
            this.Xml = objectToClone.Xml;
            this.XmlDocument = objectToClone.XmlDocument;
            this.encoding = (UTF8Encoding) objectToClone.DefaultEncoding;
            this.config = objectToClone.Config;
            this.namespaceManager = objectToClone.NamespaceManager;
        }

        public string Xml
        {
            get
            {
                return this.xml;
            }

            set
            {
                this.xml = value;
            }
        }

        public XmlDocument XmlDocument
        {
            get
            {
                return this.xmlDocument;
            }

            set
            {
                this.xmlDocument = value;
            }
        }

        protected Config Config
        {
            get
            {
                return this.config;
            }
        }

        protected Encoding DefaultEncoding
        {
            get
            {
                return this.encoding;
            }
        }

        protected XmlNamespaceManager NamespaceManager
        {
            get
            {
                return this.namespaceManager;
            }
        }

        public static List<string> ExtractWordsFromString(string text)
        {
            List<string> result = new List<string>();

            for (Match m = Regex.Match(text, @"\b\w+\b"); m.Success; m = m.NextMatch())
            {
                result.Add(m.Value);
            }

            result = result.Distinct().ToList();
            result.Sort();

            return result;
        }

        public static List<string> ExtractWordsFromXml(XmlDocument xml)
        {
            return Base.ExtractWordsFromString(xml.InnerText);
        }

        public static XmlNamespaceManager TaxPubNamespceManager()
        {
            XmlNamespaceManager nspm = new XmlNamespaceManager(new XmlDocument().NameTable);
            nspm.AddNamespace("tp", "http://www.plazi.org/taxpub");
            nspm.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nspm.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
            nspm.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            return nspm;
        }

        public static XmlNamespaceManager TaxPubNamespceManager(XmlNameTable nameTable)
        {
            XmlNamespaceManager nspm = new XmlNamespaceManager(nameTable);
            nspm.AddNamespace("tp", "http://www.plazi.org/taxpub");
            nspm.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nspm.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
            nspm.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            return nspm;
        }

        public static XmlNamespaceManager TaxPubNamespceManager(XmlDocument xmlDocument)
        {
            XmlNamespaceManager nspm = new XmlNamespaceManager(xmlDocument.NameTable);
            nspm.AddNamespace("tp", "http://www.plazi.org/taxpub");
            nspm.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            nspm.AddNamespace("xml", "http://www.w3.org/XML/1998/namespace");
            nspm.AddNamespace("mml", "http://www.w3.org/1998/Math/MathML");
            return nspm;
        }

        public static List<string> GetStringListOfUniqueXmlNodes(XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
        {
            List<string> result = new List<string>();
            try
            {
                XmlNodeList nodeList;
                if (namespaceManager == null)
                {
                    nodeList = xml.SelectNodes(xpath);
                }
                else
                {
                    nodeList = xml.SelectNodes(xpath, namespaceManager);
                }

                result = Base.GetStringListOfUniqueXmlNodes(nodeList);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        public static List<string> GetStringListOfUniqueXmlNodes(IEnumerable xmlNodeList)
        {
            List<string> result = new List<string>();
            try
            {
                result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerXml).Distinct().ToList();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        public static List<string> GetStringListOfUniqueXmlNodeContent(XmlNode xml, string xpath, XmlNamespaceManager namespaceManager = null)
        {
            List<string> result = new List<string>();
            try
            {
                XmlNodeList nodeList;
                if (namespaceManager == null)
                {
                    nodeList = xml.SelectNodes(xpath);
                }
                else
                {
                    nodeList = xml.SelectNodes(xpath, namespaceManager);
                }

                result = Base.GetStringListOfUniqueXmlNodeContent(nodeList);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        public static List<string> GetStringListOfUniqueXmlNodeContent(IEnumerable xmlNodeList)
        {
            List<string> result = new List<string>();
            try
            {
                result = xmlNodeList.Cast<XmlNode>().Select(c => c.InnerText).Distinct().ToList();
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, 0, 1);
            }

            return result;
        }

        public static string NormalizeNlmToSystemXml(Config config, string xml)
        {
            return XsltOnString.ApplyTransform(config.formatXslNlmToSystem, xml);
        }

        public static string NormalizeNlmToSystemXml(Config config, XmlDocument xml)
        {
            return XsltOnString.ApplyTransform(config.formatXslNlmToSystem, xml);
        }

        public static string NormalizeSystemToNlmXml(Config config, string xml)
        {
            return XsltOnString.ApplyTransform(config.formatXslSystemToNlm, xml);
        }

        public static string NormalizeSystemToNlmXml(Config config, XmlDocument xml)
        {
            return XsltOnString.ApplyTransform(config.formatXslSystemToNlm, xml);
        }

        public List<string> ExtractWordsFromXml()
        {
            this.ParseXmlStringToXmlDocument();
            return Base.ExtractWordsFromString(this.xmlDocument.InnerText);
        }

        protected void NormalizeXmlToSystemXml()
        {
            this.xml = Base.NormalizeNlmToSystemXml(this.config, this.xml);
        }

        protected void NormalizeSystemXmlToCurrent()
        {
            if (this.config.NlmStyle)
            {
                this.xml = Base.NormalizeSystemToNlmXml(this.config, this.xml);
            }
        }

        protected void ParseXmlDocumentToXmlString()
        {
            this.xml = this.xmlDocument.OuterXml;
        }

        protected void ParseXmlStringToXmlDocument()
        {
            try
            {
                this.xmlDocument.LoadXml(this.xml);
            }
            catch (Exception e)
            {
                Alert.RaiseExceptionForMethod(e, this.GetType().Name, 10, 3);
            }
        }

        private void InitializeClass(Config config, string xml)
        {
            this.config = config;
            this.xml = xml;
            this.xmlDocument = new XmlDocument();
            this.xmlDocument.PreserveWhitespace = true;
            if (this.xml != string.Empty && this.xml != null)
            {
                try
                {
                    this.xmlDocument.LoadXml(xml);
                }
                catch (Exception e)
                {
                    Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
                }
            }

            this.namespaceManager = Base.TaxPubNamespceManager(this.xmlDocument);
            this.encoding = new UTF8Encoding(false);
        }

        /// <summary>
        /// Tags plain text string (no regex) in the xmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <param name="isCaseSensitive">Must be the search case sensitive?</param>
        protected void TagTextInXmlDocument(string textToTag, TagContent tag, string xpathTemplate, bool isCaseSensitive = true)
        {
            string xpath = string.Format(xpathTemplate, "contains(string(.),'" + textToTag + "')");
            XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpath, this.NamespaceManager);

            TagTextInXmlDocument(textToTag, tag, nodeList);
        }

        /// <summary>
        /// Tags plain text string (no regex) in the xmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="nodeList">The list of nodes where we try to tag textToTag.</param>
        /// <param name="isCaseSensitive">Must be the search case sensitive?</param>
        protected void TagTextInXmlDocument(string textToTag, TagContent tag, XmlNodeList nodeList, bool isCaseSensitive = true)
        {
            string textToTagEscaped = Regex.Replace(Regex.Escape(textToTag), "'", "\\W");
            string textToTagPattern = @"(?:<[\w\!][^>]*>)*\b" + Regex.Replace(textToTagEscaped, @"([^\\])(?!\Z)", "$1(?:<[^>]*>)*") + @"\b(?:<[\/\!][^>]*>)*";

            string caseSensitiveness = string.Empty;
            if (!isCaseSensitive)
            {
                caseSensitiveness = "(?i)";
            }

            Regex textTotagPatternRegex = new Regex("(?<!<[^>]+)(" + caseSensitiveness + textToTagPattern + ")(?![^<>]*>)");
            Regex textToTagRegex = new Regex("(?<!<[^>]+)\\b(" + caseSensitiveness + textToTagEscaped + ")(?![^<>]*>)");

            TagContent replacement = new TagContent(tag);
            replacement.Attributes += @" full-string=""" + textToTag + @"""";
            replacement.FullTag = replacement.OpenTag + "$1" + replacement.CloseTag;

            foreach (XmlNode node in nodeList)
            {
                string replace = node.InnerXml;

                /*
                 * Here we need this if because the use of textTotagPatternRegex is potentialy dangerous:
                 * this is dynamically generated regex which might be too complex and slow.
                 */
                if (textToTagRegex.Match(node.InnerText).Length == textToTagRegex.Match(node.InnerXml).Length)
                {
                    replace = textToTagRegex.Replace(replace, replacement.FullTag);
                }
                else
                {
                    replace = textTotagPatternRegex.Replace(replace, replacement.FullTag);
                }

                try
                {
                    XmlNode testNode = this.xmlDocument.CreateElement("test-node");
                    testNode.InnerXml = replace;
                }
                catch (Exception e)
                {
                    Alert.Message("\nInvalid replacement string:\n" + replace + "\n\n");

                    replace = node.InnerXml;

                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tag text in xmlDocument.");
                }
                finally
                {
                    node.InnerXml = replace;
                }
            }
        }

        protected class TagContent
        {
            private string name;

            public TagContent(string name = "", string attributes = "", string fullTag = "")
            {
                this.Name = name;
                this.Attributes = attributes;
                this.FullTag = fullTag;
            }

            public TagContent(TagContent tag)
            {
                this.Name = tag.Name;
                this.Attributes = tag.Attributes;
                this.FullTag = tag.FullTag;
            }

            public bool IsClosingTag
            {
                get;
                set;
            }

            public string Name
            {
                get
                {
                    return this.name;
                }

                set
                {
                    this.name = value;
                    this.IsClosingTag = (this.name.Length > 0) ? ((this.name[0] == '/') ? true : false) : false;
                }
            }

            public string Attributes
            {
                get;
                set;
            }

            public string FullTag
            {
                get;
                set;
            }

            public string OpenTag
            {
                get
                {
                    string openTag = "<" + this.Name;
                    string attributes = this.Attributes;
                    if (attributes != string.Empty && attributes != null)
                    {
                        openTag += attributes;
                    }

                    openTag += ">";

                    return openTag;
                }
            }

            public string CloseTag
            {
                get
                {
                    string closeTag = "</" + this.Name + ">";
                    return closeTag;
                }
            }
        }
    }
}
