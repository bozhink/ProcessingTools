namespace ProcessingTools.BaseLibrary
{
    using System;
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

        public static async Task TagContentInDocument(
            this IEnumerable<string> textToTagList,
            XmlElement tagModel,
            string xpathTemplate,
            XmlNamespaceManager namespaceManager,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            foreach (string textToTag in textToTagList)
            {
                try
                {
                    await textToTag.TagContentInDocument(tagModel, xpathTemplate, namespaceManager, document, caseSensitive, minimalTextSelect, logger);
                }
                catch (Exception e)
                {
                    logger?.Log(e, "Item: {0}.", textToTag);
                }
            }
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
            return nodeList.Cast<XmlNode>().Tag(caseSensitive, minimalTextSelect, logger, item);
        }

        public static async Task Tag(this IEnumerable<XmlNode> nodeList, bool caseSensitive, bool minimalTextSelect, ILogger logger, params XmlElement[] items)
        {
            if (nodeList == null || nodeList.Count() < 1)
            {
                return;
            }

            if (items == null || items.Length < 1)
            {
                return;
            }

            string caseSensitiveness = caseSensitive ? string.Empty : "(?i)";

            foreach (var node in nodeList)
            {
                foreach (var item in items)
                {
                    string replace = node.InnerXml;
                    string textToTag = item.InnerXml;

                    bool firstCharIsSpecial = Regex.IsMatch(textToTag, @"\A\W");
                    string startWordBound = firstCharIsSpecial ? string.Empty : @"\b";
                    string regexPatternPrefix = "(?<!<[^>]+)" + startWordBound + "(" + caseSensitiveness;

                    bool lastCharIsSpecial = Regex.IsMatch(textToTag, @"\W\Z");
                    string endWordBound = lastCharIsSpecial ? string.Empty : @"\b";
                    string regexPatternSuffix = ")" + endWordBound + "(?![^<>]*>)";

                    string textToTagEscaped = Regex.Replace(Regex.Escape(textToTag), "'", "\\W");
                    Regex textToTagRegex = new Regex(regexPatternPrefix + textToTagEscaped + regexPatternSuffix);

                    string replacement = item.GetReplacementOfTagNode();

                    if (textToTagRegex.Matches(node.InnerText).Count == textToTagRegex.Matches(node.InnerXml).Count)
                    {
                        replace = textToTagRegex.Replace(replace, replacement);
                    }
                    else
                    {
                        string textToTagPattern = startWordBound + Regex.Replace(textToTagEscaped, @"([^\\])(?!\Z)", "$1(?:<[^>]*>)*") + endWordBound;
                        if (!minimalTextSelect)
                        {
                            textToTagPattern = @"(?:<[\w\!][^>]*>)*" + textToTagPattern + @"(?:<[\/\!][^>]*>)*";
                        }

                        Regex textToTagPatternRegex = new Regex(regexPatternPrefix + textToTagPattern + regexPatternSuffix);
                        replace = textToTagPatternRegex.Replace(replace, replacement);
                    }

                    await node.SafeReplaceInnerXml(replace, logger);
                }
            }
        }
    }
}