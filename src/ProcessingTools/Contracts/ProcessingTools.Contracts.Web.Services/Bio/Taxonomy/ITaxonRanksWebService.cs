// <copyright file="ITaxonRanksWebService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Web.Models.Bio.Taxonomy.TaxonRanks;

namespace ProcessingTools.Contracts.Web.Services.Bio.Taxonomy
{
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
        Task<SearchResponseModel> SearchAsync(SearchRequestModel model);
    }
}
