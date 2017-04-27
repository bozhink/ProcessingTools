namespace ProcessingTools.DbSeeder.Seeders
{
    using Abstractions.Seeders;
    using Contracts.Seeders;
    using ProcessingTools.DataResources.Data.Entity.Contracts;
    using ProcessingTools.DataResources.Data.Seed.Contracts;

    public class DataResourcesDbSeeder : GenericDbSeeder<IResourcesDatabaseInitializer, IResourcesDataSeeder>, IDataResourcesDbSeeder
    {
        public DataResourcesDbSeeder(IResourcesDatabaseInitializer initializer, IResourcesDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
