// <copyright file="IMendeleyDataRequester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Contracts.References
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.ServiceClient.Mendeley.Models;

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
