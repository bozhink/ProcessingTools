namespace ProcessingTools.Data.Miners
{
    using Contracts.Miners;
    using ProcessingTools.Data.Miners.Common;
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
