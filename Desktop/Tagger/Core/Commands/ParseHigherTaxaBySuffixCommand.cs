namespace ProcessingTools.Tagger.Core.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    [Description("Parse higher taxa by suffix.")]
    public class ParseHigherTaxaBySuffixCommand : GenericParseHigherTaxaCommand<ISuffixHigherTaxaRankResolver>, IParseHigherTaxaBySuffixCommand
    {
        public ParseHigherTaxaBySuffixCommand(
            IHigherTaxaParserWithDataService<ISuffixHigherTaxaRankResolver, ITaxonRank> parser,
            IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
