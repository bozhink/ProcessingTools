namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using Contracts;
    using Factories;

    public class CatalogueOfLifeTaxaRankResolverDataService : TaxaRankResolverOverTaxaClassificationResolverDataServiceFactory, ICatalogueOfLifeTaxaRankResolverDataService
    {
        public CatalogueOfLifeTaxaRankResolverDataService(ICatalogueOfLifeTaxaClassificationResolverDataService service)
            : base(service)
        {
        }
    }
}
