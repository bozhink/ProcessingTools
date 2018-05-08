// <copyright file="XmlPresenter.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;
    using HtmlAgilityPack;
    using ProcessingTools.Data.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Documents;

    /// <summary>
    /// XML presenter.
    /// </summary>
    public class XmlPresenter : IXmlPresenter
    {
        private readonly IDocumentsDataAccessObject dataAccessObject;
        private readonly IDocumentsFormatTransformersFactory transformersFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlPresenter"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        /// <param name="transformersFactory">Transformers factory.</param>
        public XmlPresenter(IDocumentsDataAccessObject dataAccessObject, IDocumentsFormatTransformersFactory transformersFactory)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        /// <inheritdoc/>
        public async Task<string> GetHtmlAsync(object id, string articleId)
        {
            if (id == null || string.IsNullOrWhiteSpace(articleId))
            {
                return null;
            }

            string content = await this.dataAccessObject.GetDocumentContentAsync(id).ConfigureAwait(false);
            string transformedContent = await this.transformersFactory.GetFormatXmlToHtmlTransformer().TransformAsync(content).ConfigureAwait(false);

            HtmlDocument htmlDocument = new HtmlDocument
            {
                OptionOutputAsXml = false
            };

            if (!string.IsNullOrWhiteSpace(transformedContent))
            {
                htmlDocument.LoadHtml(transformedContent);
            }

            return htmlDocument.DocumentNode.OuterHtml;
        }

        /// <inheritdoc/>
        public async Task<string> GetXmlAsync(object id, string articleId)
        {
            if (id == null || string.IsNullOrWhiteSpace(articleId))
            {
                return null;
            }

            string content = await this.dataAccessObject.GetDocumentContentAsync(id).ConfigureAwait(false);

            XmlDocument xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            if (!string.IsNullOrWhiteSpace(content))
            {
                xmlDocument.LoadXml(content);
            }

            return xmlDocument.DocumentElement.OuterXml;
        }

        /// <inheritdoc/>
        public async Task<bool> SetHtmlAsync(object id, string articleId, string content)
        {
            if (id == null || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            HtmlDocument htmlDocument = new HtmlDocument
            {
                OptionOutputAsXml = true
            };

            if (!string.IsNullOrWhiteSpace(content))
            {
                htmlDocument.LoadHtml(content);
            }

            string transformedContent = await this.transformersFactory.GetFormatHtmlToXmlTransformer().TransformAsync(htmlDocument.DocumentNode.OuterHtml).ConfigureAwait(false);

            XmlDocument xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            xmlDocument.LoadXml(transformedContent);

            var result = await this.dataAccessObject.SetDocumentContentAsync(id, xmlDocument.DocumentElement.OuterXml).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result > 0L;
        }

        /// <inheritdoc/>
        public async Task<bool> SetXmlAsync(object id, string articleId, string content)
        {
            if (id == null || string.IsNullOrWhiteSpace(articleId))
            {
                return false;
            }

            XmlDocument xmlDocument = new XmlDocument
            {
                PreserveWhitespace = true
            };

            if (!string.IsNullOrWhiteSpace(content))
            {
                xmlDocument.LoadXml(content);
            }

            var result = await this.dataAccessObject.SetDocumentContentAsync(id, xmlDocument.DocumentElement.OuterXml).ConfigureAwait(false);
            await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);

            return result > 0L;
        }
    }
}
