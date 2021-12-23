// <copyright file="EnvironmentTermsTagger.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.EnvironmentTerms
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio.Environments;
    using ProcessingTools.Contracts.Services.Bio.EnvironmentTerms;
    using ProcessingTools.Contracts.Services.Content;
    using ProcessingTools.Services.Models.Bio.Environments;
    using ProcessingTools.Services.Models.Content;

    /// <summary>
    /// Environment terms tagger.
    /// </summary>
    public class EnvironmentTermsTagger : IEnvironmentTermsTagger
    {
        private const string XPath = "./*";

        private readonly IEnvoTermsDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentTermsTagger"/> class.
        /// </summary>
        /// <param name="miner">Instance of <see cref="IEnvoTermsDataMiner"/>.</param>
        /// <param name="contentHarvester">Instance of <see cref="ITextContentHarvester"/>.</param>
        /// <param name="contentTagger">Instance of <see cref="ISimpleXmlSerializableObjectTagger{EnvoTermSerializableModel}"/>.</param>
        public EnvironmentTermsTagger(IEnvoTermsDataMiner miner, ITextContentHarvester contentHarvester, ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger)
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
                .Select(t => new EnvoTermResponseModel
                {
                    EntityId = t.EntityId,
                    EnvoId = t.EnvoId,
                    Content = t.Content,
                })
                .Select(t => new EnvoTermSerializableModel
                {
                    Value = t.Content,
                    EnvoId = t.EnvoId,
                    Id = t.EntityId,
                    VerbatimTerm = t.Content,
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
