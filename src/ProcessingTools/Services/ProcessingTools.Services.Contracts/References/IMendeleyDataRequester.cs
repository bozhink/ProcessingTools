﻿// <copyright file="IMendeleyDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using ProcessingTools.Clients.Models.References.Mendeley;

namespace ProcessingTools.Contracts.Services.References
{
    /// <summary>
    /// Mendeley data requester.
    /// </summary>
    public interface IMendeleyDataRequester
    {
        /// <summary>
        /// Retrieves information about and article by its DOI.
        /// </summary>
        /// <param name="doi">DOI of the article.</param>
        /// <returns>Response model.</returns>
        Task<CatalogResponseModel[]> GetDocumentInformationByDoi(string doi);
    }
}
