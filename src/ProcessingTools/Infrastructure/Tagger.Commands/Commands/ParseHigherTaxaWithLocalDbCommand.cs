namespace ProcessingTools.Tagger.Commands.Commands
{
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Commands.Tagger;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

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
