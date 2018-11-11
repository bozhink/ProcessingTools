// <copyright file="IMediatypesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Files
{
    using System.Threading.Tasks;
    using ProcessingTools.Data.Models.Contracts.Files.Mediatypes;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

    /// <summary>
    /// Mediatypes data access object.
    /// </summary>
    public interface IMediatypesDataAccessObject : IDataAccessObject<IMediatypeDataModel, IMediatypeDetailsDataModel, IMediatypeInsertModel, IMediatypeUpdateModel>
    {
        /// <summary>
        /// Gets all known MIME types.
        /// </summary>
        /// <returns>Array of all known MIME types.</returns>
        Task<string[]> GetMimeTypesAsync();

        /// <summary>
        /// Gets all known MIME subtypes.
        /// </summary>
        /// <returns>Array of all known MIME subtypes.</returns>
        Task<string[]> GetMimeSubtypesAsync();

        /// <summary>
        /// Gets the first mediatype mathing the specified file extension.
        /// </summary>
        /// <param name="extension">File extension.</param>
        /// <returns>Mediatype for the specified file extension.</returns>
        Task<IMediatypeMetaModel> GetMediatypeByExtensionAsync(string extension);

        /// <summary>
        /// Gets all mediatypes mathing the specified file extension.
        /// </summary>
        /// <param name="extension">File extension.</param>
        /// <returns>Mediatype for the specified file extension.</returns>
        Task<IMediatypeMetaModel[]> GetMediatypesByExtensionAsync(string extension);
    }
}
