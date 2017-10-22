namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Data.Miners.Generics;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts.Data.Bio;
    using ProcessingTools.Services.Models.Contracts.Data.Bio;

    public class MorphologicalEpithetsDataMiner : SimpleServiceStringDataMiner<IMorphologicalEpithetsDataService, IMorphologicalEpithet, IFilter>, IMorphologicalEpithetsDataMiner
    {
        public MorphologicalEpithetsDataMiner(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}
