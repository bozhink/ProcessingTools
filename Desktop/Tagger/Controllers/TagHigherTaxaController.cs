namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;

    using ProcessingTools.Attributes;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.BaseLibrary.Taxonomy;
    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Normalizers;

    [Description("Tag higher taxa.")]
    public class TagHigherTaxaController : TaggerControllerFactory, ITagHigherTaxaController
    {
        private readonly IBiotaxonomicBlackListIterableDataService service;
        private readonly IHigherTaxaDataMiner miner;
        private readonly IDocumentNormalizer documentNormalizer;
        private readonly ILogger logger;

        public TagHigherTaxaController(
            IDocumentFactory documentFactory,
            IBiotaxonomicBlackListIterableDataService service,
            IHigherTaxaDataMiner miner,
            IDocumentNormalizer documentNormalizer,
            ILogger logger)
            : base(documentFactory)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (documentNormalizer == null)
            {
                throw new ArgumentNullException(nameof(documentNormalizer));
            }

            this.service = service;
            this.miner = miner;
            this.documentNormalizer = documentNormalizer;
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var tagger = new HigherTaxaTagger(this.miner, this.service, this.logger);

            await tagger.Tag(document);

            await this.documentNormalizer.NormalizeToSystem(document);
        }
    }
}
