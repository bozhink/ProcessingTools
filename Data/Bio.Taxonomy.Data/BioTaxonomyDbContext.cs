namespace ProcessingTools.Bio.Taxonomy.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Models;

    public class BioTaxonomyDbContext : DbContext
    {
        public BioTaxonomyDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<TaxonRank> TaxonRanks { get; set; }

        public IDbSet<TaxonName> TaxonNames { get; set; }

        public IDbSet<BlackListedItem> BlackListedItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}