namespace ProcessingTools.Web.Api.Controllers
{
    using Factories;
    using Models.ProductModels;
    using Services.Data.Contracts;
    using Services.Data.Models.Contracts;

    public class ProductController : GenericDataServiceControllerFactory<IProduct, ProductRequestModel, ProductResponseModel>
    {
        public ProductController(IProductsDataService service)
        {
            this.Service = service;
        }
    }
}