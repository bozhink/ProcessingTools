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
