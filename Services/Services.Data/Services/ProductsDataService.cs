namespace ProcessingTools.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Contracts;
    using Models;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class ProductsDataService : RepositoryMultiDataServiceFactory<Product, ProductServiceModel>, IProductsDataService
    {
        public ProductsDataService(IDataRepository<Product> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Product, IEnumerable<ProductServiceModel>>> MapDbModelToServiceModel => e => new ProductServiceModel[]
        {
            new ProductServiceModel
            {
                Id = e.Id,
                Name = e.Name
            }
        };

        protected override Expression<Func<ProductServiceModel, IEnumerable<Product>>> MapServiceModelToDbModel => m => new Product[]
        {
            new Product
            {
                Id = m.Id,
                Name = m.Name
            }
        };

        protected override Expression<Func<Product, object>> SortExpression => p => p.Name;
    }
}
