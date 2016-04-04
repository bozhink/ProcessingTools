namespace ProcessingTools.Data.Miners
{
    using Contracts;
    using ProcessingTools.Data.Miners.Common.Factories;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models;

    public class InstitutionsDataMiner : SimpleServiceStringDataMinerFactory<IInstitutionsDataService, InstitutionServiceModel>, IInstitutionsDataMiner
    {
        public InstitutionsDataMiner(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}