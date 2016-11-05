namespace ProcessingTools.Bio.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Models;

    public class BioDbContext : DbContext
    {
        public BioDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<MorphologicalEpithet> MorphologicalEpithets { get; set; }

        public IDbSet<TypeStatus> TypesStatuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
