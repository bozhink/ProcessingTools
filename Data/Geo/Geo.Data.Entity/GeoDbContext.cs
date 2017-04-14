namespace ProcessingTools.Geo.Data.Entity
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using ProcessingTools.Data.Common.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Contracts;
    using ProcessingTools.Geo.Data.Entity.Models;

    public class GeoDbContext : DbContext, IGeoDbContext
    {
        public GeoDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Continent> Continents { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<GeoEpithet> GeoEpithets { get; set; }

        public IDbSet<GeoName> GeoNames { get; set; }

        public IDbSet<PostCode> PostCodes { get; set; }

        public IDbSet<State> States { get; set; }

        IDbSet<T> IDbContext.Set<T>() => this.Set<T>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
