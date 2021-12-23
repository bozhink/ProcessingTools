// <copyright file="GeoDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Geo;
    using ProcessingTools.Data.Seed.Geo;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Geo database seeder.
    /// </summary>
    public class GeoDbSeeder : GenericDbSeeder<IGeoDataInitializer, IGeoDataSeeder>, IGeoDbSeeder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeoDbSeeder"/> class.
        /// </summary>
        /// <param name="initializer">Instance of <see cref="IGeoDataInitializer"/>.</param>
        /// <param name="seeder">Instance of <see cref="IGeoDataSeeder"/>.</param>
        public GeoDbSeeder(IGeoDataInitializer initializer, IGeoDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
