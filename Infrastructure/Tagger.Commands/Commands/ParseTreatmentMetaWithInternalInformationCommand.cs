namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    [Description("Parse treatment meta with internal information.")]
    public class ParseTreatmentMetaWithInternalInformationCommand : GenericDocumentParserCommand<ITreatmentMetaParserWithInternalInformation>, IParseTreatmentMetaWithInternalInformationCommand
    {
        public ParseTreatmentMetaWithInternalInformationCommand(ITreatmentMetaParserWithInternalInformation parser)
            : base(parser)
        {
        }
    }
}
