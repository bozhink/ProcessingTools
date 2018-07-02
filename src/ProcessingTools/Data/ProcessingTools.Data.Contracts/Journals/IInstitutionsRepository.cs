// <copyright file="IInstitutionsRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Journals
{
    using ProcessingTools.Models.Contracts.Journals;

    /// <summary>
    /// Institutions repository.
    /// </summary>
    public interface IInstitutionsRepository : IAddressableRepository, ICrudRepository<IInstitution>
    {
    }
}
