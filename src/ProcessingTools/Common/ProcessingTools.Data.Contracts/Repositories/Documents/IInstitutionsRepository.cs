// <copyright file="IInstitutionsRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Documents.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Models.Contracts.Documents;

    /// <summary>
    /// Institutions repository.
    /// </summary>
    public interface IInstitutionsRepository : IAddressableRepository, ICrudRepository<IInstitution>
    {
    }
}
