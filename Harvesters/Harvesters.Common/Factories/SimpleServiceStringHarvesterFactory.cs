namespace ProcessingTools.Harvesters.Common.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using ProcessingTools.Common.Constants;
    using Services.Common.Contracts;
    using Services.Common.Models.Contracts;

    public abstract class SimpleServiceStringHarvesterFactory<TDataService, TDataServiceModel> : IStringHarvester
        where TDataServiceModel : INamedDataServiceModel
        where TDataService : ICrudDataService<TDataServiceModel>
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private TDataService service;

        public SimpleServiceStringHarvesterFactory(TDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.service = service;
        }

        public Task<IQueryable<string>> Harvest(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentNullException("content");
            }

            string text = content.ToLower();

            return Task.Run(() =>
            {
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

                    matches.AddRange(items
                        .Where(t => text.Contains(t.ToLower())));
                }

                var result = new HashSet<string>(matches);
                return result.AsQueryable();
            });
        }
    }
}