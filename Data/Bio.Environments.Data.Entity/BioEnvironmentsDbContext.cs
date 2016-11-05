namespace ProcessingTools.Bio.Environments.Data.Entity
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Contracts;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class BioEnvironmentsDbContext : DbContext, IBioEnvironmentsDbContext
    {
        public BioEnvironmentsDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<EnvoEntity> EnvoEntities { get; set; }

        public IDbSet<EnvoGlobal> EnvoGlobals { get; set; }

        public IDbSet<EnvoGroup> EnvoGroups { get; set; }

        public IDbSet<EnvoName> EnvoNames { get; set; }

        IDbSet<T> IDbContext.Set<T>() => this.Set<T>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
