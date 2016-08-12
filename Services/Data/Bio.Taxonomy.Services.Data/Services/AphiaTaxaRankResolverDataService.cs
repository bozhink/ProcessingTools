namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using Contracts;
    using Factories;

    public class AphiaTaxaRankResolverDataService : TaxaRankResolverOverTaxaClassificationResolverDataServiceFactory, IAphiaTaxaRankResolverDataService
    {
        private readonly IAphiaTaxaClassificationResolverDataService service;

        public AphiaTaxaRankResolverDataService(IAphiaTaxaClassificationResolverDataService service)
            : base(service)
        {
        }
    }
}
