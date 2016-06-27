namespace ProcessingTools.Web.Documents.Areas.Journals.ViewModels.Publishers
{
    using System.Collections.Generic;

    public class PublisherDetailsViewModel : PublisherViewModel
    {
        public PublisherDetailsViewModel()
            : base()
        {
            this.Addresses = new HashSet<AddressViewModel>();
            this.Journals = new HashSet<JournalViewModel>();
        }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public ICollection<AddressViewModel> Addresses { get; set; }

        public ICollection<JournalViewModel> Journals { get; set; }
    }
}
