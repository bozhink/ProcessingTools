namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;

    using ProcessingTools.Extensions;
    using ProcessingTools.Xml.Cache;
    using ProcessingTools.Xml.Contracts;
    using ProcessingTools.Xml.Transformers;

    using Providers;

    public class XmlPresenter : IXmlPresenter
    {
        private readonly IDocumentsDataService service;

        // TODO: DI
        private readonly IXslTransformer<IFormatXmlToHtmlXslTransformProvider> formatXmlToHtmlXslTransformer = new XslTransformer<IFormatXmlToHtmlXslTransformProvider>(new FormatXmlToHtmlXslTransformProvider(new XslTransformCache()));

        // TODO: DI
        private readonly IXslTransformer<IFormatHtmlToXmlXslTransformProvider> formatHtmlToXmlXslTransformer = new XslTransformer<IFormatHtmlToXmlXslTransformProvider>(new FormatHtmlToXmlXslTransformProvider(new XslTransformCache()));

        public XmlPresenter(IDocumentsDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public async Task<string> GetHtml(object userId, object articleId, object documentId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            var reader = await this.service.GetReader(userId, articleId, documentId);
            var content = await this.formatXmlToHtmlXslTransformer.Transform(reader, true);
            return content;
        }

        public async Task<string> GetXml(object userId, object articleId, object documentId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            var xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            var reader = await this.service.GetReader(userId, articleId, documentId);
            xmlDocument.Load(reader);
            reader.Close();
            reader.TryDispose();

            return xmlDocument.OuterXml;
        }

        public async Task<object> SaveHtml(object userId, object articleId, DocumentServiceModel document, string content)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xmlDocument.LoadXml(content.Replace("&nbsp;", " "));

            var xmlContent = await this.formatHtmlToXmlXslTransformer.Transform(xmlDocument);
            xmlDocument.LoadXml(xmlContent);

            var result = await this.service.Update(userId, articleId, document, xmlDocument.OuterXml);

            return result;
        }

        public async Task<object> SaveXml(object userId, object articleId, DocumentServiceModel document, string content)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xmlDocument.LoadXml(content);

            var result = await this.service.Update(userId, articleId, document, xmlDocument.OuterXml);

            return result;
        }
    }
}
