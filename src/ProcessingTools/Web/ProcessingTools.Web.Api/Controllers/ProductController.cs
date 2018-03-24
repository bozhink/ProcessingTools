namespace ProcessingTools.Web.Api.Controllers
{
    using AutoMapper;
    using ProcessingTools.Models.Contracts;
    using ProcessingTools.Models.Contracts.Resources;
    using ProcessingTools.Services.Contracts.Resources;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Models.Resources.Products;

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
