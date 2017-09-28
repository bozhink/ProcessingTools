// <copyright file="IIterableSpecimenCodesMiningRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Bio.SpecimenCodes
{
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Models.Contracts.Bio.SpecimenCodes;

    /// <summary>
    /// Iterable specimen codes mining repository.
    /// </summary>
    public interface IIterableSpecimenCodesMiningRepository : IIterableRepository<ISpecimenCode>
    {
    }
}
