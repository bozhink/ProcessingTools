namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    [Description("Parse treatment meta with Aphia.")]
    public class ParseTreatmentMetaWithAphiaCommand : GenericDocumentParserCommand<ITreatmentMetaParserWithDataService<IAphiaTaxaClassificationResolver>>, IParseTreatmentMetaWithAphiaCommand
    {
        public ParseTreatmentMetaWithAphiaCommand(ITreatmentMetaParserWithDataService<IAphiaTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
