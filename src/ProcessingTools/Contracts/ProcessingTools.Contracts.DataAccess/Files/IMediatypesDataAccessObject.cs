// <copyright file="IMediatypesDataAccessObject.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.DataAccess.Files
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.DataAccess.Models.Files.Mediatypes;
    using ProcessingTools.Contracts.Models.Files.Mediatypes;

    /// <summary>
    /// Media-types data access object.
    /// </summary>
    public interface IMediatypesDataAccessObject : IDataAccessObject<IMediatypeDataTransferObject, IMediatypeDetailsDataTransferObject, IMediatypeInsertModel, IMediatypeUpdateModel>
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
        /// Gets the first media-type matching the specified file extension.
        /// </summary>
        /// <param name="extension">File extension.</param>
        /// <returns>Media-type for the specified file extension.</returns>
        Task<IMediatypeMetaModel> GetMediatypeByExtensionAsync(string extension);

        /// <summary>
        /// Gets all media-types matching the specified file extension.
        /// </summary>
        /// <param name="extension">File extension.</param>
        /// <returns>Media-type for the specified file extension.</returns>
        Task<IMediatypeMetaModel[]> GetMediatypesByExtensionAsync(string extension);
    }
}
