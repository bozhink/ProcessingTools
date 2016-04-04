namespace ProcessingTools.Web.Api.Controllers
{
    using Factories;
    using Models.Products;
    using Services.Data.Contracts;
    using Services.Data.Models;

    public class ProductController : GenericDataServiceControllerFactory<ProductServiceModel, ProductRequestModel, ProductResponseModel>
    {
        public ProductController(IProductsDataService service)
            : base(service)
        {
        }
    }
}