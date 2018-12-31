// <copyright file="TaxonRankDataService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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
        private readonly ITaxonRanksDataAccessObject dataAccessObject;
        private readonly Regex matchNonWhiteListedHigherTaxon = new Regex(TaxaRegexPatterns.HigherTaxaMatchPattern);

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankDataService"/> class.
        /// </summary>
        /// <param name="dataAccessObject">Data access object.</param>
        public TaxonRankDataService(ITaxonRanksDataAccessObject dataAccessObject)
        {
            this.dataAccessObject = dataAccessObject ?? throw new ArgumentNullException(nameof(dataAccessObject));
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
        public virtual async Task<object> InsertAsync(IEnumerable<ITaxonRank> taxonRanks)
        {
            var validTaxa = this.ValidateTaxa(taxonRanks);

            var tasks = validTaxa
                .Select(this.MapServiceModelToDbModel)
                .Select(t => this.dataAccessObject.UpsertAsync(t))
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<object> DeleteAsync(IEnumerable<ITaxonRank> taxonRanks)
        {
            var validTaxa = this.ValidateTaxa(taxonRanks);

            var tasks = validTaxa
                .Select(t => this.dataAccessObject.DeleteAsync(t.ScientificName))
                .ToArray();

            await Task.WhenAll(tasks).ConfigureAwait(false);
            return await this.dataAccessObject.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IList<ITaxonRank>> SearchAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return Array.Empty<ITaxonRank>();
            }

            var data = await this.dataAccessObject.FindAsync(filter).ConfigureAwait(false);

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

        private IList<ITaxonRank> ValidateTaxa(IEnumerable<ITaxonRank> taxonRanks)
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
