namespace ProcessingTools.Tagger.Commands.Commands
{
    using Contracts.Commands;
    using Generics;
    using ProcessingTools.Attributes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using Processors.Contracts.Processors.Bio.Taxonomy.Parsers;

    [Description("Parse higher taxa using Aphia.")]
    public class ParseHigherTaxaWithAphiaCommand : GenericParseHigherTaxaCommand<IAphiaTaxaRankResolver>, IParseHigherTaxaWithAphiaCommand
    {
        public ParseHigherTaxaWithAphiaCommand(
            IHigherTaxaParserWithDataService<IAphiaTaxaRankResolver, ITaxonRank> parser,
            IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
