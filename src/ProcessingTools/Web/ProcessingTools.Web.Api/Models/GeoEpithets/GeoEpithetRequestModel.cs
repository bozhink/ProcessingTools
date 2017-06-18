namespace ProcessingTools.Web.Api.Models.GeoEpithets
{
    using System.ComponentModel.DataAnnotations;

    public class GeoEpithetRequestModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
