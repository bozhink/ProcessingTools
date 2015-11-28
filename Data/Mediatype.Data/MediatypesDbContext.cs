namespace ProcessingTools.MediaType.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Models;
    using ProcessingTools.Data.Common.Contracts;

    public class MediaTypesDbContext : DbContext, IDbContext
    {
        public MediaTypesDbContext()
            : base("MimeDbContext")
        {
        }

        public IDbSet<FileExtension> FileExtensions { get; set; }

        public IDbSet<MimeType> MimeTypes { get; set; }

        public IDbSet<MimeSubtype> MimeSubtypes { get; set; }

        public IDbSet<MimeTypePair> MimeTypePairs { get; set; }

        public static MediaTypesDbContext Create()
        {
            return new MediaTypesDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
