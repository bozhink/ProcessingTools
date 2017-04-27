namespace ProcessingTools.DbSeeder.Seeders
{
    using Abstractions.Seeders;
    using Contracts.Seeders;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Seed.Contracts;

    public class GeoDbSeeder : GenericDbSeeder<IGeoDataInitializer, IGeoDataSeeder>, IGeoDbSeeder
    {
        public GeoDbSeeder(IGeoDataInitializer initializer, IGeoDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
