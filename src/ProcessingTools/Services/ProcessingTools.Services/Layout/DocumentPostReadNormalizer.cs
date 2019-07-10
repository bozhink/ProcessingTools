// <copyright file="DocumentPostReadNormalizer.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Layout;

namespace ProcessingTools.Services.Layout
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;

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
        public async Task<object> NormalizeAsync(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var result = await this.documentNormalizer.NormalizeToSystemAsync(document).ConfigureAwait(false);

            return result;
        }
    }
}
