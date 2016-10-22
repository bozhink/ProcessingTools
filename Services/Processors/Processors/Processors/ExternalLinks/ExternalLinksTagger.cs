namespace ProcessingTools.Processors.Processors.ExternalLinks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts.ExternalLinks;
    using Models.ExternalLinks;

    using ProcessingTools.Attributes.Extensions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Xml.Extensions;

    public class ExternalLinksTagger : IExternalLinksTagger
    {
        private const string XPath = "./*";

        private readonly INlmExternalLinksDataMiner miner;
        private readonly ISimpleXmlSerializableObjectTagger<ExternalLinkSerializableModel> contentTagger;
        private readonly ILogger logger;

        public ExternalLinksTagger(
            INlmExternalLinksDataMiner miner,
            ISimpleXmlSerializableObjectTagger<ExternalLinkSerializableModel> contentTagger,
            ILogger logger)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.miner = miner;
            this.contentTagger = contentTagger;
            this.logger = logger;
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = document.XmlDocument.GetTextContent();
            var data = (await this.miner.Mine(textContent))
                .Select(i => new ExternalLinkSerializableModel
                {
                    ExternalLinkType = i.Type.GetValue(),
                    Value = i.Content
                });

            await this.contentTagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, data, XPath, false, true);

            return true;
        }
    }
}
