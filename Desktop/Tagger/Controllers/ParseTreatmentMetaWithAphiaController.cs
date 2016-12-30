namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    [Description("Parse treatment meta with Aphia.")]
    public class ParseTreatmentMetaWithAphiaController : GenericDocumentParserController<ITreatmentMetaParser<IAphiaTaxaClassificationResolver>>, IParseTreatmentMetaWithAphiaController
    {
        public ParseTreatmentMetaWithAphiaController(ITreatmentMetaParser<IAphiaTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
