namespace ProcessingTools.Processors.Processors.Bio.EnvironmentTerms
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Bio.Environments;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.EnvironmentTerms;
    using ProcessingTools.Processors.Models;
    using ProcessingTools.Processors.Models.Bio.EnvironmentTerms;

    public class EnvironmentTermsTagger : IEnvironmentTermsTagger
    {
        private const string XPath = "./*";

        private readonly IEnvoTermsDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger;

        public EnvironmentTermsTagger(
            IEnvoTermsDataMiner miner,
            ITextContentHarvester contentHarvester,
            ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger)
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

            var textContent = await this.contentHarvester.HarvestAsync(context.XmlDocument.DocumentElement);
            var data = (await this.miner.MineAsync(textContent))
                .Select(t => new EnvoTermResponseModel
                {
                    EntityId = t.EntityId,
                    EnvoId = t.EnvoId,
                    Content = t.Content
                })
                .Select(t => new EnvoTermSerializableModel
                {
                    Value = t.Content,
                    EnvoId = t.EnvoId,
                    Id = t.EntityId,
                    VerbatimTerm = t.Content
                });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = false,
                MinimalTextSelect = true
            };

            await this.contentTagger.TagAsync(context.XmlDocument.DocumentElement, context.NamespaceManager, data, XPath, settings).ConfigureAwait(false);

            return true;
        }
    }
}
