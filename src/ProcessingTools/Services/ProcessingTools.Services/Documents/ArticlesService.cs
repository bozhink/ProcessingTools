// <copyright file="ArticlesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using AutoMapper;
    using ProcessingTools.Harvesters.Contracts.Meta;
    using ProcessingTools.Harvesters.Models.Contracts.Meta;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.IO;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;
    using ProcessingTools.Services.Models.Contracts.Documents.Documents;
    using ProcessingTools.Services.Models.Documents.Articles;
    using ProcessingTools.Services.Models.Documents.Documents;
    using ProcessingTools.Services.Models.Documents.Files;

    /// <summary>
    /// Articles service.
    /// </summary>
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesDataService articlesDataService;
        private readonly IDocumentsDataService documentsDataService;
        private readonly IFilesDataService filesDataService;
        private readonly IXmlReadService xmlReadService;
        private readonly IJatsArticleMetaHarvester articleMetaHarvester;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesService"/> class.
        /// </summary>
        /// <param name="articlesDataService">Articles data service.</param>
        /// <param name="documentsDataService">Documents data service.</param>
        /// <param name="filesDataService">Files data service.</param>
        /// <param name="xmlReadService">Xml read service.</param>
        /// <param name="articleMetaHarvester">Article meta harvester.</param>
        public ArticlesService(IArticlesDataService articlesDataService, IDocumentsDataService documentsDataService, IFilesDataService filesDataService, IXmlReadService xmlReadService, IJatsArticleMetaHarvester articleMetaHarvester)
        {
            this.articlesDataService = articlesDataService ?? throw new ArgumentNullException(nameof(articlesDataService));
            this.documentsDataService = documentsDataService ?? throw new ArgumentNullException(nameof(documentsDataService));
            this.filesDataService = filesDataService ?? throw new ArgumentNullException(nameof(filesDataService));
            this.xmlReadService = xmlReadService ?? throw new ArgumentNullException(nameof(xmlReadService));
            this.articleMetaHarvester = articleMetaHarvester ?? throw new ArgumentNullException(nameof(articleMetaHarvester));

            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IArticleMetaModel, ArticleInsertModel>();
                c.CreateMap<FileInsertModel, FileUpdateModel>();
            });
            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc/>
        public Task<object> DeleteAsync(object id) => this.articlesDataService.DeleteAsync(id);

        /// <inheritdoc/>
        public Task<IArticleJournalModel[]> GetArticleJournalsAsync() => this.articlesDataService.GetArticleJournalsAsync();

        /// <inheritdoc/>
        public Task<IDocumentModel[]> GetArticleDocumentsAsync(string articleId) => this.documentsDataService.GetArticleDocumentsAsync(articleId);

        /// <inheritdoc/>
        public Task<IArticleDetailsModel> GetDetailsByIdAsync(object id) => this.articlesDataService.GetDetailsByIdAsync(id);

        /// <inheritdoc/>
        public Task<object> CreateAsync(IArticleInsertModel model) => this.articlesDataService.InsertAsync(model);

        /// <inheritdoc/>
        public Task<long> SelectCountAsync() => this.articlesDataService.SelectCountAsync();

        /// <inheritdoc/>
        public Task<IArticleDetailsModel[]> SelectDetailsAsync(int skip, int take) => this.articlesDataService.SelectDetailsAsync(skip, take);

        /// <inheritdoc/>
        public Task<object> UpdateAsync(IArticleUpdateModel model) => this.articlesDataService.UpdateAsync(model);

        /// <inheritdoc/>
        public async Task<object> CreateFromFileAsync(IArticleFileModel model, Stream stream, string journalId)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new InvalidOperationException("File stream can not be read.");
            }

            var fileInsertModel = new FileInsertModel
            {
                OriginalContentLength = model.Length,
                OriginalContentType = model.ContentType,
                OriginalFileName = model.FileName
            };

            var fileId = await this.filesDataService.InsertAsync(fileInsertModel).ConfigureAwait(false);

            XmlDocument xmlDocument = await this.xmlReadService.ReadStreamToXmlDocumentAsync(stream).ConfigureAwait(false);
            if (xmlDocument == null)
            {
                throw new InvalidOperationException("Invalid XML document.");
            }

            var meta = await this.articleMetaHarvester.HarvestAsync(xmlDocument.DocumentElement).ConfigureAwait(false);

            var articleInsertModel = this.mapper.Map<IArticleMetaModel, ArticleInsertModel>(meta);
            articleInsertModel.JournalId = journalId;
            articleInsertModel.ELocationId = articleInsertModel.ELocationId ?? meta.ArticleId;

            var articleId = await this.articlesDataService.InsertAsync(articleInsertModel).ConfigureAwait(false);

            var documentInsertModel = new DocumentInsertModel
            {
                Description = fileInsertModel.FileName,
                ArticleId = articleId.ToString(),
                FileId = fileId.ToString()
            };

            var documentId = await this.documentsDataService.InsertAsync(documentInsertModel).ConfigureAwait(false);

            long length = await this.documentsDataService.SetDocumentContentAsync(documentId, xmlDocument.OuterXml);

            var fileUpdateModel = this.mapper.Map<FileInsertModel, FileUpdateModel>(fileInsertModel);
            fileUpdateModel.Id = fileId.ToString();
            fileUpdateModel.ContentLength = length;

            await this.filesDataService.UpdateAsync(fileUpdateModel).ConfigureAwait(false);

            return articleId;
        }
    }
}
