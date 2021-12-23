// <copyright file="ExternalLinksTagger.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.ExternalLinks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Content;
    using ProcessingTools.Contracts.Services.ExternalLinks;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Models.Content;
    using ProcessingTools.Services.Models.ExternalLinks;

    /// <summary>
    /// External links tagger.
    /// </summary>
    public class ExternalLinksTagger : IExternalLinksTagger
    {
        private const string XPath = "./*";

        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<ExternalLinkXmlModel> contentTagger;
        private readonly IExternalLinksDataMiner miner;

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
            var data = (await this.miner.MineAsync(textContent).ConfigureAwait(false))
                .Select(i => new ExternalLinkXmlModel
                {
                    Href = i.Href,
                    ExternalLinkType = i.Type.GetName(),
                    Value = i.Content,
                });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = false,
                MinimalTextSelect = true,
            };

            await this.contentTagger.TagAsync(context.XmlDocument.DocumentElement, context.NamespaceManager, data, XPath, settings).ConfigureAwait(false);

            return true;
        }
    }
}
