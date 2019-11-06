﻿namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Seed.Files;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public class MediatypesMongoDbSeeder : GenericDbSeeder<IDatabaseInitializer, IMediatypesMongoDatabaseSeeder>, IMediatypesMongoDbSeeder
    {
        public MediatypesMongoDbSeeder(IDatabaseInitializer initializer, IMediatypesMongoDatabaseSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}