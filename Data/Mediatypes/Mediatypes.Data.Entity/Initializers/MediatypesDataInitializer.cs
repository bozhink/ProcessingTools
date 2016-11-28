namespace ProcessingTools.Mediatypes.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Abstractions;

    public class MediatypesDataInitializer : GenericDbContextInitializer<MediatypesDbContext>, IMediatypesDataInitializer
    {
        public MediatypesDataInitializer(IMediatypesDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediatypesDbContext, Configuration>());
        }
    }
}
