namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifController : TaggerControllerFactory, IParseTreatmentMetaWithGbifController
    {
        private readonly ITreatmentMetaParser<IGbifTaxaClassificationResolverDataService> parser;

        public ParseTreatmentMetaWithGbifController(
            IDocumentFactory documentFactory,
            ITreatmentMetaParser<IGbifTaxaClassificationResolverDataService> parser)
            : base(documentFactory)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            await this.parser.Parse(document);
        }
    }
}
