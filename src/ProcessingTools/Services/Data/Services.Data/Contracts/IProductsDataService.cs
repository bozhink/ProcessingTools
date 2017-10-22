namespace ProcessingTools.Services.Data.Contracts
{
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;
    using ProcessingTools.Services.Contracts.Data;

    public interface IProductsDataService : IMultiDataServiceAsync<IProduct, IFilter>
    {
    }
}
