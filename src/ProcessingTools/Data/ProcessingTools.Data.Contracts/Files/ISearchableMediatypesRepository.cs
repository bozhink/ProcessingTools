// <copyright file="ISearchableMediatypesRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Files
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

    /// <summary>
    /// Searchable mediatypes repository.
    /// </summary>
    public interface ISearchableMediatypesRepository
    {
        /// <summary>
        /// Get mediatypes by file extension.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>Mediatypes matching the specified file extension.</returns>
        IEnumerable<IMediatypeBaseModel> GetByFileExtension(string fileExtension);
    }
}
