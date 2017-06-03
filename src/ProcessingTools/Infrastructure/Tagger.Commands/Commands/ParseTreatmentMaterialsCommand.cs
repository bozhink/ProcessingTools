namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Materials;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Parse treatment materials.")]
    public class ParseTreatmentMaterialsCommand : GenericDocumentParserCommand<ITreatmentMaterialsParser>, IParseTreatmentMaterialsCommand
    {
        public ParseTreatmentMaterialsCommand(ITreatmentMaterialsParser parser)
            : base(parser)
        {
        }
    }
}
