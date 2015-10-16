namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Configurator;
    using Globals;

    public static class ContentTagger
    {
        public static string GetReplacementOfTagNode(this XmlNode tagNode)
        {
            XmlNode replacementNode = tagNode.Clone();
            replacementNode.InnerText = "$1";

            string replacement = replacementNode.OuterXml;
            return replacement;
        }

        public static string GetReplacementOfTagNode(this TagContent tag, string textToTag)
        {
            TagContent replacementTag = new TagContent(tag);
            replacementTag.Attributes += @" full-string=""" + textToTag + @"""";
            replacementTag.FullTag = replacementTag.OpenTag + "$1" + replacementTag.CloseTag;

            string replacement = replacementTag.FullTag;
            return replacement;
        }

        public static void TagContentInDocument(
                            this IEnumerable<string> textToTagList,
            TagContent tag,
            string xpathTemplate,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            foreach (string item in textToTagList)
            {
                item.TagContentInDocument(tag, xpathTemplate, document, caseSensitive, minimalTextSelect, logger);
            }
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <param name="document">XmlDocument object to be tagged.</param>
        /// <param name="caseSensitive">Must be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        public static void TagContentInDocument(
            this string textToTag,
            TagContent tag,
            string xpathTemplate,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            string xpath = string.Format(xpathTemplate, "contains(string(.),'" + textToTag + "')");
            XmlNodeList nodeList = document.SelectNodes(xpath, Config.TaxPubNamespceManager());

            textToTag.TagContentInDocument(tag, nodeList, caseSensitive, minimalTextSelect, logger);
        }

        /// <summary>
        /// Tags IEnumerable of plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="textToTagList">The IEnumerable object of plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="nodeList">The list of nodes where we try to tag textToTag.</param>
        /// <param name="caseSensitive">Should be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        public static void TagContentInDocument(
            this IEnumerable<string> textToTagList,
            TagContent tag,
            XmlNodeList nodeList,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            foreach (string item in textToTagList)
            {
                item.TagContentInDocument(tag, nodeList, caseSensitive, minimalTextSelect, logger);
            }
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="textToTag">The plain text string to be tagged in the XML.</param>
        /// <param name="tag">The tag model.</param>
        /// <param name="nodeList">The list of nodes where we try to tag textToTag.</param>
        /// <param name="caseSensitive">Should be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        public static void TagContentInDocument(
            this string textToTag,
            TagContent tag,
            XmlNodeList nodeList,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            string caseSensitiveness = string.Empty;
            if (!caseSensitive)
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

            string replacement = tag.GetReplacementOfTagNode(textToTag);

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

                node.SafeReplaceInnerXml(replace, logger);
            }
        }

        public static void TagContentInDocument(
            this XmlNodeList tagNodeList,
            XmlNodeList documentNodeList,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            foreach (XmlNode tagNode in tagNodeList)
            {
                tagNode.TagContentInDocument(documentNodeList, caseSensitive, minimalTextSelect, logger);
            }
        }

        public static void TagContentInDocument(
            this XmlNode tagNode,
            XmlNodeList documentNodeList,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            string caseSensitiveness = string.Empty;
            if (!caseSensitive)
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

            foreach (XmlNode node in documentNodeList)
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

                // TODO SafeReplaceInnerXml
                try
                {
                    XmlDocumentFragment testNode = tagNode.OwnerDocument.CreateDocumentFragment();
                    testNode.InnerXml = replace;
                }
                catch (Exception e)
                {
                    try
                    {
                        replace = replace.TagOrderNormalizer(tagNode);
                    }
                    catch (Exception tagOrderNormalizerException)
                    {
                        replace = node.InnerXml;
                        logger?.LogException(e, "\nInvalid replacement string:\n{0}\n\n", replace);
                        logger?.LogException(tagOrderNormalizerException, string.Empty);
                    }
                }
                finally
                {
                    node.InnerXml = replace;
                }
            }
        }

        // TODO: Remove this method
        public static string TagNodeContent(this string text, string keyString, string openTag)
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
        public static string TagOrderNormalizer(this string text, XmlNode tagNode)
        {
            XmlDocumentFragment testXmlNode = tagNode.OwnerDocument.CreateDocumentFragment();

            string tagName = tagNode.Name;

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
    }
}