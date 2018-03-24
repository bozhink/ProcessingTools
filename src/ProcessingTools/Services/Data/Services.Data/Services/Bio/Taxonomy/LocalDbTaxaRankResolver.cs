namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    public class LocalDbTaxaRankResolver : ILocalDbTaxaRankResolver
    {
        private readonly IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider;

        public LocalDbTaxaRankResolver(IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public async Task<ITaxonRank[]> ResolveAsync(params string[] scientificNames)
        {
            var result = new ConcurrentQueue<ITaxonRank>();

            await this.ResolveAsync(scientificNames, result).ConfigureAwait(false);

            return result.ToArray();
        }

        private Task ResolveAsync(string[] scientificNames, ConcurrentQueue<ITaxonRank> outputCollection)
        {
            if (scientificNames == null || scientificNames.Length < 1)
            {
                return Task.CompletedTask;
            }

            var names = scientificNames.Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => s.ToLowerInvariant())
                .Distinct()
                .ToList();

            return this.repositoryProvider.ExecuteAsync(async (repository) =>
            {
                var tasks = names.Select(name => this.FindRankForSingleTaxonAsync(repository, name, outputCollection)).ToArray();

                await Task.WhenAll(tasks).ConfigureAwait(false);
            });
        }

        private async Task FindRankForSingleTaxonAsync(ITaxonRanksRepository repository, string name, ConcurrentQueue<ITaxonRank> outputCollection)
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
                var taxon = new TaxonRank
                {
                    ScientificName = entity.Name,
                    Rank = rank
                };

                outputCollection.Enqueue(taxon);
            }
        }
    }
}
