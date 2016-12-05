namespace ProcessingTools.Processors.Processors.Bio.EnvironmentTerms
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio.Environments;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Models.Taggers;
    using ProcessingTools.Processors.Contracts.Bio.EnvironmentTerms;
    using ProcessingTools.Processors.Models.Bio.EnvironmentTerms;

    public class EnvironmentTermsWithExtractTagger : IEnvironmentTermsWithExtractTagger
    {
        private const string XPath = "./*";

        private readonly IExtractHcmrDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel> contentTagger;

        public EnvironmentTermsWithExtractTagger(
            IExtractHcmrDataMiner miner,
            ITextContentHarvester contentHarvester,
            ISimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel> contentTagger)
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
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = await this.contentHarvester.Harvest(document.XmlDocument.DocumentElement);
            var data = (await this.miner.Mine(textContent))
                .Select(t => new EnvoExtractHcmrSerializableModel
                {
                    Value = t.Content,
                    Type = string.Join("|", t.Types),
                    Identifier = string.Join("|", t.Identifiers)
                });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = false,
                MinimalTextSelect = true
            };

            await this.contentTagger.Tag(document.XmlDocument, document.NamespaceManager, data, XPath, settings);

            return true;
        }
    }
}
