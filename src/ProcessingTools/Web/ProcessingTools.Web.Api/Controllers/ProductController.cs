namespace ProcessingTools.Web.Api.Controllers
{
    using ProcessingTools.Contracts.Filters;
    using ProcessingTools.Services.Data.Contracts;
    using ProcessingTools.Services.Data.Contracts.Models;
    using ProcessingTools.Web.Api.Abstractions;
    using ProcessingTools.Web.Api.Models.Products;

    public class ProductController : GenericDataServiceController<IProductsDataService, IProduct, ProductRequestModel, ProductResponseModel, IFilter>
    {
        public ProductController(IProductsDataService service)
            : base(service)
        {
        }
    }
}
