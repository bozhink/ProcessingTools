// <copyright file="CollectionCodesTagger.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Codes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Bio.Biorepositories;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio;
    using ProcessingTools.Contracts.Services.Bio.Codes;
    using ProcessingTools.Contracts.Services.Content;
    using ProcessingTools.Services.Models.Bio.Codes;
    using ProcessingTools.Services.Models.Content;

    /// <summary>
    /// Collection codes tagger.
    /// </summary>
    public class CollectionCodesTagger : ICollectionCodesTagger
    {
        private const string XPath = "./*";
        private readonly ITextContentHarvester contentHarvester;
        private readonly IBiorepositoriesCollectionsDataMiner miner;
        private readonly ISimpleXmlSerializableObjectTagger<BiorepositoriesCollectionCodeSerializableModel> collectionCodesTagger;
        private readonly ISimpleXmlSerializableObjectTagger<BiorepositoriesCollectionSerializableModel> collectionsTagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionCodesTagger"/> class.
        /// </summary>
        /// <param name="contentHarvester">Instance of <see cref="ITextContentHarvester"/>.</param>
        /// <param name="miner">Instance of <see cref="IBiorepositoriesCollectionsDataMiner"/>.</param>
        /// <param name="collectionCodesTagger">Instance of <see cref="ISimpleXmlSerializableObjectTagger{BiorepositoriesCollectionCodeSerializableModel}"/>.</param>
        /// <param name="collectionsTagger">Instance of <see cref="ISimpleXmlSerializableObjectTagger{BiorepositoriesCollectionSerializableModel}"/>.</param>
        public CollectionCodesTagger(ITextContentHarvester contentHarvester, IBiorepositoriesCollectionsDataMiner miner, ISimpleXmlSerializableObjectTagger<BiorepositoriesCollectionCodeSerializableModel> collectionCodesTagger, ISimpleXmlSerializableObjectTagger<BiorepositoriesCollectionSerializableModel> collectionsTagger)
        {
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.collectionCodesTagger = collectionCodesTagger ?? throw new ArgumentNullException(nameof(collectionCodesTagger));
            this.collectionsTagger = collectionsTagger ?? throw new ArgumentNullException(nameof(collectionsTagger));
        }

        /// <inheritdoc/>
        public Task<object> TagAsync(IDocument context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.TagInternalAsync(context);
        }

        private async Task<object> TagInternalAsync(IDocument context)
        {
            var textContent = await this.contentHarvester.HarvestAsync(context.XmlDocument.DocumentElement).ConfigureAwait(false);
            var data = await this.miner.MineAsync(textContent).ConfigureAwait(false);

            await this.TagCollectionCodes(context, data).ConfigureAwait(false);
            await this.TagCollections(context, data).ConfigureAwait(false);

            return true;
        }

        private async Task TagCollectionCodes(IDocument document, IEnumerable<ICollectionMetaModel> data)
        {
            var collectionCodes = data.Select(c => new BiorepositoriesCollectionCodeSerializableModel
            {
                Url = c.Url,
                Value = c.Code,
                XLinkTitle = c.Name,
            });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = true,
                MinimalTextSelect = true,
            };

            await this.collectionCodesTagger.TagAsync(document.XmlDocument.DocumentElement, document.NamespaceManager, collectionCodes, XPath, settings).ConfigureAwait(false);
        }

        private async Task TagCollections(IDocument document, IEnumerable<ICollectionMetaModel> data)
        {
            var collections = data.Select(c => new BiorepositoriesCollectionSerializableModel
            {
                Url = c.Url,
                Value = c.Name,
            });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = true,
                MinimalTextSelect = true,
            };

            await this.collectionsTagger.TagAsync(document.XmlDocument.DocumentElement, document.NamespaceManager, collections, XPath, settings).ConfigureAwait(false);
        }
    }
}
