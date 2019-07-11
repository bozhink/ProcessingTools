﻿// <copyright file="BiorepositoriesInstitutionsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Services.Bio;
    using ProcessingTools.Contracts.Services.Bio.Biorepositories;
    using ProcessingTools.Contracts.Services.Models.Bio.Biorepositories;
    using ProcessingTools.Services.Abstractions;

    /// <summary>
    /// Biorepositories institutions data miner.
    /// </summary>
    public class BiorepositoriesInstitutionsDataMiner : BiorepositoriesDataMinerBase<IInstitution>, IBiorepositoriesInstitutionsDataMiner
    {
        private readonly IBiorepositoriesInstitutionsDataService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="BiorepositoriesInstitutionsDataMiner"/> class.
        /// </summary>
        /// <param name="service"><see cref="IBiorepositoriesInstitutionsDataService"/> instance.</param>
        public BiorepositoriesInstitutionsDataMiner(IBiorepositoriesInstitutionsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <inheritdoc/>
        public async Task<IInstitution[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            bool filter(IInstitution x) => Regex.IsMatch(context, Regex.Escape(x.Code) + "|" + Regex.Escape(x.Name));

            var matches = new List<IInstitution>();

            await this.GetMatches(this.service, matches, filter).ConfigureAwait(false);

            return matches.ToArray();
        }
    }
}
