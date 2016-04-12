namespace ProcessingTools.Documents.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Models;

    public class DocumentsDbContext : DbContext
    {
        public DocumentsDbContext()
            : base("DocumentsDatabase")
        {
        }

        public IDbSet<Address> Addresses { get; set; }

        public IDbSet<Publisher> Publishers { get; set; }

        public IDbSet<Journal> Journals { get; set; }

        public IDbSet<Institution> Institutions { get; set; }

        public IDbSet<Affiliation> Affiliations { get; set; }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<Document> Documents { get; set; }

        public static DocumentsDbContext Create()
        {
            return new DocumentsDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
