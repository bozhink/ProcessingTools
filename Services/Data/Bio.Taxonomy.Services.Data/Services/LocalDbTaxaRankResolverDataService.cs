namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Models;
    using ProcessingTools.Contracts.Data.Repositories;

    public class LocalDbTaxaRankResolverDataService : ILocalDbTaxaRankResolverDataService
    {
        private readonly IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider;

        public LocalDbTaxaRankResolverDataService(IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<IEnumerable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            if (scientificNames == null || scientificNames.Length < 1)
            {
                throw new ArgumentNullException(nameof(scientificNames));
            }

            var names = scientificNames.Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLower())
                .Distinct()
                .ToList();

            var result = new ConcurrentQueue<ITaxonRank>();

            await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = new ConcurrentQueue<Task>();

                foreach (var name in names)
                {
                    tasks.Enqueue(this.FindRankForSingleTaxon(repository, name, result));
                }

                await Task.WhenAll(tasks);
            });

            return new HashSet<ITaxonRank>(result);
        }

        private async Task FindRankForSingleTaxon(ITaxonRankRepository repository, string name, ConcurrentQueue<ITaxonRank> outputCollection)
        {
            var entity = await repository.FindFirst(t => t.Name.ToLower() == name);

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
