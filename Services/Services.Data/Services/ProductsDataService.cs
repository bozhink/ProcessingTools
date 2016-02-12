namespace ProcessingTools.Services.Data
{
    using AutoMapper;

    using Contracts;
    using Models.Contracts;

    using ProcessingTools.Data.Models;
    using ProcessingTools.Data.Repositories.Contracts;
    using ProcessingTools.Services.Common.Factories;

    public class ProductsDataService : EfGenericCrudDataServiceFactory<Product, IProduct, int>, IProductsDataService
    {
        private readonly IMapper mapper;

        public ProductsDataService(IDataRepository<Product> repository)
            : base(repository, p => p.Name.Length)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, IProduct>();
                c.CreateMap<IProduct, Product>();
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}