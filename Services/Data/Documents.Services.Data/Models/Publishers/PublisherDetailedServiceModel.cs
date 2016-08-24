namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System.Collections.Generic;
    using Contracts;

    public class PublisherDetailedServiceModel : PublisherSimpleServiceModel, IPublisherDetailedServiceModel
    {
        public PublisherDetailedServiceModel()
            : base()
        {
            this.Addresses = new HashSet<IPublisherAddress>();
            this.Journals = new HashSet<IPublisherJournal>();
        }

        public ICollection<IPublisherAddress> Addresses { get; set; }

        public ICollection<IPublisherJournal> Journals { get; set; }
    }
}
