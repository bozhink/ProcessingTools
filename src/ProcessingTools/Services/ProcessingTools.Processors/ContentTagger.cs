// <copyright file="ContentTagger.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Models.Contracts;

    /// <summary>
    /// Content tagger.
    /// </summary>
    public class ContentTagger : IContentTagger
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTagger"/> class.
        /// </summary>
        /// <param name="logger">Logger</param>
        public ContentTagger(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task TagContentInDocumentAsync(IEnumerable<string> textToTagList, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings)
        {
            if (textToTagList == null)
            {
                throw new ArgumentNullException(nameof(textToTagList));
            }

            if (tagModel == null)
            {
                throw new ArgumentNullException(nameof(tagModel));
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            foreach (string textToTag in textToTagList)
            {
                try
                {
                    await this.TagContentInDocumentAsync(textToTag, tagModel, xpath, document, settings).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    this.logger?.Log(e, "Item: {0}.", textToTag);
                }
            }
        }

        /// <inheritdoc/>
        public async Task TagContentInDocumentAsync(string textToTag, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings)
        {
            if (string.IsNullOrWhiteSpace(textToTag))
            {
                throw new ArgumentNullException(nameof(textToTag));
            }

            if (tagModel == null)
            {
                throw new ArgumentNullException(nameof(tagModel));
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            XmlElement item = (XmlElement)tagModel.CloneNode(true);
            item.InnerText = textToTag;

            await this.TagContentInDocumentAsync(item, xpath, document, settings).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task TagContentInDocumentAsync(IEnumerable<XmlNode> nodeList, IContentTaggerSettings settings, params XmlElement[] items)
        {
            if (nodeList == null || !nodeList.Any())
            {
                return;
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (items == null || items.Length < 1)
            {
                return;
            }

            string caseSensitiveness = settings.CaseSensitive ? string.Empty : "(?i)";

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
                        if (!settings.MinimalTextSelect)
                        {
                            textToTagPattern = @"(?:<[\w\!][^>]*>)*" + textToTagPattern + @"(?:<[\/\!][^>]*>)*";
                        }

                        Regex textToTagPatternRegex = new Regex(regexPatternPrefix + textToTagPattern + regexPatternSuffix);
                        replace = textToTagPatternRegex.Replace(replace, replacement);
                    }

                    await node.SafeReplaceInnerXml(replace, this.logger).ConfigureAwait(false);
                }
            }
        }

        private string GetReplacementOfTagNode(XmlNode item)
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
        /// <param name="settings">Tagging settings.</param>
        /// <returns>Task</returns>
        private async Task TagContentInDocumentAsync(XmlElement item, string xpath, IDocument document, IContentTaggerSettings settings)
        {
            var nodeList = document.SelectNodes(xpath)
                .AsEnumerable()
                .Where(this.GetMatchNodePredicate(item.InnerText, settings.CaseSensitive));

            await this.TagContentInDocumentAsync(nodeList, settings, item).ConfigureAwait(false);
        }

        private Func<XmlNode, bool> GetMatchNodePredicate(string value, bool caseSensitive)
        {
            if (caseSensitive)
            {
                return x => x.InnerText.Contains(value);
            }
            else
            {
                return x => x.InnerText.ToLowerInvariant().Contains(value.ToLowerInvariant());
            }
        }
    }
}
