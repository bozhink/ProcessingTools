namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    [Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifCommand : GenericDocumentParserCommand<ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver>>, IParseTreatmentMetaWithGbifCommand
    {
        public ParseTreatmentMetaWithGbifCommand(ITreatmentMetaParserWithDataService<IGbifTaxaClassificationResolver> parser)
            : base(parser)
        {
        }
    }
}
