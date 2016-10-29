namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse treatment meta with Aphia.")]
    public class ParseTreatmentMetaWithAphiaController : GenericDocumentParserController<ITreatmentMetaParser<IAphiaTaxaClassificationResolverDataService>>, IParseTreatmentMetaWithAphiaController
    {
        public ParseTreatmentMetaWithAphiaController(ITreatmentMetaParser<IAphiaTaxaClassificationResolverDataService> parser)
            : base(parser)
        {
        }
    }
}
