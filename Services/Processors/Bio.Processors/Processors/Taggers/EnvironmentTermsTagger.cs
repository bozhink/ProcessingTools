namespace ProcessingTools.Bio.Processors.Taggers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts.Taggers;
    using Models.Taggers;

    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Xml.Extensions;

    public class EnvironmentTermsTagger : IEnvironmentTermsTagger
    {
        private const string XPath = "/*";

        private readonly IEnvoTermsDataMiner miner;
        private readonly ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger;
        private readonly ILogger logger;

        public EnvironmentTermsTagger(
            IEnvoTermsDataMiner miner,
            ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger,
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

            await this.contentTagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, data, XPath, false, true);

            return true;
        }
    }
}
