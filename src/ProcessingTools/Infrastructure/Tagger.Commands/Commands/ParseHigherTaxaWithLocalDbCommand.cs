namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    [System.ComponentModel.Description("Parse higher taxa with local database.")]
    public class ParseHigherTaxaWithLocalDbCommand : ParseHigherTaxaCommand<ILocalDbTaxaRankResolver>, IParseHigherTaxaWithLocalDbCommand
    {
        public ParseHigherTaxaWithLocalDbCommand(
            IHigherTaxaParserWithDataService<ILocalDbTaxaRankResolver, ITaxonRank> parser,
            IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
