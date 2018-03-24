// <copyright file="TaxonNamePartsRemover.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon name parts remover.
    /// </summary>
    public class TaxonNamePartsRemover : ITaxonNamePartsRemover
    {
        private readonly IBioTaxonomyTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonNamePartsRemover"/> class.
        /// </summary>
        /// <param name="transformerFactory">Transformer factory.</param>
        public TaxonNamePartsRemover(IBioTaxonomyTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public async Task<object> FormatAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformerFactory
                .GetRemoveTaxonNamePartsTransformer()
                .TransformAsync(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;

            return true;
        }
    }
}
