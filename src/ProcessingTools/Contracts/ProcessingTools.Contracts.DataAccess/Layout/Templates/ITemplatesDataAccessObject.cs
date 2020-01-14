// <copyright file="ITemplatesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Layout.Templates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Models.Layout.Templates;

    /// <summary>
    /// Templates data access object (DAO).
    /// </summary>
    public interface ITemplatesDataAccessObject
    {
        /// <summary>
        /// Get template content by ID.
        /// </summary>
        /// <param name="id">ID of the template object.</param>
        /// <returns>String content of the template.</returns>
        Task<string> GetTemplateContentByIdAsync(object id);

        /// <summary>
        /// Get templates for select.
        /// </summary>
        /// <returns>Templates data transfer objects (DTOs) with object ID and name.</returns>
        Task<IList<IIdentifiedTemplateMetaDataTransferObject>> GetTemplatesForSelectAsync();
    }
}
