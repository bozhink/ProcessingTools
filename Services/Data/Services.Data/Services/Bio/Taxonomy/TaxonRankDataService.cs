namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Models;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Models.Bio.Taxonomy;

    public class TaxonRankDataService : ITaxonRankDataService
    {
        private readonly IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider;

        private Regex matchNonWhiteListedHigherTaxon = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public TaxonRankDataService(IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
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

        public virtual async Task<object> Add(params ITaxonRank[] taxa)
        {
            var validTaxa = this.ValidateTaxa(taxa);

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = validTaxa.Select(this.MapServiceModelToDbModel)
                    .Select(t => repository.Add(t))
                    .ToArray();

                await Task.WhenAll(tasks);

                var result = await repository.SaveChangesAsync();
                return result;
            });
        }

        public virtual async Task<object> Delete(params ITaxonRank[] taxa)
        {
            var validTaxa = this.ValidateTaxa(taxa);

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var tasks = validTaxa.Select(t => repository.Delete(t.ScientificName)).ToArray();

                await Task.WhenAll(tasks);

                var result = await repository.SaveChangesAsync();
                return result;
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
