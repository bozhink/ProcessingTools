namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Processors.Contracts.Materials;

    [Description("Parse treatment materials.")]
    public class ParseTreatmentMaterialsController : GenericDocumentParserController<ITreatmentMaterialsParser>, IParseTreatmentMaterialsController
    {
        public ParseTreatmentMaterialsController(ITreatmentMaterialsParser parser)
            : base(parser)
        {
        }
    }
}
