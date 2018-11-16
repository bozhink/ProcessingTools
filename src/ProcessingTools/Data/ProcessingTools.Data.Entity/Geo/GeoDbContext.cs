namespace ProcessingTools.Data.Entity.Geo
{
    using Microsoft.EntityFrameworkCore;
    using ProcessingTools.Data.Models.Entity.Geo;

    public class GeoDbContext : DbContext
    {
        public GeoDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<CitySynonym> CitySynonyms { get; set; }

        public DbSet<Continent> Continents { get; set; }

        public DbSet<ContinentSynonym> ContinentSynonyms { get; set; }

        public DbSet<County> Counties { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<CountrySynonym> CountrySynonyms { get; set; }

        public DbSet<CountySynonym> CountySynonyms { get; set; }

        public DbSet<District> District { get; set; }

        public DbSet<DistrictSynonym> DistrictSynonyms { get; set; }

        public DbSet<GeoEpithet> GeoEpithets { get; set; }

        public DbSet<GeoName> GeoNames { get; set; }

        public DbSet<Municipality> Municipalities { get; set; }

        public DbSet<MunicipalitySynonym> MunicipalitySynonyms { get; set; }

        public DbSet<PostCode> PostCodes { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<ProvinceSynonym> ProvinceSynonyms { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<RegionSynonym> RegionSynonyms { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<StateSynonym> StateSynonyms { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
