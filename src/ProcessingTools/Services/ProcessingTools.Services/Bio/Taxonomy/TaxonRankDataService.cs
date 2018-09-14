// <copyright file="TaxonRankDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Constants;
    using ProcessingTools.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank data service.
    /// </summary>
    public class TaxonRankDataService : ITaxonRankDataService
    {
        private readonly ITaxonRanksDataAccessObject taxonRanksDataAccessObject;
        private readonly Regex matchNonWhiteListedHigherTaxon = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankDataService"/> class.
        /// </summary>
        /// <param name="taxonRanksDataAccessObject">Data access object.</param>
        public TaxonRankDataService(ITaxonRanksDataAccessObject taxonRanksDataAccessObject)
        {
            this.taxonRanksDataAccessObject = taxonRanksDataAccessObject ?? throw new ArgumentNullException(nameof(taxonRanksDataAccessObject));
        }

        private Func<ITaxonRank, ITaxonRankItem> MapServiceModelToDbModel => t =>
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
        public virtual async Task<object> AddAsync(params ITaxonRank[] models)
        {
            var validTaxa = this.ValidateTaxa(models);

            var tasks = validTaxa
                .Select(this.MapServiceModelToDbModel)
                .Select(t => this.taxonRanksDataAccessObject.UpsertAsync(t))
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return await this.taxonRanksDataAccessObject.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeleteAsync(params ITaxonRank[] models)
        {
            var validTaxa = this.ValidateTaxa(models);

            var tasks = validTaxa
                .Select(t => this.taxonRanksDataAccessObject.DeleteAsync(t.ScientificName))
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return await this.taxonRanksDataAccessObject.SaveChangesAsync().ConfigureAwait(false);
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
