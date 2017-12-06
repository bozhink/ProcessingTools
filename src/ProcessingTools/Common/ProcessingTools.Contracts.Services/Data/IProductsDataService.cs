namespace ProcessingTools.Services.Data.Contracts
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.Contracts.Services.Data;

    public interface IProductsDataService : IMultiDataServiceAsync<IProduct, IFilter>
    {
    }
}
