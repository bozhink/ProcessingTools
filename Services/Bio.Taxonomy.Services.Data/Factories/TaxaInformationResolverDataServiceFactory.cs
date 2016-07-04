namespace ProcessingTools.Bio.Taxonomy.Services.Data.Factories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    public abstract class TaxaInformationResolverDataServiceFactory<TServiceModel> : ITaxaInformationResolverDataService<TServiceModel>
    {
        public Task<IQueryable<TServiceModel>> Resolve(params string[] scientificNames)
        {
            var queue = new ConcurrentQueue<TServiceModel>();
            var exceptions = new ConcurrentQueue<Exception>();

            Parallel.ForEach(
                scientificNames,
                (scientificName, state) =>
                {
                    this.Delay();
                    try
                    {
                        this.ResolveScientificName(scientificName, queue).Wait();
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

            var result = new HashSet<TServiceModel>(queue);

            return Task.FromResult(result.AsQueryable());
        }

        protected abstract void Delay();

        protected abstract Task ResolveScientificName(string scientificName, ConcurrentQueue<TServiceModel> taxaRanks);
    }
}