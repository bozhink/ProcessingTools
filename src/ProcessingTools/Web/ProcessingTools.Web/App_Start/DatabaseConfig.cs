namespace ProcessingTools.Web
{
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public static class DatabaseConfig
    {
        public static async Task Initialize()
        {
            await DependencyResolver.Current
                .GetService<ProcessingTools.Users.Data.Entity.Contracts.IUsersDataInitializer>()
                .InitializeAsync()
                .ConfigureAwait(false);

            await DependencyResolver.Current
                .GetService<ProcessingTools.History.Data.Entity.Contracts.IHistoryDatabaseInitializer>()
                .InitializeAsync()
                .ConfigureAwait(false);

            await DependencyResolver.Current
                .GetService<ProcessingTools.Journals.Data.Entity.Contracts.IJournalsDatabaseInitializer>()
                .InitializeAsync()
                .ConfigureAwait(false);

            ////await DependencyResolver.Current
            ////    .GetService<ProcessingTools.Documents.Data.Entity.Contracts.IDocumentsDataInitializer>()
            ////    .Initialize().ConfigureAwait(false);

            await DependencyResolver.Current
               .GetService<ProcessingTools.Geo.Data.Entity.Contracts.IGeoDataInitializer>()
               .InitializeAsync()
               .ConfigureAwait(false);

            ////await DependencyResolver.Current
            ////    .GetService<ProcessingTools.Bio.Data.Entity.Contracts.IBioDataInitializer>()
            ////    .Initialize().ConfigureAwait(false);

            ////Database.SetInitializer(
            ////    new MigrateDatabaseToLatestVersion<ProcessingTools.DataResources.Data.Entity.ResourcesDbContext, ProcessingTools.DataResources.Data.Entity.Migrations.Configuration>());
        }
    }
}
