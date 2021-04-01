// <copyright file="IResourcesDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.DataResources
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IResourcesDatabaseInitializer : IDbContextInitializer<ResourcesDbContext>
    {
    }
}
