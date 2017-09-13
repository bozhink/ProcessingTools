namespace ProcessingTools.Web.Documents
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
                .GetService<ProcessingTools.Documents.Data.Entity.Contracts.IDocumentsDataInitializer>()
                .InitializeAsync()
                .ConfigureAwait(false);

            await DependencyResolver.Current
               .GetService<ProcessingTools.Geo.Data.Entity.Contracts.IGeoDataInitializer>()
               .InitializeAsync()
               .ConfigureAwait(false);

            await DependencyResolver.Current
                .GetService<ProcessingTools.Bio.Data.Entity.Contracts.IBioDataInitializer>()
                .InitializeAsync()
                .ConfigureAwait(false);

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ProcessingTools.DataResources.Data.Entity.ResourcesDbContext, ProcessingTools.DataResources.Data.Entity.Migrations.Configuration>());
        }
    }
}