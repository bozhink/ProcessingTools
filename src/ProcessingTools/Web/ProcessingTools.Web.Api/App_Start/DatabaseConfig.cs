namespace ProcessingTools.Web.Api
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public static class DatabaseConfig
    {
        public static async Task Initialize()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.Users.Data.Entity.UsersDbContext, ProcessingTools.Users.Data.Entity.Migrations.Configuration>());

            await DependencyResolver.Current
                .GetService<ProcessingTools.Geo.Data.Entity.Contracts.IGeoDataInitializer>()
                .Initialize()
                .ConfigureAwait(false);

            await DependencyResolver.Current
                .GetService<ProcessingTools.Bio.Data.Entity.Contracts.IBioDataInitializer>()
                .Initialize()
                .ConfigureAwait(false);
        }
    }
}