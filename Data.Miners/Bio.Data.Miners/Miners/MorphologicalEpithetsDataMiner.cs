namespace ProcessingTools.Bio.Data.Miners
{
    using Contracts;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Data.Miners.Common.Factories;

    public class MorphologicalEpithetsDataMiner : SimpleServiceStringDataMinerFactory<IMorphologicalEpithetsDataService, MorphologicalEpithetServiceModel>, IMorphologicalEpithetsDataMiner
    {
        public MorphologicalEpithetsDataMiner(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}