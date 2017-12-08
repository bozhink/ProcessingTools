namespace ProcessingTools.Data.Miners.Miners.Products
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.Data.Miners.Contracts.Miners.Products;
    using ProcessingTools.Data.Miners.Generics;
    using ProcessingTools.Services.Data.Contracts;

    public class ProductsDataMiner : SimpleServiceStringDataMiner<IProductsDataService, IProduct, IFilter>, IProductsDataMiner
    {
        public ProductsDataMiner(IProductsDataService service)
            : base(service)
        {
        }
    }
}
