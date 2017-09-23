namespace ProcessingTools.Services.Data.Contracts
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Models.Contracts.Resources;
    using ProcessingTools.Contracts.Services.Data;

    public interface IProductsDataService : IMultiDataServiceAsync<IProduct, IFilter>
    {
    }
}
