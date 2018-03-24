namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifCommand : DocumentParserCommand<ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver>>, IParseTreatmentMetaWithGbifCommand
    {
        public ParseTreatmentMetaWithGbifCommand(ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
