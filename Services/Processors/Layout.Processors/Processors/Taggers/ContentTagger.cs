namespace ProcessingTools.Layout.Processors.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts.Taggers;

    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Extensions;

    // TODO: xpathTemplate should be changed to user Linq
    public class ContentTagger : IContentTagger
    {
        private readonly ILogger logger;

        public ContentTagger(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task TagContentInDocument(
            IEnumerable<string> textToTagList,
            XmlElement tagModel,
            string xpath,
            IDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false)
        {
            foreach (string textToTag in textToTagList)
            {
                try
                {
                    await this.TagContentInDocument(textToTag, tagModel, xpath, document, caseSensitive, minimalTextSelect);
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, "Item: {0}.", textToTag);
                }
            }
        }

        public async Task TagContentInDocument(
            string textToTag,
            XmlElement tagModel,
            string xpath,
            IDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false)
        {
            XmlElement item = (XmlElement)tagModel.CloneNode(true);
            item.InnerText = textToTag;

            await this.TagContentInDocument(item, xpath, document, caseSensitive, minimalTextSelect);
        }

        public async Task TagContentInDocument(IEnumerable<XmlNode> nodeList, bool caseSensitive, bool minimalTextSelect, params XmlElement[] items)
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

                    string replacement = this.GetReplacementOfTagNode(item);

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

                    await node.SafeReplaceInnerXml(replace, this.logger);
                }
            }
        }

        private string GetReplacementOfTagNode(XmlElement item)
        {
            XmlElement replacementNode = (XmlElement)item.CloneNode(true);
            replacementNode.InnerText = "$1";
            return replacementNode.OuterXml;
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="item">XmlElement to be set in the XmlDocument.</param>
        /// <param name="xpath">XPath string.</param>
        /// <param name="document">IDocument object to be tagged.</param>
        /// <param name="caseSensitive">Should be the search case sensitive?</param>
        /// <param name="minimalTextSelect">Select minimal text or extend to surrounding tags.</param>
        /// <returns></returns>
        private async Task TagContentInDocument(
            XmlElement item,
            string xpath,
            IDocument document,
            bool caseSensitive = true,
            bool minimalTextSelect = false)
        {
            var nodeList = document.SelectNodes(xpath)
                .AsEnumerable()
                .Where(this.GetMatchNodePredicate(item.InnerText, caseSensitive));

            await this.TagContentInDocument(nodeList, caseSensitive, minimalTextSelect, item);
        }

        private Func<XmlNode, bool> GetMatchNodePredicate(string value, bool caseSensitive)
        {
            if (caseSensitive)
            {
                return x => x.InnerText.Contains(value);
            }
            else
            {
                return x => x.InnerText.ToLower().Contains(value.ToLower());
            }
        }
    }
}
