// <copyright file="ICountiesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// Counties repository.
    /// </summary>
    public interface ICountiesRepository : IRepositoryAsync<ICounty, ICountiesFilter>, IGeoSynonymisableRepository<ICounty, ICountySynonym, ICountySynonymsFilter>
    {
    }
}
