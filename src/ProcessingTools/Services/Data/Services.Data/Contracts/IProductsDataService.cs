namespace ProcessingTools.Services.Data.Contracts
{
    using ProcessingTools.Contracts.Services.Data;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;

    public interface IProductsDataService : IMultiDataServiceAsync<IProduct, IFilter>
    {
    }
}
