namespace ProcessingTools.Geo.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Contracts;
    using Models;

    public class GeoDbContext : DbContext, IGeoDbContext
    {
        public GeoDbContext()
            : base("GeoDbContext")
        {
        }

        public IDbSet<GeoName> GeoNames { get; set; }

        public IDbSet<GeoEpithet> GeoEpithets { get; set; }

        public static GeoDbContext Create()
        {
            return new GeoDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}