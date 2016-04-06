namespace ProcessingTools.Bio.Services.Data
{
    using Contracts;
    using Models;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class MorphologicalEpithetsDataService : GenericRepositoryDataService<MorphologicalEpithet, MorphologicalEpithetServiceModel>, IMorphologicalEpithetsDataService
    {
        public MorphologicalEpithetsDataService(IBioDataRepository<MorphologicalEpithet> repository)
            : base(repository)
        {
        }
    }
}
