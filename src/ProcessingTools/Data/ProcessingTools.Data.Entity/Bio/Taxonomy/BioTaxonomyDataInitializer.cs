namespace ProcessingTools.Data.Entity.Bio.Taxonomy
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class BioTaxonomyDataInitializer : DbContextInitializer<BioTaxonomyDbContext>, IBioTaxonomyDataInitializer
    {
        public BioTaxonomyDataInitializer(BioTaxonomyDbContext context)
            : base(context)
        {
        }

        protected override void SetInitializer()
        {
        }
    }
}
