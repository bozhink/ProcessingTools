// <copyright file="IStatesRepository.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Contracts.Models.Geo;

    /// <summary>
    /// States repository.
    /// </summary>
    public interface IStatesRepository : IRepositoryAsync<IState, IStatesFilter>, IGeoSynonymisableRepository<IState, IStateSynonym, IStateSynonymsFilter>
    {
    }
}
