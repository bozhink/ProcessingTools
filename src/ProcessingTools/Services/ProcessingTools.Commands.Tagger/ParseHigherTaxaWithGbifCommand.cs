namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Parse higher taxa using GBIF.")]
    public class ParseHigherTaxaWithGbifCommand : ParseHigherTaxaCommand<IGbifTaxaRankResolver>, IParseHigherTaxaWithGbifCommand
    {
        public ParseHigherTaxaWithGbifCommand(IHigherTaxaParserWithDataService<IGbifTaxaRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
