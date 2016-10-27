namespace ProcessingTools.Tagger.Factories
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Contracts;

    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Xml.Extensions;

    public abstract class StringMinerTaggerControllerFactory : ITaggerController
    {
        private readonly IStringDataMiner miner;
        private readonly IDocumentFactory documentFactory;
        private readonly IStringTagger tagger;

        public StringMinerTaggerControllerFactory(IStringDataMiner miner, IDocumentFactory documentFactory, IStringTagger tagger)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (documentFactory == null)
            {
                throw new ArgumentNullException(nameof(documentFactory));
            }

            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            this.miner = miner;
            this.documentFactory = documentFactory;
            this.tagger = tagger;
        }

        protected abstract XmlElement TagModel { get; }

        public async Task<object> Run(IDocument document, IProgramSettings settings)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var textContent = document.XmlDocument.GetTextContent();
            var data = await this.miner.Mine(textContent);

            await this.tagger.Tag(document, data, this.TagModel, XPathConstants.SelectContentNodesXPath);

            return true;
        }
    }
}
