// <copyright file="TaxonRankDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Taxon rank data service.
    /// </summary>
    public class TaxonRankDataService : ITaxonRankDataService
    {
        private readonly IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider;
        private readonly Regex matchNonWhiteListedHigherTaxon = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankDataService"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
