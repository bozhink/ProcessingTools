namespace ProcessingTools.Services.Data
{
    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common;

    public class ProductsDataService : GenericEfDataService<Product, IProductServiceModel, int>, IProductsDataService
    {
        public ProductsDataService(IDataRepository<Product> repository)
            : base(repository, p => p.Name.Length)
        {
        }
    }
}
