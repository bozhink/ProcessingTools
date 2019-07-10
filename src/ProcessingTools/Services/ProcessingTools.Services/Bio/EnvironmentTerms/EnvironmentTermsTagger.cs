// <copyright file="EnvironmentTermsTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services;
using ProcessingTools.Contracts.Services.Bio.Environments;
using ProcessingTools.Contracts.Services.Bio.EnvironmentTerms;
using ProcessingTools.Contracts.Services.Content;
using ProcessingTools.Services.Models.Content;

namespace ProcessingTools.Services.Bio.EnvironmentTerms
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Services.Models;
    using ProcessingTools.Services.Models.Bio.EnvironmentTerms;

    public class EnvironmentTermsTagger : IEnvironmentTermsTagger
    {
        private const string XPath = "./*";

        private readonly IEnvoTermsDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger;

        public EnvironmentTermsTagger(
            IEnvoTermsDataMiner miner,
            ITextContentHarvester contentHarvester,
            ISimpleXmlSerializableObjectTagger<EnvoTermSerializableModel> contentTagger)
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
            var data = (await this.miner.MineAsync(textContent))
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
