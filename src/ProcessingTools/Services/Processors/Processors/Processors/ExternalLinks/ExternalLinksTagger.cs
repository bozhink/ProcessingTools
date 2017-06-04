namespace ProcessingTools.Processors.Processors.ExternalLinks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners.ExternalLinks;
    using ProcessingTools.Harvesters.Contracts.Harvesters.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Models.Taggers;
    using ProcessingTools.Models.Serialization.Nlm;
    using ProcessingTools.Processors.Contracts.Processors.ExternalLinks;

    public class ExternalLinksTagger : IExternalLinksTagger
    {
        private const string XPath = "./*";

        private readonly IExternalLinksDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<ExternalLinkXmlModel> contentTagger;
        private readonly ILogger logger;

        public ExternalLinksTagger(
            IExternalLinksDataMiner miner,
            ITextContentHarvester contentHarvester,
            ISimpleXmlSerializableObjectTagger<ExternalLinkXmlModel> contentTagger,
            ILogger logger)
        {
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.contentTagger = contentTagger ?? throw new ArgumentNullException(nameof(contentTagger));
            this.logger = logger;
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = await this.contentHarvester.Harvest(document.XmlDocument.DocumentElement);
            var data = (await this.miner.Mine(textContent))
                .Select(i => new ExternalLinkXmlModel
                {
                    Href = i.Href,
                    ExternalLinkType = i.Type.GetName(),
                    Value = i.Content
                });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = false,
                MinimalTextSelect = true
            };

            await this.contentTagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, data, XPath, settings);

            return true;
        }
    }
}
