namespace ProcessingTools.Data.Miners.Miners.Products
{
    using ProcessingTools.Data.Miners.Contracts.Miners.Products;
    using ProcessingTools.Data.Miners.Generics;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Contracts.Models;

    public class ProductsDataMiner : SimpleServiceStringDataMiner<IProductsDataService, IProduct>, IProductsDataMiner
    {
        public ProductsDataMiner(IProductsDataService service)
            : base(service)
        {
        }
    }
}
