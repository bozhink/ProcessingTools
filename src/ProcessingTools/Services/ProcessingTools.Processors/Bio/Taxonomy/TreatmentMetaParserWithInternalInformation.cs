// <copyright file="TreatmentMetaParserWithInternalInformation.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Treatment meta parser with internal information.
    /// </summary>
    public class TreatmentMetaParserWithInternalInformation : ITreatmentMetaParserWithInternalInformation
    {
        private readonly IBioTaxonomyTransformerFactory transformerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreatmentMetaParserWithInternalInformation"/> class.
        /// </summary>
        /// <param name="transformerFactory">Transformer factory.</param>
        public TreatmentMetaParserWithInternalInformation(IBioTaxonomyTransformerFactory transformerFactory)
        {
            this.transformerFactory = transformerFactory ?? throw new ArgumentNullException(nameof(transformerFactory));
        }

        /// <inheritdoc/>
        public async Task<object> ParseAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var content = await this.transformerFactory
                .GetParseTreatmentMetaWithInternalInformationTransformer()
                .TransformAsync(context.Xml)
                .ConfigureAwait(false);

            context.Xml = content;

            return true;
        }
    }
}
