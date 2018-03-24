namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Parse higher taxa using GBIF.")]
    public class ParseHigherTaxaWithGbifCommand : ParseHigherTaxaCommand<IGbifTaxaRankResolver>, IParseHigherTaxaWithGbifCommand
    {
        public ParseHigherTaxaWithGbifCommand(
            IHigherTaxaParserWithDataService<IGbifTaxaRankResolver, ITaxonRank> parser,
            IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
