namespace ProcessingTools.Services.Data
{
    using System;
    using System.Linq.Expressions;
    using Contracts;
    using Models;
    using ProcessingTools.DataResources.Data.Entity.Contracts.Repositories;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.Services.Common.Factories;

    public class ProductsDataService : SimpleDataServiceWithRepositoryFactory<Product, ProductServiceModel>, IProductsDataService
    {
        public ProductsDataService(IResourcesRepository<Product> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Product, ProductServiceModel>> MapDbModelToServiceModel => e => new ProductServiceModel
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<ProductServiceModel, Product>> MapServiceModelToDbModel => m => new Product
        {
            Id = m.Id,
            Name = m.Name
        };

        protected override Expression<Func<Product, object>> SortExpression => p => p.Name;
    }
}
