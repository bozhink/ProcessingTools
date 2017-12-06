// <copyright file="IMediatypesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Mediatypes
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Mediatypes;

    /// <summary>
    /// Mediatypes repository.
    /// </summary>
    public interface IMediatypesRepository
    {
        Task<object> Add(IMediatypeEntity mediatype);

        Task<object> Remove(string fileExtension);

        Task<object> UpdateDescription(string fileExtension, string description);

        Task<long> SaveChanges();
    }
}
