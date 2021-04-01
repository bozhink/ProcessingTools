// <copyright file="GeoDataInitializer.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Entity.Geo
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class GeoDataInitializer : DbContextInitializer<GeoDbContext>, IGeoDataInitializer
    {
        public GeoDataInitializer(GeoDbContext context)
            : base(context)
        {
        }

        protected override void SetInitializer()
        {
        }
    }
}
