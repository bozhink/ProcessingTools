namespace ProcessingTools.Web.Areas.Data.Models.Shared
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Constants.Data.Geo;
    using Strings = ProcessingTools.Web.Areas.Data.Resources.Shared.Models_Strings;

    public class SynonymViewModel
    {
        public int Id { get; set; }

        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(Strings.NameEmptyErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [StringLength(
            maximumLength: ValidationConstants.MaximalLengthOfSynonymName,
            MinimumLength = ValidationConstants.MinimalLengthOfSynonymName,
            ErrorMessageResourceName = nameof(Strings.NameErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        public string Name { get; set; }

        [Display(Name = nameof(Strings.LanguageCode), ResourceType = typeof(Strings))]
        public string LanguageCode { get; set; }
    }
}
