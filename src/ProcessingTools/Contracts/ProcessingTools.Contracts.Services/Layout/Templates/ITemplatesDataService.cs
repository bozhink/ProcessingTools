// <copyright file="ITemplatesDataService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Layout.Templates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Layout.Templates;

    /// <summary>
    /// Templates data service.
    /// </summary>
    public interface ITemplatesDataService
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
        Task<IList<IIdentifiedTemplateMetaModel>> GetTemplatesForSelectAsync();
    }
}
