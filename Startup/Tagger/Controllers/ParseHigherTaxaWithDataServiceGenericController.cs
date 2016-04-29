namespace ProcessingTools.Tagger.Controllers
{
    using System;

    using Contracts;
    using Factories;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaWithDataServiceGenericController<TService> : ParseHigherTaxaControllerFactory, IParseHigherTaxaWithDataServiceGenericController<TService>
        where TService : ITaxaInformationResolverDataService<ITaxonClassification>
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

        protected override ITaxaInformationResolverDataService<ITaxonClassification> Service => this.service;
    }
}
