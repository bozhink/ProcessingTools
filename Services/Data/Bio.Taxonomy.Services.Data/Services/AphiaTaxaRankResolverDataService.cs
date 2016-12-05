using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
using ProcessingTools.Bio.Taxonomy.Services.Data.Factories;

namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    public class AphiaTaxaRankResolverDataService : TaxaRankResolverOverTaxaClassificationResolverDataServiceFactory, IAphiaTaxaRankResolverDataService
    {
        public AphiaTaxaRankResolverDataService(IAphiaTaxaClassificationResolverDataService service)
            : base(service)
        {
        }
    }
}
