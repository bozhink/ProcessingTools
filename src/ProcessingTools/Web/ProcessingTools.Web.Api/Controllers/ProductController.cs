namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Contracts.Models;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.Products;

    public class ProductController : GenericDataServiceController<IProductsDataService, IProduct, ProductRequestModel, ProductResponseModel, IFilter>
    {
        private readonly IMapper mapper;

        public ProductController(IProductsDataService service)
            : base(service)
        {
            var mapperConfiguration = new MapperConfiguration(c =>
            {
                c.CreateMap<IProduct, ProductResponseModel>();
                c.CreateMap<ProductRequestModel, IProduct>().ConvertUsing(p => p);
            });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        protected override IMapper Mapper => this.mapper;
    }
}
