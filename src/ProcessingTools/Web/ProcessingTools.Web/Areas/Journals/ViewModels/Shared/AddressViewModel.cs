namespace ProcessingTools.Web.Areas.Journals.ViewModels.Shared
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Common.Constants.Data.Journals;
    using Strings = ProcessingTools.Web.Resources.Areas.Journals.ViewModels.Shared.Strings;

    public class AddressViewModel
    {
        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(Strings.IdEmptyErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [StringLength(
            maximumLength: ValidationConstants.MaximalLengthOfId,
            MinimumLength = 1,
            ErrorMessageResourceName = nameof(Strings.IdErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [Display(
            Name = nameof(Strings.Id),
            ResourceType = typeof(Strings))]
        public string Id { get; set; }

        [Required(
            AllowEmptyStrings = false,
            ErrorMessageResourceName = nameof(Strings.AddressStringEmptyErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [StringLength(
            maximumLength: ValidationConstants.MaximalLengthOfAddressString,
            MinimumLength = 1,
            ErrorMessageResourceName = nameof(Strings.AddressStringErrorMessage),
            ErrorMessageResourceType = typeof(Strings))]
        [Display(
            Name = nameof(Strings.AddressString),
            ResourceType = typeof(Strings))]
        public string AddressString { get; set; }

        [Display(
            Name = nameof(Strings.CityId),
            ResourceType = typeof(Strings))]
        public int? CityId { get; set; }

        [Display(
            Name = nameof(Strings.CountryId),
            ResourceType = typeof(Strings))]
        public int? CountryId { get; set; }
    }
}
