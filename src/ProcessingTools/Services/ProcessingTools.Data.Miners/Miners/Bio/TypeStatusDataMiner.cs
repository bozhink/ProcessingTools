// <copyright file="TypeStatusDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Contracts.Bio;

    /// <summary>
    /// Type status data miner.
    /// </summary>
    public class TypeStatusDataMiner : ITypeStatusDataMiner
    {
        private readonly ITypeStatusDataService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeStatusDataMiner"/> class.
        /// </summary>
        /// <param name="service"><see cref="ITypeStatusDataService"/> instance.</param>
        public TypeStatusDataMiner(ITypeStatusDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <inheritdoc/>
        public async Task<string[]> MineAsync(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                throw new ArgumentNullException(nameof(context));
            }

            var data = (await this.service.SelectAsync(null).ConfigureAwait(false))
                .Select(t => t.Name)
                .Distinct()
                .ToList();
            var matchers = data.Select(t => new Regex(@"(?i)\b" + t + @"s?\b"));

            var matches = new List<string>();
            foreach (var matcher in matchers)
            {
                matches.AddRange(await context.GetMatchesAsync(matcher).ConfigureAwait(false));
            }

            return matches.Distinct().ToArray();
        }
    }
}
