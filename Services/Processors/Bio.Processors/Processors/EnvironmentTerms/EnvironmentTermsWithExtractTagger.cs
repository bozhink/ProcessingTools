namespace ProcessingTools.Bio.Processors.EnvironmentTerms
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts.EnvironmentTerms;
    using Models.EnvironmentTerms;

    using ProcessingTools.Bio.Data.Miners.Contracts;
    using ProcessingTools.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Taggers;
    using ProcessingTools.Serialization.Serializers;
    using ProcessingTools.Xml.Extensions;

    public class EnvironmentTermsWithExtractTagger : IEnvironmentTermsWithExtractTagger
    {
        private const string XPath = "./*";

        private readonly IExtractHcmrDataMiner miner;
        private readonly ISimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel> contentTagger;
        private readonly ILogger logger;

        public EnvironmentTermsWithExtractTagger(
            IExtractHcmrDataMiner miner,
            ISimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel> contentTagger,
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
            var textContent = document.XmlDocument.GetTextContent();
            var data = (await this.miner.Mine(textContent))
                .Select(t => new EnvoExtractHcmrSerializableModel
                {
                    Value = t.Content,
                    Type = string.Join("|", t.Types),
                    Identifier = string.Join("|", t.Identifiers)
                });

            var tagger = new SimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel>(new XmlSerializer<EnvoExtractHcmrSerializableModel>(), this.logger);

            await tagger.Tag(document.XmlDocument, document.NamespaceManager, data, XPath, false, true);

            return true;
        }
    }
}
