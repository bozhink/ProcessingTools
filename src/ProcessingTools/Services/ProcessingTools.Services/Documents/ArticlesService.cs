// <copyright file="ArticlesService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Harvesters.Contracts.Meta;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.IO;
    using ProcessingTools.Services.Models.Contracts.Documents.Articles;

    /// <summary>
    /// Articles service.
    /// </summary>
    public class ArticlesService : IArticlesService
    {
        private readonly IArticlesDataService articlesDataService;
        private readonly IXmlReadService xmlReadService;
        private readonly IJatsArticleMetaHarvester articleMetaHarvester;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticlesService"/> class.
        /// </summary>
        /// <param name="articlesDataService">Articles data service.</param>
        /// <param name="xmlReadService">Xml read service.</param>
        /// <param name="articleMetaHarvester">Article meta harvester.</param>
        public ArticlesService(IArticlesDataService articlesDataService, IXmlReadService xmlReadService, IJatsArticleMetaHarvester articleMetaHarvester)
        {
            this.articlesDataService = articlesDataService ?? throw new ArgumentNullException(nameof(articlesDataService));
            this.xmlReadService = xmlReadService ?? throw new ArgumentNullException(nameof(xmlReadService));
            this.articleMetaHarvester = articleMetaHarvester ?? throw new ArgumentNullException(nameof(articleMetaHarvester));
        }

        /// <inheritdoc/>
        public Task<object> DeleteAsync(object id) => this.articlesDataService.DeleteAsync(id);

        /// <inheritdoc/>
        public Task<IArticleJournalModel[]> GetArticleJournalsAsync() => this.articlesDataService.GetArticleJournalsAsync();

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

            XmlDocument xmlDocument = await this.xmlReadService.ReadStreamToXmlDocumentAsync(stream).ConfigureAwait(false);
            if (xmlDocument == null)
            {
                throw new InvalidOperationException("Invalid XML document.");
            }

            var meta = await this.articleMetaHarvester.HarvestAsync(xmlDocument.DocumentElement).ConfigureAwait(false);

            // TODO

            throw new NotImplementedException();
        }
    }
}
