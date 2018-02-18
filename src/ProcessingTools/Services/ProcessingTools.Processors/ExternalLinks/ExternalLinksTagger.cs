// <copyright file="ExternalLinksTagger.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.ExternalLinks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Harvesters.Content;
    using ProcessingTools.Data.Miners.Contracts.Miners.ExternalLinks;
    using ProcessingTools.Extensions;
    using ProcessingTools.Processors.Contracts.ExternalLinks;
    using ProcessingTools.Processors.Contracts.Layout;
    using ProcessingTools.Processors.Models.ExternalLinks;
    using ProcessingTools.Processors.Models.Layout;

    /// <summary>
    /// External links tagger.
    /// </summary>
    public class ExternalLinksTagger : IExternalLinksTagger
    {
        private const string XPath = "./*";

        private readonly IExternalLinksDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<ExternalLinkXmlModel> contentTagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalLinksTagger"/> class.
        /// </summary>
        /// <param name="miner">External links data miner.</param>
        /// <param name="contentHarvester">Content harvester.</param>
        /// <param name="contentTagger">Content tagger.</param>
        public ExternalLinksTagger(IExternalLinksDataMiner miner, ITextContentHarvester contentHarvester, ISimpleXmlSerializableObjectTagger<ExternalLinkXmlModel> contentTagger)
        {
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.contentTagger = contentTagger ?? throw new ArgumentNullException(nameof(contentTagger));
        }

        /// <inheritdoc/>
        public async Task<object> TagAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var textContent = await this.contentHarvester.HarvestAsync(context.XmlDocument.DocumentElement);
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

            await this.contentTagger.TagAsync(context.XmlDocument.DocumentElement, context.NamespaceManager, data, XPath, settings).ConfigureAwait(false);

            return true;
        }
    }
}
