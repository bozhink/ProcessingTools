namespace ProcessingTools.Data.Miners.Common.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Infrastructure.Extensions;
    using ProcessingTools.Services.Common.Contracts;
    using ProcessingTools.Services.Common.Models.Contracts;

    public abstract class SimpleServiceStringDataMinerFactory<TDataService, TDataServiceModel> : IStringDataMiner
        where TDataServiceModel : INamedDataServiceModel
        where TDataService : ICrudDataService<TDataServiceModel>
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private TDataService service;

        public SimpleServiceStringDataMinerFactory(TDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public async Task<IQueryable<string>> Mine(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException(nameof(content));
            }

            var matches = new List<string>();

            for (int i = 0; true; i += NumberOfItemsToTake)
            {
                var items = this.service.Get(i, NumberOfItemsToTake)
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
            return result.AsQueryable();
        }
    }
}