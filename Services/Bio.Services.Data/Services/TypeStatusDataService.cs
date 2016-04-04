namespace ProcessingTools.Bio.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Bio.Data.Models;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class TypeStatusDataService : GenericEfDataService<TypeStatus, ITypeStatusServiceModel, int>, ITypeStatusDataService
    {
        public TypeStatusDataService(IBioDataRepository<TypeStatus> repository)
            : base(repository, e => e.Name.Length)
        {
        }
    }
}
