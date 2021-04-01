// <copyright file="IMediatypesDataService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Files
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;

    /// <summary>
    /// Mediatypes data service.
    /// </summary>
    public interface IMediatypesDataService : IDataService<IMediatypeModel, IMediatypeDetailsModel, IMediatypeInsertModel, IMediatypeUpdateModel>
    {
        /// <summary>
        /// Gets all known MIME types.
        /// </summary>
        /// <returns>Array of all known MIME types.</returns>
        Task<IList<string>> GetMimeTypesAsync();

        /// <summary>
        /// Gets all known MIME subtypes.
        /// </summary>
        /// <returns>Array of all known MIME subtypes.</returns>
        Task<IList<string>> GetMimeSubtypesAsync();

        /// <summary>
        /// Gets the first mediatype matching the specified file extension.
        /// </summary>
        /// <param name="extension">File extension.</param>
        /// <returns>Mediatype for the specified file extension.</returns>
        Task<IMediatypeMetaModel> GetMediatypeByExtensionAsync(string extension);

        /// <summary>
        /// Gets all mediatypes matching the specified file extension.
        /// </summary>
        /// <param name="extension">File extension.</param>
        /// <returns>Mediatype for the specified file extension.</returns>
        Task<IList<IMediatypeMetaModel>> GetMediatypesByExtensionAsync(string extension);
    }
}
