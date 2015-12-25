namespace ProcessingTools.Services.Data
{
    using AutoMapper;
    using Common.Factories;
    using Contracts;
    using Models.Contracts;
    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories;

    public class ProductsDataService : EfGenericCrudDataServiceFactory<Product, IProduct, int>, IProductsDataService
    {
        public ProductsDataService(IDataRepository<Product> repository)
            : base(repository, p => p.Name.Length)
        {
            Mapper.CreateMap<Product, IProduct>();
            Mapper.CreateMap<IProduct, Product>();
        }
    }
}