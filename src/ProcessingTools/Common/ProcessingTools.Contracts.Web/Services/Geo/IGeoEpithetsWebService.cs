// <copyright file="IGeoEpithetsWebService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services.Geo
{
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Geo.GeoEpithets;

    /// <summary>
    /// Web service for management of geo epithet objects.
    /// </summary>
    public interface IGeoEpithetsWebService
    {
        /// <summary>
        /// Gets paged geo epithets.
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <param name="numberOfItemsPerPage">The number of items per page</param>
        /// <returns>Task of view model</returns>
        Task<GeoEpithetsIndexPageViewModel> SelectAsync(int currentPage, int numberOfItemsPerPage);

        /// <summary>
        /// Inserts new geo epithets.
        /// </summary>
        /// <param name="model">Geo epithets to be inserted</param>
        /// <returns>Task</returns>
        Task InsertAsync(GeoEpithetsRequestModel model);

        /// <summary>
        /// Updates single geo epithet object.
        /// </summary>
        /// <param name="model">Geo epithet object to update</param>
        /// <returns>Task</returns>
        Task UpdateAsync(GeoEpithetRequestModel model);

        /// <summary>
        /// Deletes single geo epithet object by its ID.
        /// </summary>
        /// <param name="id">ID of the geo epithet object to delete</param>
        /// <returns>Task</returns>
        Task DeleteAsync(int id);
    }
}
