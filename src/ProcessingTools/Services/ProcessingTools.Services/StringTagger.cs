// <copyright file="StringTagger.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Services.Models.Content;

    /// <summary>
    /// String tagger.
    /// </summary>
    public class StringTagger : IStringTagger
    {
        private readonly IContentTagger contentTagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringTagger"/> class.
        /// </summary>
        /// <param name="contentTagger">Content tagger.</param>
        public StringTagger(IContentTagger contentTagger)
        {
            this.contentTagger = contentTagger ?? throw new ArgumentNullException(nameof(contentTagger));
        }

        /// <inheritdoc/>
        public Task<object> TagAsync(IDocument document, IEnumerable<string> items, XmlElement tagModel, string xpath)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (tagModel is null)
            {
                throw new ArgumentNullException(nameof(tagModel));
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentNullException(nameof(xpath));
            }

            return this.TagInternalAsync(document, items, tagModel, xpath);
        }

        private async Task<object> TagInternalAsync(IDocument document, IEnumerable<string> items, XmlElement tagModel, string xpath)
        {
            var dataList = items.ToList();
            var itemsToTag = dataList.Select(d => this.XmlEncode(document, d))
                .OrderByDescending(i => i.Length)
                .ToList();

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = false,
                MinimalTextSelect = true,
            };

            await this.contentTagger.TagContentInDocumentAsync(itemsToTag, tagModel, xpath, document, settings).ConfigureAwait(false);

            return true;
        }

        private string XmlEncode(IDocument document, string text)
        {
            var bufferXml = document.XmlDocument.CreateDocumentFragment();
            bufferXml.InnerText = text;
            return bufferXml.InnerXml;
        }
    }
}
