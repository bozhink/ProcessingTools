namespace ProcessingTools.MediaType.Data.Entity.Initializers
{
    using System.Data.Entity;
    using Contracts;
    using Migrations;
    using ProcessingTools.Data.Common.Entity.Factories;

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
