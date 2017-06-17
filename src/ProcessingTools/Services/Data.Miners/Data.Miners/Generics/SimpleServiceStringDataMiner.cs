namespace ProcessingTools.Data.Miners.Generics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Services.Data;

    public class SimpleServiceStringDataMiner<TService, TServiceModel, TFilter> : IStringDataMiner
        where TServiceModel : class, INameableIntegerIdentifiable
        where TService : class, IMultiDataServiceAsync<TServiceModel, TFilter>
        where TFilter : class, IFilter
    {
        private const int NumberOfItemsToTake = PagingConstants.MaximalItemsPerPageAllowed;

        private TService service;

        public SimpleServiceStringDataMiner(TService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IEnumerable<string>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var matches = new List<string>();

            for (int i = 0; true; i += NumberOfItemsToTake)
            {
                var items = (await this.service.SelectAsync(null, i, NumberOfItemsToTake, nameof(INameableIntegerIdentifiable.Name)))
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
                    matches.AddRange(await content.GetMatchesAsync(matcher));
                }
            }

            var result = new HashSet<string>(matches);
            return result;
        }
    }
}
