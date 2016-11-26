namespace ProcessingTools.Mediatypes.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MediatypesDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextType = typeof(MediatypesDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(MediatypesDbContext context)
        {
        }
    }
}
