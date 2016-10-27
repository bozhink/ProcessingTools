﻿namespace ProcessingTools.Tagger.Controllers
{
    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse higher taxa using Aphia.")]
    public class ParseHigherTaxaWithAphiaController : ParseHigherTaxaControllerFactory<IAphiaTaxaRankResolverDataService>, IParseHigherTaxaWithAphiaController
    {
        public ParseHigherTaxaWithAphiaController(
            IHigherTaxaParserWithDataService<IAphiaTaxaRankResolverDataService, ITaxonRank> parser,
            ILogger logger)
            : base(parser, logger)
        {
        }
    }
}
