// <copyright file="ITaxonRanksWebService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Api.Models;

    /// <summary>
    /// Taxon ranks web service.
    /// </summary>
    public interface ITaxonRanksWebService
    {
        /// <summary>
        /// Inserts taxon-rank items to the data store.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertAsync(TaxonRanksRequestModel model);

        /// <summary>
        /// Searches taxon-rank items by specified search string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Response model.</returns>
        Task<TaxonRankSearchResponseModel> SearchAsync(TaxonRankSearchRequestModel model);
    }
}
