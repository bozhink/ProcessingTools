namespace ProcessingTools.Tagger.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;

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

        public TagWebLinksController(INlmExternalLinksDataMiner miner)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            this.miner = miner;
        }

        protected override async Task Run(IDocument document, XmlNamespaceManager namespaceManager, ProgramSettings settings, ILogger logger)
        {
            var textContent = document.XmlDocument.GetTextContent();
            var data = (await this.miner.Mine(textContent))
                .Select(i => new ExternalLinkSerializableModel
                {
                    ExternalLinkType = i.Type.GetValue(),
                    Value = i.Content
                });

            var tagger = new SimpleXmlSerializableObjectTagger<ExternalLinkSerializableModel>(document.Xml, data, XPath, namespaceManager, false, true, logger);

            await tagger.Tag();

            document.Xml = tagger.Xml;
        }
    }
}
