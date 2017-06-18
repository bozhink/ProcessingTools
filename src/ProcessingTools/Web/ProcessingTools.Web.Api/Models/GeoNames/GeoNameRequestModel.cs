namespace ProcessingTools.Web.Api.Models.GeoNames
{
    using System.ComponentModel.DataAnnotations;

    public class GeoNameRequestModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}
