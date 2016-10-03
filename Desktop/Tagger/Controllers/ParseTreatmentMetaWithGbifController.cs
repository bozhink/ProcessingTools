namespace ProcessingTools.Tagger.Controllers
{
    using System;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;

    [Description("Parse treatment meta with GBIF.")]
    public class ParseTreatmentMetaWithGbifController : ParseTreatmentMetaControllerFactory, IParseTreatmentMetaWithGbifController
    {
        private readonly IGbifTaxaClassificationResolverDataService service;

        public ParseTreatmentMetaWithGbifController(
            IDocumentFactory documentFactory,
            IGbifTaxaClassificationResolverDataService service,
            ILogger logger)
            : base(documentFactory, logger)
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