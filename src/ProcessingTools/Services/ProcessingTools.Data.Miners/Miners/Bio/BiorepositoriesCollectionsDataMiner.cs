// <copyright file="BiorepositoriesCollectionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Abstractions;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Models.Contracts.Services.Data.Bio.Biorepositories;
    using ProcessingTools.Services.Contracts.Bio.Biorepositories;

    /// <summary>
    /// Biorepositories collections data miner.
    /// </summary>
    public class BiorepositoriesCollectionsDataMiner : BiorepositoriesDataMinerBase<ICollection>, IBiorepositoriesCollectionsDataMiner
    {
        private readonly IBiorepositoriesInstitutionalCollectionsDataService institutionalCollectionsDataService;
        private readonly IBiorepositoriesPersonalCollectionsDataService personalCollectionsDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiorepositoriesCollectionsDataMiner"/> class.
        /// </summary>
        /// <param name="institutionalCollectionsDataService"><see cref="IBiorepositoriesInstitutionalCollectionsDataService"/> instance.</param>
        /// <param name="personalCollectionsDataService"><see cref="IBiorepositoriesPersonalCollectionsDataService"/> instance.</param>
        public BiorepositoriesCollectionsDataMiner(IBiorepositoriesInstitutionalCollectionsDataService institutionalCollectionsDataService, IBiorepositoriesPersonalCollectionsDataService personalCollectionsDataService)
        {
            this.institutionalCollectionsDataService = institutionalCollectionsDataService ?? throw new ArgumentNullException(nameof(institutionalCollectionsDataService));
            this.personalCollectionsDataService = personalCollectionsDataService ?? throw new ArgumentNullException(nameof(personalCollectionsDataService));
        }

        /// <inheritdoc/>
        public async Task<ICollection[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            bool filter(ICollection x) => Regex.IsMatch(context, Regex.Escape(x.Code) + "|" + Regex.Escape(x.Name));

            var matches = new HashSet<ICollection>();

            await this.GetMatches(this.institutionalCollectionsDataService, matches, filter).ConfigureAwait(false);
            await this.GetMatches(this.personalCollectionsDataService, matches, filter).ConfigureAwait(false);

            return matches.ToArray();
        }
    }
}
