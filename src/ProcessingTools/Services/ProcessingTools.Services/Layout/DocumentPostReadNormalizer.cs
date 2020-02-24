// <copyright file="DocumentPostReadNormalizer.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Layout;

    /// <summary>
    /// Document post-read normalizer.
    /// </summary>
    public class DocumentPostReadNormalizer : IDocumentPostReadNormalizer
    {
        private readonly IDocumentSchemaNormalizer documentNormalizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentPostReadNormalizer"/> class.
        /// </summary>
        /// <param name="documentNormalizer">Document normalizer.</param>
        public DocumentPostReadNormalizer(IDocumentSchemaNormalizer documentNormalizer)
        {
            this.documentNormalizer = documentNormalizer ?? throw new ArgumentNullException(nameof(documentNormalizer));
        }

        /// <inheritdoc/>
        public Task<object> NormalizeAsync(IDocument document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return this.NormalizeInternalAsync(document);
        }

        private async Task<object> NormalizeInternalAsync(IDocument document)
        {
            return await this.documentNormalizer.NormalizeToSystemAsync(document).ConfigureAwait(false);
        }
    }
}
