namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Data.Repositories.Bio.Taxonomy;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    public class TaxonRankDataService : ITaxonRankDataService
    {
        private readonly IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider;
        private readonly Regex matchNonWhiteListedHigherTaxon = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public TaxonRankDataService(IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        private Func<ITaxonRank, ITaxonRankEntity> MapServiceModelToDbModel => t =>
        {
            var taxon = new TaxonRankEntity
            {
                Name = t.ScientificName,
                IsWhiteListed = !this.matchNonWhiteListedHigherTaxon.IsMatch(t.ScientificName)
            };

            taxon.Ranks.Add(t.Rank);

            return taxon;
        };

        public virtual Task<object> AddAsync(params ITaxonRank[] models)
        {
            var validTaxa = this.ValidateTaxa(models);

            return this.repositoryProvider.ExecuteAsync(async (repository) =>
            {
                var tasks = validTaxa.Select(this.MapServiceModelToDbModel).Select(t => repository.AddAsync(t)).ToArray();
                await Task.WhenAll(tasks).ConfigureAwait(false);
                return await repository.SaveChangesAsync().ConfigureAwait(false);
            });
        }

        public virtual Task<object> DeleteAsync(params ITaxonRank[] models)
        {
            var validTaxa = this.ValidateTaxa(models);

            return this.repositoryProvider.ExecuteAsync(async (repository) =>
            {
                var tasks = validTaxa.Select(t => repository.DeleteAsync(t.ScientificName)).ToArray();
                await Task.WhenAll(tasks).ConfigureAwait(false);
                return await repository.SaveChangesAsync().ConfigureAwait(false);
            });
        }

        private ITaxonRank[] ValidateTaxa(ITaxonRank[] taxa)
        {
            if (taxa == null || taxa.Length < 1)
            {
                throw new ArgumentNullException(nameof(taxa));
            }

            var validTaxa = taxa.Where(t => t != null).ToArray();

            if (validTaxa.Length < 1)
            {
                throw new ArgumentNullException(nameof(taxa));
            }

            return validTaxa;
        }
    }
}
