namespace ProcessingTools.Web.Api.Models.ProductModels
{
    using System.ComponentModel.DataAnnotations;

    using Contracts.Mapping;
    using Services.Data.Models.Contracts;

    public class ProductRequestModel : IMapFrom<IProduct>
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}