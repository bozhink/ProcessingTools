namespace ProcessingTools.Mediatypes.Data.Entity
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Contracts;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class MediatypesDbContext : DbContext, IMediatypesDbContext
    {
        public MediatypesDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<FileExtension> FileExtensions { get; set; }

        public IDbSet<Mimetype> Mimetypes { get; set; }

        public IDbSet<Mimesubtype> Mimesubtypes { get; set; }

        public IDbSet<MimetypePair> MimetypePairs { get; set; }

        IDbSet<T> IDbContext.Set<T>() => this.Set<T>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
