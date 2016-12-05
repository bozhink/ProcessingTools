using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using ProcessingTools.Contracts;
using ProcessingTools.Layout.Processors.Contracts.Taggers;
using ProcessingTools.Layout.Processors.Models.Taggers;

namespace ProcessingTools.Layout.Processors.Processors.Taggers
{
    public class StringTagger : IStringTagger
    {
        private readonly IContentTagger contentTagger;

        public StringTagger(IContentTagger contentTagger)
        {
            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.contentTagger = contentTagger;
        }

        public async Task<object> Tag(IDocument document, IEnumerable<string> data, XmlElement tagModel, string contentNodesXPath)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (tagModel == null)
            {
                throw new ArgumentNullException(nameof(tagModel));
            }

            if (string.IsNullOrWhiteSpace(contentNodesXPath))
            {
                throw new ArgumentNullException(nameof(contentNodesXPath));
            }

            var itemsToTag = data.ToList()
                .Select(d => this.XmlEncode(document, d))
                .OrderByDescending(i => i.Length)
                .ToList();

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = false,
                MinimalTextSelect = true
            };

            await this.contentTagger.TagContentInDocument(itemsToTag, tagModel, contentNodesXPath, document, settings);

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
