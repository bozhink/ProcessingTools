namespace ProcessingTools.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class InstitutionsDataService : GenericEfDataService<Institution, IInstitutionServiceModel, int>, IInstitutionsDataService
    {
        public InstitutionsDataService(IDataRepository<Institution> repository)
            : base(repository, i => i.Name.Length)
        {
        }
    }
}
