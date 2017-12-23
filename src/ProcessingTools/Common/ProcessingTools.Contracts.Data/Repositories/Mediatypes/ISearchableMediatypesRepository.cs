// <copyright file="ISearchableMediatypesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Mediatypes
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Models.Mediatypes;

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
        IEnumerable<IMediatypeEntity> GetByFileExtension(string fileExtension);
    }
}
