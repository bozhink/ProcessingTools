namespace ProcessingTools.Services.Data.Services
{
    using System;
    using System.Linq.Expressions;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Contracts.Models.Resources;
    using ProcessingTools.Data.Entity.DataResources;
    using ProcessingTools.Data.Models.Entity.DataResources;
    using ProcessingTools.Services.Abstractions;
    using ProcessingTools.Services.Contracts.Resources;

    /// <summary>
    /// Products data service.
    /// </summary>
    public class ProductsDataService : AbstractMultiDataServiceAsync<Product, IProduct, IFilter>, IProductsDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsDataService"/> class.
        /// </summary>
        /// <param name="repository">Products repository.</param>
        public ProductsDataService(IResourcesRepository<Product> repository)
            : base(repository)
        {
        }

        /// <inheritdoc/>
        protected override Expression<Func<Product, IProduct>> MapEntityToModel => e => new ProcessingTools.Services.Models.Data.Resources.Product
        {
            Id = e.Id,
            Name = e.Name,
        };

        /// <inheritdoc/>
        protected override Expression<Func<IProduct, Product>> MapModelToEntity => m => new Product
        {
            Id = m.Id,
            Name = m.Name,
        };
    }
}
