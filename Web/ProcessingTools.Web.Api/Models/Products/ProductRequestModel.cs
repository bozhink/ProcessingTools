namespace ProcessingTools.Web.Api.Models.Products
{
    using System.ComponentModel.DataAnnotations;

    using Mappings.Contracts;
    using Services.Data.Models;

    public class ProductRequestModel : IMapFrom<ProductServiceModel>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}