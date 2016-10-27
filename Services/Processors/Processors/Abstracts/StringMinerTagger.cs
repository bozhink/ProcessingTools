namespace ProcessingTools.Processors.Abstracts
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using ProcessingTools.Constants.Schema;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Common.Contracts;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Xml.Extensions;

    public abstract class StringMinerTagger<TMiner> : IDocumentTagger
         where TMiner : IStringDataMiner
    {
        private readonly TMiner miner;
        private readonly IStringTagger tagger;

        public StringMinerTagger(TMiner miner, IStringTagger tagger)
        {
            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (tagger == null)
            {
                throw new ArgumentNullException(nameof(tagger));
            }

            this.miner = miner;
            this.tagger = tagger;
        }

        protected abstract Func<XmlDocument, XmlElement> BuildTagModel { get; }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var tagModel = this.BuildTagModel(document.XmlDocument);

            var textContent = document.XmlDocument.GetTextContent();
            var data = await this.miner.Mine(textContent);

            await this.tagger.Tag(document, data, tagModel, XPathConstants.SelectContentNodesXPath);

            return true;
        }
    }
}
