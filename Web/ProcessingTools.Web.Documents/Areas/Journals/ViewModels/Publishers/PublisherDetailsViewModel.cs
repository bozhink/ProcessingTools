namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PublisherDetailsViewModel : PublisherViewModel
    {
        public PublisherDetailsViewModel()
            : base()
        {
            this.Addresses = new HashSet<AddressViewModel>();
            this.Journals = new HashSet<JournalViewModel>();
        }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Addresses")]
        public ICollection<AddressViewModel> Addresses { get; set; }

        [Display(Name = "Journals")]
        public ICollection<JournalViewModel> Journals { get; set; }
    }
}
