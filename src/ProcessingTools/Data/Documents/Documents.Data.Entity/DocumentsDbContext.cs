namespace ProcessingTools.Documents.Data.Entity
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Documents.Data.Entity.Contracts;
    using ProcessingTools.Documents.Data.Entity.Models;

    public class DocumentsDbContext : DbContext, IDocumentsDbContext
    {
        public DocumentsDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<Address> Addresses { get; set; }

        public IDbSet<Affiliation> Affiliations { get; set; }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Document> Documents { get; set; }

        public IDbSet<File> Files { get; set; }

        public IDbSet<Institution> Institutions { get; set; }

        public IDbSet<Journal> Journals { get; set; }

        public IDbSet<Publisher> Publishers { get; set; }

        IDbSet<T> IDbContext.Set<T>() => this.Set<T>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
