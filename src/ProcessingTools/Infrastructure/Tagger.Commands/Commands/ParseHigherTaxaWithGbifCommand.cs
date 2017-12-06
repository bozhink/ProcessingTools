namespace ProcessingTools.Tagger.Commands.Commands
{
    using System.ComponentModel;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Processors.Processors.Bio.Taxonomy.Parsers;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;
    using ProcessingTools.Tagger.Commands.Contracts.Commands;
    using ProcessingTools.Tagger.Commands.Generics;

    [Description("Parse higher taxa using GBIF.")]
    public class ParseHigherTaxaWithGbifCommand : GenericParseHigherTaxaCommand<IGbifTaxaRankResolver>, IParseHigherTaxaWithGbifCommand
    {
        public ParseHigherTaxaWithGbifCommand(
            IHigherTaxaParserWithDataService<IGbifTaxaRankResolver, ITaxonRank> parser,
            IReporter reporter)
            : base(parser, reporter)
        {
        }
    }
}
