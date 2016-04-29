namespace ProcessingTools.Geo.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Common.Constants;
    using Models;

    public class GeoDbContext : DbContext
    {
        public GeoDbContext()
            : base(ConnectionConstants.GeoDbContextConnectionKey)
        {
        }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<State> States { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<PostCode> PostCodes { get; set; }

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