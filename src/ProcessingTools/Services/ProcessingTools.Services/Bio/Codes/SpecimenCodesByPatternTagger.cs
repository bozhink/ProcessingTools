// <copyright file="SpecimenCodesByPatternTagger.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Codes
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants.Schema;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services;
    using ProcessingTools.Contracts.Services.Bio.Codes;
    using ProcessingTools.Contracts.Services.Bio.SpecimenCodes;
    using ProcessingTools.Contracts.Services.Content;
    using ProcessingTools.Services.Models.Bio.Codes;
    using ProcessingTools.Services.Models.Content;

    /// <summary>
    /// Specimen codes by-pattern tagger.
    /// </summary>
    public class SpecimenCodesByPatternTagger : ISpecimenCodesByPatternTagger
    {
        private readonly ITextContentHarvester contentHarvester;
        private readonly ISpecimenCodesByPatternDataMiner miner;
        private readonly ISimpleXmlSerializableObjectTagger<SpecimenCodeSerializableModel> tagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecimenCodesByPatternTagger"/> class.
        /// </summary>
        /// <param name="contentHarvester">Instance of <see cref="ITextContentHarvester"/>.</param>
        /// <param name="miner">Instance of <see cref="ISpecimenCodesByPatternDataMiner"/>.</param>
        /// <param name="tagger">Instance of <see cref="ISimpleXmlSerializableObjectTagger{SpecimenCodeSerializableModel}"/>.</param>
        public SpecimenCodesByPatternTagger(ITextContentHarvester contentHarvester, ISpecimenCodesByPatternDataMiner miner, ISimpleXmlSerializableObjectTagger<SpecimenCodeSerializableModel> tagger)
        {
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.tagger = tagger ?? throw new ArgumentNullException(nameof(tagger));
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
