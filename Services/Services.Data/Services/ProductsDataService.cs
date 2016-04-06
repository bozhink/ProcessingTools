namespace ProcessingTools.Services.Data
{
    using Contracts;
    using Models;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class ProductsDataService : GenericRepositoryDataService<Product, ProductServiceModel>, IProductsDataService
    {
        public ProductsDataService(IDataRepository<Product> repository)
            : base(repository)
        {
        }
    }
}
