namespace ProcessingTools.Services.Data.Contracts
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Services.Data.Contracts.Models;

    public interface IProductsDataService : IMultiDataServiceAsync<IProduct, IFilter>
    {
    }
}
