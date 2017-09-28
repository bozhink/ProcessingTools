// <copyright file="ISearchableMediatypesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Mediatypes
{
    using System.Collections.Generic;
    using ProcessingTools.Models.Contracts.Mediatypes;

    /// <summary>
    /// Searchable mediatypes repository.
    /// </summary>
    public interface ISearchableMediatypesRepository
    {
        IEnumerable<IMediatypeEntity> GetByFileExtension(string fileExtension);
    }
}
