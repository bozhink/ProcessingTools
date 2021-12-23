// <copyright file="ResourcesDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.DataResources
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class ResourcesDatabaseInitializer : DbContextInitializer<ResourcesDbContext>, IResourcesDatabaseInitializer
    {
        public ResourcesDatabaseInitializer(ResourcesDbContext context)
            : base(context)
        {
        }

        protected override void SetInitializer()
        {
        }
    }
}
