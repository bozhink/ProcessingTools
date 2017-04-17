namespace ProcessingTools.Web.Areas.Data.ViewModels.GeoNames
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;

    public class GeoNameViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false)]
        [MinLength(ValidationConstants.MinimalLengthOfGeoName)]
        [MaxLength(ValidationConstants.MaximalLengthOfGeoName)]
        public string Name { get; set; }
    }
}
