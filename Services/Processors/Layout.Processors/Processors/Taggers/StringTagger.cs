namespace ProcessingTools.Layout.Processors.Taggers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts.Taggers;

    using ProcessingTools.Contracts;

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

        public async Task<object> Tag(IDocument document, IEnumerable<string> data, XmlElement tagModel, string contentNodesXPathTemplate)
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

            if (string.IsNullOrWhiteSpace(contentNodesXPathTemplate))
            {
                throw new ArgumentNullException(nameof(contentNodesXPathTemplate));
            }

            var itemsToTag = data.ToList()
                .Select(d => this.XmlEncode(document, d))
                .OrderByDescending(i => i.Length)
                .ToList();

            await this.contentTagger.TagContentInDocument(
                itemsToTag,
                tagModel,
                contentNodesXPathTemplate,
                document,
                caseSensitive: false,
                minimalTextSelect: true);

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
