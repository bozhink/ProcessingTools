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

        public ExternalLinksTagger(
            IExternalLinksDataMiner miner,
            ITextContentHarvester contentHarvester,
            ISimpleXmlSerializableObjectTagger<ExternalLinkXmlModel> contentTagger)
        {
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.contentTagger = contentTagger ?? throw new ArgumentNullException(nameof(contentTagger));
        }

        public async Task<object> TagAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var textContent = await this.contentHarvester.Harvest(context.XmlDocument.DocumentElement).ConfigureAwait(false);
            var data = (await this.miner.MineAsync(textContent).ConfigureAwait(false))
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

            await this.contentTagger.Tag(context.XmlDocument.DocumentElement, context.NamespaceManager, data, XPath, settings).ConfigureAwait(false);

            return true;
        }
    }
}
