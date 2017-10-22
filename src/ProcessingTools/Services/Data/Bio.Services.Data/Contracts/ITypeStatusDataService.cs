namespace ProcessingTools.Bio.Services.Data.Contracts
{
    using ProcessingTools.Bio.Services.Data.Models;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Services.Contracts.Data;

    public interface ITypeStatusDataService : IMultiDataServiceAsync<TypeStatusServiceModel, IFilter>
    {
    }
}
