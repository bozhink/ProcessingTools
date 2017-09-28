// <copyright file="ICountiesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Counties repository.
    /// </summary>
    public interface ICountiesRepository : IRepositoryAsync<ICounty, ICountiesFilter>, IGeoSynonymisableRepository<ICounty, ICountySynonym, ICountySynonymsFilter>
    {
    }
}
