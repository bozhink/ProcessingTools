// <copyright file="DocumentsService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using AutoMapper;
    using ProcessingTools.Models.Contracts.IO;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.IO;
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;
    using ProcessingTools.Services.Models.Documents.Documents;

    /// <summary>
    /// Documents service.
    /// </summary>
    public class DocumentsService : IDocumentsService
    {
        private readonly IDocumentsDataService documentsDataService;
        private readonly IXmlReadService xmlReadService;
        private readonly IXmlPresenter xmlPresenter;
        private readonly IMapper mapper;

        public DocumentsService(IDocumentsDataService documentsDataService, IXmlReadService xmlReadService, IXmlPresenter xmlPresenter)
        {
            this.documentsDataService = documentsDataService ?? throw new ArgumentNullException(nameof(documentsDataService));
            this.xmlReadService = xmlReadService ?? throw new ArgumentNullException(nameof(xmlReadService));
            this.xmlPresenter = xmlPresenter ?? throw new ArgumentNullException(nameof(xmlPresenter));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IFileMetadata, DocumentFileStreamModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public async Task<object> DeleteAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return await this.documentsDataService.DeleteAsync(id).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IDocumentFileStreamModel> DownloadAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var document = await this.documentsDataService.GetByIdAsync(id).ConfigureAwait(false);
            if (document == null || document.ArticleId != articleId)
            {
                return null;
            }

            var model = this.mapper.Map<IFileMetadata, DocumentFileStreamModel>(document.File);

            var content = await this.documentsDataService.GetDocumentContentAsync(id).ConfigureAwait(false);
            model.Stream = this.xmlReadService.GetStreamForXmlString(content);

            return model;
        }

        /// <inheritdoc/>
        public async Task<IDocumentModel> GetByIdAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var document = await this.documentsDataService.GetByIdAsync(id).ConfigureAwait(false);
            if (document == null || document.ArticleId != articleId)
            {
                return null;
            }

            return document;
        }

        /// <inheritdoc/>
        public async Task<IDocumentDetailsModel> GetDetailsByIdAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var document = await this.documentsDataService.GetDetailsByIdAsync(id).ConfigureAwait(false);
            if (document == null || document.ArticleId != articleId)
            {
                return null;
            }

            return document;
        }

        public Task<IDocumentArticle> GetDocumentArticleAsync(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<string> GetHtmlAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            string result = await this.xmlPresenter.GetHtmlAsync(id, articleId).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<string> GetXmlAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            string result = await this.xmlPresenter.GetXmlAsync(id, articleId).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<object> SetAsFinalAsync(string id, string articleId)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var result = await this.documentsDataService.SetAsFinalAsync(id, articleId).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<object> SetHtmlAsync(string id, string articleId, string content)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var result = await this.xmlPresenter.SetHtmlAsync(id, articleId, content).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<object> SetXmlAsync(string id, string articleId, string content)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var result = await this.xmlPresenter.SetXmlAsync(id, articleId, content).ConfigureAwait(false);
            return result;
        }

        /// <inheritdoc/>
        public async Task<object> UpdateAsync(IDocumentUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return await this.documentsDataService.UpdateAsync(model).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<object> UploadAsync(IDocumentFileModel model, Stream stream, string articleId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            if (!stream.CanRead)
            {
                throw new InvalidOperationException("File stream can not be read.");
            }

            XmlDocument xmlDocument = await this.xmlReadService.ReadStreamToXmlDocumentAsync(stream).ConfigureAwait(false);
            if (xmlDocument == null)
            {
                throw new InvalidOperationException("XML document cannot be processed.");
            }

            var documentInsertModel = new DocumentInsertModel
            {
                ArticleId = articleId,
                Description = model.FileName,
                File = new DocumentFileModel
                {
                    ContentLength = xmlDocument.OuterXml.Length,
                    ContentType = model.ContentType,
                    FileExtension = Path.GetExtension(model.FileName),
                    FileName = Path.GetFileName(model.FileName)
                }
            };

            var documentId = await this.documentsDataService.InsertAsync(documentInsertModel).ConfigureAwait(false);

            await this.documentsDataService.SetDocumentContentAsync(documentId, xmlDocument.OuterXml).ConfigureAwait(false);

            return articleId;
        }
    }
}
