namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ProcessingTools.Documents.Data.Common.Constants;

    public class PublisherCreateViewModel
    {
        public PublisherCreateViewModel()
        {
            this.Addresses = new List<AddressViewModel>();
        }

        [Required]
        [MaxLength(ValidationConstants.MaximalLengthOfPublisherName)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaximalLengthOfAbbreviatedPublisherName)]
        [Display(Name = "Abbreviated Name")]
        public string AbbreviatedName { get; set; }

        public ICollection<AddressViewModel> Addresses { get; set; }
    }
}
