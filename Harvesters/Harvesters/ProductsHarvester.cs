namespace ProcessingTools.Harvesters
{
    using Contracts;
    using ProcessingTools.Harvesters.Common.Factories;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Models.Contracts;

    public class ProductsHarvester : SimpleServiceStringHarvesterFactory<IProductsDataService, IProduct>, IProductsHarvester
    {
        public ProductsHarvester(IProductsDataService service)
            : base(service)
        {
        }
    }
}