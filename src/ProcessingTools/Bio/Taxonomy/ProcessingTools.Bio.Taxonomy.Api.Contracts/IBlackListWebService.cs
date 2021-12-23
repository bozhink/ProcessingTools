// <copyright file="IBlackListWebService.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.Api.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Api.Models;

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
        Task<object> InsertAsync(BlackListItemsRequestModel model);

        /// <summary>
        /// Searches blacklist items by specified search string.
        /// </summary>
        /// <param name="model">Request model.</param>
        /// <returns>Response model.</returns>
        Task<BlackListSearchResponseModel> SearchAsync(BlackListSearchRequestModel model);
    }
}
