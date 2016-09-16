namespace ProcessingTools.Tagger.Controllers
{
    using System;

    using Contracts;
    using Factories;

    using ProcessingTools.BaseLibrary.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaWithDataServiceGenericController<TService> : ParseHigherTaxaControllerFactory<TService>, IParseHigherTaxaWithDataServiceGenericController<TService>
        where TService : ITaxonRankResolverDataService
    {
        private readonly IHigherTaxaParserWithDataService<TService, ITaxonRank> parser;

        public ParseHigherTaxaWithDataServiceGenericController(IHigherTaxaParserWithDataService<TService, ITaxonRank> parser)
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
