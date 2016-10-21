namespace ProcessingTools.BaseLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Contracts;
    using ProcessingTools.Xml.Extensions;

    public class StringTagger
    {
        private readonly ILogger logger;

        public StringTagger(ILogger logger)
        {
            this.logger = logger;
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

            await data.ToList()
                .Select(d => this.XmlEncode(document, d))
                .OrderByDescending(i => i.Length)
                .TagContentInDocument(
                    tagModel,
                    contentNodesXPathTemplate,
                    document,
                    false,
                    true,
                    this.logger);

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
