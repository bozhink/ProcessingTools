// <copyright file="IGeoNamesWebService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Contracts.Services.Geo
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Geo.GeoNames;

    /// <summary>
    /// Web service for management of geo name objects.
    /// </summary>
    public interface IGeoNamesWebService
    {
        /// <summary>
        /// Gets paged geo names.
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <param name="numberOfItemsPerPage">The number of items per page</param>
        /// <returns>Task of view model</returns>
        Task<GeoNamesIndexPageViewModel> SelectAsync(int currentPage, int numberOfItemsPerPage);

        /// <summary>
        /// Inserts new geo names.
        /// </summary>
        /// <param name="model">Geo names to be inserted</param>
        /// <returns>Task</returns>
        Task InsertAsync(GeoNamesRequestModel model);

        /// <summary>
        /// Updates single geo name object.
        /// </summary>
        /// <param name="model">Geo name object to update</param>
        /// <returns>Task</returns>
        Task UpdateAsync(GeoNameRequestModel model);

        /// <summary>
        /// Deletes single geo name object by its ID.
        /// </summary>
        /// <param name="id">ID of the geo name object to delete</param>
        /// <returns>Task</returns>
        Task DeleteAsync(int id);
    }
}
