namespace ProcessingTools.Tagger.Controllers
{
    using System;

    using Contracts;
    using Factories;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Infrastructure.Attributes;

    [Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifController : ParseTreatmentMetaControllerFactory, IParseTreatmentMetaWithGbifController
    {
        private readonly IGbifTaxaClassificationDataService service;

        public ParseTreatmentMetaWithGbifController(IGbifTaxaClassificationDataService service)
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