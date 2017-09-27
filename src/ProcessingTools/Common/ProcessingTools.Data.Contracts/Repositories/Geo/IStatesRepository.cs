// <copyright file="IStatesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Repositories.Geo
{
    using ProcessingTools.Contracts.Filters.Geo;
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// States repository.
    /// </summary>
    public interface IStatesRepository : IRepositoryAsync<IState, IStatesFilter>, IGeoSynonymisableRepository<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
