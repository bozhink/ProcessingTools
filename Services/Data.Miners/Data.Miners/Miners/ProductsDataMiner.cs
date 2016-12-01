namespace ProcessingTools.Data.Miners
{
    using Contracts.Miners;
    using ProcessingTools.Data.Miners.Common;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models;

    public class ProductsDataMiner : SimpleServiceStringDataMiner<IProductsDataService, ProductServiceModel>, IProductsDataMiner
    {
        public ProductsDataMiner(IProductsDataService service)
            : base(service)
        {
        }
    }
}
