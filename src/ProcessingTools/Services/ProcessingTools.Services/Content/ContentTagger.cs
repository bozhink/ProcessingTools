// <copyright file="ContentTagger.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Content
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using Microsoft.Extensions.Logging;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Models.Content;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Content tagger.
    /// </summary>
    public class ContentTagger : IContentTagger
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTagger"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public ContentTagger(ILogger<ContentTagger> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public Task TagContentInDocumentAsync(IEnumerable<string> textToTagList, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings)
        {
            if (textToTagList is null)
            {
                throw new ArgumentNullException(nameof(textToTagList));
            }

            if (tagModel is null)
            {
                throw new ArgumentNullException(nameof(tagModel));
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return this.TagContentInDocumentInternalAsync(textToTagList, tagModel, xpath, document, settings);
        }

        /// <inheritdoc/>
        public Task TagContentInDocumentAsync(string textToTag, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings)
        {
            if (string.IsNullOrWhiteSpace(textToTag))
            {
                throw new ArgumentNullException(nameof(textToTag));
            }

            if (tagModel is null)
            {
                throw new ArgumentNullException(nameof(tagModel));
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            return this.TagContentInDocumentInternalAsync(textToTag, tagModel, xpath, document, settings);
        }

        /// <inheritdoc/>
        public Task TagContentInDocumentAsync(IEnumerable<XmlNode> nodeList, IContentTaggerSettings settings, params XmlElement[] items)
        {
            if (nodeList is null || !nodeList.Any())
            {
                return Task.CompletedTask;
            }

            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            if (items is null || items.Length < 1)
            {
                return Task.CompletedTask;
            }

            return this.TagContentInDocumentInternalAsync(nodeList, settings, items);
        }

        private static Func<XmlNode, bool> GetMatchNodePredicate(string value, bool caseSensitive)
        {
            if (caseSensitive)
            {
                return x => x.InnerText.Contains(value, StringComparison.InvariantCulture);
            }
            else
            {
                return x => x.InnerText.Contains(value, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        private string GetReplacementOfTagNode(XmlNode item)
        {
            XmlElement replacementNode = (XmlElement)item.CloneNode(true);
            replacementNode.InnerText = "$1";
            return replacementNode.OuterXml;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Expected")]
        private async Task TagContentInDocumentInternalAsync(IEnumerable<string> textToTagList, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings)
        {
            foreach (string textToTag in textToTagList)
            {
                try
                {
                    await this.TagContentInDocumentAsync(textToTag, tagModel, xpath, document, settings).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    this.logger.LogError(e, $"Item: {textToTag}.");
                }
            }
        }

        private async Task TagContentInDocumentInternalAsync(string textToTag, XmlElement tagModel, string xpath, IDocument document, IContentTaggerSettings settings)
        {
            XmlElement item = (XmlElement)tagModel.CloneNode(true);
            item.InnerText = textToTag;

            await this.TagContentInDocumentInternalAsync(item, xpath, document, settings).ConfigureAwait(false);
        }

        private Task TagContentInDocumentInternalAsync(IEnumerable<XmlNode> nodeList, IContentTaggerSettings settings, XmlElement[] items)
        {
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

                    node.SafeReplaceInnerXml(replace);
                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Tags plain text string (no regex) in XmlDocument.
        /// </summary>
        /// <param name="item">XmlElement to be set in the XmlDocument.</param>
        /// <param name="xpath">XPath string.</param>
        /// <param name="document">IDocument object to be tagged.</param>
        /// <param name="settings">Tagging settings.</param>
        /// <returns>Task.</returns>
        private async Task TagContentInDocumentInternalAsync(XmlElement item, string xpath, IDocument document, IContentTaggerSettings settings)
        {
            var nodeList = document.SelectNodes(xpath)
                .AsEnumerable()
                .Where(GetMatchNodePredicate(item.InnerText, settings.CaseSensitive));

            await this.TagContentInDocumentAsync(nodeList, settings, item).ConfigureAwait(false);
        }
    }
}
