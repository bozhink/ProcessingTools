namespace ProcessingTools.Bio.Environments.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Contracts;
    using Models;

    public class BioEnvironmentsDbContext : DbContext, IBioEnvironmentsDbContext
    {
        public BioEnvironmentsDbContext()
            : base("BioEnvironmentsDbContext")
        {
        }

        public IDbSet<EnvoEntity> EnvoEntities { get; set; }

        public IDbSet<EnvoName> EnvoNames { get; set; }

        public IDbSet<EnvoGroup> EnvoGroups { get; set; }

        public IDbSet<EnvoGlobal> EnvoGlobals { get; set; }

        public static BioEnvironmentsDbContext Create()
        {
            return new BioEnvironmentsDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}