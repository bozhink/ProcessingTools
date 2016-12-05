namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Factories;

    public class AphiaTaxaRankResolverDataService : TaxaRankResolverOverTaxaClassificationResolverDataServiceFactory, IAphiaTaxaRankResolverDataService
    {
        public AphiaTaxaRankResolverDataService(IAphiaTaxaClassificationResolverDataService service)
            : base(service)
        {
        }
    }
}
