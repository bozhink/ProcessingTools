namespace ProcessingTools.Web.Api.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Contracts.Models;
    using ProcessingTools.Services.Data.Models;

    public class ProductRequestModel : IMapFrom<ProductServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}
