namespace ProcessingTools.MainProgram.Controllers
{
    using System;

    using Contracts;
    using Factories;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaWithDataServiceGenericController<TService> : ParseHigherTaxaControllerFactory, IParseHigherTaxaWithDataServiceGenericController<TService>
        where TService : ITaxaDataService<ITaxonClassification>
    {
        private readonly TService service;

        public ParseHigherTaxaWithDataServiceGenericController(TService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.service = service;
        }

        protected override ITaxaDataService<ITaxonClassification> Service => this.service;
    }
}
