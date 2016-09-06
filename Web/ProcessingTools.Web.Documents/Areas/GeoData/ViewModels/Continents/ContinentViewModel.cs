namespace ProcessingTools.Web.Documents.Areas.GeoData.ViewModels.Continents
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Geo.Data.Common.Constants;
    using ProcessingTools.Web.Common.ViewModels;

    public class ContinentViewModel : NamedViewModel
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(ValidationConstants.ContinentNameRegexPattern)]
        [MinLength(ValidationConstants.MinimalLengthOfContinentName)]
        [MaxLength(ValidationConstants.MaximalLengthOfContinentName)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public override string ModelName => ContentConstants.ContinentModelName;
    }
}