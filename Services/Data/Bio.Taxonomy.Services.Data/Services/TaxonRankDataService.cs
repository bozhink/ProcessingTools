namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Constants;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Models;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;

    public class TaxonRankDataService : ITaxonRankDataService
    {
        private readonly ITaxonRankRepositoryProvider provider;

        private Regex matchNonWhiteListedHigherTaxon = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        public TaxonRankDataService(ITaxonRankRepositoryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        private Func<ITaxonRankEntity, IEnumerable<ITaxonRank>> MapDbModelToServiceModel => t => t.Ranks.Select(r => new TaxonRankServiceModel
        {
            ScientificName = t.Name,
            Rank = r
        });

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

            var repository = this.provider.Create();

            {
                var tasks = validTaxa.Select(this.MapServiceModelToDbModel)
                    .Select(t => repository.Add(t))
                    .ToArray();

                await Task.WhenAll(tasks);
            }

            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public virtual async Task<object> Delete(params ITaxonRank[] taxa)
        {
            var validTaxa = this.ValidateTaxa(taxa);

            var repository = this.provider.Create();

            {
                var tasks = validTaxa.Select(this.MapServiceModelToDbModel)
                    .Select(t => repository.Delete(t))
                    .ToArray();

                await Task.WhenAll(tasks);
            }

            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
        }

        public virtual async Task<IEnumerable<ITaxonRank>> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var repository = this.provider.Create();

            var query = await repository.Find(
                t => t.Name == name,
                t => t.Name,
                SortOrder.Ascending);

            var result = query.ToList()
                .SelectMany(this.MapDbModelToServiceModel)
                .ToList();

            repository.TryDispose();

            return result;
        }

        public virtual async Task<IEnumerable<ITaxonRank>> GetWhiteListedTaxa()
        {
            var repository = this.provider.Create();

            var result = (await repository.Find(t => t.IsWhiteListed == true))
                .ToList()
                .SelectMany(this.MapDbModelToServiceModel)
                .ToList();

            repository.TryDispose();

            return result;
        }

        public virtual async Task<IEnumerable<ITaxonRank>> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var repository = this.provider.Create();

            var query = await repository.Find(
                t => t.Name.ToLower().Contains(name.ToLower()),
                t => t.Name,
                SortOrder.Ascending);

            var result = query.ToList()
                .SelectMany(this.MapDbModelToServiceModel)
                .ToList();

            repository.TryDispose();

            return result;
        }

        public virtual async Task<object> Update(params ITaxonRank[] taxa)
        {
            var validTaxa = this.ValidateTaxa(taxa);

            var repository = this.provider.Create();

            {
                var tasks = validTaxa.Select(this.MapServiceModelToDbModel)
                    .Select(t => repository.Update(t))
                    .ToArray();

                await Task.WhenAll(tasks);
            }

            var result = await repository.SaveChanges();

            repository.TryDispose();

            return result;
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
