namespace ProcessingTools.Data.Miners
{
    using Contracts;
    using ProcessingTools.Data.Miners.Common.Factories;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models;

    public class ProductsDataMiner : SimpleServiceStringDataMinerFactory<IProductsDataService, ProductServiceModel>, IProductsDataMiner
    {
        public ProductsDataMiner(IProductsDataService service)
            : base(service)
        {
        }
    }
}