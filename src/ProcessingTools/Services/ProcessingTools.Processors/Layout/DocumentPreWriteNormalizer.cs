// <copyright file="DocumentPreWriteNormalizer.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Layout
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Layout;

    /// <summary>
    /// Document pre-write normalizer.
    /// </summary>
    public class DocumentPreWriteNormalizer : IDocumentPreWriteNormalizer
    {
        private readonly IDocumentSchemaNormalizer documentNormalizer;
        private readonly IDocumentFinalFormatter documentFinalFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentPreWriteNormalizer"/> class.
        /// </summary>
        /// <param name="documentNormalizer">Document normalizer.</param>
        /// <param name="documentFinalFormatter">Document final formatter.</param>
        public DocumentPreWriteNormalizer(IDocumentSchemaNormalizer documentNormalizer, IDocumentFinalFormatter documentFinalFormatter)
        {
            this.documentNormalizer = documentNormalizer ?? throw new ArgumentNullException(nameof(documentNormalizer));
            this.documentFinalFormatter = documentFinalFormatter ?? throw new ArgumentNullException(nameof(documentFinalFormatter));
        }

        /// <inheritdoc/>
        public async Task<object> NormalizeAsync(IDocument document)
        {
            if (document == null)
            {
                return false;
            }

            // Due to some XSL characteristics, double normalization is better than a single one.
            var result = await this.documentNormalizer.NormalizeToDocumentSchemaAsync(document)
                .ContinueWith(
                    _ =>
                    {
                        _.Wait();
                        return this.documentNormalizer.NormalizeToDocumentSchemaAsync(document);
                    })
                .ContinueWith(
                    _ =>
                    {
                        _.Wait();
                        return this.documentFinalFormatter.FormatAsync(document).Result;
                    })
                .ConfigureAwait(false);

            return result;
        }
    }
}
