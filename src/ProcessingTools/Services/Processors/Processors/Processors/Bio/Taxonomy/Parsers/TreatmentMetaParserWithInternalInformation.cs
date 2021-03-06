﻿namespace ProcessingTools.Processors.Processors.Bio.Taxonomy.Parsers
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Processors.Contracts.Factories.Bio;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    public class TreatmentMetaParserWithInternalInformation : ITreatmentMetaParserWithInternalInformation
    {
        private readonly IBioTaxonomyTransformersFactory transformersFactory;

        public TreatmentMetaParserWithInternalInformation(IBioTaxonomyTransformersFactory transformersFactory)
        {
            this.transformersFactory = transformersFactory ?? throw new ArgumentNullException(nameof(transformersFactory));
        }

        public async Task<object> Parse(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var content = await this.transformersFactory
                .GetParseTreatmentMetaWithInternalInformationTransformer()
                .Transform(document.Xml);

            document.Xml = content;

            return true;
        }
    }
}
