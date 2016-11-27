namespace ProcessingTools.DbSeeder.Seeders
{
    using Abstractions.Seeders;
    using Contracts.Seeders;
    using ProcessingTools.Mediatypes.Data.Entity.Contracts;
    using ProcessingTools.Mediatypes.Data.Seed.Contracts;

    public class MediatypesDbSeeder : GenericDbSeeder<IMediatypesDataInitializer, IMediatypesDataSeeder>, IMediatypesDbSeeder
    {
        public MediatypesDbSeeder(IMediatypesDataInitializer initializer, IMediatypesDataSeeder seeder)
            : base(initializer, seeder)
        {
        }
    }
}
