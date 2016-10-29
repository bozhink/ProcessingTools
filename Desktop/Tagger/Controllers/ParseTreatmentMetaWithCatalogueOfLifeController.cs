namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse treatment meta with CoL.")]
    public class ParseTreatmentMetaWithCatalogueOfLifeController : GenericDocumentParserController<ITreatmentMetaParser<ICatalogueOfLifeTaxaClassificationResolverDataService>>, IParseTreatmentMetaWithCatalogueOfLifeController
    {
        public ParseTreatmentMetaWithCatalogueOfLifeController(ITreatmentMetaParser<ICatalogueOfLifeTaxaClassificationResolverDataService> parser)
            : base(parser)
        {
        }
    }
}
