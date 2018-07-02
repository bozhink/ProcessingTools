// <copyright file="SimpleServiceStringDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Miners.Contracts;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts;

    /// <summary>
    /// Simple service string data miner.
    /// </summary>
    /// <typeparam name="TService">Type of the service.</typeparam>
    /// <typeparam name="TServiceModel">Type of the service model.</typeparam>
    /// <typeparam name="TFilter">Type of the filter.</typeparam>
    public class SimpleServiceStringDataMiner<TService, TServiceModel, TFilter> : IStringDataMiner
        where TServiceModel : class, INameableIntegerIdentifiable
        where TService : class, IMultiDataServiceAsync<TServiceModel, TFilter>
        where TFilter : class, IFilter
    {
        private const int NumberOfItemsToTake = PaginationConstants.MaximalItemsPerPageAllowed;

        private readonly TService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleServiceStringDataMiner{TService, TServiceModel, TFilter}"/> class.
        /// </summary>
        /// <param name="service">Service instance.</param>
        public SimpleServiceStringDataMiner(TService service)
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

            var matches = new List<string>();

            for (int i = 0; ; i += NumberOfItemsToTake)
            {
                var items = (await this.service.SelectAsync(null, i, NumberOfItemsToTake, nameof(INameableIntegerIdentifiable.Name), SortOrder.Ascending).ConfigureAwait(false))
                    .Select(t => t.Name)
                    .Distinct()
                    .ToList();

                if (items.Count < 1)
                {
                    break;
                }

                var matchers = items.Select(t => new Regex(@"(?i)" + t));

                foreach (var matcher in matchers)
                {
                    matches.AddRange(context.GetMatches(matcher));
                }
            }

            var result = new HashSet<string>(matches);
            return result.ToArray();
        }
    }
}
