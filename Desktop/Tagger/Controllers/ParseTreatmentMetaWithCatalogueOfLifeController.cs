namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    [Description("Parse treatment meta with CoL.")]
    public class ParseTreatmentMetaWithCatalogueOfLifeController : GenericDocumentParserController<ITreatmentMetaParserWithDataService<ICatalogueOfLifeTaxaClassificationResolver>>, IParseTreatmentMetaWithCatalogueOfLifeController
    {
        public ParseTreatmentMetaWithCatalogueOfLifeController(ITreatmentMetaParserWithDataService<ICatalogueOfLifeTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
