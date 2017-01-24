namespace ProcessingTools.DbSeeder.Seeders
{
    using Abstractions.Seeders;
    using Contracts.Seeders;
    using ProcessingTools.Mediatypes.Data.Mongo.Contracts;
    using ProcessingTools.Mediatypes.Data.Seed.Contracts;

    public class MediatypesMongoDbSeeder : GenericDbSeeder<IMediatypesMongoDatabaseInitializer, IMediatypesMongoDatabaseSeeder>, IMediatypesMongoDbSeeder
    {
        public MediatypesMongoDbSeeder(IMediatypesMongoDatabaseInitializer initializer, IMediatypesMongoDatabaseSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
