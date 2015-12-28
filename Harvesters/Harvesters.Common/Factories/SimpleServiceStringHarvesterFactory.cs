namespace ProcessingTools.Harvesters.Common.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Services.Common.Contracts;
    using ProcessingTools.Services.Common.Models.Contracts;

    public abstract class SimpleServiceStringHarvesterFactory<TDataService, TDataServiceModel> : IStringHarvester
        where TDataServiceModel : INamedDataServiceModel
        where TDataService : ICrudDataService<TDataServiceModel>
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private TDataService service;
        private ICollection<string> items;

        public SimpleServiceStringHarvesterFactory(TDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service");
            }

            this.service = service;
            this.items = new HashSet<string>();
        }

        public IQueryable<string> Data
        {
            get
            {
                return this.items.AsQueryable();
            }
        }

        public void Harvest(string content)
        {
            string text = content.ToLower();

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

            this.items = new HashSet<string>(matches);
        }
    }
}
