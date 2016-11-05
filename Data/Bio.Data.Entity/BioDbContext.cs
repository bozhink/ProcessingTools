namespace ProcessingTools.Bio.Data.Entity
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Contracts;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class BioDbContext : DbContext, IBioDbContext
    {
        public BioDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<MorphologicalEpithet> MorphologicalEpithets { get; set; }

        public IDbSet<TypeStatus> TypesStatuses { get; set; }

        IDbSet<T> IDbContext.Set<T>() => this.Set<T>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
