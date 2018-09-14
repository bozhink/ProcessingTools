// <copyright file="DocumentProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.References;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Models.Documents.Documents;

    /// <summary>
    /// Document processing service.
    /// </summary>
    public class DocumentProcessingService : IDocumentProcessingService
    {
        private readonly IDocumentsDataService documentsDataService;
        private readonly IDocumentFactory documentFactory;
        private readonly IArticlesDataService articlesDataService;
        private readonly IJournalStylesDataService journalStylesDataService;
        private readonly IReferencesParser referencesParser;
        private readonly IReferencesTagger referencesTagger;
        private readonly IDocumentMetaService documentMetaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentProcessingService"/> class.
        /// </summary>
        /// <param name="documentsDataService">Document data service.</param>
        /// <param name="documentFactory">Document factory.</param>
        /// <param name="articlesDataService">Articles data service.</param>
        /// <param name="journalStylesDataService">Journal styles data service.</param>
        /// <param name="referencesParser">References parser.</param>
        /// <param name="referencesTagger">References tagger.</param>
        /// <param name="documentMetaService">Document meta-data processing service.</param>
        public DocumentProcessingService(
           IDocumentsDataService documentsDataService,
           IDocumentFactory documentFactory,
           IArticlesDataService articlesDataService,
           IJournalStylesDataService journalStylesDataService,
           IReferencesParser referencesParser,
           IReferencesTagger referencesTagger,
           IDocumentMetaService documentMetaService)
        {
            this.documentsDataService = documentsDataService ?? throw new ArgumentNullException(nameof(documentsDataService));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.articlesDataService = articlesDataService ?? throw new ArgumentNullException(nameof(articlesDataService));
            this.journalStylesDataService = journalStylesDataService ?? throw new ArgumentNullException(nameof(journalStylesDataService));
            this.referencesParser = referencesParser ?? throw new ArgumentNullException(nameof(referencesParser));
            this.referencesTagger = referencesTagger ?? throw new ArgumentNullException(nameof(referencesTagger));
            this.documentMetaService = documentMetaService ?? throw new ArgumentNullException(nameof(documentMetaService));
        }

        /// <inheritdoc/>
        public async Task<object> ParseReferencesAsync(object documentId, object articleId)
        {
            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var journalStyleId = await this.articlesDataService.GetJournalStyleIdAsync(articleId).ConfigureAwait(false);
            if (journalStyleId == null)
            {
                return null;
            }

            var styles = await this.journalStylesDataService.GetReferenceParseStylesAsync(journalStyleId).ConfigureAwait(false);

            var document = await this.GetDocumentAsync(documentId);

            var parsed = await this.referencesParser.ParseAsync(document.XmlDocument.DocumentElement, styles).ConfigureAwait(false);
            if (parsed == null)
            {
                return null;
            }

            string description = "Parse references";

            return await this.CreateDocumentAsync(documentId, articleId, document, description).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<object> TagReferencesAsync(object documentId, object articleId)
        {
            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            var journalStyleId = await this.articlesDataService.GetJournalStyleIdAsync(articleId).ConfigureAwait(false);
            if (journalStyleId == null)
            {
                return null;
            }

            var styles = await this.journalStylesDataService.GetReferenceTagStylesAsync(journalStyleId).ConfigureAwait(false);

            var document = await this.GetDocumentAsync(documentId);

            var parsed = await this.referencesTagger.TagAsync(document.XmlDocument.DocumentElement, styles).ConfigureAwait(false);
            if (parsed == null)
            {
                return null;
            }

            string description = "Tag references";

            return await this.CreateDocumentAsync(documentId, articleId, document, description).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<object> UpdateDocumentMetaAsync(object documentId, object articleId)
        {
            if (documentId == null)
            {
                throw new ArgumentNullException(nameof(documentId));
            }

            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return await this.documentMetaService.UpdateDocumentAsync(documentId.ToString()).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<object> UpdateArticleDocumentsMetaAsync(object articleId)
        {
            if (articleId == null)
            {
                throw new ArgumentNullException(nameof(articleId));
            }

            return await this.documentMetaService.UpdateArticleDocumentsAsync(articleId.ToString()).ConfigureAwait(false);
        }

        private async Task<object> CreateDocumentAsync(object documentId, object articleId, IDocument document, string description)
        {
            var originalDocument = await this.documentsDataService.GetDetailsByIdAsync(documentId).ConfigureAwait(false);
            var insertDocument = new DocumentInsertModel
            {
                ArticleId = articleId.ToString(),
                Description = description,
                FileId = originalDocument.FileId,
                File = new DocumentFileModel
                {
                    FileName = originalDocument.File.FileName,
                    FileExtension = originalDocument.File.FileExtension,
                    ContentLength = document.Xml.Length,
                    ContentType = ContentTypes.Xml
                }
            };

            var id = await this.documentsDataService.InsertAsync(insertDocument).ConfigureAwait(false);
            await this.documentsDataService.SetDocumentContentAsync(id, document.Xml).ConfigureAwait(false);

            return id;
        }

        private async Task<IDocument> GetDocumentAsync(object documentId)
        {
            string content = await this.documentsDataService.GetDocumentContentAsync(documentId).ConfigureAwait(false);
            var document = this.documentFactory.Create(content);
            return document;
        }
    }
}
