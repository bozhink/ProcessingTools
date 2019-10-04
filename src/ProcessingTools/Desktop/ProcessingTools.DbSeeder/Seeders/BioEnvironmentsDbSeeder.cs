namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Bio.Environments;
    using ProcessingTools.Data.Seed.Bio.Environments;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public class BioEnvironmentsDbSeeder : GenericDbSeeder<IBioEnvironmentsDataInitializer, IBioEnvironmentsDataSeeder>, IBioEnvironmentsDbSeeder
    {
        public BioEnvironmentsDbSeeder(IBioEnvironmentsDataInitializer initializer, IBioEnvironmentsDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
