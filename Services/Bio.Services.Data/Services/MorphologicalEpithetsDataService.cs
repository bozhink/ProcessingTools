namespace ProcessingTools.Bio.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class MorphologicalEpithetsDataService : GenericEfDataService<MorphologicalEpithet, IMorphologicalEpithetServiceModel, int>, IMorphologicalEpithetsDataService
    {
        public MorphologicalEpithetsDataService(IBioDataRepository<MorphologicalEpithet> repository)
            : base(repository, e => e.Name.Length)
        {
        }
    }
}
