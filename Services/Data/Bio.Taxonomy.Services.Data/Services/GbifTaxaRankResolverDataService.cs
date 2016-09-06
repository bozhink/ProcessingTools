namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using Contracts;
    using Factories;

    public class GbifTaxaRankResolverDataService : TaxaRankResolverOverTaxaClassificationResolverDataServiceFactory, IGbifTaxaRankResolverDataService
    {
        public GbifTaxaRankResolverDataService(IGbifTaxaClassificationResolverDataService service)
            : base(service)
        {
        }
    }
}
