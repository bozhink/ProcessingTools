﻿// <copyright file="SpecimenCodesByPatternTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Codes
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Data.Miners.Contracts.Bio.SpecimenCodes;
    using ProcessingTools.Harvesters.Contracts.Content;
    using ProcessingTools.Processors.Contracts;
    using ProcessingTools.Processors.Contracts.Bio.Codes;
    using ProcessingTools.Processors.Models;
    using ProcessingTools.Processors.Models.Bio.Codes;

    public class SpecimenCodesByPatternTagger : ISpecimenCodesByPatternTagger
    {
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISpecimenCodesByPatternDataMiner miner;
        private readonly ISimpleXmlSerializableObjectTagger<SpecimenCodeSerializableModel> tagger;

        public SpecimenCodesByPatternTagger(
            ITextContentHarvester contentHarvester,
            ISpecimenCodesByPatternDataMiner miner,
            ISimpleXmlSerializableObjectTagger<SpecimenCodeSerializableModel> tagger)
        {
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
        }

        /// <inheritdoc/>
        public async Task<object> TagAsync(IDocument context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var textContent = await this.contentHarvester.HarvestAsync(context.XmlDocument.DocumentElement).ConfigureAwait(false);
            var data = (await this.miner.MineAsync(textContent).ConfigureAwait(false))
                .ToArray();

            var specimenCodes = data.Select(s => new SpecimenCodeSerializableModel
            {
                Title = s.ContentType.IndexOf("http") == 0 ? null : s.ContentType,
                Href = s.ContentType.IndexOf("http") == 0 ? s.ContentType : null,
                Value = s.Content,
            });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = true,
                MinimalTextSelect = true,
            };

            return await this.tagger.TagAsync(
                context.XmlDocument.DocumentElement,
                context.NamespaceManager,
                specimenCodes,
                XPathStrings.RootNodesOfContext,
                settings)
                .ConfigureAwait(false);
        }
    }
}
