namespace ProcessingTools.Bio.Taxonomy.Processors.Processors.Parsers
{
    using System;
    using System.Threading.Tasks;
    using Contracts.Factories;
    using Contracts.Parsers;
    using ProcessingTools.Contracts;

    public class TreatmentMetaParserWithInternalInformation : ITreatmentMetaParserWithInternalInformation
    {
        private readonly IBioTaxonomyTransformersFactory transformersFactory;

        public TreatmentMetaParserWithInternalInformation(IBioTaxonomyTransformersFactory transformersFactory)
        {
            if (transformersFactory == null)
            {
                throw new ArgumentNullException(nameof(transformersFactory));
            }

            this.transformersFactory = transformersFactory;
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
