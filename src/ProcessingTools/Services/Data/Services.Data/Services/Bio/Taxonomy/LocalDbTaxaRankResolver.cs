namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Models.Bio.Taxonomy;

    public class LocalDbTaxaRankResolver : ILocalDbTaxaRankResolver
    {
        private readonly IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider;

        public LocalDbTaxaRankResolver(IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public async Task<IEnumerable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            var result = new ConcurrentQueue<ITaxonRank>();

            await this.Resolve(scientificNames, result).ConfigureAwait(false);

            return new HashSet<ITaxonRank>(result);
        }

        private async Task Resolve(string[] scientificNames, ConcurrentQueue<ITaxonRank> outputCollection)
        {
            if (scientificNames == null || scientificNames.Length < 1)
            {
                return;
            }

            var names = scientificNames.Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLowerInvariant())
                .Distinct()
                .ToList();

            await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = new ConcurrentQueue<Task>();

                foreach (var name in names)
                {
                    tasks.Enqueue(this.FindRankForSingleTaxon(repository, name, outputCollection));
                }

                await Task.WhenAll(tasks).ConfigureAwait(false);
            })
            .ConfigureAwait(false);
        }

        private async Task FindRankForSingleTaxon(ITaxonRankRepository repository, string name, ConcurrentQueue<ITaxonRank> outputCollection)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var entity = await repository.FindFirstAsync(t => t.Name.ToLower() == name.ToLower()).ConfigureAwait(false);
            if (entity == null)
            {
                return;
            }

            foreach (var rank in entity.Ranks)
            {
                var taxon = new TaxonRankServiceModel
                {
                    ScientificName = entity.Name,
                    Rank = rank
                };

                outputCollection.Enqueue(taxon);
            }
        }
    }
}
