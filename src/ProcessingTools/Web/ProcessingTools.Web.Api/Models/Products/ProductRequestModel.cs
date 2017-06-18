namespace ProcessingTools.Web.Api.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.DataResources;
    using ProcessingTools.Services.Data.Contracts.Models;

    public class ProductRequestModel : IProduct
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.ProductNameMaximalLength)]
        public string Name { get; set; }
    }
}
