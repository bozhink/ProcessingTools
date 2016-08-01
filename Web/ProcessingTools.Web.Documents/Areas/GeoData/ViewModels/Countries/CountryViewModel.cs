namespace ProcessingTools.Web.Documents.Areas.GeoData.ViewModels.Countries
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Geo.Data.Common.Constants;
    using ProcessingTools.Web.Common.ViewModels;

    public class CountryViewModel : NamedViewModel
    {
        public int Id { get; set; }

        [RegularExpression(ValidationConstants.CountryNameRegexPattern)]
        [MaxLength(ValidationConstants.MaximalLengthOfCountryName)]
        public string Name { get; set; }

        public override string ModelName => ContentConstants.CountryModelName;
    }
}