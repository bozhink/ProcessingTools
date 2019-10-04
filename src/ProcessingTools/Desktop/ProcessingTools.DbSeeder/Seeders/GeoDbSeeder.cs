namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Geo;
    using ProcessingTools.Data.Seed.Geo;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public class GeoDbSeeder : GenericDbSeeder<IGeoDataInitializer, IGeoDataSeeder>, IGeoDbSeeder
    {
        public GeoDbSeeder(IGeoDataInitializer initializer, IGeoDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
