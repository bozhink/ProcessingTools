namespace ProcessingTools.Services.Data
{
    using Contracts;
    using Models;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class InstitutionsDataService : GenericRepositoryDataService<Institution, InstitutionServiceModel>, IInstitutionsDataService
    {
        public InstitutionsDataService(IDataRepository<Institution> repository)
            : base(repository)
        {
        }
    }
}
