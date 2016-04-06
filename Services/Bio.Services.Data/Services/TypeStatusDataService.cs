namespace ProcessingTools.Bio.Services.Data
{
    using Contracts;
    using Models;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class TypeStatusDataService : GenericRepositoryDataService<TypeStatus, TypeStatusServiceModel>, ITypeStatusDataService
    {
        public TypeStatusDataService(IBioDataRepository<TypeStatus> repository)
            : base(repository)
        {
        }
    }
}
