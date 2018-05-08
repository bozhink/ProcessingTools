// <copyright file="IStylesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Layout.Styles
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.Models.Contracts.Layout.Styles;

    /// <summary>
    /// Styles data access object.
    /// </summary>
    public interface IStylesDataAccessObject
    {
        /// <summary>
        /// Gets style by specified ID.
        /// </summary>
        /// <param name="id">Object ID of the style.</param>
        /// <returns>Style data model.</returns>
        Task<IIdentifiedStyleDataModel> GetStyleByIdAsync(object id);

        /// <summary>
        /// Gets styles for select.
        /// </summary>
        /// <returns>Styles data models with object ID, name and description.</returns>
        Task<IIdentifiedStyleDataModel[]> GetStylesForSelectAsync();
    }
}
