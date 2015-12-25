namespace ProcessingTools.Bio.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Contracts;
    using Models;

    public class BioDbContext : DbContext, IBioDbContext
    {
        public BioDbContext()
            : base("BioDbContext")
        {
        }

        public IDbSet<MorphologicalEpithet> MorphologicalEpithets { get; set; }

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
