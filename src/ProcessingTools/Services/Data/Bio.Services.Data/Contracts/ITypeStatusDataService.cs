namespace ProcessingTools.Bio.Services.Data.Contracts
{
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Models.Contracts;

    public interface ITypeStatusDataService : IMultiDataServiceAsync<TypeStatusServiceModel, IFilter>
    {
    }
}
