namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Parse treatment meta with internal information.")]
    public class ParseTreatmentMetaWithInternalInformationCommand : DocumentParserCommand<ITreatmentMetaParserWithInternalInformation>, IParseTreatmentMetaWithInternalInformationCommand
    {
        public ParseTreatmentMetaWithInternalInformationCommand(ITreatmentMetaParserWithInternalInformation parser)
            : base(parser)
        {
        }
    }
}
