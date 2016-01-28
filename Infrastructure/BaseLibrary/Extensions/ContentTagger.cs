namespace ProcessingTools.BaseLibrary
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;

    public static class ContentTagger
    {
        public static string GetReplacementOfTagNode(this XmlElement item)
        {
            XmlElement replacementNode = (XmlElement)item.CloneNode(true);

            ////XmlAttribute fullStringAttribute = replacementNode.OwnerDocument.CreateAttribute("full-string");
            ////fullStringAttribute.InnerText = replacementNode.InnerText;
            ////replacementNode.Attributes.Append(fullStringAttribute);

            replacementNode.InnerText = "$1";
            return replacementNode.OuterXml;
        }

        public static Task TagContentInDocument(
            this IEnumerable<string> textToTagList,
            XmlElement tagModel,
            string xpathTemplate,
            XmlNamespaceManager namespaceManager,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            return Task.Run(() =>
            {
                foreach (string textToTag in textToTagList)
                {
                    textToTag.TagContentInDocument(tagModel, xpathTemplate, namespaceManager, document, caseSensitive, minimalTextSelect, logger).Wait();
                }
            });
        }

        public static async Task TagContentInDocument(
            this string textToTag,
            XmlElement tagModel,
            string xpathTemplate,
            XmlNamespaceManager namespaceManager,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            XmlElement item = (XmlElement)tagModel.CloneNode(true);
            item.InnerText = textToTag;

            await item.TagContentInDocument(xpathTemplate, namespaceManager, document, caseSensitive, minimalTextSelect, logger);
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="item">XmlElement to be set in the XmlDocument.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <param name="namespaceManager">XmlNamespaceManager to execute xpathTemplate.</param>
        /// <param name="document">XmlDocument object to be tagged.</param>
        /// <param name="caseSensitive">Should be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static async Task TagContentInDocument(
            this XmlElement item,
            string xpathTemplate,
            XmlNamespaceManager namespaceManager,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            string xpath = string.Format(xpathTemplate, "contains(string(.),'" + item.InnerText + "')");
            XmlNodeList nodeList = document.SelectNodes(xpath, namespaceManager);

            await item.TagContentInDocument(nodeList, caseSensitive, minimalTextSelect, logger);
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="item">XmlElement to be set in the XmlDocument.</param>
        /// <param name="nodeList">The list of nodes where we try to tag item.InnerXml.</param>
        /// <param name="caseSensitive">Should be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        /// <param name="logger">ILogger object to log potential exceptions.</param>
        /// <returns>Task for async call.</returns>
        public static Task TagContentInDocument(
            this XmlElement item,
            XmlNodeList nodeList,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            return Task.Run(() => Tag(item, nodeList, caseSensitive, minimalTextSelect, logger));
        }

        private static void Tag(XmlElement item, XmlNodeList nodeList, bool caseSensitive, bool minimalTextSelect, ILogger logger)
        {
            string caseSensitiveness = caseSensitive ? string.Empty : "(?i)";

            string textToTag = item.InnerXml;

            bool firstCharIsSpecial = Regex.IsMatch(textToTag, @"\A\W");
            bool lastCharIsSpecial = Regex.IsMatch(textToTag, @"\W\Z");

            string startWordBound = firstCharIsSpecial ? string.Empty : @"\b";
            string endWordBound = lastCharIsSpecial ? string.Empty : @"\b";

            string textToTagEscaped = Regex.Replace(Regex.Escape(textToTag), "'", "\\W");
            string textToTagPattern = startWordBound + Regex.Replace(textToTagEscaped, @"([^\\])(?!\Z)", "$1(?:<[^>]*>)*") + endWordBound;

            if (!minimalTextSelect)
            {
                textToTagPattern = @"(?:<[\w\!][^>]*>)*" + textToTagPattern + @"(?:<[\/\!][^>]*>)*";
            }

            ////Regex textToTagPatternRegex = new Regex("(?<!<[^>]+)\\b(" + caseSensitiveness + textToTagPattern + ")(?![^<>]*>)\\b");
            Regex textToTagPatternRegex = new Regex("(?<!<[^>]+)" + startWordBound + "(" + caseSensitiveness + textToTagPattern + ")" + endWordBound + "(?![^<>]*>)");
            ////Regex textToTagRegex = new Regex("(?<!<[^>]+)\\b(" + caseSensitiveness + textToTagEscaped + ")(?![^<>]*>)\\b");
            Regex textToTagRegex = new Regex("(?<!<[^>]+)" + startWordBound + "(" + caseSensitiveness + textToTagEscaped + ")" + endWordBound + "(?![^<>]*>)");

            string replacement = item.GetReplacementOfTagNode();

            nodeList.Cast<XmlNode>()
                .All(node =>
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

                    return node.SafeReplaceInnerXml(replace, logger);
                });
        }
    }
}