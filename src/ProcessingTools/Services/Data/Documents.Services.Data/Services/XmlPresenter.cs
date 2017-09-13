namespace ProcessingTools.Documents.Services.Data.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using Contracts;
    using Contracts.Factories;
    using Contracts.Models;
    using Models;
    using ProcessingTools.Common.Extensions;

    public class XmlPresenter : IXmlPresenter
    {
        private readonly IDocumentsDataService service;
        private readonly IDocumentsFormatTransformersFactory transformersFactory;

        public XmlPresenter(IDocumentsDataService service, IDocumentsFormatTransformersFactory transformersFactory)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (transformersFactory == null)
            {
                throw new ArgumentNullException(nameof(transformersFactory));
            }

            this.service = service;
            this.transformersFactory = transformersFactory;
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

            var reader = await this.service.GetReader(userId, articleId, documentId).ConfigureAwait(false);
            var content = await this.transformersFactory
                .GetFormatXmlToHtmlTransformer()
                .TransformAsync(reader, true)
                .ConfigureAwait(false);

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

            var reader = await this.service.GetReader(userId, articleId, documentId).ConfigureAwait(false);
            xmlDocument.Load(reader);
            reader.Close();
            reader.TryDispose();

            return xmlDocument.OuterXml;
        }

        public async Task<object> SaveHtml(object userId, object articleId, IDocumentServiceModel document, string content)
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

            xmlDocument.LoadXml(content
                .Replace("&nbsp;", " ")
                .Replace("<br>", @"<span elem-name=""break""></span>"));

            var xmlContent = await this.transformersFactory
                .GetFormatHtmlToXmlTransformer()
                .TransformAsync(xmlDocument)
                .ConfigureAwait(false);

            xmlDocument.LoadXml(xmlContent);

            var result = await this.service.UpdateContent(userId, articleId, document, xmlDocument.OuterXml).ConfigureAwait(false);

            return result;
        }

        public async Task<object> SaveXml(object userId, object articleId, IDocumentServiceModel document, string content)
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

            var result = await this.service.UpdateContent(userId, articleId, document, xmlDocument.OuterXml).ConfigureAwait(false);

            return result;
        }
    }
}
