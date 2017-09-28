// <copyright file="IIterableSpecimenCodesMiningRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Bio.SpecimenCodes.Repositories
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Models.Contracts.Bio.SpecimenCodes;

    /// <summary>
    /// Iterable specimen codes mining repository.
    /// </summary>
    public interface IIterableSpecimenCodesMiningRepository : IIterableRepository<ISpecimenCode>
    {
    }
}
