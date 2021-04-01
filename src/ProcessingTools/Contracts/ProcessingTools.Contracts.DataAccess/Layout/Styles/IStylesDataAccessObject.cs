// <copyright file="IStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Styles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Styles;

    /// <summary>
    /// Styles data access object (DAO).
    /// </summary>
    public interface IStylesDataAccessObject
    {
        /// <summary>
        /// Gets style by specified ID.
        /// </summary>
        /// <param name="id">Object ID of the style.</param>
        /// <returns>Style data model.</returns>
        Task<IIdentifiedStyleDataTransferObject> GetStyleByIdAsync(object id);

        /// <summary>
        /// Gets styles for select.
        /// </summary>
        /// <returns>Styles data transfer objects (DTOs) with object ID, name and description.</returns>
        Task<IList<IIdentifiedStyleDataTransferObject>> GetStylesForSelectAsync();
    }
}
