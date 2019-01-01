namespace ProcessingTools.Data.Entity.Bio
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Models.Entity.Bio;

    public class BioDbContext : DbContext
    {
        public BioDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<MorphologicalEpithet> MorphologicalEpithets { get; set; }

        public DbSet<TypeStatus> TypesStatuses { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MorphologicalEpithet>().HasIndex(m => m.Name).IsUnique();

            modelBuilder.Entity<TypeStatus>().HasIndex(m => m.Name).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
