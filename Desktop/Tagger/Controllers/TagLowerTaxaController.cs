namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.Bio.Taxonomy.Processors.Processors.Taggers;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    [Description("Tag lower taxa.")]
    public class TagLowerTaxaController : TaggerControllerFactory, ITagLowerTaxaController
    {
        private readonly IBiotaxonomicBlackListIterableDataService service;
        private readonly IDocumentNormalizer documentNormalizer;
        private readonly ILogger logger;

        public TagLowerTaxaController(
            IDocumentFactory documentFactory,
            IBiotaxonomicBlackListIterableDataService service,
            IDocumentNormalizer documentNormalizer,
            ILogger logger)
            : base(documentFactory)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            this.service = service;
            this.documentNormalizer = documentNormalizer;
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var tagger = new LowerTaxaTagger(this.service, this.logger);

            await tagger.Tag(document);

            await this.documentNormalizer.NormalizeToSystem(document);
        }
    }
}
