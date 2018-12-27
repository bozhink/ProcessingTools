// <copyright file="TaxonRankDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
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
        public virtual async Task<object> AddAsync(IEnumerable<ITaxonRank> taxonRanks)
        {
            var validTaxa = this.ValidateTaxa(taxonRanks);

            var tasks = validTaxa
                .Select(this.MapServiceModelToDbModel)
                .Select(t => this.taxonRanksDataAccessObject.UpsertAsync(t))
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return await this.taxonRanksDataAccessObject.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeleteAsync(IEnumerable<ITaxonRank> taxonRanks)
        {
            var validTaxa = this.ValidateTaxa(taxonRanks);

            var tasks = validTaxa
                .Select(t => this.taxonRanksDataAccessObject.DeleteAsync(t.ScientificName))
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return await this.taxonRanksDataAccessObject.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ITaxonRank[]> SearchAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return Array.Empty<ITaxonRank>();
            }

            var data = await this.taxonRanksDataAccessObject.FindAsync(filter).ConfigureAwait(false);

            var result = data.SelectMany(
                t => t.Ranks.Select(rank => new TaxonRank
                {
                    ScientificName = t.Name,
                    Rank = rank
                }))
                .Take(PaginationConstants.DefaultLargeNumberOfItemsPerPage)
                .ToArray();

            return result;
        }

        private ITaxonRank[] ValidateTaxa(IEnumerable<ITaxonRank> taxonRanks)
        {
            if (taxonRanks == null || !taxonRanks.Any())
            {
                throw new ArgumentNullException(nameof(taxonRanks));
            }

            var validTaxa = taxonRanks.Where(t => t != null).ToArray();

            if (validTaxa.Length < 1)
            {
                throw new ArgumentNullException(nameof(taxonRanks));
            }

            return validTaxa;
        }
    }
}
