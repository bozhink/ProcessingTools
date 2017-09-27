namespace ProcessingTools.Data.Miners.Miners.Institutions
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Institutions;
    using ProcessingTools.Data.Miners.Generics;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;
    using ProcessingTools.Services.Data.Contracts;

    public class InstitutionsDataMiner : SimpleServiceStringDataMiner<IInstitutionsDataService, IInstitution, IFilter>, IInstitutionsDataMiner
    {
        public InstitutionsDataMiner(IInstitutionsDataService service)
            : base(service)
        {
        }
    }
}
