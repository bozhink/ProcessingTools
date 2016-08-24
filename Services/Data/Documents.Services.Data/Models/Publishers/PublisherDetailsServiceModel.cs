namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System.Collections.Generic;

    public class PublisherDetailsServiceModel : PublisherSimpleServiceModel
    {
        public PublisherDetailsServiceModel()
            : base()
        {
            this.Addresses = new HashSet<PublisherAddress>();
            this.Journals = new HashSet<PublisherJournal>();
        }

        public ICollection<PublisherAddress> Addresses { get; set; }

        public ICollection<PublisherJournal> Journals { get; set; }
    }
}
