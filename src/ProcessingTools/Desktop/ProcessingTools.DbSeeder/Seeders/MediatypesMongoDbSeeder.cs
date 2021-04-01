// <copyright file="MediatypesMongoDbSeeder.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Seed.Files;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    /// <summary>
    /// Mediatypes MongoDB database seeder.
    /// </summary>
    public class MediatypesMongoDbSeeder : GenericDbSeeder<IDatabaseInitializer, IMediatypesMongoDatabaseSeeder>, IMediatypesMongoDbSeeder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesMongoDbSeeder"/> class.
        /// </summary>
        /// <param name="initializer">Instance of <see cref="IDatabaseInitializer"/>.</param>
        /// <param name="seeder">Instance of <see cref="IMediatypesMongoDatabaseSeeder"/>.</param>
        public MediatypesMongoDbSeeder(IDatabaseInitializer initializer, IMediatypesMongoDatabaseSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
