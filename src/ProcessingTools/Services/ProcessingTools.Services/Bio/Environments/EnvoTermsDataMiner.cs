// <copyright file="EnvoTermsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Environments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Environments;
    using ProcessingTools.Contracts.Services.Bio.Environments;

    /// <summary>
    /// ENVO terms data miner.
    /// </summary>
    public class EnvoTermsDataMiner : IEnvoTermsDataMiner
    {
        private readonly IEnvoTermsDataService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvoTermsDataMiner"/> class.
        /// </summary>
        /// <param name="service"><see cref="IEnvoTermsDataService"/> instance.</param>
        public EnvoTermsDataMiner(IEnvoTermsDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <inheritdoc/>
        public Task<IList<IEnvoTerm>> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.MineInternalAsync(context);
        }

        private async Task<IList<IEnvoTerm>> MineInternalAsync(string context)
        {
            string text = context.ToUpperInvariant();
            var data = await this.service.AllAsync().ConfigureAwait(false);
            return data.Where(t => text.Contains(t.Content.ToUpperInvariant())).ToArray();
        }
    }
}
