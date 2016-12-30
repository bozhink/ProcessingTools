namespace ProcessingTools.Web.Api.Controllers
{
    using Abstractions;
    using Models.Products;
    using Services.Data.Contracts;
    using Services.Data.Contracts.Models;

    public class ProductController : GenericDataServiceController<IProduct, ProductRequestModel, ProductResponseModel>
    {
        public ProductController(IProductsDataService service)
            : base(service)
        {
        }
    }
}