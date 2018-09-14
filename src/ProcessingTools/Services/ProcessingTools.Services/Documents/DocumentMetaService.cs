// <copyright file="DocumentMetaService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Models.Contracts.Documents.Meta;
    using ProcessingTools.Processors.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Documents;

    /// <summary>
    /// Service for processing document meta-data.
    /// </summary>
    public class DocumentMetaService : IDocumentMetaService
    {
        private readonly IDocumentMetaResolver documentMetaResolver;
        private readonly IDocumentsDataService documentsDataService;
        private readonly IDocumentMetaUpdater documentMetaUpdater;
        private readonly IDocumentFactory documentFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentMetaService"/> class.
        /// </summary>
        /// <param name="documentMetaResolver">Document meta-data resolver.</param>
        /// <param name="documentsDataService">Documents data service.</param>
        /// <param name="documentMetaUpdater">Document meta-data updater.</param>
        /// <param name="documentFactory">Document factory.</param>
        public DocumentMetaService(IDocumentMetaResolver documentMetaResolver, IDocumentsDataService documentsDataService, IDocumentMetaUpdater documentMetaUpdater, IDocumentFactory documentFactory)
        {
            this.documentMetaResolver = documentMetaResolver ?? throw new ArgumentNullException(nameof(documentMetaResolver));
            this.documentMetaUpdater = documentMetaUpdater ?? throw new ArgumentNullException(nameof(documentMetaUpdater));
            this.documentsDataService = documentsDataService ?? throw new ArgumentNullException(nameof(documentsDataService));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
        }

        /// <inheritdoc/>
        public async Task<object> UpdateDocumentAsync(string documentId)
        {
            if (string.IsNullOrWhiteSpace(documentId))
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            var metaData = await this.documentMetaResolver.GetDocumentAsync(documentId).ConfigureAwait(false);
            if (metaData == null)
            {
                return false;
            }

            return await this.UpdateDocumentAsync(documentId, metaData).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<object> UpdateArticleDocumentsAsync(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var metaData = await this.documentMetaResolver.GetArticleDocumentsAsync(articleId).ConfigureAwait(false);
            if (metaData == null || !metaData.Documents.Any())
            {
                return false;
            }

            ConcurrentQueue<bool> results = new ConcurrentQueue<bool>();
            List<Task> tasks = new List<Task>();

            foreach (var document in metaData.Documents)
            {
                async Task taskFactory() => results.Enqueue(await this.UpdateDocumentAsync(document.Id, metaData).ConfigureAwait(false));

                tasks.Add(taskFactory());
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);

            return results.LogicalAnd();
        }

        private async Task<bool> UpdateDocumentAsync(string documentId, IArticleFullModel metaData)
        {
            string content = await this.documentsDataService.GetDocumentContentAsync(documentId).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(content))
            {
                return false;
            }

            var document = this.documentFactory.Create(content);

            await this.documentMetaUpdater.UpdateMetaAsync(document, metaData.Article, metaData.Journal, metaData.Publisher).ConfigureAwait(false);

            await this.documentsDataService.SetDocumentContentAsync(documentId, document.Xml).ConfigureAwait(false);

            return true;
        }
    }
}
