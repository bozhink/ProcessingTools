namespace ProcessingTools.Bio.Harvesters
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;

    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Harvesters.Common.Factories;

    public class MorphologicalEpithetsHarvester : StringHarvesterFactory, IMorphologicalEpithetHarvester
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        private IMorphologicalEpithetsDataService service;

        public MorphologicalEpithetsHarvester(IMorphologicalEpithetsDataService service)
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
