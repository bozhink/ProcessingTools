namespace ProcessingTools.Data.Miners.Generics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Common.Contracts;

    public class SimpleServiceStringDataMiner<TDataService, TDataServiceModel> : IStringDataMiner
        where TDataServiceModel : INameableIntegerIdentifiable
        where TDataService : IMultiEntryDataService<TDataServiceModel>
    {
        private const int NumberOfItemsToTake = PagingConstants.MaximalItemsPerPageAllowed;

        private TDataService service;

        public SimpleServiceStringDataMiner(TDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
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
                var items = (await this.service.Query(i, NumberOfItemsToTake))
                    .Select(t => t.Name)
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
