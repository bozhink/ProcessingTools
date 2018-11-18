namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Models.Entity.Bio.Environments;

    public class BioEnvironmentsDbContext : DbContext
    {
        public BioEnvironmentsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<EnvoEntity> EnvoEntities { get; set; }

        public DbSet<EnvoGlobal> EnvoGlobals { get; set; }

        public DbSet<EnvoGroup> EnvoGroups { get; set; }

        public DbSet<EnvoName> EnvoNames { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
