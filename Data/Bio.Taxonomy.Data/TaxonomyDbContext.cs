namespace ProcessingTools.Bio.Taxonomy.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Models;

    public class TaxonomyDbContext : DbContext
    {
        public TaxonomyDbContext()
            : base("TaxonomyDbContext")
        {
        }

        public IDbSet<TaxonRank> TaxonRanks { get; set; }

        public IDbSet<TaxonName> TaxonNames { get; set; }

        public TaxonomyDbContext Create()
        {
            return new TaxonomyDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}