// <copyright file="EnvoTermsDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Bio.Environments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Contracts.Bio.Environments;
    using ProcessingTools.Services.Contracts.Bio.Environments;
    using ProcessingTools.Services.Models.Contracts.Bio.Environments;

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
        public async Task<IEnvoTerm[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            string text = context.ToUpperInvariant();

            var data = await this.service.AllAsync().ConfigureAwait(false);
            return data.Where(t => text.Contains(t.Content.ToUpperInvariant())).ToArray();
        }
    }
}
