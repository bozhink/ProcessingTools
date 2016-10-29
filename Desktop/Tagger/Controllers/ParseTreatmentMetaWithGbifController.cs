namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifController : GenericDocumentParserController<ITreatmentMetaParser<IGbifTaxaClassificationResolverDataService>>, IParseTreatmentMetaWithGbifController
    {
        public ParseTreatmentMetaWithGbifController(ITreatmentMetaParser<IGbifTaxaClassificationResolverDataService> parser)
            : base(parser)
        {
        }
    }
}
