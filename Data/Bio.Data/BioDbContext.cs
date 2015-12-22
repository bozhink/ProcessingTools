namespace ProcessingTools.Bio.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Contracts;

    public class BioDbContext : DbContext, IBioDbContext
    {
        public BioDbContext()
            : base("BioDbContext")
        {
        }

        public static BioDbContext Create()
        {
            return new BioDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
