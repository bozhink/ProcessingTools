namespace ProcessingTools.Web.Api.Models.ProductModels
{
    using Contracts.Mapping;
    using Services.Data.Models.Contracts;

    public class ProductResponseModel : IMapFrom<IProduct>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}