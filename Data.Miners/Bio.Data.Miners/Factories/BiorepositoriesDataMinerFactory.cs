namespace ProcessingTools.Bio.Data.Miners.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProcessingTools.Bio.Biorepositories.Services.Data.Contracts;
    using ProcessingTools.Common.Constants;

    public abstract class BiorepositoriesDataMinerFactory<TModel, TServiceModel>
        where TServiceModel : class
        where TModel : class
    {
        private const int NumberOfItemsToTake = DefaultPagingConstants.MaximalItemsPerPageAllowed;

        protected abstract Func<TServiceModel, TModel> Project { get; }

        protected async Task GetMatches(IBiorepositoriesDataService<TServiceModel> service, ICollection<TModel> matches, Func<TServiceModel, bool> filter)
        {
            for (int i = 0; true; i += NumberOfItemsToTake)
            {
                var items = (await service.Get(i, NumberOfItemsToTake)).ToList();

                if (items.Count < 1)
                {
                    break;
                }

                items.Where(filter).Select(this.Project)
                    .ToList()
                    .ForEach(m => matches.Add(m));
            }
        }
    }
}
