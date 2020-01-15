// <copyright file="IGeoDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Geo
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IGeoDataInitializer : IDbContextInitializer<GeoDbContext>
    {
    }
}
