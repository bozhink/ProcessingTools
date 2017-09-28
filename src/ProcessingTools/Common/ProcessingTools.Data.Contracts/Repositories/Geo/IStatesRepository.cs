// <copyright file="IStatesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// States repository.
    /// </summary>
    public interface IStatesRepository : IRepositoryAsync<IState, IStatesFilter>, IGeoSynonymisableRepository<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
