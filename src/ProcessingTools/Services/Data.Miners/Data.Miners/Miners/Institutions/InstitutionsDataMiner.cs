namespace ProcessingTools.Data.Miners.Miners.Institutions
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Data.Miners.Contracts.Miners.Institutions;
    using ProcessingTools.Data.Miners.Generics;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Contracts.Models;

    public class InstitutionsDataMiner : SimpleServiceStringDataMiner<IInstitutionsDataService, IInstitution, IFilter>, IInstitutionsDataMiner
    {
        public InstitutionsDataMiner(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}
