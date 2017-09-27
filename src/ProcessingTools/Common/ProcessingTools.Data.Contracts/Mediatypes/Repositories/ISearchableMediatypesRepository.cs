// <copyright file="ISearchableMediatypesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Mediatypes.Repositories
{
    using System.Collections.Generic;
    using ProcessingTools.Contracts.Data.Mediatypes.Models;

    public interface ISearchableMediatypesRepository
    {
        IEnumerable<IMediatypeEntity> GetByFileExtension(string fileExtension);
    }
}
