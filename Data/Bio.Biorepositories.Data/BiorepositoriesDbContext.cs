namespace ProcessingTools.Bio.Biorepositories.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class BiorepositoriesDbContext : DbContext
    {
        public BiorepositoriesDbContext()
            : base("BiorepositoriesDbContext")
        {
        }

        public static BiorepositoriesDbContext Create()
        {
            return new BiorepositoriesDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}