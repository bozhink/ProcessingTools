namespace ProcessingTools.DbSeeder.Seeders
{
    using ProcessingTools.Data.Entity.Files;
    using ProcessingTools.Data.Seed.Files;
    using ProcessingTools.DbSeeder.Abstractions.Seeders;
    using ProcessingTools.DbSeeder.Contracts.Seeders;

    public class MediatypesDbSeeder : GenericDbSeeder<IMediatypesDataInitializer, IMediatypesDataSeeder>, IMediatypesDbSeeder
    {
        public MediatypesDbSeeder(IMediatypesDataInitializer initializer, IMediatypesDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
