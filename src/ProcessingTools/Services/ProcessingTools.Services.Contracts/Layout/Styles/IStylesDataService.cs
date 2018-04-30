// <copyright file="IStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.Layout.Styles
{
    using System.Threading.Tasks;
    using ProcessingTools.Services.Models.Contracts.Layout.Styles;

    /// <summary>
    /// Styles data service.
    /// </summary>
    public interface IStylesDataService
    {
        /// <summary>
        /// Gets style specified by ID;
        /// </summary>
        /// <param name="id">Object ID of the style.</param>
        /// <returns>Style.</returns>
        Task<IIdentifiedStyleModel> GetStyleByIdAsync(object id);

        /// <summary>
        /// Gets styles for select.
        /// </summary>
        /// <returns>Styles data models with object ID, name and description.</returns>
        Task<IIdentifiedStyleModel[]> GetStylesForSelectAsync();
    }
}
