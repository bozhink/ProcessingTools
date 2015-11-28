namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Infrastructure.Concurrency;

    public abstract class TaxaRankDataService : ITaxaRankDataService
    {
        public IQueryable<ITaxonRank> Resolve(params string[] scientificNames)
        {
            var taxaRanks = new ConcurrentQueue<ITaxonRank>();
            var exceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(
                scientificNames,
                (scientificName, state) =>
                {
                    Delayer.Delay();
                    try
                    {
                        this.ResolveRank(scientificName, taxaRanks);
                    }
                    catch (Exception e)
                    {
                        exceptions.Enqueue(e);
                        state.Break();
                    }
                });

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions.ToList());
            }

            var result = new HashSet<ITaxonRank>(taxaRanks);

            return result.AsQueryable();
        }

        protected abstract void ResolveRank(string scientificName, ConcurrentQueue<ITaxonRank> taxaRanks);
    }
}