namespace ProcessingTools.Bio.Taxonomy.Services.Data.Factories
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    public abstract class TaxaDataServiceFactory<TServiceModel> : ITaxaDataService<TServiceModel>
    {
        public Task<IQueryable<TServiceModel>> Resolve(params string[] scientificNames)
        {
            return Task.Run(() =>
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

                return result.AsQueryable();
            });
        }

        protected abstract void Delay();

        protected abstract Task ResolveScientificName(string scientificName, ConcurrentQueue<TServiceModel> taxaRanks);
    }
}