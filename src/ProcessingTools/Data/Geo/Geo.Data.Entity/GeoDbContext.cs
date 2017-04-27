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

        public IDbSet<CitySynonym> CitySynonyms { get; set; }

        public IDbSet<Continent> Continents { get; set; }

        public IDbSet<ContinentSynonym> ContinentSynonyms { get; set; }

        public IDbSet<County> Counties { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<CountrySynonym> CountrySynonyms { get; set; }

        public IDbSet<CountySynonym> CountySynonyms { get; set; }

        public IDbSet<District> District { get; set; }

        public IDbSet<DistrictSynonym> DistrictSynonyms { get; set; }

        public IDbSet<GeoEpithet> GeoEpithets { get; set; }

        public IDbSet<GeoName> GeoNames { get; set; }

        public IDbSet<Municipality> Municipalities { get; set; }

        public IDbSet<MunicipalitySynonym> MunicipalitySynonyms { get; set; }

        public IDbSet<PostCode> PostCodes { get; set; }

        public IDbSet<Province> Provinces { get; set; }

        public IDbSet<ProvinceSynonym> ProvinceSynonyms { get; set; }

        public IDbSet<Region> Regions { get; set; }

        public IDbSet<RegionSynonym> RegionSynonyms { get; set; }

        public IDbSet<State> States { get; set; }

        public IDbSet<StateSynonym> StateSynonyms { get; set; }

        IDbSet<T> IDbContext.Set<T>() => this.Set<T>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
