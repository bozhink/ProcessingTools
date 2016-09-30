namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

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

        public TagHigherTaxaController(IBiotaxonomicBlackListIterableDataService service, IHigherTaxaDataMiner miner)
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
        }

        protected override async Task Run(IDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var tagger = new HigherTaxaTagger(document.Xml, this.miner, this.service, logger);

            await tagger.Tag();

            document.Xml = tagger.Xml.NormalizeXmlToSystemXml();
        }
    }
}
