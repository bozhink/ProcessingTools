namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Services.Data.Bio;
    using ProcessingTools.Contracts.Services.Data.Bio;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Data.Miners.Generics;

    public class MorphologicalEpithetsDataMiner : SimpleServiceStringDataMiner<IMorphologicalEpithetsDataService, IMorphologicalEpithet, IFilter>, IMorphologicalEpithetsDataMiner
    {
        public MorphologicalEpithetsDataMiner(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}
