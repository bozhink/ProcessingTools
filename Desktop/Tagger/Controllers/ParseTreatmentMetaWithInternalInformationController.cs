namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;

    [Description("Parse treatment meta with internal information.")]
    public class ParseTreatmentMetaWithInternalInformationController : GenericDocumentParserController<ITreatmentMetaParserWithInternalInformation>, IParseTreatmentMetaWithInternalInformationController
    {
        public ParseTreatmentMetaWithInternalInformationController(ITreatmentMetaParserWithInternalInformation parser)
            : base(parser)
        {
        }
    }
}
