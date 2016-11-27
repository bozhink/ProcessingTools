namespace ProcessingTools.DbSeeder.Seeders
{
    using Abstractions.Seeders;
    using Contracts.Seeders;
    using ProcessingTools.Bio.Environments.Data.Entity.Contracts;
    using ProcessingTools.Bio.Environments.Data.Seed.Contracts;

    public class BioEnvironmentsDbSeeder : GenericDbSeeder<IBioEnvironmentsDataInitializer, IBioEnvironmentsDataSeeder>, IBioEnvironmentsDbSeeder
    {
        public BioEnvironmentsDbSeeder(IBioEnvironmentsDataInitializer initializer, IBioEnvironmentsDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
