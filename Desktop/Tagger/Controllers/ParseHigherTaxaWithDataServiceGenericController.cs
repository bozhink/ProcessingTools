namespace ProcessingTools.Tagger.Controllers
{
    using System;

    using Contracts;
    using Factories;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Processors.Contracts.Parsers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    public class ParseHigherTaxaWithDataServiceGenericController<TService> : ParseHigherTaxaControllerFactory<TService>, IParseHigherTaxaWithDataServiceGenericController<TService>
        where TService : ITaxonRankResolverDataService
    {
        private readonly IHigherTaxaParserWithDataService<TService, ITaxonRank> parser;

        public ParseHigherTaxaWithDataServiceGenericController(
            IDocumentFactory documentFactory,
            IHigherTaxaParserWithDataService<TService, ITaxonRank> parser,
            ILogger logger)
            : base(documentFactory, logger)
        {
            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            this.parser = parser;
        }

        protected override IHigherTaxaParserWithDataService<TService, ITaxonRank> Parser => this.parser;
    }
}
