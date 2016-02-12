namespace ProcessingTools.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class ProductsDataService : EfGenericCrudDataServiceFactory<Product, IProduct, int>, IProductsDataService
    {
        public ProductsDataService(IDataRepository<Product> repository)
            : base(repository, p => p.Name.Length)
        {
        }
    }
}
