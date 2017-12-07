namespace ProcessingTools.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.DataResources.Data.Entity.Contracts.Repositories;
    using ProcessingTools.DataResources.Data.Entity.Models;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Data.Contracts;

    public class ProductsDataService : AbstractMultiDataServiceAsync<Product, IProduct, IFilter>, IProductsDataService
    {
        public ProductsDataService(IResourcesRepository<Product> repository)
            : base(repository)
        {
        }

        protected override Expression<Func<Product, IProduct>> MapEntityToModel => e => new ProcessingTools.Services.Models.Data.Resources.Product
        {
            Id = e.Id,
            Name = e.Name
        };

        protected override Expression<Func<IProduct, Product>> MapModelToEntity => m => new Product
        {
            Id = m.Id,
            Name = m.Name
        };
    }
}
