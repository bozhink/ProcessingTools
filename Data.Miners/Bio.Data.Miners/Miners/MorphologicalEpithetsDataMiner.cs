namespace ProcessingTools.Bio.Data.Miners
{
    using Contracts;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models.Contracts;
    using ProcessingTools.Data.Miners.Common.Factories;

    public class MorphologicalEpithetsDataMiner : SimpleServiceStringDataMinerFactory<IMorphologicalEpithetsDataService, IMorphologicalEpithetServiceModel>, IMorphologicalEpithetsDataMiner
    {
        public MorphologicalEpithetsDataMiner(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}