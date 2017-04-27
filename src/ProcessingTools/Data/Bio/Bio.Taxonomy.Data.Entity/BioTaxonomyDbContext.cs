namespace ProcessingTools.Bio.Taxonomy.Data.Entity
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using Contracts;
    using Models;
    using ProcessingTools.Data.Common.Entity.Contracts;

    public class BioTaxonomyDbContext : DbContext, IBioTaxonomyDbContext
    {
        public BioTaxonomyDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<BlackListEntity> BlackListedItems { get; set; }

        public IDbSet<TaxonName> TaxonNames { get; set; }

        public IDbSet<TaxonRank> TaxonRanks { get; set; }

        IDbSet<T> IDbContext.Set<T>() => this.Set<T>();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
