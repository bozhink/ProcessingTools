namespace ProcessingTools.Processors.Providers.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProcessingTools.Contracts.Strategies.Bio.Taxonomy;

    public class ParseLowerTaxaStrategiesProvider : IParseLowerTaxaStrategiesProvider
    {
        private readonly IEnumerable<IParseLowerTaxaStrategy> strategies;

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

        public IEnumerable<IParseLowerTaxaStrategy> Strategies => this.strategies;
    }
}
