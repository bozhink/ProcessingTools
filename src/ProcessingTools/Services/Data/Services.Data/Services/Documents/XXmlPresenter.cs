﻿namespace ProcessingTools.Documents.Services.Data.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Services.Data.Documents;
    using ProcessingTools.Services.Contracts.Documents;

    public class XXmlPresenter : IXXmlPresenter
    {
        private readonly IXDocumentsDataService service;
        private readonly IDocumentsFormatTransformersFactory transformerFactory;

        public XXmlPresenter(IXDocumentsDataService service, IDocumentsFormatTransformersFactory transformerFactory)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        public async Task<string> GetHtmlAsync(object userId, object articleId, object documentId)
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

            var reader = await this.service.GetReaderAsync(userId, articleId, documentId).ConfigureAwait(false);
            var content = await this.transformerFactory
                .GetFormatXmlToHtmlTransformer()
                .TransformAsync(reader, true)
                .ConfigureAwait(false);

            return content;
        }

        public async Task<string> GetXmlAsync(object userId, object articleId, object documentId)
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

            var reader = await this.service.GetReaderAsync(userId, articleId, documentId).ConfigureAwait(false);
            xmlDocument.Load(reader);
            reader.Close();
            reader.TryDispose();

            return xmlDocument.OuterXml;
        }

        public async Task<object> SaveHtmlAsync(object userId, object articleId, IDocument document, string content)
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

            var xmlContent = await this.transformerFactory
                .GetFormatHtmlToXmlTransformer()
                .TransformAsync(xmlDocument)
                .ConfigureAwait(false);

            xmlDocument.LoadXml(xmlContent);

            var result = await this.service.UpdateContentAsync(userId, articleId, document, xmlDocument.OuterXml).ConfigureAwait(false);

            return result;
        }

        public async Task<object> SaveXmlAsync(object userId, object articleId, IDocument document, string content)
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

            var result = await this.service.UpdateContentAsync(userId, articleId, document, xmlDocument.OuterXml).ConfigureAwait(false);

            return result;
        }
    }
}