namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProcessingTools.Documents.Data.Common.Constants;

    public class AddressViewModel
    {
        public AddressViewModel()
        {
            this.Id = Guid.NewGuid();
        }

        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAddressString)]
        [Display(Name = "Address")]
        public string AddressString { get; set; }

        public int? CityId { get; set; }

        public int? CountryId { get; set; }
    }
}
