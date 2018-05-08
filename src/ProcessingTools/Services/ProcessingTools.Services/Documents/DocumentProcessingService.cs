// <copyright file="DocumentProcessingService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Documents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Rules;
    using ProcessingTools.Processors.Contracts.Layout;
    using ProcessingTools.Processors.Contracts.References;
    using ProcessingTools.Services.Contracts.Documents;
    using ProcessingTools.Services.Contracts.Layout.Styles;
    using ProcessingTools.Services.Contracts.Rules;
    using ProcessingTools.Services.Models.Documents.Documents;

    /// <summary>
    /// Document processing service.
    /// </summary>
    public class DocumentProcessingService : IDocumentProcessingService
    {
        private readonly IDocumentsDataService documentsDataService;
        private readonly IDocumentFactory documentFactory;
        private readonly IDocumentSchemaNormalizer documentSchemaNormalizer;
        private readonly IArticlesDataService articlesDataService;
        private readonly IJournalStylesDataService journalStylesDataService;
        private readonly IXmlReplaceRuleSetParser ruleSetParser;
        private readonly IReferencesParser referencesParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentProcessingService"/> class.
        /// </summary>
        /// <param name="documentsDataService">Document data service.</param>
        /// <param name="documentFactory">Document factory.</param>
        /// <param name="documentSchemaNormalizer">Document schema normalizer.</param>
        /// <param name="articlesDataService">Articles data service.</param>
        /// <param name="journalStylesDataService">Journal styles data service.</param>
        /// <param name="ruleSetParser">Rule set parser.</param>
        /// <param name="referencesParser">References parser.</param>
        public DocumentProcessingService(
           IDocumentsDataService documentsDataService,
           IDocumentFactory documentFactory,
           IDocumentSchemaNormalizer documentSchemaNormalizer,
           IArticlesDataService articlesDataService,
           IJournalStylesDataService journalStylesDataService,
           IXmlReplaceRuleSetParser ruleSetParser,
           IReferencesParser referencesParser)
        {
            this.documentsDataService = documentsDataService ?? throw new ArgumentNullException(nameof(documentsDataService));
            this.documentFactory = documentFactory ?? throw new ArgumentNullException(nameof(documentFactory));
            this.documentSchemaNormalizer = documentSchemaNormalizer ?? throw new ArgumentNullException(nameof(documentSchemaNormalizer));
            this.articlesDataService = articlesDataService ?? throw new ArgumentNullException(nameof(articlesDataService));
            this.journalStylesDataService = journalStylesDataService ?? throw new ArgumentNullException(nameof(journalStylesDataService));
            this.ruleSetParser = ruleSetParser ?? throw new ArgumentNullException(nameof(ruleSetParser));
            this.referencesParser = referencesParser ?? throw new ArgumentNullException(nameof(referencesParser));
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

            var ruleSets = await this.GetReferenceParseRuleSetsFromStylesAsync(articleId);
            var document = await this.GetDocumentAsync(documentId);

            var parsed = await this.referencesParser.ParseAsync(document.XmlDocument.DocumentElement, ruleSets).ConfigureAwait(false);
            if (parsed == null)
            {
                return null;
            }

            var normalized = await this.documentSchemaNormalizer.NormalizeToDocumentSchemaAsync(document).ConfigureAwait(false);
            if (normalized == null)
            {
                return null;
            }

            string description = "Parse references";

            return await this.CreateDocumentAsync(documentId, articleId, document, description).ConfigureAwait(false);
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

        private async Task<IList<IXmlReplaceRuleSetModel>> GetReferenceParseRuleSetsFromStylesAsync(object articleId)
        {
            var journalStyleId = await this.articlesDataService.GetJournalStyleIdAsync(articleId).ConfigureAwait(false);
            if (journalStyleId == null)
            {
                return null;
            }

            var styles = await this.journalStylesDataService.GetReferenceParseStylesAsync(journalStyleId).ConfigureAwait(false);

            var scripts = styles.Select(s => s.Script);

            var ruleSets = new List<IXmlReplaceRuleSetModel>();
            foreach (var script in scripts)
            {
                var scriptRuleSets = await this.ruleSetParser.ParseStringToRuleSetsAsync(script).ConfigureAwait(false);
                if (scriptRuleSets != null && scriptRuleSets.Any())
                {
                    ruleSets.AddRange(scriptRuleSets);
                }
            }

            return ruleSets;
        }

        private async Task<IDocument> GetDocumentAsync(object documentId)
        {
            string content = await this.documentsDataService.GetDocumentContentAsync(documentId).ConfigureAwait(false);
            var document = this.documentFactory.Create(content);

            var normalized = await this.documentSchemaNormalizer.NormalizeToDocumentSchemaAsync(document).ConfigureAwait(false);
            if (normalized == null)
            {
                return null;
            }

            return document;
        }
    }
}
