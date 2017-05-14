namespace ProcessingTools.Users.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<UsersDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextType = typeof(UsersDbContext);
            this.ContextKey = this.ContextType.FullName;
        }

        protected override void Seed(UsersDbContext context)
        {
        }
    }
}
