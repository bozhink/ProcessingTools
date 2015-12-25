namespace ProcessingTools.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;

    using ProcessingTools.Common.Constants;
    using ProcessingTools.Harvesters.Common.Factories;
    using ProcessingTools.Services.Data.Contracts;

    public class ProductsHarvester : StringHarvesterFactory, IProductsHarvester
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private IProductsDataService service;

        public ProductsHarvester(IProductsDataService service)
            : base()
        {
            this.service = service;
        }

        public override void Harvest(string content)
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

            this.Items = new HashSet<string>(matches);
        }
    }
}