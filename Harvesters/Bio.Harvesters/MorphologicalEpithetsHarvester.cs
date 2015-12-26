namespace ProcessingTools.Bio.Harvesters
{
    using Contracts;
    using ProcessingTools.Bio.Services.Data.Contracts;
    using ProcessingTools.Bio.Services.Data.Models.Contracts;
    using ProcessingTools.Harvesters.Common.Factories;

    public class MorphologicalEpithetsHarvester : SimpleServiceStringHarvesterFactory<IMorphologicalEpithetsDataService, IMorphologicalEpithet>, IMorphologicalEpithetHarvester
    {
        public MorphologicalEpithetsHarvester(IMorphologicalEpithetsDataService service)
            : base(service)
        {
        }
    }
}