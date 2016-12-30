namespace ProcessingTools.Services.Data.Abstractions.Bio.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;

    public abstract class AbstractTaxaInformationResolver<T> : ITaxaInformationResolver<T>
    {
        public async Task<IEnumerable<T>> Resolve(params string[] scientificNames)
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
                        var data = this.ResolveScientificName(scientificName).Result;
                        foreach (var item in data)
                        {
                            queue.Enqueue(item);
                        }
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

            return await Task.FromResult(result);
        }

        protected abstract void Delay();

        protected abstract Task<IEnumerable<T>> ResolveScientificName(string scientificName);
    }
}
