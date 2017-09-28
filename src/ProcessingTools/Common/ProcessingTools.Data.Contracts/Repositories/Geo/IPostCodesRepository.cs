// <copyright file="IPostCodesRepository.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Repositories.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Post codes repository.
    /// </summary>
    public interface IPostCodesRepository : IRepositoryAsync<IPostCode, IPostCodesFilter>
    {
    }
}
