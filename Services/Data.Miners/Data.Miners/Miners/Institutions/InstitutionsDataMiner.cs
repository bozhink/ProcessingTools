namespace ProcessingTools.Data.Miners.Miners.Institutions
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Institutions;
    using ProcessingTools.Data.Miners.Generics;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models;

    public class InstitutionsDataMiner : SimpleServiceStringDataMiner<IInstitutionsDataService, InstitutionServiceModel>, IInstitutionsDataMiner
    {
        public InstitutionsDataMiner(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}
