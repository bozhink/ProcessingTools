namespace ProcessingTools.Processors.Bio.EnvironmentTerms
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.EnvironmentTerms;
    using Models.Bio.EnvironmentTerms;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Models.Taggers;

    public class EnvironmentTermsTagger : IEnvironmentTermsTagger
    {
        private const string XPath = "./*";

        private readonly IEnvoTermsDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger;
        private readonly ILogger logger;

        public EnvironmentTermsTagger(
            IEnvoTermsDataMiner miner,
            ITextContentHarvester contentHarvester,
            ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger,
            ILogger logger)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (contentHarvester == null)
            {
                throw new ArgumentNullException(nameof(contentHarvester));
            }

            if (contentTagger == null)
            {
                throw new ArgumentNullException(nameof(contentTagger));
            }

            this.miner = miner;
            this.contentHarvester = contentHarvester;
            this.contentTagger = contentTagger;
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

            await this.contentTagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, data, XPath, settings);

            return true;
        }
    }
}
