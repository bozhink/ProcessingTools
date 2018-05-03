namespace ProcessingTools.Commands.Tagger
{
    using ProcessingTools.Commands.Tagger.Abstractions;
    using ProcessingTools.Commands.Tagger.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Parse higher taxa by suffix.")]
    public class ParseHigherTaxaBySuffixCommand : ParseHigherTaxaCommand<ISuffixHigherTaxaRankResolver>, IParseHigherTaxaBySuffixCommand
    {
        public ParseHigherTaxaBySuffixCommand(IHigherTaxaParserWithDataService<ISuffixHigherTaxaRankResolver, ITaxonRank> parser, IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
