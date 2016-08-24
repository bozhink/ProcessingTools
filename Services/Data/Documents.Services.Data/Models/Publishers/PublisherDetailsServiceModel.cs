namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System.Collections.Generic;

    public class PublisherDetailsServiceModel : PublisherServiceModel
    {
        public PublisherDetailsServiceModel()
            : base()
        {
            this.Addresses = new HashSet<IPublisherAddress>();
            this.Journals = new HashSet<PublisherJournal>();
        }

        public ICollection<IPublisherAddress> Addresses { get; set; }

        public ICollection<PublisherJournal> Journals { get; set; }
    }
}
