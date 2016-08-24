namespace ProcessingTools.Documents.Services.Data.Models.Publishers
{
    using System.Collections.Generic;
    using Contracts;

    public class PublisherAddableServiceModel : PublisherUpdatableServiceModel, IPublisherAddableServiceModel
    {
        public PublisherAddableServiceModel()
        {
            this.Addresses = new HashSet<IPublisherAddress>();
        }

        public ICollection<IPublisherAddress> Addresses { get; set; }
    }
}
