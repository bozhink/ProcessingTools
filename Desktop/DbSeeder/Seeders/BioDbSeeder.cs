namespace ProcessingTools.DbSeeder.Seeders
{
    using Abstractions.Seeders;
    using Contracts.Seeders;
    using ProcessingTools.Bio.Data.Entity.Contracts;
    using ProcessingTools.Bio.Data.Seed.Contracts;

    public class BioDbSeeder : GenericDbSeeder<IBioDataInitializer, IBioDataSeeder>, IBioDbSeeder
    {
        public BioDbSeeder(IBioDataInitializer initializer, IBioDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
