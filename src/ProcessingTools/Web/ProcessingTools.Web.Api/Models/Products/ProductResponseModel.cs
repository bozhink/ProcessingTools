namespace ProcessingTools.Web.Api.Models.Products
{
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Services.Data.Models;

    public class ProductResponseModel : IMapFrom<ProductServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
