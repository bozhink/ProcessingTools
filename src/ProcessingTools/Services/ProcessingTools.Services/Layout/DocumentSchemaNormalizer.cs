﻿// <copyright file="DocumentSchemaNormalizer.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Layout
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Layout;

    /// <summary>
    /// Document schema normalizer.
    /// </summary>
    public class DocumentSchemaNormalizer : IDocumentSchemaNormalizer
    {
        private readonly INormalizationTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentSchemaNormalizer"/> class.
        /// </summary>
        /// <param name="transformerFactory">Transformer factory.</param>
        public DocumentSchemaNormalizer(INormalizationTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentOutOfRangeException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public Task<object> NormalizeToDocumentSchemaAsync(IDocument document) => this.NormalizeAsync(document, document.SchemaType);

        /// <inheritdoc/>
        public Task<object> NormalizeToSystemAsync(IDocument document) => this.NormalizeAsync(document, SchemaType.System);

        private Task<object> NormalizeAsync(IDocument document, SchemaType schemaType)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return this.NormalizeInternalAsync(document, schemaType);
        }

        private async Task<object> NormalizeInternalAsync(IDocument document, SchemaType schemaType)
        {
            var transformer = this.transformerFactory.Create(schemaType);

            document.Xml = await transformer.TransformAsync(document.Xml).ConfigureAwait(false);

            return true;
        }
    }
}
