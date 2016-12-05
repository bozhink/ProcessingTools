using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
using ProcessingTools.Data.Miners.Contracts.Models.Bio;

namespace ProcessingTools.Processors.Processors.Bio.Codes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Codes;
    using Models.Bio.Codes;
    using ProcessingTools.Contracts;
    using ProcessingTools.Data.Miners.Contracts.Miners;
    using ProcessingTools.Data.Miners.Contracts.Models;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Layout.Processors.Contracts.Taggers;
    using ProcessingTools.Layout.Processors.Models.Taggers;

    public class CollectionCodesTagger : ICollectionCodesTagger
    {
        private const string XPath = "./*";
        private readonly ITextContentHarvester contentHarvester;
        private readonly IBiorepositoriesCollectionsDataMiner miner;
        private readonly ISimpleXmlSerializableObjectTagger<BiorepositoriesCollectionCodeSerializableModel> collectionCodesTagger;
        private readonly ISimpleXmlSerializableObjectTagger<BiorepositoriesCollectionSerializableModel> collectionsTagger;

        public CollectionCodesTagger(
            ITextContentHarvester contentHarvester,
            IBiorepositoriesCollectionsDataMiner miner,
            ISimpleXmlSerializableObjectTagger<BiorepositoriesCollectionCodeSerializableModel> collectionCodesTagger,
            ISimpleXmlSerializableObjectTagger<BiorepositoriesCollectionSerializableModel> collectionsTagger)
        {
            if (contentHarvester == null)
            {
                throw new ArgumentNullException(nameof(contentHarvester));
            }

            if (miner == null)
            {
                throw new ArgumentNullException(nameof(miner));
            }

            if (collectionCodesTagger == null)
            {
                throw new ArgumentNullException(nameof(collectionCodesTagger));
            }

            if (collectionsTagger == null)
            {
                throw new ArgumentNullException(nameof(collectionsTagger));
            }

            this.contentHarvester = contentHarvester;
            this.miner = miner;
            this.collectionCodesTagger = collectionCodesTagger;
            this.collectionsTagger = collectionsTagger;
        }

        public async Task<object> Tag(IDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var textContent = await this.contentHarvester.Harvest(document.XmlDocument.DocumentElement);
            var data = (await this.miner.Mine(textContent))
                .ToArray();

            await this.TagCollectionCodes(document, data);
            await this.TagCollections(document, data);

            return true;
        }

        private async Task TagCollectionCodes(
            IDocument document,
            IEnumerable<IBiorepositoriesCollection> data)
        {
            var collectionCodes = data.Select(c => new BiorepositoriesCollectionCodeSerializableModel
            {
                Url = c.Url,
                Value = c.CollectionCode,
                XLinkTitle = c.CollectionName
            });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = true,
                MinimalTextSelect = true
            };

            await this.collectionCodesTagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, collectionCodes, XPath, settings);
        }

        private async Task TagCollections(
            IDocument document,
            IEnumerable<IBiorepositoriesCollection> data)
        {
            var collections = data.Select(c => new BiorepositoriesCollectionSerializableModel
            {
                Url = c.Url,
                Value = c.CollectionName
            });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = true,
                MinimalTextSelect = true
            };

            await this.collectionsTagger.Tag(document.XmlDocument.DocumentElement, document.NamespaceManager, collections, XPath, settings);
        }
    }
}
