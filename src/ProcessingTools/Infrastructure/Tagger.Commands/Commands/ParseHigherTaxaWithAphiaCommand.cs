namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Processors.Contracts.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

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
