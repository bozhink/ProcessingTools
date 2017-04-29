namespace ProcessingTools.Web.Areas.Data.ViewModels.Continents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;
    using Strings = ProcessingTools.Web.Resources.Areas.Data.ViewModels.Continents.Strings;

    public class ContinentViewModel
    {
        public int Id { get; set; }

        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Strings.NameValidationErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        [StringLength(ValidationConstants.MaximalLengthOfContinentName, ErrorMessageResourceName = nameof(Strings.NameLengthValidationErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.MinimalLengthOfContinentName)]
        public string Name { get; set; }

        [Display(Name = nameof(Strings.Synonyms), ResourceType = typeof(Strings))]
        public string Synonyms { get; set; }

        [Display(Name = nameof(Strings.NumberOfCountries), ResourceType = typeof(Strings))]
        public int NumberOfCountries { get; set; }

        [Display(Name = nameof(Strings.Countries), ResourceType = typeof(Strings))]
        public IEnumerable<CountryViewModel> Countries { get; set; }
    }
}