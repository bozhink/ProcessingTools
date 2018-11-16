namespace ProcessingTools.Data.Entity.Documents
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Models.Entity.Documents;

    public class DocumentsDbContext : DbContext
    {
        public DocumentsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Affiliation> Affiliations { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Journal> Journals { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasIndex(m => m.FilePath).IsUnique();
            modelBuilder.Entity<File>().HasIndex(m => m.FullName).IsUnique();
            modelBuilder.Entity<Institution>().HasIndex(m => m.Name).IsUnique();
            modelBuilder.Entity<Journal>().HasIndex(m => m.Name).IsUnique();
            modelBuilder.Entity<Publisher>().HasIndex(m => m.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
