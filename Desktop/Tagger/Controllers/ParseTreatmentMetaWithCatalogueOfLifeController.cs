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

    [Description("Parse treatment meta with CoL.")]
    public class ParseTreatmentMetaWithCatalogueOfLifeController : TaggerControllerFactory, IParseTreatmentMetaWithCatalogueOfLifeController
    {
        private readonly ITreatmentMetaParser<ICatalogueOfLifeTaxaClassificationResolverDataService> parser;

        public ParseTreatmentMetaWithCatalogueOfLifeController(
            IDocumentFactory documentFactory,
            ITreatmentMetaParser<ICatalogueOfLifeTaxaClassificationResolverDataService> parser)
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
