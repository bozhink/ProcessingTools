namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

    [System.ComponentModel.Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifCommand : DocumentParserCommand<ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver>>, IParseTreatmentMetaWithGbifCommand
    {
        public ParseTreatmentMetaWithGbifCommand(ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
