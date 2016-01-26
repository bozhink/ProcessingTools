namespace ProcessingTools.MainProgram.Controllers
{
    using System;

    using Contracts;
    using Factories;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public class ParseHigherTaxaWithAphiaController : ParseHigherTaxaControllerFactory, IParseHigherTaxaWithAphiaController
    {
        private readonly IAphiaTaxaClassificationDataService service;

        public ParseHigherTaxaWithAphiaController(IAphiaTaxaClassificationDataService service)
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
