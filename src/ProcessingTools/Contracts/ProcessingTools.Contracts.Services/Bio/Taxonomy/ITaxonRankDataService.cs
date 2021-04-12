// <copyright file="ITaxonRankDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;

    /// <summary>
    /// Taxon rank data service.
    /// </summary>
    public interface ITaxonRankDataService
    {
        /// <summary>
        /// Insert taxon ranks.
        /// </summary>
        /// <param name="taxonRanks">Taxon ranks to be added.</param>
        /// <returns>Task.</returns>
        Task<object> InsertAsync(IEnumerable<ITaxonRank> taxonRanks);

        /// <summary>
        /// Delete taxon ranks.
        /// </summary>
        /// <param name="taxonRanks">Taxon ranks to be deleted.</param>
        /// <returns>Task.</returns>
        Task<object> DeleteAsync(IEnumerable<ITaxonRank> taxonRanks);

        /// <summary>
        /// Do search with a specified filter.
        /// </summary>
        /// <param name="filter">Filter string for search.</param>
        /// <returns>Collection of found taxon ranks.</returns>
        Task<IList<ITaxonRank>> SearchAsync(string filter);
    }
}
