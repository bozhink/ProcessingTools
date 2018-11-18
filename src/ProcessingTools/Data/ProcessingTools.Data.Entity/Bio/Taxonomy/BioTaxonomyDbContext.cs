namespace ProcessingTools.Data.Entity.Bio.Taxonomy
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Models.Entity.Bio.Taxonomy;

    public class BioTaxonomyDbContext : DbContext
    {
        public BioTaxonomyDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<BlackListEntity> BlackListedItems { get; set; }

        public DbSet<TaxonName> TaxonNames { get; set; }

        public DbSet<TaxonRank> TaxonRanks { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxonName>().HasIndex(m => m.Name).IsUnique();
            modelBuilder.Entity<TaxonName>().HasMany(m => m.Ranks);

            modelBuilder.Entity<TaxonRank>().HasIndex(m => m.Name).IsUnique();
            modelBuilder.Entity<TaxonRank>().HasMany(m => m.Taxa);

            base.OnModelCreating(modelBuilder);
        }
    }
}
