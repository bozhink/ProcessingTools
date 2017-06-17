namespace ProcessingTools.Data.Miners.Miners.Bio
{
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
    using ProcessingTools.Data.Miners.Generics;

    public class MorphologicalEpithetsDataMiner : SimpleServiceStringDataMiner<IMorphologicalEpithetsDataService, MorphologicalEpithetServiceModel, IFilter>, IMorphologicalEpithetsDataMiner
    {
        public MorphologicalEpithetsDataMiner(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}
