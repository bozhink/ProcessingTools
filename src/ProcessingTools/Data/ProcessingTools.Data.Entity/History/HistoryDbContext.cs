namespace ProcessingTools.Data.Entity.History
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Models.Entity.History;

    public class HistoryDbContext : DbContext
    {
        public HistoryDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ObjectHistory> ObjectHistories { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjectHistory>().HasIndex(m => m.ObjectId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
