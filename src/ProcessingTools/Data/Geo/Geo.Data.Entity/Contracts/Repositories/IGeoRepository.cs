﻿namespace ProcessingTools.Geo.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Contracts.Repositories;

    public interface IGeoRepository<T> : IEfRepository<IGeoDbContext, T>
        where T : class
    {
    }
}
