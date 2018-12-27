// <copyright file="ITaxonRankDataService.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank data service.
    /// </summary>
    public interface ITaxonRankDataService
    {
        /// <summary>
        /// Insert taxon ranks.
        /// </summary>
        /// <param name="taxonRanks">Taxon ranks to be added.</param>
        /// <returns>Task</returns>
        Task<object> InsertAsync(IEnumerable<ITaxonRank> taxonRanks);

        /// <summary>
        /// Delete taxon ranks.
        /// </summary>
        /// <param name="taxonRanks">Taxon ranks to be deleted.</param>
        /// <returns>Task</returns>
        Task<object> DeleteAsync(IEnumerable<ITaxonRank> taxonRanks);

        /// <summary>
        /// Do search with a specified filter.
        /// </summary>
        /// <param name="filter">Filter string for search.</param>
        /// <returns>Array of found taxon ranks.</returns>
        Task<ITaxonRank[]> SearchAsync(string filter);
    }
}
