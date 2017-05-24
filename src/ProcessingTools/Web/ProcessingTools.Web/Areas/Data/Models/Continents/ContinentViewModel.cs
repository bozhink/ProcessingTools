namespace ProcessingTools.Web.Areas.Data.Models.Continents
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;
    using Strings = ProcessingTools.Web.Areas.Data.Resources.Continents.Models_Strings;

    public class ContinentViewModel
    {
        public int Id { get; set; }

        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Strings.NameValidationErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        [StringLength(ValidationConstants.MaximalLengthOfContinentName, ErrorMessageResourceName = nameof(Strings.NameLengthValidationErrorMessage), ErrorMessageResourceType = typeof(Strings), MinimumLength = ValidationConstants.MinimalLengthOfContinentName)]
        public string Name { get; set; }

        [Display(Name = nameof(Strings.AbbreviatedName), ResourceType = typeof(Strings))]
        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedName, ErrorMessageResourceName = nameof(Strings.AbbreviatedNameValidationErrorMessage), ErrorMessageResourceType = typeof(Strings))]
        public string AbbreviatedName { get; set; }

        [Display(Name = nameof(Strings.Synonyms), ResourceType = typeof(Strings))]
        public string Synonyms { get; set; }

        [Display(Name = nameof(Strings.NumberOfCountries), ResourceType = typeof(Strings))]
        public int NumberOfCountries { get; set; }

        [Display(Name = nameof(Strings.Countries), ResourceType = typeof(Strings))]
        public IEnumerable<CountryViewModel> Countries { get; set; }
    }
}