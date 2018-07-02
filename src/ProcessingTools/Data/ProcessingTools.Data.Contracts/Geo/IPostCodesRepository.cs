// <copyright file="IPostCodesRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Contracts.Geo
{
    using ProcessingTools.Models.Contracts.Geo;

    /// <summary>
    /// Post codes repository.
    /// </summary>
    public interface IPostCodesRepository : IRepositoryAsync<IPostCode, IPostCodesFilter>
    {
    }
}
