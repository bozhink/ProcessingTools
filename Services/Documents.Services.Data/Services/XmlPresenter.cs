namespace ProcessingTools.Documents.Services.Data
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;
    using Models;
    using ProcessingTools.Xml.Extensions;

    public class XmlPresenter : IXmlPresenter
    {
        private const string FormatXmlToHtmlXslFilePathKey = "FormatXmlToHtmlXslFilePath";
        private const string FormatHtmlToXmlXslFilePathKey = "FormatHtmlToXmlXslFilePath";

        private readonly IDocumentsDataService service;

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

            string xslFileName = ConfigurationManager.AppSettings[FormatXmlToHtmlXslFilePathKey];

            var content = (await this.service.GetReader(userId, articleId, documentId))
                    .ApplyXslTransform(xslFileName);

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

            var reader = await this.service.GetReader(userId, articleId, documentId);

            var xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xmlDocument.Load(reader);

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

            string xslFileName = ConfigurationManager.AppSettings[FormatHtmlToXmlXslFilePathKey];
            var xmlContent = xmlDocument.ApplyXslTransform(xslFileName);

            var result = await this.service.Update(userId, articleId, document, xmlContent);

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
