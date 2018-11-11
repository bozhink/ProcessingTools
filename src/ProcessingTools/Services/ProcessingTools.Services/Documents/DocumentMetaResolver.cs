// <copyright file="DocumentMetaResolver.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Documents.Meta;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Models.Documents.Meta;

    /// <summary>
    /// Resolver of document meta-data.
    /// </summary>
    public class DocumentMetaResolver : IDocumentMetaResolver
    {
        private readonly IDocumentsDataService documentsDataService;
        private readonly IArticlesDataService articlesDataService;
        private readonly IJournalsDataService journalsDataService;
        private readonly IPublishersDataService publishersDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentMetaResolver"/> class.
        /// </summary>
        /// <param name="documentsDataService">Documents data service.</param>
        /// <param name="articlesDataService">Articles data service.</param>
        /// <param name="journalsDataService">Journals data service.</param>
        /// <param name="publishersDataService">Publishers data service.</param>
        public DocumentMetaResolver(IDocumentsDataService documentsDataService, IArticlesDataService articlesDataService, IJournalsDataService journalsDataService, IPublishersDataService publishersDataService)
        {
            this.documentsDataService = documentsDataService ?? throw new ArgumentNullException(nameof(documentsDataService));
            this.articlesDataService = articlesDataService ?? throw new ArgumentNullException(nameof(articlesDataService));
            this.journalsDataService = journalsDataService ?? throw new ArgumentNullException(nameof(journalsDataService));
            this.publishersDataService = publishersDataService ?? throw new ArgumentNullException(nameof(publishersDataService));
        }

        /// <inheritdoc/>
        public async Task<IArticleFullModel> GetArticleAsync(string articleId)
        {
            if (articleId == null)
            {
                return null;
            }

            var article = await this.articlesDataService.GetByIdAsync(articleId).ConfigureAwait(false);
            if (article == null)
            {
                return null;
            }

            var journal = await this.journalsDataService.GetByIdAsync(article.JournalId).ConfigureAwait(false);
            if (journal == null)
            {
                return null;
            }

            var publisher = await this.publishersDataService.GetByIdAsync(journal.PublisherId).ConfigureAwait(false);
            if (publisher == null)
            {
                return null;
            }

            return new ArticleFullModel
            {
                Article = article,
                Journal = journal,
                Publisher = publisher
            };
        }

        /// <inheritdoc/>
        public async Task<IArticleDocumentsFullModel> GetArticleDocumentsAsync(string articleId)
        {
            if (articleId == null)
            {
                return null;
            }

            var documentsTask = this.documentsDataService.GetArticleDocumentsAsync(articleId);

            var article = await this.articlesDataService.GetByIdAsync(articleId).ConfigureAwait(false);
            if (article == null)
            {
                return null;
            }

            var journal = await this.journalsDataService.GetByIdAsync(article.JournalId).ConfigureAwait(false);
            if (journal == null)
            {
                return null;
            }

            var publisher = await this.publishersDataService.GetByIdAsync(journal.PublisherId).ConfigureAwait(false);
            if (publisher == null)
            {
                return null;
            }

            var documents = await documentsTask.ConfigureAwait(false);

            return new ArticleDocumentsFullModel
            {
                Article = article,
                Journal = journal,
                Publisher = publisher,
                Documents = documents
            };
        }

        /// <inheritdoc/>
        public async Task<IDocumentFullModel> GetDocumentAsync(string documentId)
        {
            if (documentId == null)
            {
                return null;
            }

            var document = await this.documentsDataService.GetByIdAsync(documentId).ConfigureAwait(false);
            if (document == null)
            {
                return null;
            }

            var article = await this.articlesDataService.GetByIdAsync(document.ArticleId).ConfigureAwait(false);
            if (article == null)
            {
                return null;
            }

            var journal = await this.journalsDataService.GetByIdAsync(article.JournalId).ConfigureAwait(false);
            if (journal == null)
            {
                return null;
            }

            var publisher = await this.publishersDataService.GetByIdAsync(journal.PublisherId).ConfigureAwait(false);
            if (publisher == null)
            {
                return null;
            }

            return new DocumentFullModel
            {
                Document = document,
                Article = article,
                Journal = journal,
                Publisher = publisher
            };
        }
    }
}
