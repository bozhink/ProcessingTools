namespace ProcessingTools.Tagger.Controllers
{
    using System;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    [Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifController : ParseTreatmentMetaControllerFactory, IParseTreatmentMetaWithGbifController
    {
        private readonly IGbifTaxaClassificationDataService service;

        public ParseTreatmentMetaWithGbifController(IGbifTaxaClassificationDataService service)
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