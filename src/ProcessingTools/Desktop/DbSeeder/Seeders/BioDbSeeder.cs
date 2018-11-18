namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Bio;
    using ProcessingTools.Data.Seed.Bio;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public class BioDbSeeder : GenericDbSeeder<IBioDataInitializer, IBioDataSeeder>, IBioDbSeeder
    {
        public BioDbSeeder(IBioDataInitializer initializer, IBioDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
