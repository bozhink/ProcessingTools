namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Contracts;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;
    using ProcessingTools.Mediatypes.Data.Seed.Contracts;

    public class MediatypesMongoDbSeeder : GenericDbSeeder<IDatabaseInitializer, IMediatypesMongoDatabaseSeeder>, IMediatypesMongoDbSeeder
    {
        public MediatypesMongoDbSeeder(IDatabaseInitializer initializer, IMediatypesMongoDatabaseSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
