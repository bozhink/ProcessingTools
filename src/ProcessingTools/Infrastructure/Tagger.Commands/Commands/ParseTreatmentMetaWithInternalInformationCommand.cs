namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Parse treatment meta with internal information.")]
    public class ParseTreatmentMetaWithInternalInformationCommand : GenericDocumentParserCommand<ITreatmentMetaParserWithInternalInformation>, IParseTreatmentMetaWithInternalInformationCommand
    {
        public ParseTreatmentMetaWithInternalInformationCommand(ITreatmentMetaParserWithInternalInformation parser)
            : base(parser)
        {
        }
    }
}
