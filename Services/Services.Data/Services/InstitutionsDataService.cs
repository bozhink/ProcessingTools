namespace ProcessingTools.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class InstitutionsDataService : EfGenericCrudDataServiceFactory<Institution, IInstitutionServiceModel, int>, IInstitutionsDataService
    {
        public InstitutionsDataService(IDataRepository<Institution> repository)
            : base(repository, i => i.Name.Length)
        {
        }
    }
}
