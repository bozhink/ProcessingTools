// <copyright file="InstitutionalCodesTagger.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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
    /// Institutional codes tagger.
    /// </summary>
    public class InstitutionalCodesTagger : IInstitutionalCodesTagger
    {
        private const string XPath = "./*";
        private readonly ITextContentHarvester contentHarvester;
        private readonly IBiorepositoriesInstitutionsDataMiner miner;
        private readonly ISimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionalCodeSerializableModel> institutionalCodesTagger;
        private readonly ISimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionSerializableModel> institutionsTagger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstitutionalCodesTagger"/> class.
        /// </summary>
        /// <param name="contentHarvester">Instance of <see cref="ITextContentHarvester"/>.</param>
        /// <param name="miner">Instance of <see cref="IBiorepositoriesInstitutionsDataMiner"/>.</param>
        /// <param name="institutionalCodesTagger">Instance of <see cref="ISimpleXmlSerializableObjectTagger{BiorepositoriesInstitutionalCodeSerializableModel}"/>.</param>
        /// <param name="institutionsTagger">Instance of <see cref="ISimpleXmlSerializableObjectTagger{BiorepositoriesInstitutionSerializableModel}"/>.</param>
        public InstitutionalCodesTagger(ITextContentHarvester contentHarvester, IBiorepositoriesInstitutionsDataMiner miner, ISimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionalCodeSerializableModel> institutionalCodesTagger, ISimpleXmlSerializableObjectTagger<BiorepositoriesInstitutionSerializableModel> institutionsTagger)
        {
            this.contentHarvester = contentHarvester ?? throw new ArgumentNullException(nameof(contentHarvester));
            this.miner = miner ?? throw new ArgumentNullException(nameof(miner));
            this.institutionalCodesTagger = institutionalCodesTagger ?? throw new ArgumentNullException(nameof(institutionalCodesTagger));
            this.institutionsTagger = institutionsTagger ?? throw new ArgumentNullException(nameof(institutionsTagger));
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
            var textContent = await this.contentHarvester.HarvestAsync(context.XmlDocument.DocumentElement).ConfigureAwait(false);
            var data = (await this.miner.MineAsync(textContent).ConfigureAwait(false))
                .ToArray();

            await this.TagInstitutionalCodes(context, data).ConfigureAwait(false);
            await this.TagInstitutions(context, data).ConfigureAwait(false);

            return true;
        }

        private async Task TagInstitutionalCodes(IDocument document, IEnumerable<IInstitutionMetaModel> data)
        {
            var institutionalCodes = data.Select(i => new BiorepositoriesInstitutionalCodeSerializableModel
            {
                Description = i.Name,
                Url = i.Url,
                Value = i.Code,
            });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = true,
                MinimalTextSelect = true,
            };

            await this.institutionalCodesTagger.TagAsync(document.XmlDocument.DocumentElement, document.NamespaceManager, institutionalCodes, XPath, settings).ConfigureAwait(false);
        }

        private async Task TagInstitutions(IDocument document, IEnumerable<IInstitutionMetaModel> data)
        {
            var institutions = data.Select(i => new BiorepositoriesInstitutionSerializableModel
            {
                Url = i.Url,
                Value = i.Name,
            });

            var settings = new ContentTaggerSettings
            {
                CaseSensitive = true,
                MinimalTextSelect = true,
            };

            await this.institutionsTagger.TagAsync(document.XmlDocument.DocumentElement, document.NamespaceManager, institutions, XPath, settings).ConfigureAwait(false);
        }
    }
}
