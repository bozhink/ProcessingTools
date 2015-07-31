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

        public Base(IBase objectToClone)
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
                        SetTextContent();
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
                        SetTextContent();
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

        private void SetTextContent()
        {
            this.textContent = XsltOnString.ApplyTransform(this.config.textContentXslFileName, this.xml);
            this.textContent = Regex.Replace(this.textContent, @"(?<=\n)\s+", string.Empty);
            this.textWords = ExtractWordsFromString(this.textContent);

            ////Alert.Log(this.textContent.Length);
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
                    try
                    {
                        replace = this.TagOrderNormalizer(replace, tagNode.Name);
                    }
                    catch (Exception tagOrderNormalizerException)
                    {
                        Alert.Log("\nInvalid replacement string:\n" + replace + "\n\n");

                        replace = node.InnerXml;

                        Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tag text in xmlDocument.");
                        Alert.RaiseExceptionForMethod(tagOrderNormalizerException, this.GetType().Name, 0, "Tag text in xmlDocument.");
                    }
                }
                finally
                {
                    node.InnerXml = replace;
                }
            }
        }

        protected string TagNodeContent(string text, string keyString, string openTag)
        {
            string tagName = Regex.Match(openTag, @"(?<=<)[^\s>/""']+").Value;
            string closeTag = "</" + tagName + ">";
            openTag = Regex.Replace(openTag, "/", string.Empty);

            StringBuilder sb = new StringBuilder();
            StringBuilder charStack = new StringBuilder();

            int j = 0, len = keyString.Length;

            for (int i = 0; i < text.Length; i++)
            {
                char ch = text[i];
                j = 0;
                if (ch == keyString[j])
                {
                    charStack.Clear();

                TestCharInKeyString:
                    while (ch == keyString[j])
                    {
                        charStack.Append(ch);
                        if (++j >= len)
                        {
                            break;
                        }

                        ch = text[++i];
                    }

                    if (j < len)
                    {
                        // In the loop above we didn’t reach the end of the keyString
                        // Here we have the ch character which is not appended to the charStack StringBuilder
                        charStack.Append(ch);
                        if (ch == '<')
                        {
                            while (ch != '>')
                            {
                                ch = text[++i];
                                charStack.Append(ch);
                            }

                            // Here ch == '>'. Everything is in charStack up to ch = '>'.
                            ch = text[++i];
                            goto TestCharInKeyString;
                        }
                        else
                        {
                            // This means that ch != keyString[j] because here we have no match
                            // then just append the stored content in charStack and go ahead
                            sb.Append(charStack.ToString());
                        }
                    }
                    else
                    {
                        // Here we have the whole keyString match in the charStack
                        sb.Append(openTag + charStack.ToString() + closeTag);
                    }
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Normalizes crossed tags where tagName is the name of the tag which must not be splitted.
        /// Example:
        ///   &lt;y&gt;__&lt;x1&gt;__&lt;x2&gt;__&lt;/y&gt;__&lt;/x2&gt;__&lt;/x1&gt;
        /// will be transformed to
        ///   &lt;y&gt;__&lt;x1&gt;__&lt;x2&gt;__&lt;/x2&gt;&lt;/x1&gt;&lt;/y&gt;&lt;x1&gt;&lt;x2&gt;__&lt;/x2&gt;__&lt;/x1&gt;
        /// </summary>
        /// <param name="text">Text to be normalized.</param>
        /// <param name="tagName">Name of the non-breakable tag.</param>
        /// <returns>Normalized text.</returns>
        protected string TagOrderNormalizer(string text, string tagName)
        {
            XmlElement testXmlNode = this.xmlDocument.CreateElement("test");

            // Control counter and maximal value of iterations
            const int MaxNumberOfIterations = 100;
            int iter = 0;
        NormalizeXmlNode:
            try
            {
                testXmlNode.InnerXml = text;
            }
            catch (XmlException)
            {
                //// <tagName> ... <x1> ...<x2> ... </x2> ... </x1> ... </x3> ... </x4> ... </tagName>
                //// will become
                //// </x3></x4><tagName><x4><x3> ... <x1> ...<x2> ... </x2> ... </x1> ... </x3> ... </x4> ... </tagName>

                List<TagContent> tags = GetTagModel(text);

                /*
                 * Broken block are pieces of xml in which the number of opening and closing tags is equal,
                 * i.e. the problem in broken blocks are crossed tags
                 * 
                 * Example of different broken blocks
                 * 
                 * envo
                 * some-tag x="y"
                 * nested-tag y="z" z="w"
                 * /envo
                 * /nested-tag
                 * /some-tag
                 * 
                 * some-tag
                 * nested-tag y="z" z="w"
                 * envo
                 * /nested-tag
                 * /some-tag
                 * /envo
                 * 
                 */
                for (int brokenBlockIndex = 0, len = tags.Count; brokenBlockIndex < len; brokenBlockIndex++)
                {
                    // Generate the RegEx pattern to select every broken block
                    string singleBrokenPattern = GenerateSingleBrokenBlockSearchPattern(tags, ref brokenBlockIndex);
                    Regex matchSingleBroken = new Regex(singleBrokenPattern);

                    for (Match singleBrokenBlock = matchSingleBroken.Match(text); singleBrokenBlock.Success; singleBrokenBlock = singleBrokenBlock.NextMatch())
                    {
                        string matchValue = singleBrokenBlock.Value;

                        // Get the rightmost match of the singleBrokenPattern in m.Value
                        {
                            Match m1;
                            while (true)
                            {
                                m1 = matchSingleBroken.Match(matchValue.Substring(2));

                                if (!m1.Success)
                                {
                                    break;
                                }

                                matchValue = m1.Value;
                            }
                        }

                        string replace = matchValue;
                        List<TagContent> localTags = GetTagModel(replace);

                        // Here we have 2 cases: localTags[0].Name == 'tagName' and localTags[localTags.Count - 1].Name == '/tagName'
                        int firstLocalItem = 0;
                        int lastLocalItem = localTags.Count - 1;

                        if (string.Compare(tagName, localTags[firstLocalItem].Name) == 0)
                        {
                            // The first tag in localTags is the tagName-tag
                            // <envo>.*?<some-tag\ x="y">.*?<nested-tag\ y="z"\ z="w">.*?</envo>.*?</nested-tag>.*?</some-tag>
                            string prefix = Regex.Replace(replace, @"\A(.*?)</" + tagName + @">.*\Z", "$1");
                            string suffix = Regex.Replace(replace, @"\A.*?</" + tagName + @">(.*)\Z", "$1");
                            string body = "</" + tagName + ">";

                            for (int i = firstLocalItem + 1; i <= lastLocalItem; ++i)
                            {
                                if (string.Compare(tagName, localTags[i].Name.Substring(1)) == 0)
                                {
                                    // Here we are looking for the closing tag
                                    break;
                                }

                                body = "</" + localTags[i].Name + ">" + body + localTags[i].FullTag;
                            }

                            replace = prefix + body + suffix;
                        }
                        else
                        {
                            // The last tag in localTags is the tagName-tag
                            // <some-tag>.*?<nested-tag\ y="z"\ z="w">.*?<envo>.*?</nested-tag>.*?</some-tag>.*?</envo>
                            string prefix = Regex.Replace(replace, @"\A(.*)<" + tagName + @"[^>]*>.*?\Z", "$1");
                            string suffix = Regex.Replace(replace, @"\A.*<" + tagName + @"[^>]*>(.*?)\Z", "$1");
                            string body = Regex.Replace(replace, @"\A.*(<" + tagName + @"[^>]*>).*?\Z", "$1");

                            for (int i = firstLocalItem; i < lastLocalItem; ++i)
                            {
                                if (string.Compare(tagName, localTags[i].Name) == 0)
                                {
                                    // Here we are looking for the opening tag
                                    break;
                                }

                                body = "</" + localTags[i].Name + ">" + body + localTags[i].FullTag;
                            }

                            replace = prefix + body + suffix;
                        }

                        text = Regex.Replace(text, Regex.Escape(matchValue), replace);
                    }
                }

                // Try again if text is a valid XmlNode content
                if (++iter > MaxNumberOfIterations)
                {
                    throw new Exception("TagOrderNormalizer: invalid XmlBlock");
                }

                goto NormalizeXmlNode;
            }

            return testXmlNode.InnerXml;
        }

        private static List<TagContent> GetTagModel(string text)
        {
            List<TagContent> tags = new List<TagContent>();
            Regex matchTag = new Regex(@"</?\w[^>]*>");
            Regex matchTagName = new Regex(@"\A/?[^\s/<>""'!\?]+");
            Regex matchTagAttributes = new Regex(@"\s+.*\Z");
            for (Match wholeTag = matchTag.Match(text); wholeTag.Success; wholeTag = wholeTag.NextMatch())
            {
                // For every tag name
                string internalOfTag = wholeTag.Value.Substring(1, wholeTag.Value.Length - 2);
                string tagName = matchTagName.Match(internalOfTag).Value;
                string tagAttributes = matchTagAttributes.Match(internalOfTag).Value;
                TagContent tag = new TagContent(tagName, tagAttributes, wholeTag.Value);

                if (tag.IsClosingTag)
                {
                    int lastItemIndex = tags.Count - 1;
                    if (string.Compare(tags[lastItemIndex].Name, tag.Name.Substring(1)) == 0)
                    {
                        tags.RemoveAt(lastItemIndex);
                    }
                    else
                    {
                        tags.Add(tag);
                    }
                }
                else
                {
                    tags.Add(tag);
                }
            }

            return tags;
        }

        private static string GenerateSingleBrokenBlockSearchPattern(List<TagContent> tags, ref int initialIndexInTags)
        {
            StringBuilder singleBrokenPattern = new StringBuilder();
            List<string> brokenStack = new List<string>();

            for (int k = initialIndexInTags, len = tags.Count; k < len; ++k)
            {
                singleBrokenPattern.Append(Regex.Escape(tags[k].FullTag));

                // The main idea here is that the number of opening tags must be equal to the number of closing tag.
                if (tags[k].IsClosingTag)
                {
                    for (int kk = brokenStack.Count - 1; kk >= 0; --kk)
                    {
                        if (string.Compare(brokenStack[kk], tags[k].Name.Substring(1)) == 0)
                        {
                            brokenStack.RemoveAt(kk);
                            break;
                        }
                    }
                }
                else
                {
                    brokenStack.Add(tags[k].Name);
                }

                if (brokenStack.Count > 0)
                {
                    singleBrokenPattern.Append(".*?");
                }
                else
                {
                    initialIndexInTags = k;
                    break;
                }
            }

            return singleBrokenPattern.ToString();
        }
    }
}
