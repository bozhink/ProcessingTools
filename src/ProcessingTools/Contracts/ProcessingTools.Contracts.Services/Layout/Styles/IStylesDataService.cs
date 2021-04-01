// <copyright file="IStylesDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Layout.Styles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Layout.Styles;

    /// <summary>
    /// Styles data service.
    /// </summary>
    public interface IStylesDataService
    {
        /// <summary>
        /// Gets style specified by ID.
        /// </summary>
        /// <param name="id">Object ID of the style.</param>
        /// <returns>Style.</returns>
        Task<IIdentifiedStyleModel> GetStyleByIdAsync(object id);

        /// <summary>
        /// Gets styles for select.
        /// </summary>
        /// <returns>Styles data models with object ID, name and description.</returns>
        Task<IList<IIdentifiedStyleModel>> GetStylesForSelectAsync();
    }
}
