namespace ProcessingTools.Tagger.Controllers
{
    using Contracts.Controllers;
    using Generics;

    using ProcessingTools.Attributes;
    using ProcessingTools.Processors.Contracts.Bio.Materials;

    [Description("Parse treatment materials.")]
    public class ParseTreatmentMaterialsController : GenericDocumentParserController<ITreatmentMaterialsParser>, IParseTreatmentMaterialsController
    {
        public ParseTreatmentMaterialsController(ITreatmentMaterialsParser parser)
            : base(parser)
        {
        }
    }
}
