// <copyright file="IGeoRepository.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Geo
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IGeoRepository<T> : IEfRepository<GeoDbContext, T>
        where T : class
    {
    }
}
