namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.DataResources;
    using ProcessingTools.Data.Seed.DataResources;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public class DataResourcesDbSeeder : GenericDbSeeder<IResourcesDatabaseInitializer, IResourcesDataSeeder>, IDataResourcesDbSeeder
    {
        public DataResourcesDbSeeder(IResourcesDatabaseInitializer initializer, IResourcesDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
