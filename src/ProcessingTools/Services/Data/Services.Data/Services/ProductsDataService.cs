namespace ProcessingTools.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.DataResources.Data.Entity.Contracts.Repositories;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Contracts.Models;
    using ProcessingTools.Services.Data.Models;

    public class ProductsDataService : AbstractDataServiceWithRepository<Product, IProduct, IFilter>, IProductsDataService
    {
        public ProductsDataService(IResourcesRepository<Product> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Product, IProduct>> MapDbModelToServiceModel => e => new ProductServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<IProduct, Product>> MapServiceModelToDbModel => m => new Product
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Expression<Func<Product, object>> SortExpression => p => p.Name;
    }
}
