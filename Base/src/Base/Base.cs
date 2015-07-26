using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public abstract class Base : IBase
    {
        protected string xml;
        protected XmlDocument xmlDocument;
        private string textContent;
        private HashSet<string> textWords;
        private Config config;
        private XmlNamespaceManager namespaceManager;

        public Base(string xml)
        {
            Initialize(null, xml);
        }

        public Base(Config config, string xml)
        {
            Initialize(config, xml);
        }

        public Base(Base objectToClone)
        {
            this.Xml = objectToClone.Xml;
            this.XmlDocument = objectToClone.XmlDocument;
            this.config = objectToClone.Config;
            this.namespaceManager = objectToClone.NamespaceManager;
        }

        public Config Config
        {
            get
            {
                return this.config;
            }
        }

        public XmlNamespaceManager NamespaceManager
        {
            get
            {
                return this.namespaceManager;
            }
        }

        public string Xml
        {
            get
            {
                return this.xml;
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    try
                    {
                        this.xml = value;
                        this.xmlDocument.LoadXml(this.xml);
                        this.textContent = XsltOnString.ApplyTransform(this.config.textContentXslFileName, this.xml);
                        this.textWords = Base.ExtractWordsFromString(this.textContent);
                    }
                    catch (XmlException e)
                    {
                        throw e;
                    }
                    catch (Exception e)
                    {
                        Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
                    }
                }
                else
                {
                    throw new ArgumentNullException("Invalid value for Xml: null or empty.");
                }
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
                if (value != null)
                {
                    try
                    {
                        this.xmlDocument = value;
                        this.xml = this.xmlDocument.OuterXml;
                        this.textContent = XsltOnString.ApplyTransform(this.config.textContentXslFileName, this.xml);
                        this.textWords = ExtractWordsFromString(this.textContent);
                    }
                    catch (XmlException e)
                    {
                        throw e;
                    }
                    catch (Exception e)
                    {
                        Alert.RaiseExceptionForType(e, this.GetType().Name, 51);
                    }
                }
                else
                {
                    throw new ArgumentNullException("Invalid value for XmlDocument: null.");
                }
            }
        }

        protected string TextContent
        {
            get
            {
                return this.textContent;
            }
        }

        protected HashSet<string> TextWords
        {
            get
            {
                return this.textWords;
            }
        }

        public static HashSet<string> ExtractWordsFromString(string text)
        {
            List<string> result = new List<string>();

            for (Match m = Regex.Match(text, @"\b\w+\b"); m.Success; m = m.NextMatch())
            {
                result.Add(m.Value);
            }

            result = result.Distinct().ToList();
            result.Sort();

            return new HashSet<string>(result);
        }

        public static HashSet<string> ExtractWordsFromXml(XmlDocument xml)
        {
            return ExtractWordsFromString(xml.InnerText);
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

        public HashSet<string> ExtractWordsFromXml()
        {
            this.ParseXmlStringToXmlDocument();
            return ExtractWordsFromString(this.TextContent);
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

        private void Initialize(Config config, string xml)
        {
            this.config = config;
            this.namespaceManager = ProcessingTools.Config.TaxPubNamespceManager();
            this.xmlDocument = new XmlDocument(this.namespaceManager.NameTable);
            this.xmlDocument.PreserveWhitespace = true;
            this.Xml = xml; // This must not precede this.xmlDocument initialization
        }

        protected static List<string> GetMatchesInXmlText(XmlNodeList nodeList, Regex search, bool clearList = true)
        {
            List<string> result = new List<string>();

            foreach (XmlNode node in nodeList)
            {
                result.AddRange(GetMatchesInXmlText(node, search, false));
            }

            if (clearList)
            {
                result = result.Distinct().ToList();
                result.Sort();
            }

            return result;
        }

        protected static List<string> GetMatchesInXmlText(XmlNode node, Regex search, bool clearList = true)
        {
            List<string> result = new List<string>();

            string text = node.InnerText;
            for (Match m = search.Match(text); m.Success; m = m.NextMatch())
            {
                result.Add(m.Value);
            }

            if (clearList)
            {
                result = result.Distinct().ToList();
                result.Sort();
            }

            return result;
        }

        /// <summary>
        /// Tags plain text string (no regex) in the xmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <param name="isCaseSensitive">Must be the search case sensitive?</param>
        /// /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        protected void TagTextInXmlDocument(string textToTag, TagContent tag, string xpathTemplate, bool isCaseSensitive = true, bool minimalTextSelect = false)
        {
            string xpath = string.Format(xpathTemplate, "contains(string(.),'" + textToTag + "')");
            XmlNodeList nodeList = this.xmlDocument.SelectNodes(xpath, this.NamespaceManager);

            TagTextInXmlDocument(textToTag, tag, nodeList, isCaseSensitive, minimalTextSelect);
        }

        /// <summary>
        /// Tags plain text string (no regex) in the xmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="nodeList">The list of nodes where we try to tag textToTag.</param>
        /// <param name="isCaseSensitive">Must be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        protected void TagTextInXmlDocument(string textToTag, TagContent tag, XmlNodeList nodeList, bool isCaseSensitive = true, bool minimalTextSelect = false)
        {
            ////TagContent replacement = new TagContent(tag);
            ////replacement.Attributes += @" full-string=""" + textToTag + @"""";
            ////replacement.FullTag = replacement.OpenTag + textToTag + replacement.CloseTag;

            ////XmlDocument tagNode = new System.Xml.XmlDocument();
            ////tagNode.LoadXml(replacement.FullTag);
            ////TagTextInXmlDocument(tagNode, nodeList, isCaseSensitive, minimalTextSelect);

            string caseSensitiveness = string.Empty;
            if (!isCaseSensitive)
            {
                caseSensitiveness = "(?i)";
            }

            string textToTagEscaped = Regex.Replace(Regex.Escape(textToTag), "'", "\\W");
            string textToTagPattern = @"\b" + Regex.Replace(textToTagEscaped, @"([^\\])(?!\Z)", "$1(?:<[^>]*>)*") + @"\b";
            if (!minimalTextSelect)
            {
                textToTagPattern = @"(?:<[\w\!][^>]*>)*" + textToTagPattern + @"(?:<[\/\!][^>]*>)*";
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
                    Alert.Log("\nInvalid replacement string:\n" + replace + "\n\n");

                    replace = node.InnerXml;

                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tag text in xmlDocument.");
                }
                finally
                {
                    node.InnerXml = replace;
                }
            }
        }

        protected void TagTextInXmlDocument(XmlDocument tagSet, XmlNodeList nodeList, bool isCaseSensitive = true, bool minimalTextSelect = false)
        {
            foreach (XmlNode tagNode in tagSet.DocumentElement.ChildNodes)
            {
                TagTextInXmlDocument(tagNode, nodeList, isCaseSensitive, minimalTextSelect);
            }
        }

        protected void TagTextInXmlDocument(XmlNode tagNode, XmlNodeList nodeList, bool isCaseSensitive = true, bool minimalTextSelect = false)
        {
            string caseSensitiveness = string.Empty;
            if (!isCaseSensitive)
            {
                caseSensitiveness = "(?i)";
            }

            string textToTagEscaped = Regex.Replace(Regex.Escape(tagNode.InnerText), "'", "\\W");
            string textToTagPattern = @"\b" + Regex.Replace(textToTagEscaped, @"([^\\])(?!\Z)", "$1(?:<[^>]*>)*") + @"\b";
            if (!minimalTextSelect)
            {
                textToTagPattern = @"(?:<[\w\!][^>]*>)*" + textToTagPattern + @"(?:<[\/\!][^>]*>)*";
            }

            Regex textTotagPatternRegex = new Regex("(?<!<[^>]+)(" + caseSensitiveness + textToTagPattern + ")(?![^<>]*>)");
            Regex textToTagRegex = new Regex("(?<!<[^>]+)\\b(" + caseSensitiveness + textToTagEscaped + ")(?![^<>]*>)");

            XmlNode replacementNode = tagNode.Clone();
            replacementNode.InnerText = "$1";

            string replacement = replacementNode.OuterXml;

            foreach (XmlNode node in nodeList)
            {
                string replace = node.InnerXml;

                /*
                 * Here we need this if because the use of textTotagPatternRegex is potentialy dangerous:
                 * this is dynamically generated regex which might be too complex and slow.
                 */
                if (textToTagRegex.Match(node.InnerText).Length == textToTagRegex.Match(node.InnerXml).Length)
                {
                    replace = textToTagRegex.Replace(replace, replacement);
                }
                else
                {
                    replace = textTotagPatternRegex.Replace(replace, replacement);
                }

                try
                {
                    XmlNode testNode = this.xmlDocument.CreateElement("test-node");
                    testNode.InnerXml = replace;
                }
                catch (Exception e)
                {
                    Alert.Log("\nInvalid replacement string:\n" + replace + "\n\n");

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
