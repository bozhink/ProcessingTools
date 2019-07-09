﻿// <copyright file="EnvironmentTermsWithExtractTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.EnvironmentTerms
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Data.Miners.Contracts.Bio.Environments;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.EnvironmentTerms;
    using ProcessingTools.Processors.Models;
    using ProcessingTools.Processors.Models.Bio.EnvironmentTerms;

    public class EnvironmentTermsWithExtractTagger : IEnvironmentTermsWithExtractTagger
    {
        private const string XPath = "./*";

        private readonly IExtractHcmrDataMiner miner;
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel> contentTagger;

        public EnvironmentTermsWithExtractTagger(
            IExtractHcmrDataMiner miner,
            ITextContentHarvester contentHarvester,
            ISimpleXmlSerializableObjectTagger<EnvoExtractHcmrSerializableModel> contentTagger)
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
