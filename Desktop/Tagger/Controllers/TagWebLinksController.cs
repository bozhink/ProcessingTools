namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Attributes;
    using ProcessingTools.Attributes.Extensions;
    using ProcessingTools.BaseLibrary;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Xml.Extensions;

    [Description("Tag web links and DOI.")]
    public class TagWebLinksController : TaggerControllerFactory, ITagWebLinksController
    {
        private const string XPath = "/*";
        private readonly INlmExternalLinksDataMiner miner;
        private readonly ILogger logger;

        public TagWebLinksController(
            IDocumentFactory documentFactory,
            INlmExternalLinksDataMiner miner,
            ILogger logger)
            : base(documentFactory)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            this.miner = miner;
            this.logger = logger;
        }

        protected override async Task Run(IDocument document, ProgramSettings settings)
        {
            var textContent = document.XmlDocument.GetTextContent();
            var data = (await this.miner.Mine(textContent))
                .Select(i => new ExternalLinkSerializableModel
                {
                    ExternalLinkType = i.Type.GetValue(),
                    Value = i.Content
                });

            var tagger = new SimpleXmlSerializableObjectTagger<ExternalLinkSerializableModel>(document.Xml, data, XPath, document.NamespaceManager, false, true, this.logger);

            await tagger.Tag();

            document.Xml = tagger.Xml;
        }
    }
}
