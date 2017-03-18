namespace ProcessingTools.Web.Areas.Journals.ViewModels.Publishers
{
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Journals.Data.Common.Constants;

    public class AddressViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfId)]
        [Display(Name = "ID", Description = "ID")]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        [Display(Name = "Address String", Description = "Address String")]
        public string AddressString { get; set; }

        [Display(Name = "City ID", Description = "City ID")]
        public int? CityId { get; set; }

        [Display(Name = "Country ID", Description = "Country ID")]
        public int? CountryId { get; set; }
    }
}
