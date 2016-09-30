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

    [Description("Tag higher taxa.")]
    public class TagHigherTaxaController : TaggerControllerFactory, ITagHigherTaxaController
    {
        private readonly IBiotaxonomicBlackListIterableDataService service;
        private readonly IHigherTaxaDataMiner miner;
        private readonly ILogger logger;

        public TagHigherTaxaController(
            IDocumentFactory documentFactory,
            IBiotaxonomicBlackListIterableDataService service,
            IHigherTaxaDataMiner miner,
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

            this.service = service;
            this.miner = miner;
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var tagger = new HigherTaxaTagger(document.Xml, this.miner, this.service, this.logger);

            await tagger.Tag();

            document.Xml = tagger.Xml.NormalizeXmlToSystemXml();
        }
    }
}
