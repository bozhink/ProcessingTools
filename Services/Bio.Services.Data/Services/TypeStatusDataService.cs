namespace ProcessingTools.Bio.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class TypeStatusDataService : EfGenericCrudDataServiceFactory<TypeStatus, ITypeStatus, int>, ITypeStatusDataService
    {
        public TypeStatusDataService(IBioDataRepository<TypeStatus> repository)
            : base(repository, e => e.Name.Length)
        {
        }
    }
}
