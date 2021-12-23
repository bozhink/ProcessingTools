// <copyright file="IResourcesDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.DataResources
{
    using ProcessingTools.Data.Entity.Abstractions;

    public interface IResourcesDatabaseInitializer : IDbContextInitializer<ResourcesDbContext>
    {
    }
}
