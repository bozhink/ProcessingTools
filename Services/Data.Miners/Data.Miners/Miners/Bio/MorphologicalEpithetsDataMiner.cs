using ProcessingTools.Bio.Services.Data.Contracts;
using ProcessingTools.Bio.Services.Data.Models;
using ProcessingTools.Data.Miners.Contracts.Miners.Bio;
using ProcessingTools.Data.Miners.Generics;

namespace ProcessingTools.Data.Miners.Miners.Bio
{
    public class MorphologicalEpithetsDataMiner : SimpleServiceStringDataMiner<IMorphologicalEpithetsDataService, MorphologicalEpithetServiceModel>, IMorphologicalEpithetsDataMiner
    {
        public MorphologicalEpithetsDataMiner(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}
