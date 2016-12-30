namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    [Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifController : GenericDocumentParserController<ITreatmentMetaParser<IGbifTaxaClassificationResolver>>, IParseTreatmentMetaWithGbifController
    {
        public ParseTreatmentMetaWithGbifController(ITreatmentMetaParser<IGbifTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
