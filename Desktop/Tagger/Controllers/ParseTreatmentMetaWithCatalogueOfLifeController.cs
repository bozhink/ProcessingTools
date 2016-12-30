namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    [Description("Parse treatment meta with CoL.")]
    public class ParseTreatmentMetaWithCatalogueOfLifeController : GenericDocumentParserController<ITreatmentMetaParser<ICatalogueOfLifeTaxaClassificationResolver>>, IParseTreatmentMetaWithCatalogueOfLifeController
    {
        public ParseTreatmentMetaWithCatalogueOfLifeController(ITreatmentMetaParser<ICatalogueOfLifeTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
