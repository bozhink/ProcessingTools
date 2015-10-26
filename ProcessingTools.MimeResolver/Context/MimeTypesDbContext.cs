namespace ProcessingTools.MimeResolver.Context
{
    using System.Data.Entity;
    using Models;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class MimeTypesDbContext : DbContext
    {
        public MimeTypesDbContext()
            : base("MimeDbContext")
        {
        }

        public IDbSet<FileExtension> FileExtensions { get; set; }

        public IDbSet<MimeType> MimeTypes { get; set; }

        public IDbSet<MimeSubtype> MimeSubtypes { get; set; }

        public IDbSet<MimeTypePair> MimeTypePairs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();
        }
    }
}
