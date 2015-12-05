namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    public abstract class TaxaDataServiceFactory<T> : ITaxaDataService<T>
    {
        public IQueryable<T> Resolve(params string[] scientificNames)
        {
            var queue = new ConcurrentQueue<T>();
            var exceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(
                scientificNames,
                (scientificName, state) =>
                {
                    this.Delay();
                    try
                    {
                        this.ResolveScientificName(scientificName, queue);
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

            var result = new HashSet<T>(queue);

            return result.AsQueryable();
        }

        protected abstract void Delay();

        protected abstract void ResolveScientificName(string scientificName, ConcurrentQueue<T> taxaRanks);
    }
}