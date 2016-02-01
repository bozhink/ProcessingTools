namespace ProcessingTools.MainProgram.Controllers
{
    using System;

    using Contracts;
    using Factories;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Parse treatment meta with CoL.")]
    public class ParseTreatmentMetaWithCatalogueOfLifeController : ParseTreatmentMetaControllerFactory, IParseTreatmentMetaWithCatalogueOfLifeController
    {
        private readonly ICatalogueOfLifeTaxaClassificationDataService service;

        public ParseTreatmentMetaWithCatalogueOfLifeController(ICatalogueOfLifeTaxaClassificationDataService service)
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