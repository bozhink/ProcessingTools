namespace ProcessingTools.Data.Miners
{
    using Contracts.Miners;
    using Generics;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models;

    public class MorphologicalEpithetsDataMiner : SimpleServiceStringDataMiner<IMorphologicalEpithetsDataService, MorphologicalEpithetServiceModel>, IMorphologicalEpithetsDataMiner
    {
        public MorphologicalEpithetsDataMiner(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}
