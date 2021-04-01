// <copyright file="IBlackListWebService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services.Bio.Taxonomy
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Bio.Taxonomy.BlackList;

    /// <summary>
    /// Blacklist web service.
    /// </summary>
    public interface IBlackListWebService
    {
        /// <summary>
        /// Inserts blacklist items to the data store.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Task of result.</returns>
        Task<object> InsertAsync(ItemsRequestModel model);

        /// <summary>
        /// Searches blacklist items by specified search string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Response model.</returns>
        Task<SearchResponseModel> SearchAsync(SearchRequestModel model);
    }
}
