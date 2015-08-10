using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ProcessingTools.Base
{
    public abstract class TaggerBase : Base
    {
        private string textContent;
        private HashSet<string> textWords;

        public TaggerBase(string xml)
            : base(xml)
        {
            this.Initialize();
        }

        public TaggerBase(Config config, string xml)
            : base(config, xml)
        {
            this.Initialize();
        }

        public TaggerBase(IBase baseObject)
            : base(baseObject)
        {
            this.Initialize();
        }

        /// <summary>
        /// Gets the text content of the xml document.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Throws when the xml document does not contain text content.</exception>
        protected string TextContent
        {
            get
            {
                if (this.textContent == null || this.NeedsUpdate)
                {
                    this.SetTextContent();
                    this.NeedsUpdate = false;
                }

                return this.textContent;
            }

            private set
            {
                if (value == null || value.Length < 1)
                {
                    throw new ArgumentNullException("This document does not contain valid text content.");
                }

                this.textContent = value;
            }
        }

        /// <summary>
        /// Gets the HashSet of words in the xml document.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">Throws when the xml document does not contain valid words.</exception>
        protected HashSet<string> TextWords
        {
            get
            {
                if (this.textWords == null || this.textWords.Count < 1 || this.NeedsUpdate)
                {
                    this.SetTextContent();
                    this.NeedsUpdate = false;
                }

                return this.textWords;
            }

            private set
            {
                if (value == null || value.Count < 1)
                {
                    throw new ArgumentNullException("This document does not contain valid words.");
                }

                this.textWords = value;
            }
        }

        /// <summary>
        /// Tags plain text strings (no regex) in XmlDocument.
        /// </summary>
        /// <param name="textToTagList">List of text fragments to tag.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <param name="isCaseSensitive">Must be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        protected void TagTextInXmlDocument(IEnumerable<string> textToTagList, TagContent tag, string xpathTemplate, bool isCaseSensitive = true, bool minimalTextSelect = false)
        {
            foreach (string textToTag in textToTagList)
            {
                TagTextInXmlDocument(textToTag, tag, xpathTemplate, isCaseSensitive, minimalTextSelect);
            }
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <param name="isCaseSensitive">Must be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        protected void TagTextInXmlDocument(string textToTag, TagContent tag, string xpathTemplate, bool isCaseSensitive = true, bool minimalTextSelect = false)
        {
            string xpath = string.Format(xpathTemplate, "contains(string(.),'" + textToTag + "')");
            XmlNodeList nodeList = this.XmlDocument.SelectNodes(xpath, this.NamespaceManager);

            TagTextInXmlDocument(textToTag, tag, nodeList, isCaseSensitive, minimalTextSelect);
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="nodeList">The list of nodes where we try to tag textToTag.</param>
        /// <param name="isCaseSensitive">Must be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        protected void TagTextInXmlDocument(string textToTag, TagContent tag, XmlNodeList nodeList, bool isCaseSensitive = true, bool minimalTextSelect = false)
        {
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

            Regex textToTagPatternRegex = new Regex("(?<!<[^>]+)(" + caseSensitiveness + textToTagPattern + ")(?![^<>]*>)");
            Regex textToTagRegex = new Regex("(?<!<[^>]+)\\b(" + caseSensitiveness + textToTagEscaped + ")(?![^<>]*>)");

            string replacement = GetReplacementOfTagNode(textToTag, tag);

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
                    replace = textToTagPatternRegex.Replace(replace, replacement);
                }

                try
                {
                    XmlNode testNode = this.XmlDocument.CreateElement("test-node");
                    testNode.InnerXml = replace;
                }
                catch (Exception e)
                {
                    Alert.Log("\nInvalid replacement string:\n" + replace + "\n\n");

                    replace = node.InnerXml;

                    Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tag text in XmlDocument.");
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

            Regex textToTagPatternRegex = new Regex("(?<!<[^>]+)(" + caseSensitiveness + textToTagPattern + ")(?![^<>]*>)");
            Regex textToTagRegex = new Regex("(?<!<[^>]+)\\b(" + caseSensitiveness + textToTagEscaped + ")(?![^<>]*>)");

            string replacement = GetReplacementOfTagNode(tagNode);

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
                    replace = textToTagPatternRegex.Replace(replace, replacement);
                }

                try
                {
                    XmlNode testNode = this.XmlDocument.CreateElement("test-node");
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

                        Alert.RaiseExceptionForMethod(e, this.GetType().Name, 0, "Tag text in XmlDocument.");
                        Alert.RaiseExceptionForMethod(tagOrderNormalizerException, this.GetType().Name, 0, "Tag text in XmlDocument.");
                    }
                }
                finally
                {
                    node.InnerXml = replace;
                }
            }
        }

        private static string GetReplacementOfTagNode(XmlNode tagNode)
        {
            XmlNode replacementNode = tagNode.Clone();
            replacementNode.InnerText = "$1";

            string replacement = replacementNode.OuterXml;
            return replacement;
        }

        private static string GetReplacementOfTagNode(string textToTag, TagContent tag)
        {
            TagContent replacementTag = new TagContent(tag);
            replacementTag.Attributes += @" full-string=""" + textToTag + @"""";
            replacementTag.FullTag = replacementTag.OpenTag + "$1" + replacementTag.CloseTag;

            string replacement = replacementTag.FullTag;
            return replacement;
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
            XmlElement testXmlNode = this.XmlDocument.CreateElement("test");

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

        private void Initialize()
        {
            this.textContent = null;
            this.textWords = new HashSet<string>();
        }

        private void SetTextContent()
        {
            if (this.Config == null)
            {
                throw new NullReferenceException("Null config.");
            }

            string text = this.XmlDocument.ApplyXslTransform(this.Config.textContentXslFileName);
            text = Regex.Replace(text, @"(?<=\n)\s+", string.Empty);

            this.TextContent = text;
            this.TextWords = text.ExtractWordsFromString();
        }
    }
}
