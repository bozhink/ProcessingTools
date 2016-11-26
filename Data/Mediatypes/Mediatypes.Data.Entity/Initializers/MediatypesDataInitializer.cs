namespace ProcessingTools.Mediatypes.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Factories;

    public class MediatypesDataInitializer : DbContextInitializerFactory<MediatypesDbContext>, IMediatypesDataInitializer
    {
        public MediatypesDataInitializer(IMediatypesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediatypesDbContext, Configuration>());
        }
    }
}
