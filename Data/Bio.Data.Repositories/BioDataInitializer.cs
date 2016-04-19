namespace ProcessingTools.Bio.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Data.Migrations;

    public class BioDataInitializer : IBioDataInitializer
    {
        private IBioDbContextProvider contextProvider;

        public BioDataInitializer(IBioDbContextProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public async Task Init()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BioDbContext, Configuration>());

            using (var context = this.contextProvider.Create())
            {
                context.Database.CreateIfNotExists();
                context.Database.Initialize(true);
                await context.SaveChangesAsync();
            }
        }
    }
}
