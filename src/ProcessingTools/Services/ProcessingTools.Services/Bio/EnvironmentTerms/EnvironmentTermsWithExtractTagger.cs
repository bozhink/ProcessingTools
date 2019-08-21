// <copyright file="EnvironmentTermsWithExtractTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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
    /// Environment terms with EXTRACT tagger.
    /// </summary>
    public class EnvironmentTermsWithExtractTagger : IEnvironmentTermsWithExtractTagger
    {
        private const string XPath = "./*";

        private readonly IExtractHcmrDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel> contentTagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentTermsWithExtractTagger"/> class.
        /// </summary>
        /// <param name="miner">Instance of <see cref="IExtractHcmrDataMiner"/>.</param>
        /// <param name="contentHarvester">Instance of <see cref="ITextContentHarvester"/>.</param>
        /// <param name="contentTagger">Instance of <see cref="ISimpleXmlSerializableObjectTagger{EnvoExtractHcmrSerializableModel}"/>.</param>
        public EnvironmentTermsWithExtractTagger(IExtractHcmrDataMiner miner, ITextContentHarvester contentHarvester, ISimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel> contentTagger)
        {
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.contentTagger = contentTagger ?? throw new ArgumentNullException(nameof(contentTagger));
        }

        /// <inheritdoc/>
        public Task<object> TagAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.TagInternalAsync(context);
        }

        private async Task<object> TagInternalAsync(IDocument context)
        {
            var textContent = await this.contentHarvester.HarvestAsync(context.XmlDocument.DocumentElement);
            var data = (await this.miner.MineAsync(textContent).ConfigureAwait(false))
                .Select(t => new EnvoExtractHcmrSerializableModel
                {
                    Value = t.Content,
                    Type = string.Join("|", t.Types),
                    Identifier = string.Join("|", t.Identifiers),
                });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = false,
                MinimalTextSelect = true,
            };

            await this.contentTagger.TagAsync(context.XmlDocument, context.NamespaceManager, data, XPath, settings).ConfigureAwait(false);

            return true;
        }
    }
}
