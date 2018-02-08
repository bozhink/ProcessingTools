// <copyright file="ISearchableMediatypesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Mediatypes
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Mediatypes;

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
