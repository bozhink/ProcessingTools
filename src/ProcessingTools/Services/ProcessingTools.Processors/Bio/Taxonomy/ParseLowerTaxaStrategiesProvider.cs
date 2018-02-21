// <copyright file="ParseLowerTaxaStrategiesProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProcessingTools.Contracts.Strategies.Bio.Taxonomy;

    /// <summary>
    /// Parse lower taxa strategies provider.
    /// </summary>
    public class ParseLowerTaxaStrategiesProvider : IParseLowerTaxaStrategiesProvider
    {
        private readonly IEnumerable<IParseLowerTaxaStrategy> strategies;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParseLowerTaxaStrategiesProvider"/> class.
        /// </summary>
        /// <param name="strategyFactory">Strategy factory.</param>
        public ParseLowerTaxaStrategiesProvider(Func<Type, IParseLowerTaxaStrategy> strategyFactory)
        {
            if (strategyFactory == null)
            {
                throw new ArgumentNullException(nameof(strategyFactory));
            }

            this.strategies = this.GetType().Assembly
                .GetTypes()
                .Where(
                    t =>
                        t.IsInterface &&
                        !t.IsGenericType &&
                        t.GetInterfaces().Contains(typeof(IParseLowerTaxaStrategy)))
                .Select(strategyFactory)
                .ToArray();
        }

        /// <inheritdoc/>
        public IEnumerable<IParseLowerTaxaStrategy> Strategies => this.strategies;
    }
}
