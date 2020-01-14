// <copyright file="IMediatypesApiService.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Web.Services.Files
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Web.Models.Resources.MediaTypes;

    /// <summary>
    /// Mediatypes API service.
    /// </summary>
    public interface IMediatypesApiService
    {
        /// <summary>
        /// Resolves file mediatype by its extension.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>Matched mediatypes.</returns>
        Task<IList<MediatypeResponseModel>> ResolveMediatypeAsync(string fileName);
    }
}
