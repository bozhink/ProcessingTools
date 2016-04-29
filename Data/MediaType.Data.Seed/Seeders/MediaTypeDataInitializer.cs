namespace ProcessingTools.MediaType.Data.Seed
{
    using System.Data.Entity;

    using Contracts;

    using ProcessingTools.Data.Common.Entity.Factories;
    using ProcessingTools.MediaType.Data.Contracts;
    using ProcessingTools.MediaType.Data.Migrations;
    using ProcessingTools.MediaType.Data.Repositories.Contracts;

    public class MediaTypeDataInitializer : DbContextInitializerFactory<MediaTypesDbContext>, IMediaTypeDataInitializer
    {
        public MediaTypeDataInitializer(IMediaTypesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }

        protected override void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaTypesDbContext, Configuration>());
        }
    }
}