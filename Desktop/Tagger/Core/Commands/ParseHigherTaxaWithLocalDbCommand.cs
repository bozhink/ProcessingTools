namespace ProcessingTools.Tagger.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Contracts;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

    [Description("Parse higher taxa with local database.")]
    public class ParseHigherTaxaWithLocalDbCommand : GenericParseHigherTaxaCommand<ILocalDbTaxaRankResolver>, IParseHigherTaxaWithLocalDbCommand
    {
        public ParseHigherTaxaWithLocalDbCommand(
            IHigherTaxaParserWithDataService<ILocalDbTaxaRankResolver, ITaxonRank> parser,
            ILogger logger)
            : base(parser, logger)
        {
        }
    }
}
