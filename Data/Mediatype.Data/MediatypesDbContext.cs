namespace ProcessingTools.Mediatype.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Models;

    public class MediatypesDbContext : DbContext
    {
        public MediatypesDbContext()
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
