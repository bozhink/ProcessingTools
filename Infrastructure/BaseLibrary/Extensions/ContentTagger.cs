namespace ProcessingTools.BaseLibrary
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Xml;

    using DocumentProvider;
    using ProcessingTools.Contracts.Log;

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

        public static void TagContentInDocument(
            this IEnumerable<string> textToTagList,
            XmlElement tagModel,
            string xpathTemplate,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            foreach (string textToTag in textToTagList)
            {
                textToTag.TagContentInDocument(tagModel, xpathTemplate, document, caseSensitive, minimalTextSelect, logger);
            }
        }

        public static void TagContentInDocument(
            this string textToTag,
            XmlElement tagModel,
            string xpathTemplate,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            XmlElement item = (XmlElement)tagModel.CloneNode(true);
            item.InnerText = textToTag;
            item.TagContentInDocument(xpathTemplate, document, caseSensitive, minimalTextSelect, logger);
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="items">IEnumerable of XmlElement to be set in the XmlDocument.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <param name="document">XmlDocument object to be tagged.</param>
        /// <param name="caseSensitive">Must be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        public static void TagContentInDocument(
            this IEnumerable<XmlElement> items,
            string xpathTemplate,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            foreach (var item in items)
            {
                item.TagContentInDocument(xpathTemplate, document, caseSensitive, minimalTextSelect, logger);
            }
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="item">XmlElement to be set in the XmlDocument.</param>
        /// <param name="xpathTemplate">XPath string template of the type "//node-to-search-in[{0}]".</param>
        /// <param name="document">XmlDocument object to be tagged.</param>
        /// <param name="caseSensitive">Must be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        public static void TagContentInDocument(
            this XmlElement item,
            string xpathTemplate,
            XmlDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            string xpath = string.Format(xpathTemplate, "contains(string(.),'" + item.InnerText + "')");
            XmlNodeList nodeList = document.SelectNodes(xpath, TaxPubDocument.NamespceManager());

            item.TagContentInDocument(nodeList, caseSensitive, minimalTextSelect, logger);
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="items">IEnumerable of XmlElement to be set in the XmlDocument.</param>
        /// <param name="nodeList">The list of nodes where we try to tag item.InnerXml.</param>
        /// <param name="caseSensitive">Should be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        public static void TagContentInDocument(
            this IEnumerable<XmlElement> items,
            XmlNodeList nodeList,
            bool caseSensitive = true,
            bool minimalTextSelect = false,
            ILogger logger = null)
        {
            foreach (var item in items)
            {
                item.TagContentInDocument(nodeList, caseSensitive, minimalTextSelect, logger);
            }
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="item">XmlElement to be set in the XmlDocument.</param>
        /// <param name="nodeList">The list of nodes where we try to tag item.InnerXml.</param>
        /// <param name="caseSensitive">Should be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        public static void TagContentInDocument(
            this XmlElement item,
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
    }
}