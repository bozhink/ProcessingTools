namespace ProcessingTools.Data.Miners.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Services.Contracts.Data.Bio.Biorepositories;

    public abstract class BiorepositoriesDataMinerBase<T, S>
        where S : class
        where T : class
    {
        protected async Task GetMatches(IBiorepositoriesDataService<S> service, ICollection<T> matches, Func<S, bool> filter, Func<S, T> projection)
        {
            int n = PaginationConstants.MaximalItemsPerPageAllowed;
            for (int i = 0; ; i += n)
            {
                var data = await service.GetAsync(i, n).ConfigureAwait(false);

                if (data.Length < 1)
                {
                    break;
                }

                data
                    .Where(filter)
                    .Select(projection)
                    .ToList()
                    .ForEach(m => matches.Add(m));
            }
        }
    }
}
