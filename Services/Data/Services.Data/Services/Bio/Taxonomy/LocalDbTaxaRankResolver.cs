namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using Models.Bio.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;

    public class LocalDbTaxaRankResolver : ILocalDbTaxaRankResolver
    {
        private readonly IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider;

        public LocalDbTaxaRankResolver(IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<IEnumerable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            var result = new ConcurrentQueue<ITaxonRank>();

            await this.Resolve(scientificNames, result);

            return new HashSet<ITaxonRank>(result);
        }

        private async Task Resolve(string[] scientificNames, ConcurrentQueue<ITaxonRank> outputCollection)
        {
            if (scientificNames == null || scientificNames.Length < 1)
            {
                return;
            }

            var names = scientificNames.Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLower())
                .Distinct()
                .ToList();

            await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = new ConcurrentQueue<Task>();

                foreach (var name in names)
                {
                    tasks.Enqueue(this.FindRankForSingleTaxon(repository, name, outputCollection));
                }

                await Task.WhenAll(tasks);
            });
        }

        private async Task FindRankForSingleTaxon(ITaxonRankRepository repository, string name, ConcurrentQueue<ITaxonRank> outputCollection)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var entity = await repository.FindFirst(t => t.Name.ToLower() == name.ToLower());
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
