// <copyright file="IIterableSpecimenCodesMiningRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Bio.SpecimenCodes
{
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.Bio.SpecimenCodes;

    /// <summary>
    /// Iterable specimen codes mining repository.
    /// </summary>
    public interface IIterableSpecimenCodesMiningRepository : IIterableRepository<ISpecimenCode>
    {
    }
}
