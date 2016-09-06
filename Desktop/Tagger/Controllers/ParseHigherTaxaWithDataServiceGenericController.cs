namespace ProcessingTools.Tagger.Controllers
{
    using System;

    using Contracts;
    using Factories;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaWithDataServiceGenericController<TService> : ParseHigherTaxaControllerFactory, IParseHigherTaxaWithDataServiceGenericController<TService>
        where TService : ITaxonRankResolverDataService
    {
        private readonly TService service;

        public ParseHigherTaxaWithDataServiceGenericController(TService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        protected override ITaxonRankResolverDataService Service => this.service;
    }
}
