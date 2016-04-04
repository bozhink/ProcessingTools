namespace ProcessingTools.Bio.Services.Data
{
    using Contracts;
    using Models;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class TypeStatusDataService : GenericEfDataService<TypeStatus, TypeStatusServiceModel, int>, ITypeStatusDataService
    {
        public TypeStatusDataService(IBioDataRepository<TypeStatus> repository)
            : base(repository, e => e.Name.Length)
        {
        }
    }
}
