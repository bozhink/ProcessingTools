namespace ProcessingTools.Web.Api.Models.Products
{
    using Mappings.Contracts;
    using Services.Data.Models;

    public class ProductResponseModel : IMapFrom<ProductServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}