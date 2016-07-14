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

        public async Task<string> GetHtml(IDocumentsDataService service, object userId, object articleId, object documentId)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

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

            var content = (await service.GetReader(userId, articleId, documentId))
                    .ApplyXslTransform(xslFileName);

            return content;
        }

        public async Task<string> GetXml(IDocumentsDataService service, object userId, object articleId, object documentId)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

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

            var reader = await service.GetReader(userId, articleId, documentId);

            var xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xmlDocument.Load(reader);

            return xmlDocument.OuterXml;
        }

        public async Task<object> SaveHtml(IDocumentsDataService service, object userId, object articleId, DocumentServiceModel document, string content)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

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

            string xslFileName = ConfigurationManager.AppSettings[FormatHtmlToXmlXslFilePathKey];

            var xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xmlDocument.LoadXml(content.Replace("&nbsp;", " "));

            var xmlContent = xmlDocument.ApplyXslTransform(xslFileName);

            var result = await service.Update(userId, articleId, document, xmlContent);

            return result;
        }

        public async Task<object> SaveXml(IDocumentsDataService service, object userId, object articleId, DocumentServiceModel document, string content)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

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

            var result = await service.Update(userId, articleId, document, xmlDocument.OuterXml);

            return result;
        }
    }
}
