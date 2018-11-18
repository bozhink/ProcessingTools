namespace ProcessingTools.Journals.Data.Entity
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Models.Entity.Journals;

    public class JournalsDbContext : DbContext
    {
        public JournalsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Institution> Institutions { get; set; }
    }
}
